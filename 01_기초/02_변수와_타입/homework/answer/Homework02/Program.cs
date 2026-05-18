// 두 정수의 사칙연산
Console.Write("첫 번째 정수: ");
int a = int.Parse(Console.ReadLine()!);

Console.Write("두 번째 정수: ");
int b = int.Parse(Console.ReadLine()!);

Console.WriteLine($"{a} + {b} = {a + b}");
Console.WriteLine($"{a} - {b} = {a - b}");
Console.WriteLine($"{a} * {b} = {a * b}");
Console.WriteLine($"{a} / {b} = {a / b}");
Console.WriteLine($"{a} % {b} = {a % b}");
