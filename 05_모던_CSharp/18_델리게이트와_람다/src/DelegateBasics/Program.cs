// delegate 타입을 직접 선언해서 메서드를 변수처럼 다룬다.
BinaryOp add = (a, b) => a + b;
BinaryOp mul = (a, b) => a * b;

Console.WriteLine($"add(3, 4) = {add(3, 4)}");
Console.WriteLine($"mul(3, 4) = {mul(3, 4)}");

// 델리게이트는 변수에 담아 다른 메서드로 넘길 수 있다.
int Apply(int x, int y, BinaryOp op) => op(x, y);

Console.WriteLine($"Apply(10, 5, add) = {Apply(10, 5, add)}");
Console.WriteLine($"Apply(10, 5, mul) = {Apply(10, 5, mul)}");

// top-level statements 가 있는 파일에서는 타입 선언이 뒤로 와야 한다.
delegate int BinaryOp(int a, int b);
