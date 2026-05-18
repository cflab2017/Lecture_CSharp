namespace CodingNow.Lecture.Oop09;

internal class Circle : Shape
{
    public double Radius;

    public Circle(double radius) => Radius = radius;

    public override double Area() => Math.PI * Radius * Radius;
}
