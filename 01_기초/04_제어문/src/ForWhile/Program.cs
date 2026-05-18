// for 로 합계, while 로 카운트다운
int sum = 0;
for (int i = 1; i <= 10; i++)
    sum += i;
Console.WriteLine($"1~10 합 = {sum}");

int n = 5;
while (n > 0)
{
    Console.Write($"{n} ");
    n--;
}
Console.WriteLine("발사!");
