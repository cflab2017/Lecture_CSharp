// init only setter — 객체 초기화 시점에만 대입 가능, 이후엔 readonly.
// 일반 class 에도 적용 가능 (record 전용 아님).

Person p = new()
{
    Name = "Alice",      // 객체 초기화 구문 안에서는 init setter 호출 가능
    Age = 30,
};

Console.WriteLine($"{p.Name}, {p.Age}세");

// p.Name = "Bob";        // 컴파일 에러: init 은 객체 생성 후 변경 불가

// required (07편) 와 결합하면 "반드시 지정" 을 강제할 수 있다.
Book b = new() { Title = "C# 입문", Pages = 320 };
Console.WriteLine(b);

internal sealed class Person
{
    public string Name { get; init; } = "";
    public int Age { get; init; }
}

internal sealed class Book
{
    public required string Title { get; init; }   // required → 초기화 시 필수
    public int Pages { get; init; }
    public override string ToString() => $"{Title} ({Pages}p)";
}
