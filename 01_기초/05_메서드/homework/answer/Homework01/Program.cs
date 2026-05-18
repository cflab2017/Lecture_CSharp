// 1~50 의 소수 출력
static bool IsPrime(int n)
{
    if (n < 2) return false;
    if (n == 2) return true;
    if (n % 2 == 0) return false;

    for (int i = 3; i * i <= n; i += 2)
    {
        if (n % i == 0) return false;
    }
    return true;
}

Console.WriteLine("1 ~ 50 의 소수:");
for (int i = 1; i <= 50; i++)
{
    if (IsPrime(i))
        Console.Write($"{i} ");
}
Console.WriteLine();
