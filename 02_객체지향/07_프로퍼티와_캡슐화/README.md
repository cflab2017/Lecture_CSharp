# 07. 프로퍼티와 캡슐화

이전 단원에서 `private` 필드 + `public` 메서드(`GetBalance` 등)로 데이터를 보호했습니다. C#에는 이 패턴을 깔끔하게 처리해 주는 **프로퍼티(property)** 가 따로 있습니다. 외부에서는 필드처럼 보이지만 내부에서는 메서드처럼 동작하는 멤버입니다.

## 학습 목표
- auto-property(`{ get; set; }`) 의 의미와 동작을 안다
- `init` 전용 setter 로 "한 번만 설정 가능한" 속성을 만든다
- `readonly` 필드와 `required` 키워드(C# 11)의 차이를 안다
- backing field 를 둔 full property 에서 값 검증을 한다

## 핵심 개념

### 1) auto-property
```csharp
class Person
{
    public string Name { get; set; } = "";   // 자동 생성된 숨은 필드와 연결
    public int Age { get; set; }
}
```
컴파일러가 내부에 `_name`, `_age` 같은 숨은 필드를 만들고 `get`/`set` 도 자동 작성해 줍니다.

### 2) `get` 만 가진 읽기 전용 프로퍼티
```csharp
public string Name { get; }            // 생성자에서만 설정 가능
```
객체가 만들어진 뒤에는 바꾸지 못합니다.

### 3) `init` 전용 setter (C# 9+)
```csharp
public string Name { get; init; } = "";
```
객체 초기화(`new Person { Name = "A" }`) 시점이나 생성자 안에서만 설정 가능. 그 이후엔 읽기 전용. **불변(immutable) 객체** 를 만들 때 유용합니다.

```csharp
var p = new Person { Name = "Alice" };
// p.Name = "Bob";   // 컴파일 에러
```

### 4) `required` 키워드 (C# 11)
"이 프로퍼티는 객체를 만들 때 반드시 값을 줘야 한다"는 표시입니다.
```csharp
class User
{
    public required string Email { get; init; }
}

var u = new User { Email = "a@b.com" };  // OK
// var u2 = new User();                  // 컴파일 에러
```
생성자 + `init` 의 안전성과 객체 초기화 문법의 편리함을 모두 잡습니다.

### 5) `readonly` 필드
프로퍼티 말고 **필드** 자체를 한 번만 쓰게 막는 키워드.
```csharp
class Circle
{
    public readonly double Pi = 3.14159;
}
```
생성자에서만 값 대입 가능. 보통 상수에 가까운 값에 씁니다.

### 6) full property — backing field + 검증
auto-property 만으로는 값 검증을 못 합니다. 검증이 필요하면 직접 backing field 를 두고 `set` 안에 로직을 둡니다.
```csharp
class Temperature
{
    private double celsius;

    public double Celsius
    {
        get => celsius;
        set
        {
            if (value < -273.15)
                throw new ArgumentException("절대영도보다 낮음");
            celsius = value;
        }
    }
}
```
`set` 안의 `value` 는 "들어온 새 값"을 가리키는 예약 매개변수입니다.

## 예제로 보기

### 예제 1 — `AutoProperty` : 가장 기본 형태
```csharp
// Program.cs
using CodingNow.Lecture.Oop07;

var p = new Person();
p.Name = "Alice";
p.Age = 30;
Console.WriteLine($"{p.Name} / {p.Age}");
```
```csharp
// Person.cs
namespace CodingNow.Lecture.Oop07;

internal class Person
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
}
```
**실행 결과**
```
Alice / 30
```
**메모:** 필드를 직접 노출하는 것과 비슷해 보여도, 나중에 `set` 에 로직을 넣을 여지가 남습니다.

### 예제 2 — `GetInit` : 객체 초기화 + `init`
```csharp
// Program.cs
using CodingNow.Lecture.Oop07;

var alice = new Person { Name = "Alice", Age = 30 };
Console.WriteLine($"{alice.Name} / {alice.Age}");

// alice.Name = "Bob";   // init 이라 이후 변경 불가 (컴파일 에러)
```
```csharp
// Person.cs
namespace CodingNow.Lecture.Oop07;

internal class Person
{
    public string Name { get; init; } = "";
    public int Age { get; init; }
}
```
**실행 결과**
```
Alice / 30
```
**메모:** 객체 초기화 구문 `new Person { ... }` 안에서만 값을 세팅할 수 있고, 그 뒤엔 읽기 전용입니다.

### 예제 3 — `RequiredProp` : 필수 프로퍼티
```csharp
// Program.cs
using CodingNow.Lecture.Oop07;

var u = new User { Email = "alice@example.com" };
Console.WriteLine(u.Email);

// var u2 = new User();   // Email 을 안 줘서 컴파일 에러
```
```csharp
// User.cs
namespace CodingNow.Lecture.Oop07;

internal class User
{
    public required string Email { get; init; }
    public string DisplayName { get; init; } = "익명";
}
```
**실행 결과**
```
alice@example.com
```
**메모:** `required` 가 붙은 프로퍼티는 객체를 만들 때 반드시 값을 줘야 합니다. 생성자가 없어도 안전합니다.

### 예제 4 — `FullProperty` : 값 검증 포함
```csharp
// Program.cs
using CodingNow.Lecture.Oop07;

var t = new Temperature();
t.Celsius = 25;
Console.WriteLine($"{t.Celsius}°C");

try
{
    t.Celsius = -500;   // 절대영도 아래 → 예외
}
catch (ArgumentException ex)
{
    Console.WriteLine($"에러: {ex.Message}");
}
```
```csharp
// Temperature.cs
namespace CodingNow.Lecture.Oop07;

internal class Temperature
{
    private double celsius;

    public double Celsius
    {
        get => celsius;
        set
        {
            if (value < -273.15)
                throw new ArgumentException("절대영도(-273.15°C) 아래로 내려갈 수 없습니다.");
            celsius = value;
        }
    }
}
```
**실행 결과**
```
25°C
에러: 절대영도(-273.15°C) 아래로 내려갈 수 없습니다.
```
**메모:** 외부에서는 그냥 `t.Celsius = 25;` 처럼 필드처럼 쓰지만, 내부에선 메서드가 호출됩니다. 이게 프로퍼티의 본질.

## 자주 하는 실수
1. `public string Name;` 처럼 필드를 그대로 공개 — 가능한 한 프로퍼티 사용.
2. `init` 인데 외부에서 다시 대입하려고 한다 — 객체 초기화 시점이나 생성자 안에서만 가능.
3. `required` 가 붙은 프로퍼티를 빼먹고 객체 생성 → 컴파일 에러.
4. `set` 안에서 `Celsius = value;` 처럼 자기 자신을 다시 호출해 **무한 재귀** 가 발생 (StackOverflow). 반드시 backing field(`celsius`) 에 대입할 것.
5. `readonly` 필드를 생성자가 아닌 일반 메서드에서 바꾸려 한다 — 컴파일 에러.

## 정리
- 프로퍼티는 "필드처럼 보이는 메서드". 외부 인터페이스는 단순하게 두면서 내부 로직을 숨길 수 있다.
- 변경 불가 데이터에는 `init`, 필수 입력에는 `required`, 검증이 필요하면 full property + backing field.
- 캡슐화의 본질은 "외부가 객체 내부 상태를 마음대로 못 건드리게" 막는 것.

## 직접 해 보기
```bash
cd src/AutoProperty
dotnet run

cd ../GetInit
dotnet run

cd ../RequiredProp
dotnet run

cd ../FullProperty
dotnet run
```

## 다음 단원
[08_상속](../08_상속/) — 이미 있는 클래스를 확장해 새 클래스를 만드는 법을 배웁니다.
