# 02. 변수와 타입

프로그래밍은 결국 "값을 기억하고 가공하는 일"입니다. C#은 **정적 타입(statically typed)** 언어라 모든 변수는 컴파일 타임에 타입이 정해집니다. 이 단원에서 기본 타입과 값/참조 타입의 차이를 다집니다.

## 학습 목표
- 기본 타입 `int`/`long`/`double`/`decimal`/`bool`/`char`/`string` 의 크기와 범위를 안다
- **값 타입(value type)** 과 **참조 타입(reference type)** 의 차이를 이해한다
- `var` / `const` / `readonly` 를 적재적소에 쓴다
- 문자열의 불변성(immutability)을 이해한다
- **박싱(boxing)** 이 무엇이고 왜 느린지 안다

## 핵심 개념

### 1) 기본 타입표
| 타입 | 크기 | 범위/용도 | 예 |
|---|---|---|---|
| `int` | 4 B | 약 ±21억, 일반 정수 | `int n = 100;` |
| `long` | 8 B | 약 ±9.2×10¹⁸, 큰 정수 | `long big = 10_000_000_000L;` |
| `double` | 8 B | 부동소수점, 일반 실수 | `double pi = 3.14;` |
| `decimal` | 16 B | 28자리 정밀 실수, 돈 계산용 | `decimal won = 1500m;` |
| `bool` | 1 B | `true` / `false` | `bool ok = true;` |
| `char` | 2 B | 유니코드 한 글자 | `char c = '가';` |
| `string` | 가변 | 문자열 (참조 타입) | `string s = "hi";` |

`long` 은 끝에 `L`, `decimal` 은 끝에 `m` 을 붙입니다.

### 2) 값 타입 vs 참조 타입
- **값 타입(struct, int, double, bool, char, enum...)**: 값 자체가 변수에 저장. **복사 시 통째로 복제**.
- **참조 타입(class, string, array...)**: 힙에 객체가 있고 변수는 그 주소만 가짐. **복사 시 주소만 공유**.

```csharp
int a = 10;
int b = a;      // 값 복사
b = 20;
// a == 10, b == 20  (서로 독립)

int[] arr1 = [1, 2, 3];
int[] arr2 = arr1;   // 참조 복사
arr2[0] = 99;
// arr1[0] == 99    (같은 배열을 가리킴)
```

### 3) `var` : 타입 추론
`var` 는 컴파일러가 우변을 보고 타입을 추론해 줍니다. **타입이 사라지는 게 아니라 생략될 뿐**입니다.

```csharp
var name = "Alice";   // string
var age = 30;         // int
var pi = 3.14;        // double
```

지역 변수에만 쓸 수 있고, 초기화가 없으면 못 씁니다(`var x;` 불가).

### 4) `const` 와 `readonly`
- `const`: **컴파일 타임 상수**. 선언과 동시에 초기화, 이후 변경 불가. 기본 타입과 `string` 에만 가능.
- `readonly`: **런타임에 한 번만 대입 가능**. 생성자에서 정해지는 값에 쓴다.

```csharp
const double Pi = 3.14159;        // 절대 안 변하는 값
// Pi = 3.14;  // 컴파일 에러
```

### 5) 문자열은 불변(immutable)
`string` 은 참조 타입이지만 **내용을 바꿀 수 없습니다**. `+=` 나 `Replace` 는 새 문자열을 만들어 반환합니다.

```csharp
string s = "Hello";
s += ", World!";   // 새 문자열 생성, s 는 새 객체를 가리킴
```

### 6) 박싱(boxing)
값 타입을 `object` 같은 참조 타입으로 다룰 때 **힙에 복사본을 만드는 작업**입니다. 느리고 GC 압박을 주므로 가급적 피합니다.

```csharp
int n = 42;
object o = n;     // 박싱: 힙에 새 객체 생성
int m = (int)o;   // 언박싱
```

## 예제로 보기

### 예제 1 — `Primitives/Program.cs` : 기본 타입 살펴보기
```csharp
int i = 100;
long l = 10_000_000_000L;
double d = 3.14;
decimal m = 1500.99m;
bool b = true;
char c = '가';
string s = "안녕";

Console.WriteLine($"int    : {i} ({sizeof(int)} B)");
Console.WriteLine($"long   : {l} ({sizeof(long)} B)");
Console.WriteLine($"double : {d} ({sizeof(double)} B)");
Console.WriteLine($"decimal: {m}");
Console.WriteLine($"bool   : {b}, char: {c}, string: {s}");
```
**실행 결과**
```
int    : 100 (4 B)
long   : 10000000000 (8 B)
double : 3.14 (8 B)
decimal: 1500.99
bool   : True, char: 가, string: 안녕
```
**메모:** `sizeof(decimal)` 은 unsafe 컨텍스트가 필요해 생략. `_` 로 자릿수 구분이 가능합니다(`10_000_000_000`).

### 예제 2 — `ValueVsReference/Program.cs` : 복사 동작 비교
```csharp
namespace CodingNow.Lecture.Basics02;

internal static class Program
{
    public static void Main()
    {
        Point p1 = new(1, 2);
        Point p2 = p1;          // 값 복사
        p2.X = 99;
        Console.WriteLine($"p1=({p1.X},{p1.Y})  p2=({p2.X},{p2.Y})");

        Box b1 = new() { Value = 10 };
        Box b2 = b1;            // 참조 복사
        b2.Value = 99;
        Console.WriteLine($"b1={b1.Value}  b2={b2.Value}");
    }
}

internal struct Point
{
    public int X;
    public int Y;
    public Point(int x, int y) { X = x; Y = y; }
}

internal class Box
{
    public int Value;
}
```
**실행 결과**
```
p1=(1,2)  p2=(99,2)
b1=99  b2=99
```
**메모:** `struct` 는 값 타입이라 독립적, `class` 는 참조 타입이라 같은 객체를 공유합니다.

### 예제 3 — `VarConst/Program.cs` : `var`, `const`, `readonly`
```csharp
var name = "Alice";   // 추론: string
var age = 30;         // 추론: int

const double Pi = 3.14159;
const string Greet = "안녕!";

Console.WriteLine($"{name}, {age}세, {Greet} (Pi={Pi})");
// Pi = 3.0;   // 컴파일 에러
```
**실행 결과**
```
Alice, 30세, 안녕! (Pi=3.14159)
```
**메모:** `var` 는 가독성을 위해 사용하지만, 우변 타입이 명확하지 않으면 명시 타입이 낫습니다.

### 예제 4 — `StringBasics/Program.cs` : 문자열 불변성
```csharp
string a = "Hello";
string b = a;
a += ", World!";          // 새 문자열 생성

Console.WriteLine($"a = {a}");
Console.WriteLine($"b = {b}");   // b 는 여전히 "Hello"

// 연결 방법 비교
string c1 = string.Concat("값:", 42);
string c2 = $"값:{42}";          // 문자열 보간 (권장)
Console.WriteLine(c1);
Console.WriteLine(c2);
```
**실행 결과**
```
a = Hello, World!
b = Hello
값:42
값:42
```
**메모:** `a += "..."` 이후에도 `b` 는 원래 문자열을 그대로 가리킵니다. 문자열은 불변이라 공유해도 안전합니다.

## 자주 하는 실수
1. `int` 범위(약 ±21억)를 넘기는데 `int` 를 써서 오버플로우 — 큰 수는 `long`.
2. 돈 계산에 `double` 사용 — 부동소수점 오차. **반드시 `decimal`**.
3. `var x;` 처럼 초기화 없이 `var` 사용 — 컴파일 에러.
4. `string` 을 루프에서 `+=` 로 누적 — 매번 새 객체. 많을 땐 `StringBuilder` (16편 참고).
5. `const` 와 `readonly` 혼동 — `const` 는 컴파일 타임 상수, `readonly` 는 런타임 1회 대입.

## 정리
- 정수는 `int`/`long`, 실수는 `double`(과학) / `decimal`(돈)
- 값 타입은 복사 시 통째 복제, 참조 타입은 주소만 공유
- `var` 는 타입 추론 편의, `const` 는 절대 불변
- `string` 은 참조 타입이지만 불변 — 안전하게 공유 가능
- `object` 로 값 타입을 담으면 박싱 발생 → 가급적 피하자

## 직접 해 보기
```bash
cd src/Primitives && dotnet run
cd ../ValueVsReference && dotnet run
cd ../VarConst && dotnet run
cd ../StringBasics && dotnet run
```

## 다음 단원
[03_연산자와_표현식](../03_연산자와_표현식/) — 변수로 계산하고 비교하는 도구를 배웁니다.
