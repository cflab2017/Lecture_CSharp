#nullable enable

// new() 제약 → new T() 호출 가능
Person p = Create<Person>();
p.Name = "지수";
Console.WriteLine($"새로 만든 사람: {p.Name}");

// struct 제약 → 값 타입만 허용
DoubleIt(42);
DoubleIt(3.14);
// DoubleIt("문자열");  // 컴파일 에러: string 은 struct 아님

static T Create<T>() where T : class, new() => new T();

static T DoubleIt<T>(T x) where T : struct
{
    Console.WriteLine($"받은 값: {x}");
    return x;
}

public class Person
{
    public string Name { get; set; } = "";
}
