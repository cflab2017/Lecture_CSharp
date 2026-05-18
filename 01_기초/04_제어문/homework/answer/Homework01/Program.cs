// 1부터 N까지의 합
Console.Write("N: ");
int n = int.Parse(Console.ReadLine()!);

if (n <= 0)
{
    Console.WriteLine("0 이하 입력은 처리할 수 없습니다.");
    return;
}

int sum = 0;
for (int i = 1; i <= n; i++)
    sum += i;

Console.WriteLine($"1 ~ {n} 의 합 = {sum}");
