# 03. 연산자와 표현식

연산자는 값을 가공하는 기본 도구입니다. C#은 일반적인 산술/비교/논리 외에도 **null 안전 연산자**, **패턴 매칭(`is`)**, **삼항 연산자(`?:`)** 같은 표현식 친화 기능이 풍부합니다.

## 학습 목표
- 산술/비교/논리 연산자의 우선순위를 안다
- `int / int` 와 `double / int` 의 결과 차이를 이해한다
- `&&` / `||` 의 **단락 평가(short-circuit)** 동작을 안다
- `?.` `??` `??=` 로 null 을 안전하게 처리한다
- `is` / `as` 로 타입 검사·변환을 한다
- 삼항 연산자 `?:` 로 표현식을 간결하게 쓴다

## 핵심 개념

### 1) 산술 연산자
`+`, `-`, `*`, `/`, `%` 가 있습니다. 핵심은 **타입 규칙**입니다.

```csharp
int x = 7 / 2;        // 3   (정수 나눗셈, 소수점 버림)
double y = 7 / 2;     // 3.0 (먼저 정수 나눗셈 후 double 변환)
double z = 7.0 / 2;   // 3.5 (한쪽이 double 이면 실수 나눗셈)
int r = 7 % 2;        // 1   (나머지)
```

### 2) 비교/논리 연산자
- 비교: `==`, `!=`, `<`, `<=`, `>`, `>=` → `bool` 반환
- 논리: `&&`(AND), `||`(OR), `!`(NOT)

**단락 평가(short-circuit)**: `&&` 는 왼쪽이 `false` 면 오른쪽을 안 본다. `||` 는 왼쪽이 `true` 면 오른쪽을 안 본다.

```csharp
int? n = null;
if (n != null && n.Value > 0)   // n == null 이면 .Value 평가 안 됨
    Console.WriteLine("양수");
```

### 3) null-conditional `?.` 과 null-coalescing `??`, `??=`
- `obj?.Member` : `obj` 가 `null` 이면 전체가 `null`, 아니면 `Member` 평가
- `a ?? b` : `a` 가 `null` 이면 `b`
- `a ??= b` : `a` 가 `null` 일 때만 `b` 대입

```csharp
string? name = null;
int len = name?.Length ?? 0;          // null 이면 0
name ??= "(이름 없음)";               // null 일 때만 대입
Console.WriteLine($"{name} ({len})"); // (이름 없음) (0)
```

### 4) `is` 와 `as`
- `obj is Type` : 타입이면 `true`, 패턴 매칭과 결합 가능
- `obj as Type` : 캐스팅 실패 시 **예외 대신 `null`**

```csharp
object o = "hello";
if (o is string s)                 // 타입 검사 + 변수 선언
    Console.WriteLine(s.Length);   // 5

object o2 = 42;
string? s2 = o2 as string;         // 캐스팅 실패 → s2 == null
```

### 5) 삼항 연산자 `?:`
"조건 ? 참값 : 거짓값" — 짧은 if-else 를 표현식으로.

```csharp
int score = 75;
string grade = score >= 60 ? "합격" : "불합격";
```

## 예제로 보기

### 예제 1 — `Arithmetic/Program.cs` : 정수 vs 실수 나눗셈
```csharp
int a = 7, b = 2;

Console.WriteLine($"{a} + {b} = {a + b}");
Console.WriteLine($"{a} - {b} = {a - b}");
Console.WriteLine($"{a} * {b} = {a * b}");
Console.WriteLine($"{a} / {b} = {a / b}          (정수 나눗셈)");
Console.WriteLine($"{a} % {b} = {a % b}");
Console.WriteLine($"(double)a / b = {(double)a / b}  (실수 나눗셈)");
```
**실행 결과**
```
7 + 2 = 9
7 - 2 = 5
7 * 2 = 14
7 / 2 = 3          (정수 나눗셈)
7 % 2 = 1
(double)a / b = 3.5  (실수 나눗셈)
```
**메모:** 한쪽만 `(double)` 로 캐스팅해도 결과가 `double` 이 됩니다.

### 예제 2 — `LogicalOps/Program.cs` : 단락 평가 시연
```csharp
static bool Log(string label, bool value)
{
    Console.WriteLine($"  [{label} 평가됨 → {value}]");
    return value;
}

Console.WriteLine("(false && X) 결과:");
bool r1 = Log("A", false) && Log("B", true);
Console.WriteLine($"  => {r1}");

Console.WriteLine("(true || X) 결과:");
bool r2 = Log("C", true) || Log("D", false);
Console.WriteLine($"  => {r2}");
```
**실행 결과**
```
(false && X) 결과:
  [A 평가됨 → False]
  => False
(true || X) 결과:
  [C 평가됨 → True]
  => True
```
**메모:** `B`, `D` 는 평가되지 않습니다. 이게 단락 평가입니다.

### 예제 3 — `NullSafety/Program.cs` : `?.` `??` `??=`
```csharp
string? name = null;

int len = name?.Length ?? 0;          // null 이면 0
Console.WriteLine($"길이: {len}");

name ??= "(이름 없음)";               // null 일 때만 대입
Console.WriteLine($"이름: {name}");

// 다시 한 번: 이제는 null 이 아니므로 변하지 않음
name ??= "또 다른 기본값";
Console.WriteLine($"최종: {name}");
```
**실행 결과**
```
길이: 0
이름: (이름 없음)
최종: (이름 없음)
```
**메모:** `?.` 는 null 일 때 **전체 식이 null** 이 됩니다. 그래서 `??` 와 짝꿍처럼 자주 씁니다.

### 예제 4 — `IsAs/Program.cs` : 타입 검사와 캐스팅
```csharp
object[] items = ["hello", 42, 3.14, true];

foreach (object item in items)
{
    if (item is string s)
        Console.WriteLine($"문자열: '{s}' (길이 {s.Length})");
    else if (item is int n)
        Console.WriteLine($"정수: {n}");
    else
        Console.WriteLine($"기타: {item} ({item.GetType().Name})");
}

object boxed = 100;
string? wrong = boxed as string;      // 캐스팅 실패 → null
Console.WriteLine($"as 결과: {wrong ?? "(null)"}");
```
**실행 결과**
```
문자열: 'hello' (길이 5)
정수: 42
기타: 3.14 (Double)
기타: True (Boolean)
as 결과: (null)
```
**메모:** `is Type 변수` 패턴이 가장 깔끔합니다. `(string)boxed` 처럼 강제 캐스팅하면 실패 시 예외가 발생하니, 안전하게 가려면 `as` 또는 `is` 를 쓰세요.

## 자주 하는 실수
1. `7 / 2` 가 `3.5` 일 거라 기대 — 정수끼리는 정수 나눗셈.
2. `if (count = 0)` 처럼 `=` 와 `==` 혼동 — C#은 `bool` 이 아닌 식을 `if` 에 넣을 수 없어 컴파일 에러가 나서 그나마 안전하지만 주의.
3. `??` 와 `?:` 헷갈리기 — `??` 는 **null 여부**, `?:` 는 **일반 조건**.
4. `as` 로 값 타입 캐스팅 — `as int` 는 불가, `as int?` 로 써야 함.
5. `null` 인 객체에 `.` 으로 접근 → `NullReferenceException`. **항상 `?.` 고려**.

## 정리
- 정수 나눗셈/실수 나눗셈을 구분한다
- `&&`/`||` 의 단락 평가로 null 체크와 연쇄 검사를 안전하게
- `?.` `??` `??=` 는 null 처리 3종 세트
- `is`/`as` 로 타입을 안전하게 검사·변환
- 삼항 `?:` 는 짧은 분기를 깔끔하게 표현

## 직접 해 보기
```bash
cd src/Arithmetic && dotnet run
cd ../LogicalOps && dotnet run
cd ../NullSafety && dotnet run
cd ../IsAs && dotnet run
```

## 다음 단원
[04_제어문](../04_제어문/) — 조건과 반복으로 프로그램의 흐름을 제어합니다.
