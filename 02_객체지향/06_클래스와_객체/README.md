# 06. 클래스와 객체

지금까지는 변수와 메서드를 따로따로 다뤘다면, 이제부터는 데이터와 기능을 하나로 묶어 다루는 **클래스(class)** 를 배웁니다. 클래스는 객체를 찍어 내는 **틀(설계도)** 이고, 그 틀로 만든 인스턴스가 **객체(object)** 입니다.

## 학습 목표
- `class` 키워드로 새로운 타입을 정의할 수 있다
- 필드·메서드·생성자가 무엇인지 구별할 수 있다
- `this` 키워드의 역할을 안다
- 접근 제한자(`public`/`private`/`internal`/`protected`)를 적재적소에 쓸 수 있다
- `new` 로 객체를 만들고, 참조 변수가 실제로 무엇을 가리키는지 이해한다

## 핵심 개념

### 1) 클래스 = 설계도, 객체 = 실체
```csharp
class Person   // 설계도
{
    public string Name = "";
    public int Age;
}

Person alice = new Person();  // 설계도로 찍어 낸 실체(객체)
alice.Name = "Alice";
alice.Age = 30;
```
`Person` 자체는 메모리에 데이터가 없습니다. `new Person()` 을 호출해야 비로소 메모리에 객체가 만들어지고, 변수 `alice` 는 그 객체의 **참조(주소)** 를 담습니다.

### 2) 필드와 메서드
- **필드(field)**: 클래스가 가진 데이터(변수)
- **메서드(method)**: 클래스가 가진 동작(함수)

```csharp
class Counter
{
    public int Count;                       // 필드
    public void Increase() => Count++;      // 메서드
}
```

### 3) 생성자(constructor) 와 `this`
객체가 만들어질 때 자동으로 호출되는 특수 메서드입니다. **클래스 이름과 똑같이** 짓고, 반환 타입은 쓰지 않습니다.

```csharp
class Person
{
    public string Name;
    public int Age;

    public Person(string name, int age)
    {
        this.Name = name;   // this.Name 은 필드, name 은 매개변수
        this.Age = age;
    }
}
```
`this` 는 "지금 동작 중인 그 객체 자신"을 가리키는 키워드입니다. 매개변수와 필드 이름이 겹칠 때 구분용으로 자주 씁니다.

### 4) 접근 제한자(access modifier)
| 키워드 | 누구까지 접근 가능? |
|---|---|
| `public` | 어디서든 |
| `private` | 같은 클래스 안에서만 (기본값) |
| `internal` | 같은 어셈블리(프로젝트) 안에서만 |
| `protected` | 같은 클래스 + 자식 클래스 |

캡슐화의 기본: **데이터는 가능한 한 `private`**, **외부에서 쓸 동작만 `public`** 으로 노출.

### 5) `new` 키워드와 참조
```csharp
Person a = new Person("Alice", 30);
Person b = a;          // b 는 a 와 같은 객체를 가리킴 (복사 아님!)
b.Age = 99;
Console.WriteLine(a.Age); // 99 → a 도 같이 변함
```
클래스는 **참조 타입(reference type)**. 변수에 다른 변수를 대입해도 객체가 복제되지 않고, 같은 객체를 두 변수가 함께 가리킵니다.

## 예제로 보기

### 예제 1 — `PersonBasic` : 가장 단순한 클래스
```csharp
// Program.cs
using CodingNow.Lecture.Oop06;

var alice = new Person("Alice", 30);
alice.Greet();

var bob = new Person("Bob", 25);
bob.Greet();
```
```csharp
// Person.cs
namespace CodingNow.Lecture.Oop06;

internal class Person
{
    public string Name;
    public int Age;

    public Person(string name, int age)
    {
        this.Name = name;
        this.Age = age;
    }

    public void Greet()
    {
        Console.WriteLine($"안녕, 나는 {Name}({Age}세).");
    }
}
```
**실행 결과**
```
안녕, 나는 Alice(30세).
안녕, 나는 Bob(25세).
```
**메모:** 한 `Person` 클래스로 서로 독립된 객체 두 개를 찍어 냈습니다.

### 예제 2 — `AccessModifiers` : 접근 제한자 시연
```csharp
// Program.cs
using CodingNow.Lecture.Oop06;

var account = new Account(1000);
account.Deposit(500);            // public — OK
Console.WriteLine($"잔액: {account.GetBalance()}");

// account.balance = 0;          // private — 컴파일 에러
account.LogInternal();           // internal — 같은 프로젝트 안이라 OK
```
```csharp
// Account.cs
namespace CodingNow.Lecture.Oop06;

internal class Account
{
    private int balance;             // 외부 직접 변경 금지

    public Account(int initial)
    {
        balance = initial;
    }

    public void Deposit(int amount)  // 공개된 동작
    {
        if (amount <= 0) return;
        balance += amount;
    }

    public int GetBalance() => balance;

    internal void LogInternal()      // 같은 어셈블리 안에서만
    {
        Console.WriteLine($"[내부 로그] 잔액={balance}");
    }
}
```
**실행 결과**
```
잔액: 1500
[내부 로그] 현재 잔액 = 1500
```
**메모:** `balance` 를 `private` 으로 막고 `Deposit` 만 열어 두니, 잘못된 변경(음수 입금 등)을 막을 수 있습니다.

### 예제 3 — `MultiConstructor` : 생성자 오버로딩 + `this(...)`
```csharp
// Program.cs
using CodingNow.Lecture.Oop06;

var p1 = new Point();            // (0, 0)
var p2 = new Point(5);           // (5, 5)
var p3 = new Point(3, 7);        // (3, 7)

p1.Print();
p2.Print();
p3.Print();
```
```csharp
// Point.cs
namespace CodingNow.Lecture.Oop06;

internal class Point
{
    public int X;
    public int Y;

    public Point() : this(0, 0) { }            // 다른 생성자에게 위임
    public Point(int v) : this(v, v) { }       // 한 값을 X, Y 둘 다에
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Print() => Console.WriteLine($"({X}, {Y})");
}
```
**실행 결과**
```
(0, 0)
(5, 5)
(3, 7)
```
**메모:** `: this(...)` 는 "같은 클래스의 다른 생성자를 먼저 호출"하라는 뜻. 중복 코드를 줄여 줍니다.

### 예제 4 — `NewKeyword` : 참조의 의미 확인
```csharp
// Program.cs
using CodingNow.Lecture.Oop06;

var a = new Box(10);
var b = a;            // 복사가 아니다! 같은 객체를 b 도 가리킨다.
b.Value = 99;

Console.WriteLine($"a.Value = {a.Value}");
Console.WriteLine($"b.Value = {b.Value}");

var c = new Box(10);  // 완전히 다른 객체
Console.WriteLine($"a == b ? {ReferenceEquals(a, b)}");
Console.WriteLine($"a == c ? {ReferenceEquals(a, c)}");
```
```csharp
// Box.cs
namespace CodingNow.Lecture.Oop06;

internal class Box
{
    public int Value;
    public Box(int value) => Value = value;
}
```
**실행 결과**
```
a.Value = 99
b.Value = 99
a == b ? True
a == c ? False
```
**메모:** `b = a` 는 "주소를 복사"입니다. 값 자체가 같은지를 보려면 별도의 비교 로직(다음 단원의 프로퍼티/`Equals` 등)이 필요합니다.

## 자주 하는 실수
1. `new Person` 만 쓰고 `()` 를 빠뜨려 인스턴스가 만들어지지 않는 줄 안다 — 반드시 `new Person()` 처럼 호출 형태.
2. 필드와 매개변수 이름이 같은데 `this.` 를 안 붙여 자기 자신을 자기에게 대입(`Name = Name;`)하는 코드를 쓴다.
3. `private` 필드를 외부에서 직접 바꾸려고 `public` 으로 바꿔 버린다 — 캡슐화 깨짐. 다음 단원의 프로퍼티로 해결.
4. 두 변수가 같은 객체를 가리키는 줄 모르고 한쪽만 바꿨다고 안심한다.
5. 생성자 이름을 `Person()` 가 아니라 `Person CreatePerson()` 처럼 짓는다 — 클래스 이름과 정확히 같아야 합니다.

## 정리
- 클래스는 설계도, 객체는 그 설계도로 찍은 실체(`new`로 생성)
- 필드는 데이터, 메서드는 동작, 생성자는 객체 탄생 시 초기화 코드
- `this` 는 현재 객체 자신, `private`/`public` 으로 외부 노출 범위 조절
- 클래스는 참조 타입 — 변수는 객체 자체가 아니라 객체의 주소를 담는다

## 직접 해 보기
```bash
cd src/PersonBasic
dotnet run

cd ../AccessModifiers
dotnet run

cd ../MultiConstructor
dotnet run

cd ../NewKeyword
dotnet run
```

## 다음 단원
[07_프로퍼티와_캡슐화](../07_프로퍼티와_캡슐화/) — `public` 필드 대신 더 안전한 프로퍼티로 데이터를 다룹니다.
