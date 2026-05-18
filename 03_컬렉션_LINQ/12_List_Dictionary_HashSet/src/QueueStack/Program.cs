#nullable enable

// FIFO — 줄 서기
Queue<string> queue = new();
queue.Enqueue("첫번째");
queue.Enqueue("두번째");
queue.Enqueue("세번째");

Console.WriteLine("=== Queue (FIFO) ===");
while (queue.Count > 0)
{
    Console.WriteLine(queue.Dequeue());
}

// LIFO — 책 더미
Stack<string> stack = new();
stack.Push("첫번째");
stack.Push("두번째");
stack.Push("세번째");

Console.WriteLine("=== Stack (LIFO) ===");
while (stack.Count > 0)
{
    Console.WriteLine(stack.Pop());
}
