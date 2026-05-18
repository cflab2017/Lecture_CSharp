# 10. 인터페이스

상속이 "is-a"(개는 동물이다) 관계라면, **인터페이스(interface)** 는 "can-do"(이 객체는 출력할 수 있다) 관계입니다. 데이터나 본문 없이 **"이런 메서드가 있어야 한다"** 는 계약만 제시하고, 구현은 클래스에 맡깁니다. C# 클래스는 부모를 하나만 가질 수 있지만 인터페이스는 여러 개를 동시에 구현할 수 있습니다.

## 학습 목표
- `interface` 키워드로 인터페이스를 선언할 수 있다
- 클래스가 인터페이스를 구현(`: IName`)할 수 있다
- 한 클래스가 여러 인터페이스를 동시에 구현할 수 있다
- 기본 구현(default interface member, C# 8+)을 이해한다
- 명시적 구현(explicit interface implementation) 이 필요한 상황을 안다

## 핵심 개념

### 1) 인터페이스 선언과 구현
```csharp
interface IPrintable
{
    void Print();   // 본문 없음, 세미콜론으로 끝
}

class Book : IPrintable
{
    public string Title = "";
    public void Print() => Console.WriteLine($"책: {Title}");
}
```
관례적으로 인터페이스 이름은 `I` 로 시작합니다(`IComparable`, `IDisposable` 등).

인터페이스에는 기본적으로 **필드/생성자를 둘 수 없습니다.** (프로퍼티는 OK)

### 2) 인터페이스 = 약속
구현 클래스는 모든 멤버를 빠짐없이 구현해야 합니다. 누락 시 컴파일 에러.

```csharp
class Document : IPrintable   // Print() 안 만들면 에러
{
    public void Print() => Console.WriteLine("문서 출력");
}
```

### 3) 인터페이스 변수로 다형성
```csharp
IPrintable p = new Book();
p.Print();              // Book.Print() 실행
```
**"이 변수는 Print 를 호출할 수 있다는 보장"** 만 갖습니다. 어떤 클래스든 상관없음.

### 4) 여러 인터페이스 구현
```csharp
class Image : IPrintable, IResizable
{
    public void Print() => Console.WriteLine("이미지 출력");
    public void Resize(int w, int h) => Console.WriteLine($"크기 변경: {w}x{h}");
}
```
콤마로 나열. 클래스 상속과 같이 쓸 때는 부모 클래스를 먼저, 그 뒤에 인터페이스들.

### 5) 기본 구현(default interface member, C# 8+)
인터페이스에도 기본 구현을 줄 수 있습니다. 구현 클래스가 override 안 해도 그 기본 동작이 호출됩니다.
```csharp
interface ILogger
{
    void Log(string msg);
    void Warn(string msg) => Log($"[WARN] {msg}");   // 기본 구현
}
```
단, 기본 구현은 **인터페이스 타입으로 호출**할 때만 보입니다.
```csharp
ILogger lg = new ConsoleLogger();
lg.Warn("주의");        // OK
// new ConsoleLogger().Warn("주의");   // 컴파일 에러 (인스턴스 멤버처럼 못 부름)
```

### 6) 명시적 구현(explicit interface implementation)
두 인터페이스에 같은 이름의 멤버가 있거나, 메서드를 외부에 직접 노출하고 싶지 않을 때 씁니다.
```csharp
class MyNumber : IComparable<int>
{
    public int Value;

    // 명시적 구현: 클래스 인스턴스에서는 직접 호출 못함
    int IComparable<int>.CompareTo(int other) => Value.CompareTo(other);
}

var n = new MyNumber { Value = 5 };
// n.CompareTo(10);                 // 컴파일 에러
((IComparable<int>)n).CompareTo(10); // OK — 인터페이스로 캐스팅 후 호출
```

## 예제로 보기

### 예제 1 — `IPrintable` : 한 인터페이스, 두 구현
```csharp
// Program.cs
using CodingNow.Lecture.Oop10;

IPrintable[] items = [new Book("객체지향의 사실과 오해"), new Invoice(99000)];

foreach (var item in items)
{
    item.Print();
}
```
```csharp
// IPrintable.cs
namespace CodingNow.Lecture.Oop10;

internal interface IPrintable
{
    void Print();
}

// Book.cs
internal class Book : IPrintable
{
    public string Title;
    public Book(string title) => Title = title;
    public void Print() => Console.WriteLine($"책: {Title}");
}

// Invoice.cs
internal class Invoice : IPrintable
{
    public int Amount;
    public Invoice(int amount) => Amount = amount;
    public void Print() => Console.WriteLine($"청구서: {Amount}원");
}
```
**실행 결과**
```
책: 객체지향의 사실과 오해
청구서: 99000원
```
**메모:** `Book` 과 `Invoice` 는 부모-자식 관계가 아닌데도, "Print 할 수 있다" 는 공통 능력으로 묶였습니다.

### 예제 2 — `MultipleInterfaces` : 한 클래스가 여러 인터페이스 구현
```csharp
// Program.cs
using CodingNow.Lecture.Oop10;

var img = new Image();
img.Print();
img.Resize(800, 600);
```
```csharp
// Interfaces.cs
namespace CodingNow.Lecture.Oop10;

internal interface IPrintable
{
    void Print();
}

internal interface IResizable
{
    void Resize(int width, int height);
}

// Image.cs
internal class Image : IPrintable, IResizable
{
    public void Print() => Console.WriteLine("이미지를 인쇄합니다");
    public void Resize(int width, int height)
        => Console.WriteLine($"이미지 크기를 {width}x{height} 로 변경");
}
```
**실행 결과**
```
이미지를 인쇄합니다
이미지 크기를 800x600 로 변경
```
**메모:** 클래스는 단일 상속이지만 인터페이스는 갯수 제한이 없습니다.

### 예제 3 — `DefaultMember` : 인터페이스 기본 구현
```csharp
// Program.cs
using CodingNow.Lecture.Oop10;

ILogger lg = new ConsoleLogger();
lg.Log("정상 메시지");
lg.Warn("이건 경고");        // 기본 구현이 호출됨

// new ConsoleLogger().Warn(...) 는 호출 불가 — 기본 구현은 인터페이스 타입으로만 보임
```
```csharp
// ILogger.cs
namespace CodingNow.Lecture.Oop10;

internal interface ILogger
{
    void Log(string msg);

    // 기본 구현: 구현 클래스가 override 하지 않으면 이 동작이 사용된다.
    void Warn(string msg) => Log($"[WARN] {msg}");
}

// ConsoleLogger.cs
internal class ConsoleLogger : ILogger
{
    public void Log(string msg) => Console.WriteLine(msg);
    // Warn 은 구현하지 않음 → 기본 구현이 그대로 쓰임
}
```
**실행 결과**
```
정상 메시지
[WARN] 이건 경고
```
**메모:** 기존 인터페이스에 새 메서드를 추가할 때, 기본 구현을 같이 주면 기존 구현 클래스를 깨지 않습니다.

### 예제 4 — `ExplicitImpl` : 명시적 인터페이스 구현
```csharp
// Program.cs
using CodingNow.Lecture.Oop10;

var p = new MyPrinter();

// p.Print();   // 컴파일 에러 — 명시적 구현이라 클래스에서 직접 안 보임

IPrintable pr = p;
pr.Print();    // 인터페이스로 캐스팅 후 호출

((IPrintable)p).Print();   // 같은 의미
```
```csharp
// IPrintable.cs
namespace CodingNow.Lecture.Oop10;

internal interface IPrintable
{
    void Print();
}

// MyPrinter.cs
internal class MyPrinter : IPrintable
{
    // 명시적 구현: 메서드 이름 앞에 "인터페이스명." 을 붙이고, 접근 제한자를 쓰지 않는다.
    void IPrintable.Print() => Console.WriteLine("(인터페이스로만 호출 가능) 출력 중");
}
```
**실행 결과**
```
(인터페이스로만 호출 가능) 출력 중
(인터페이스로만 호출 가능) 출력 중
```
**메모:** 같은 이름 메서드를 가진 인터페이스 두 개를 구현해야 할 때, 또는 메서드를 외부에 노출하기 싫을 때 명시적 구현이 유용합니다.

## 자주 하는 실수
1. 인터페이스 멤버에 `public` 을 붙인다 — 모든 멤버는 기본적으로 public, 명시하면 컴파일 에러(C# 8+ 일부 예외 제외).
2. 클래스에서 구현할 때 `public` 을 빠뜨린다 — 클래스 쪽에서는 반드시 `public` 명시(명시적 구현은 예외).
3. 인터페이스에 필드를 두려고 한다 — 인터페이스에는 필드 없음, 프로퍼티는 OK.
4. 명시적 구현 후 클래스 인스턴스로 직접 호출하려 한다 → 컴파일 에러. 인터페이스 변수로 캐스팅 필요.
5. `interface IFoo : IBar` 처럼 인터페이스끼리 상속할 수 있다는 걸 모른다 (확장 인터페이스 패턴).

## 정리
- 인터페이스는 "할 수 있는 능력"의 계약. 멤버 본문이 없고 필드도 없다.
- 한 클래스가 여러 인터페이스를 동시에 구현할 수 있다 — 클래스 다중 상속의 안전한 대체.
- 인터페이스 변수로 다형성이 자연스럽게 작동한다.
- 기본 구현(C# 8+)과 명시적 구현은 충돌·확장 시나리오의 해결책.

## 직접 해 보기
```bash
cd src/IPrintable
dotnet run

cd ../MultipleInterfaces
dotnet run

cd ../DefaultMember
dotnet run

cd ../ExplicitImpl
dotnet run
```

## 다음 단원
[11_배열](../../03_컬렉션_LINQ/11_배열/) — 같은 타입의 값을 일렬로 담는 가장 기본적인 컬렉션, 배열을 다룹니다.
