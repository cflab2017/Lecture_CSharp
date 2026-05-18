// property 패턴 — 객체의 속성 값을 바로 매칭한다.
Point[] points = [new(0, 0), new(0, 5), new(7, 0), new(3, 4), new(-1, -2)];

foreach (Point p in points)
{
    string zone = p switch
    {
        { X: 0, Y: 0 }            => "원점",
        { X: 0 }                  => "Y축 위",
        { Y: 0 }                  => "X축 위",
        { X: > 0, Y: > 0 }        => "1사분면",
        { X: < 0, Y: < 0 }        => "3사분면",
        _                         => "기타 사분면",
    };
    Console.WriteLine($"({p.X}, {p.Y}) → {zone}");
}

internal sealed record Point(int X, int Y);
