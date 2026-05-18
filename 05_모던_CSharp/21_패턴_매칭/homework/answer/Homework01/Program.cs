Shape[] shapes = [new Circle(2), new Rectangle(3, 4), new Triangle(5, 6)];

foreach (Shape s in shapes)
{
    string label = s switch
    {
        Circle c    => $"Circle({c.Radius})",
        Rectangle r => $"Rectangle({r.Width}, {r.Height})",
        Triangle t  => $"Triangle({t.Base}, {t.Height})",
        _           => "Unknown",
    };
    Console.WriteLine($"{label} 넓이: {Area(s):F2}");
}

// 타입 패턴 + switch 식 — 새 도형이 늘어도 한 곳만 고치면 된다.
static double Area(Shape s) => s switch
{
    Circle c    => Math.PI * c.Radius * c.Radius,
    Rectangle r => r.Width * r.Height,
    Triangle t  => 0.5 * t.Base * t.Height,
    _           => throw new InvalidOperationException($"지원 안 함: {s.GetType().Name}"),
};

internal abstract record Shape;
internal sealed record Circle(double Radius) : Shape;
internal sealed record Rectangle(double Width, double Height) : Shape;
internal sealed record Triangle(double Base, double Height) : Shape;
