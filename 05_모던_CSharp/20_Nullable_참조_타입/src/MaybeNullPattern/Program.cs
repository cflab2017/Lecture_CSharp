// "찾는 메서드" 는 결과가 없을 수 있다 → 반환 타입을 Person? 로 명시.
// 호출 쪽은 ?. / ?? 로 안전하게 다룬다.

List<Person> people =
[
    new("Alice", 30),
    new("Bob", 25),
];

Person? found = FindByName(people, "Alice");
Person? missing = FindByName(people, "Zoe");

// 안전한 멤버 접근 — null 이면 통째로 null.
Console.WriteLine($"Alice 나이: {found?.Age}");          // 30
Console.WriteLine($"Zoe 나이:   {missing?.Age}");        // (빈 값)

// 기본값 채우기.
int age = missing?.Age ?? -1;
Console.WriteLine($"Zoe 나이 (기본 -1): {age}");

// 패턴 매칭으로 분기.
if (found is { Age: > 18 } adult)
{
    Console.WriteLine($"{adult.Name} 는 성인");
}

static Person? FindByName(IEnumerable<Person> people, string name)
{
    foreach (Person p in people)
    {
        if (p.Name == name) return p;
    }
    return null;
}

internal sealed record Person(string Name, int Age);
