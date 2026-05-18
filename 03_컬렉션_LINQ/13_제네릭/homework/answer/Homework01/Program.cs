#nullable enable

Console.WriteLine("=== int 스택 ===");
MyStack<int> ints = new();
ints.Push(1);
ints.Push(2);
ints.Push(3);
Console.WriteLine("Push: 1, 2, 3");
Console.WriteLine($"Peek: {ints.Peek()}");
Console.WriteLine($"Pop: {ints.Pop()}");
Console.WriteLine($"Pop: {ints.Pop()}");
Console.WriteLine($"남은 개수: {ints.Count}");

Console.WriteLine("=== string 스택 ===");
MyStack<string> strs = new();
strs.Push("A");
strs.Push("B");
Console.WriteLine("Push: A, B");
Console.WriteLine($"Pop: {strs.Pop()}");
Console.WriteLine($"Pop: {strs.Pop()}");

public class MyStack<T>
{
    private readonly List<T> _items = new();

    public int Count => _items.Count;

    public void Push(T item) => _items.Add(item);

    public T Pop()
    {
        if (_items.Count == 0)
        {
            throw new InvalidOperationException("스택이 비어 있습니다.");
        }
        T top = _items[^1];
        _items.RemoveAt(_items.Count - 1);
        return top;
    }

    public T Peek()
    {
        if (_items.Count == 0)
        {
            throw new InvalidOperationException("스택이 비어 있습니다.");
        }
        return _items[^1];
    }
}
