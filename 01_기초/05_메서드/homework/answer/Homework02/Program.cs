// TryDivide - out 패턴
static bool TryDivide(int a, int b, out int result)
{
    if (b == 0)
    {
        result = 0;
        return false;
    }
    result = a / b;
    return true;
}

Console.Write("a: ");
int a = int.Parse(Console.ReadLine()!);

Console.Write("b: ");
int b = int.Parse(Console.ReadLine()!);

if (TryDivide(a, b, out int r))
    Console.WriteLine($"{a} / {b} = {r}");
else
    Console.WriteLine("0 으로 나눌 수 없습니다.");
