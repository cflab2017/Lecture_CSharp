// record 한 줄이면 — 생성자, 프로퍼티(init only), Equals, GetHashCode, ToString 까지 자동 생성.
Person p1 = new("Alice", 30);
Person p2 = new("Alice", 30);
Person p3 = new("Bob", 25);

// 값 동등성 — 안에 든 값이 같으면 동일하다고 본다 (class 였다면 참조 비교라 false).
Console.WriteLine($"p1 == p2 ? {p1 == p2}");   // True
Console.WriteLine($"p1 == p3 ? {p1 == p3}");   // False

// ToString 자동 생성 — 디버깅이 즐겁다.
Console.WriteLine(p1);

// 분해(deconstruction) 도 자동.
var (name, age) = p1;
Console.WriteLine($"name={name}, age={age}");

internal sealed record Person(string Name, int Age);
