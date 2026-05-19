// 메서드 오버로딩 - 같은 이름, 다른 시그니처
// 로컬 함수는 오버로딩이 안 되므로 static 클래스로 감싸 보여 준다.
Console.WriteLine($"Calc.Add(1, 2)       = {Calc.Add(1, 2)}");
Console.WriteLine($"Calc.Add(1.5, 2.5)   = {Calc.Add(1.5, 2.5)}");

static class Calc
{
    public static int Add(int a, int b) => a + b;
    public static double Add(double a, double b) => a + b;
}
