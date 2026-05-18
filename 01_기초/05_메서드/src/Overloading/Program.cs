// 메서드 오버로딩 - 같은 이름, 다른 시그니처
static int Add(int a, int b) => a + b;
static double Add(double a, double b) => a + b;

Console.WriteLine($"Add(1, 2)       = {Add(1, 2)}");
Console.WriteLine($"Add(1.5, 2.5)   = {Add(1.5, 2.5)}");
