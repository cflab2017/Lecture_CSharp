Calculator calc = new();

foreach (string op in new[] { "+", "-", "*", "/" })
{
    int r = calc.Run(op, 10, 3);
    Console.WriteLine($"10 {op} 3 = {r}");
}

internal sealed class Calculator
{
    // 연산 이름 → 람다 매핑.
    public Dictionary<string, Func<int, int, int>> Ops { get; } = new()
    {
        ["+"] = (a, b) => a + b,
        ["-"] = (a, b) => a - b,
        ["*"] = (a, b) => a * b,
        ["/"] = (a, b) => a / b,
    };

    public int Run(string op, int a, int b)
    {
        if (!Ops.TryGetValue(op, out Func<int, int, int>? fn))
        {
            throw new InvalidOperationException($"지원하지 않는 연산: {op}");
        }
        return fn(a, b);
    }
}
