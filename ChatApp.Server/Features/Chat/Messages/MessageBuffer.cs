
using System.Collections.Concurrent;
using ChatApp.Server.Common.Utils;

namespace ChatApp.Server.Features.Chat.Messages;

public class MessageBuffer : IMessageBuffer
{
    private const int Capacity = 50;
    private readonly ConcurrentDictionary<int, CircularBuffer<ChatMessage>> _buffers = [];

    public void Add(int channelId, ChatMessage message)
    {
        var buffer = _buffers.GetOrAdd(channelId,
            _ => new CircularBuffer<ChatMessage>(Capacity));

        buffer.Write(message);
    }

    public IReadOnlyList<ChatMessage> GetAll(int channelId) =>
        _buffers.TryGetValue(channelId, out var buffer)
            ? buffer.ReadAll() : [];
}
