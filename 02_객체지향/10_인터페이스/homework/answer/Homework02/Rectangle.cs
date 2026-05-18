namespace CodingNow.Lecture.Oop10;

internal class Rectangle : IDrawable, IResizable
{
    public int Width;
    public int Height;

    public Rectangle(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public void Draw() => Console.WriteLine($"사각형 그리기 ({Width}x{Height})");

    public void Resize(int width, int height)
    {
        Width = width;
        Height = height;
        Console.WriteLine($"사각형 크기 변경 → ({Width}x{Height})");
    }
}
