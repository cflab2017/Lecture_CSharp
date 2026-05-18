# 17. 문자열 처리

읽어들인 텍스트를 깔끔하게 가공하는 일은 거의 모든 프로그램에서 필요합니다. C# 의 `string` 은 풍부한 메서드를 제공하고, 반복 합성에는 `StringBuilder`, 패턴 매칭에는 `Regex` 가 있습니다. 또한 **culture(문화권)** 에 따라 숫자/날짜 파싱 결과가 달라질 수 있다는 함정도 짚어봅니다.

## 학습 목표
- `Trim`/`Split`/`Join`/`Replace`/`ToUpper`/`Contains`/`StartsWith` 같은 핵심 메서드를 안다
- 보간 문자열 `$"..."` 과 형식 지정자(`F2`, `yyyy-MM-dd` 등)를 활용한다
- 반복 합성에는 `StringBuilder` 가 빠르다는 것을 안다
- `Regex.IsMatch`/`Match`/`Matches` 기본 사용법을 익힌다
- `CultureInfo` 에 따라 파싱 결과가 달라질 수 있음을 이해한다

## 핵심 개념

### 1) 자주 쓰는 `string` 메서드
```csharp
"  hello  ".Trim();          // "hello"
"a,b,c".Split(',');          // ["a", "b", "c"]
string.Join("-", ["a","b"]); // "a-b"
"abc".Replace("b", "X");     // "aXc"
"hi".ToUpper();              // "HI"
"hello".Contains("ll");      // true
"hello".StartsWith("he");    // true
```

`string` 은 불변(02편)이라 모든 메서드는 **새 문자열을 반환**합니다.

### 2) 보간 문자열 `$"..."`
변수와 식을 그대로 끼워 넣는 가장 깔끔한 합성 방법입니다.

```csharp
double pi = 3.14159;
DateTime now = DateTime.Now;

Console.WriteLine($"파이는 약 {pi:F2}");           // 소수점 2자리
Console.WriteLine($"오늘은 {now:yyyy-MM-dd}");
Console.WriteLine($"정렬: |{42,5}|");              // 폭 5, 우측 정렬
```

형식 지정자는 콜론 뒤에 옵니다. `F2` 는 소수점 2자리, `yyyy-MM-dd` 는 ISO 날짜.

### 3) `StringBuilder` — 반복 합성용
루프 안에서 `+=` 로 문자열을 누적하면 매번 새 객체가 생깁니다. 횟수가 많아지면 매우 느려요. `StringBuilder` 는 내부 버퍼에 이어 붙이므로 빠릅니다.

```csharp
using System.Text;

var sb = new StringBuilder();
for (int i = 0; i < 1000; i++)
    sb.Append(i).Append(',');

string result = sb.ToString();
```

수십 번 이하의 합성은 그냥 `+` 도 괜찮습니다. 수백~수천 번 반복이면 `StringBuilder`.

### 4) `Regex` — 정규표현식
`System.Text.RegularExpressions` 네임스페이스의 정규표현식.

```csharp
using System.Text.RegularExpressions;

Regex.IsMatch("hello123", @"\d+");           // true (숫자 포함?)

var m = Regex.Match("주문 #4242", @"\d+");
if (m.Success) Console.WriteLine(m.Value);    // "4242"

foreach (Match x in Regex.Matches("a1 b22 c333", @"\d+"))
    Console.WriteLine(x.Value);               // 1, 22, 333
```

`@"..."` 는 백슬래시를 그대로 받는 verbatim 문자열 — 정규식과 잘 어울립니다.

### 5) Culture 함정
숫자나 날짜 파싱은 **시스템 culture** 의 영향을 받습니다. 예를 들어 독일은 `,` 가 소수점입니다.

```csharp
using System.Globalization;

// 한국·영국 등: "3.14" 가 3.14
// 독일(de-DE): "3.14" 는 314 로 해석되고, "3,14" 가 3.14
decimal de = decimal.Parse("3,14", CultureInfo.GetCultureInfo("de-DE"));
decimal inv = decimal.Parse("3.14", CultureInfo.InvariantCulture);
```

설정/CSV/로그처럼 **기계가 읽는 형식**은 항상 `CultureInfo.InvariantCulture` 를 명시합니다. 사용자 화면 표시에만 시스템 culture 를 씁니다.

## 예제로 보기

### 예제 1 — `StringMethods` : 자주 쓰는 메서드들
```csharp
string s = "  Hello, C# World  ";

Console.WriteLine($"원본    : [{s}]");
Console.WriteLine($"Trim    : [{s.Trim()}]");
Console.WriteLine($"Upper   : [{s.Trim().ToUpper()}]");
Console.WriteLine($"Replace : [{s.Trim().Replace("C#", "DotNet")}]");

string[] parts = "사과,바나나,체리".Split(',');
Console.WriteLine($"Split   : {parts.Length}개 → {string.Join(" | ", parts)}");

Console.WriteLine($"Contains 'C#'   : {s.Contains("C#")}");
Console.WriteLine($"StartsWith ' ' : {s.StartsWith(" ")}");
```
**실행 결과**
```
원본    : [  Hello, C# World  ]
Trim    : [Hello, C# World]
Upper   : [HELLO, C# WORLD]
Replace : [Hello, DotNet World]
Split   : 3개 → 사과 | 바나나 | 체리
Contains 'C#'   : True
StartsWith ' ' : True
```
**메모:** 메서드 체이닝으로 변환을 이어 붙일 수 있습니다.

### 예제 2 — `Interpolation` : 보간 문자열과 형식
```csharp
double pi = 3.14159265;
int score = 87;
DateTime now = new DateTime(2025, 5, 18, 14, 30, 0);

Console.WriteLine($"파이      : {pi:F2}");
Console.WriteLine($"점수      : {score,5}점");      // 폭 5, 우측 정렬
Console.WriteLine($"점수(좌) : {score,-5}|");       // 음수 폭 → 좌측 정렬
Console.WriteLine($"날짜      : {now:yyyy-MM-dd HH:mm}");
Console.WriteLine($"퍼센트    : {0.873:P1}");        // 1자리 백분율
```
**실행 결과**
```
파이      : 3.14
점수      :    87점
점수(좌) : 87   |
날짜      : 2025-05-18 14:30
퍼센트    : 87.3%
```
**메모:** `,N` 은 폭, `:F2` 는 형식, 둘 다 함께 쓰면 `{value,10:F2}`.

### 예제 3 — `StringBuilderUse` : 반복 합성 성능
```csharp
using System.Text;

var sb = new StringBuilder();
sb.Append("[");
for (int i = 1; i <= 5; i++)
{
    if (i > 1) sb.Append(", ");
    sb.Append(i);
}
sb.Append("]");

Console.WriteLine(sb.ToString());
Console.WriteLine($"길이: {sb.Length}");
// 성능 메모: 루프가 수백 회 이상이면 += 대비 매우 빠르다
```
**실행 결과**
```
[1, 2, 3, 4, 5]
길이: 15
```
**메모:** 짧은 합성은 `+` 도 충분하지만, 반복이 많을수록 `StringBuilder` 의 이점이 커집니다.

### 예제 4 — `RegexBasics` : 정규표현식 기본
```csharp
using System.Text.RegularExpressions;

string text = "문의 메일은 alice@example.com 또는 bob@test.io 로 보내세요.";

// 1) 포함 여부
bool hasDigit = Regex.IsMatch("주문 #42", @"\d+");
Console.WriteLine($"숫자 포함? {hasDigit}");

// 2) 첫 매치
Match first = Regex.Match("가격 1500원", @"\d+");
Console.WriteLine($"첫 숫자: {first.Value}");

// 3) 모든 이메일 추출
foreach (Match m in Regex.Matches(text, @"[\w\.-]+@[\w\.-]+\.\w+"))
{
    Console.WriteLine($"메일: {m.Value}");
}
```
**실행 결과**
```
숫자 포함? True
첫 숫자: 1500
메일: alice@example.com
메일: bob@test.io
```
**메모:** 실무 이메일 정규식은 훨씬 복잡합니다 — 학습용 단순 패턴입니다.

### 예제 5 — `Culture` : 같은 문자열, 다른 결과
```csharp
using System.Globalization;

string s = "3,14";

decimal de  = decimal.Parse(s, CultureInfo.GetCultureInfo("de-DE"));
decimal inv;
bool ok = decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out inv);

Console.WriteLine($"de-DE 해석     : {de}");           // 3.14
Console.WriteLine($"Invariant 해석 : ok={ok}, 값={inv}"); // ','를 천 단위로 보아 314 또는 실패

// 안전한 기계 포맷
decimal price = 1234.56m;
Console.WriteLine($"기계 포맷: {price.ToString(CultureInfo.InvariantCulture)}");
```
**실행 결과**
```
de-DE 해석     : 3.14
Invariant 해석 : ok=True, 값=314
기계 포맷: 1234.56
```
**메모:** CSV·설정 파일은 항상 `InvariantCulture` 로 읽고 쓰세요. UI 표시에만 사용자 culture 를 씁니다.

## 자주 하는 실수
1. 루프에서 `+=` 로 문자열 합성 — 횟수 많으면 `StringBuilder` 로.
2. 사용자 입력을 `Trim` 없이 비교 — 양 끝 공백 때문에 실패.
3. 정규식에서 `\\d` 처럼 이스케이프 두 번 — `@"\d+"` 의 verbatim 문자열 사용.
4. 숫자 파싱에 culture 무시 — 운영 환경 locale 에 따라 결과가 달라진다. **CSV/JSON 같은 데이터엔 `InvariantCulture`**.
5. 대소문자 비교에 `==` 사용 — `string.Equals(a, b, StringComparison.OrdinalIgnoreCase)` 가 안전.

## 정리
- `Trim`/`Split`/`Join`/`Replace` 가 일상 도구
- 합성은 보간 문자열 `$"..."` + 형식 지정자
- 많이 반복하면 `StringBuilder`
- 패턴 매칭은 `Regex.IsMatch`/`Match`/`Matches`
- 데이터 형식 파싱엔 `CultureInfo.InvariantCulture` 를 명시

## 직접 해 보기
```bash
cd src/StringMethods && dotnet run
cd ../Interpolation && dotnet run
cd ../StringBuilderUse && dotnet run
cd ../RegexBasics && dotnet run
cd ../Culture && dotnet run
```

## 다음 단원
[18_델리게이트와_람다](../../05_모던_CSharp/18_델리게이트와_람다/) — 함수 자체를 변수처럼 다루는 모던 C# 의 핵심을 배웁니다.
