Point[] points =
[
    new(0, 0), new(0, 5), new(7, 0), new(3, 3),
    new(2, 8), new(-1, -5), new(-3, 4),
];

foreach (Point p in points)
{
    Console.WriteLine($"({p.X},{p.Y}) → {Classify(p)}");
}

// 분기 순서가 핵심 — 가장 구체적인 (0,0) 을 맨 위에 둔다.
static string Classify(Point p) => p switch
{
    { X: 0, Y: 0 }                          => "원점",
    { X: 0 }                                => "Y축",
    { Y: 0 }                                => "X축",
    { X: var x, Y: var y } when x == y      => "y=x 직선",
    { X: > 0, Y: > 0 }                      => "1사분면",
    { X: < 0, Y: < 0 }                      => "3사분면",
    _                                       => "기타 사분면",
};

internal sealed record Point(int X, int Y);
