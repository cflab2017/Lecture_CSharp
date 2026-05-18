#nullable enable

Student[] students =
[
    new("지수", "A반", 88),
    new("민호", "B반", 72),
    new("서연", "A반", 95),
    new("윤재", "B반", 65),
    new("하늘", "A반", 78),
    new("도윤", "B반", 90),
];

double overallAvg = students.Average(s => s.Score);
Console.WriteLine($"전체 평균: {overallAvg:F1}");

Student top = students.OrderByDescending(s => s.Score).First();
Console.WriteLine($"최고 점수: {top.Name} ({top.Score}점)");

Console.WriteLine("=== 반별 평균 ===");
var byClass = students
    .GroupBy(s => s.Class)
    .OrderBy(g => g.Key);

foreach (var g in byClass)
{
    Console.WriteLine($"{g.Key}: {g.Average(s => s.Score):F1}");
}

public class Student(string name, string @class, int score)
{
    public string Name { get; } = name;
    public string Class { get; } = @class;
    public int Score { get; } = score;
}
