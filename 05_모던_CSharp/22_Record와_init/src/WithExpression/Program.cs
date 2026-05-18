// `with` 식 — record 의 일부만 바꿔서 "새 인스턴스" 를 만든다 (비파괴 복사).
Person original = new("Alice", 30, "Seoul");
Person aged     = original with { Age = 31 };
Person moved    = original with { City = "Busan", Age = 31 };

Console.WriteLine($"original: {original}");
Console.WriteLine($"aged    : {aged}");
Console.WriteLine($"moved   : {moved}");

// 원본은 그대로다.
Console.WriteLine($"\noriginal 은 여전히 {original.Age}세, {original.City}");

internal sealed record Person(string Name, int Age, string City);
