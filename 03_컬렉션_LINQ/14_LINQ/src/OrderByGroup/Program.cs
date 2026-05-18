#nullable enable

Person[] people =
[
    new("지수", "서울", 25),
    new("민호", "부산", 30),
    new("서연", "서울", 22),
    new("윤재", "부산", 28),
];

// 도시 오름차순, 같은 도시면 나이 내림차순
var sorted = people.OrderBy(p => p.City).ThenByDescending(p => p.Age);
foreach (Person p in sorted)
{
    Console.WriteLine($"{p.City} - {p.Name} ({p.Age})");
}

Console.WriteLine("---");

// 도시별 그룹화
var groups = people.GroupBy(p => p.City);
foreach (var g in groups)
{
    Console.WriteLine($"[{g.Key}] {g.Count()}명: {string.Join(", ", g.Select(p => p.Name))}");
}

public class Person(string name, string city, int age)
{
    public string Name { get; } = name;
    public string City { get; } = city;
    public int Age { get; } = age;
}
