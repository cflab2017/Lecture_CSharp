# 05. 메서드

같은 일을 여러 곳에서 한다면 **메서드(method)** 로 묶어 두는 게 답입니다. 메서드는 이름·매개변수·반환값으로 정의되며, 같은 이름을 다른 시그니처로 여러 개 둘 수도 있습니다(오버로딩).

## 학습 목표
- 메서드 선언·호출 흐름을 이해한다
- 매개변수와 반환 타입을 명시할 수 있다
- **오버로딩(overloading)** 의 의미와 규칙을 안다
- `out` / `ref` / `in` 키워드의 용도를 구분한다
- 선택적(optional) 매개변수와 명명된(named) 인수를 사용한다

## 핵심 개념

### 1) 메서드 선언과 호출
```csharp
static int Add(int a, int b)   // 반환 타입 매개변수
{
    return a + b;
}

int s = Add(3, 4);             // 호출 → 7
```
- 반환값이 없으면 `void`
- top-level statements 안에서 `static` 으로 선언하면 같은 파일에서 호출 가능

### 2) 오버로딩
**같은 이름** 의 메서드를 **시그니처(매개변수 개수/타입)** 만 다르게 여러 개 둘 수 있습니다. 반환 타입만 다른 건 오버로딩이 아닙니다.

```csharp
static int Add(int a, int b) => a + b;
static double Add(double a, double b) => a + b;

int    i = Add(1, 2);       // int 버전
double d = Add(1.5, 2.5);   // double 버전
```

### 3) `out` / `ref` / `in`
| 키워드 | 들어갈 때 | 나올 때 | 용도 |
|---|---|---|---|
| `out` | 초기화 불필요 | 메서드가 **반드시 대입** | "두 번째 반환값" |
| `ref` | 초기화 필수 | 양방향 | 호출자 값을 직접 수정 |
| `in` | 초기화 필수 | 읽기 전용 | 큰 struct 의 복사 비용 절감 |

```csharp
if (int.TryParse("42", out int n))   // out
    Console.WriteLine(n);

static void Swap(ref int a, ref int b) { (a, b) = (b, a); }   // ref
static void Print(in Point p) { Console.WriteLine($"{p.X},{p.Y}"); } // in
```

호출 시 `out`/`ref` 는 키워드를 다시 써 줘야 합니다: `Swap(ref x, ref y);`.

### 4) 선택적(optional) 매개변수
끝쪽 매개변수에 기본값을 주면 생략 가능합니다.

```csharp
static void Greet(string name, string lang = "ko")
{
    Console.WriteLine(lang == "ko" ? $"안녕, {name}!" : $"Hi, {name}!");
}

Greet("지수");           // 안녕, 지수!
Greet("Jisoo", "en");    // Hi, Jisoo!
```

### 5) 명명된(named) 인수
인수를 **이름으로 지정** 하면 순서와 무관하게 전달 가능. 가독성·기본값 건너뛰기에 유용.

```csharp
static void Order(string item, int qty = 1, bool gift = false) { /* ... */ }

Order(item: "사과", gift: true);    // qty 는 기본값 1, gift 만 명시
```

## 예제로 보기

### 예제 1 — `Basic/Program.cs` : 메서드로 분리해 호출
```csharp
static int Add(int a, int b)
{
    return a + b;
}

int x = Add(3, 4);
int y = Add(10, 20);

Console.WriteLine($"Add(3, 4)  = {x}");
Console.WriteLine($"Add(10,20) = {y}");
```
**실행 결과**
```
Add(3, 4)  = 7
Add(10,20) = 30
```
**메모:** top-level statements 파일에서는 메서드를 **파일 마지막 쪽에 모아 두는 것이 일반적**입니다. (현재 예제는 짧아 위쪽에 뒀습니다.)

### 예제 2 — `Overloading/Program.cs` : 같은 이름, 다른 시그니처
```csharp
Console.WriteLine($"Calc.Add(1, 2)       = {Calc.Add(1, 2)}");
Console.WriteLine($"Calc.Add(1.5, 2.5)   = {Calc.Add(1.5, 2.5)}");

static class Calc
{
    public static int Add(int a, int b) => a + b;
    public static double Add(double a, double b) => a + b;
}
```
**실행 결과**
```
Calc.Add(1, 2)       = 3
Calc.Add(1.5, 2.5)   = 4
```
**메모:** `=>` 는 **expression-bodied member** — 본문이 한 줄이면 깔끔하게 쓸 수 있습니다. 로컬 함수는 오버로딩이 안 되므로 같은 이름을 쓰려면 `static class` 로 감싸야 합니다.

### 예제 3 — `OutRefIn/Program.cs` : `out`, `ref`, `in` 시연
```csharp
namespace CodingNow.Lecture.Basics05;

internal static class Program
{
    public static void Main()
    {
        // out: TryParse 패턴
        if (int.TryParse("123", out int parsed))
            Console.WriteLine($"파싱 성공: {parsed}");

        // ref: 두 값 교환
        int x = 1, y = 2;
        Swap(ref x, ref y);
        Console.WriteLine($"Swap 후 x={x}, y={y}");

        // in: 읽기 전용 참조 전달
        Point p = new(10, 20);
        Print(in p);
    }

    static void Swap(ref int a, ref int b)
    {
        (a, b) = (b, a);
    }

    static void Print(in Point p)
    {
        // p.X = 0;   // 컴파일 에러: in 은 읽기 전용
        Console.WriteLine($"Point({p.X}, {p.Y})");
    }
}

internal readonly struct Point
{
    public int X { get; }
    public int Y { get; }
    public Point(int x, int y) { X = x; Y = y; }
}
```
**실행 결과**
```
파싱 성공: 123
Swap 후 x=2, y=1
Point(10, 20)
```
**메모:** `out int parsed` 는 호출 자리에서 **변수를 동시에 선언** 합니다. C# 7+ 기능.

### 예제 4 — `OptionalNamed/Program.cs` : 선택적·명명된 인수
```csharp
static void Order(string item, int qty = 1, bool gift = false)
{
    string wrap = gift ? " (선물 포장)" : "";
    Console.WriteLine($"{item} × {qty}{wrap}");
}

Order("사과");                       // 기본값
Order("배", 3);                       // qty 지정
Order(item: "포도", gift: true);     // 이름으로 — qty 는 기본 1
Order("귤", qty: 5, gift: true);
```
**실행 결과**
```
사과 × 1
배 × 3
포도 × 1 (선물 포장)
귤 × 5 (선물 포장)
```
**메모:** 선택적 매개변수는 **항상 끝쪽** 에 와야 합니다. 가운데에 두려면 호출자가 명명된 인수로 모두 지정해야 해 불편합니다.

## 자주 하는 실수
1. 메서드 이름은 같지만 **반환 타입만 다르게** 정의 — 컴파일 에러. 시그니처에 반환 타입은 포함되지 않습니다.
2. `out` 매개변수에 메서드 안에서 **값을 안 주고** 끝냄 — 컴파일 에러.
3. 호출 시 `ref`/`out` 키워드 누락 — `Swap(x, y)` 가 아니라 `Swap(ref x, ref y)`.
4. 선택적 매개변수의 **기본값을 가변 객체** 로 — 컴파일 타임 상수만 허용.
5. 너무 긴 매개변수 목록 — 4~5 개를 넘으면 객체로 묶는 걸 고려.

## 정리
- 메서드는 입력(매개변수) → 처리 → 출력(반환값) 의 단위다
- 오버로딩은 **시그니처가 달라야** 성립
- `out` 은 "추가 반환", `ref` 는 "양방향", `in` 은 "읽기 전용 참조"
- 선택적·명명된 인수로 호출 코드를 깔끔하게 유지

## 직접 해 보기
```bash
cd src/Basic && dotnet run
cd ../Overloading && dotnet run
cd ../OutRefIn && dotnet run
cd ../OptionalNamed && dotnet run
```

## 다음 단원
[../../02_객체지향/06_클래스와_객체](../../02_객체지향/06_클래스와_객체/) — 데이터와 동작을 묶는 클래스로 한 단계 도약합니다.
