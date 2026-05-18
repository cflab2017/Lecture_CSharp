// 0 으로 나누면 DivideByZeroException 이 발생한다
try
{
    int a = 10;
    int b = 0;
    int c = a / b;                  // 여기서 예외 발생
    Console.WriteLine(c);
}
catch (DivideByZeroException ex)
{
    Console.WriteLine($"0 으로 나눌 수 없습니다: {ex.Message}");
}

Console.WriteLine("프로그램 계속 진행");
