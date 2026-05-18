namespace CodingNow.Lecture.Oop06;

internal class Point
{
    public int X;
    public int Y;

    // : this(...) 는 "같은 클래스의 다른 생성자를 먼저 호출"하라는 뜻.
    public Point() : this(0, 0) { }

    public Point(int v) : this(v, v) { }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Print() => Console.WriteLine($"({X}, {Y})");
}
