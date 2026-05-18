namespace CodingNow.Lecture.Basics05;

internal static class Program
{
    public static void Main()
    {
        // out: TryParse 패턴
        if (int.TryParse("123", out int parsed))
            Console.WriteLine($"파싱 성공: {parsed}");

        // ref: 두 값 교환
        int x = 1, y = 2;
        Swap(ref x, ref y);
        Console.WriteLine($"Swap 후 x={x}, y={y}");

        // in: 읽기 전용 참조 전달
        Point p = new(10, 20);
        Print(in p);
    }

    static void Swap(ref int a, ref int b)
    {
        (a, b) = (b, a);
    }

    static void Print(in Point p)
    {
        // p.X = 0;   // 컴파일 에러: in 은 읽기 전용
        Console.WriteLine($"Point({p.X}, {p.Y})");
    }
}

internal readonly struct Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}
