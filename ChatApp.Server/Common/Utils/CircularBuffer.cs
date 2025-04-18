namespace ChatApp.Server.Common.Utils;

public class CircularBuffer<T>(int capacity)
{
    private readonly T[] _buffer = new T[capacity];
    private int _head = 0;
    private int _count = 0;

    public int Capacity { get; } = capacity;
    public int Count => _count;

    public void Write(T item)
    {
        _buffer[_head] = item;
        _head = (_head + 1) % Capacity;

        if (_count < Capacity)
            _count++;
    }

    public IReadOnlyList<T> ReadAll()
    {
        var result = new T[_count];
        int start = (_head - _count + Capacity) % Capacity;

        for (int i = 0; i < _count; i++)
        {
            result[i] = _buffer[(start + i) % Capacity];
        }

        return result;
    }
}
