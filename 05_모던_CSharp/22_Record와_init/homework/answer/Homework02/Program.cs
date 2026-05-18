Vector v1 = new(3, 4);
Vector v2 = new(1, 2);

Console.WriteLine($"v1 = {v1}");
Console.WriteLine($"v2 = {v2}");
Console.WriteLine($"v1 + v2 = {v1.Add(v2)}");
Console.WriteLine($"|v1| = {v1.Length()}");
Console.WriteLine($"v1 == new Vector(3, 4) ? {v1 == new Vector(3, 4)}");

// readonly record struct — 작은 값 타입 + 자동 값 동등성 + 불변
internal readonly record struct Vector(double X, double Y)
{
    public Vector Add(Vector other) => new(X + other.X, Y + other.Y);
    public double Length() => Math.Sqrt(X * X + Y * Y);
}
