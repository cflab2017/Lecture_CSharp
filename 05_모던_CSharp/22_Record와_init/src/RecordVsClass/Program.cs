// record class (기본) : 참조 타입 + 값 동등성
// record struct          : 값 타입 + 값 동등성

PointClass  c1 = new(1, 2);
PointClass  c2 = new(1, 2);
PointStruct s1 = new(1, 2);
PointStruct s2 = new(1, 2);

Console.WriteLine($"record class:  c1 == c2 ? {c1 == c2}  (참조? {ReferenceEquals(c1, c2)})");
Console.WriteLine($"record struct: s1 == s2 ? {s1 == s2}");

// struct 는 값 복사 → 함수에 넘기면 통째 복제.
void Shift(PointStruct p) { /* p.X = 99; (readonly 라 불가) */ }
Shift(s1);
Console.WriteLine($"s1 = {s1}");

// 두 record 모두 ToString / Equals / GetHashCode 자동 생성, with 식도 동일하게 사용 가능.
PointStruct shifted = s1 with { X = 99 };
Console.WriteLine($"shifted = {shifted}, s1 = {s1}");

internal sealed record PointClass(int X, int Y);
internal readonly record struct PointStruct(int X, int Y);
