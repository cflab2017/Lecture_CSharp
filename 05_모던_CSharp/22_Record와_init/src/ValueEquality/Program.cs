// record 가 자동 생성해 주는 것들 : Equals, GetHashCode, ToString, ==, !=, Deconstruct.
Coord a = new(1, 2);
Coord b = new(1, 2);
Coord c = new(3, 4);

// Equals — 값 비교
Console.WriteLine($"a.Equals(b) = {a.Equals(b)}");

// GetHashCode — 같은 값이면 같은 해시 → HashSet, Dictionary 키로 그대로 사용 가능
HashSet<Coord> set = [a, b, c];   // b 는 a 와 같다고 보아 중복 제거
Console.WriteLine($"set.Count = {set.Count}");

// ToString — Coord { X = 1, Y = 2 } 형태로 자동 출력
Console.WriteLine($"ToString: {a}");

// == 연산자도 값 비교
Console.WriteLine($"a == b : {a == b}");
Console.WriteLine($"a == c : {a == c}");

internal sealed record Coord(int X, int Y);
