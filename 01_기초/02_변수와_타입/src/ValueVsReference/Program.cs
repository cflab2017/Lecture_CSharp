namespace CodingNow.Lecture.Basics02;

internal static class Program
{
    public static void Main()
    {
        // 값 타입 (struct): 값을 통째로 복사
        Point p1 = new(1, 2);
        Point p2 = p1;
        p2.X = 99;
        Console.WriteLine($"p1=({p1.X},{p1.Y})  p2=({p2.X},{p2.Y})");

        // 참조 타입 (class): 주소를 공유
        Box b1 = new() { Value = 10 };
        Box b2 = b1;
        b2.Value = 99;
        Console.WriteLine($"b1={b1.Value}  b2={b2.Value}");
    }
}

internal struct Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

internal class Box
{
    public int Value;
}
