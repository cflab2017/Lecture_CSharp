// `is null` vs `== null` — 권장은 `is null`.
// 이유: == 은 사용자 정의 오버로딩이 끼어들 수 있지만, is 는 언어 차원 검사라 항상 동일하게 동작.

string? a = null;
string? b = "hello";

if (a is null) Console.WriteLine("a 는 null");
if (b is not null) Console.WriteLine($"b 는 {b}");

// 결합 연산자
string display = a ?? "(이름 없음)";       // null 이면 대체값
Console.WriteLine($"display = {display}");

a ??= "기본값";                            // null 일 때만 대입
Console.WriteLine($"a = {a}");

// 멤버 접근에서도 ?.  체이닝 가능
string? upper = b?.ToUpper();
Console.WriteLine($"upper = {upper}");

// 안전 인덱싱
char? first = b?[0];
Console.WriteLine($"first = {first}");
