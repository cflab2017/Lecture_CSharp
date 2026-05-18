namespace CodingNow.Lecture.Oop09;

internal class Rectangle : Shape
{
    public double Width;
    public double Height;

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public override double Area() => Width * Height;
}
