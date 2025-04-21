
using System.Collections.Concurrent;
using ChatApp.Server.Common.Utils;

namespace ChatApp.Server.Features.Chat.Messages;

public class ChannelMessageBuffer : IChannelMessageBuffer
{
    private const int Capacity = 50;
    private readonly ConcurrentDictionary<int, CircularBuffer<ChannelMessage>> _buffers = [];

    public void Add(int channelId, ChannelMessage message)
    {
        var buffer = _buffers.GetOrAdd(channelId,
            _ => new CircularBuffer<ChannelMessage>(Capacity));

        buffer.Write(message);
    }

    public IReadOnlyList<ChannelMessage> GetAll(int channelId) =>
        _buffers.TryGetValue(channelId, out var buffer)
            ? buffer.ReadAll() : [];
}
