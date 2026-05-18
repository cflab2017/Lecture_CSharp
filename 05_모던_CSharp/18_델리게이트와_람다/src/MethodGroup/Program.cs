// 람다 대신 기존 메서드 이름을 그대로 넘길 수 있다 — 이를 "메서드 그룹 변환" 이라 한다.
List<string> names = ["Alice", "Bob", "Charlie"];

// 람다 버전
names.ForEach(n => Console.WriteLine(n));

// 메서드 그룹 버전 — 시그니처가 맞으면 메서드 이름만으로 충분
names.ForEach(Console.WriteLine);

// Func 도 메서드 그룹으로 채울 수 있다.
Func<string, int> length = GetLength;
Console.WriteLine($"길이: {length("Charlie")}");

static int GetLength(string s) => s.Length;
