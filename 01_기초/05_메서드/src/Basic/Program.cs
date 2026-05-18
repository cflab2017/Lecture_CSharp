// 메서드로 분리해 호출
static int Add(int a, int b)
{
    return a + b;
}

int x = Add(3, 4);
int y = Add(10, 20);

Console.WriteLine($"Add(3, 4)  = {x}");
Console.WriteLine($"Add(10,20) = {y}");
