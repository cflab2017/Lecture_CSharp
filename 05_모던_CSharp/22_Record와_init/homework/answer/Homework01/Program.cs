Book original = new("CLR via C#", "Jeffrey Richter", 1100);

// with 식 — 일부만 바꾼 새 인스턴스 생성. 원본은 변경되지 않는다.
Book revised    = original with { Pages = 1200 };
Book translated = original with { Title = "CLR via C# (한국어판)" };

Console.WriteLine($"original  : {original}");
Console.WriteLine($"revised   : {revised}");
Console.WriteLine($"translated: {translated}");

Console.WriteLine();
Console.WriteLine($"original == revised        ? {original == revised}");
Console.WriteLine($"original == 같은 값의 새 객체 ? {original == new Book("CLR via C#", "Jeffrey Richter", 1100)}");

internal sealed record Book(string Title, string Author, int Pages);
