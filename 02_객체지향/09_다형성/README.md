# 09. 다형성

같은 이름의 메서드 호출이 객체의 실제 타입에 따라 다른 동작을 하는 것 — 이게 **다형성(polymorphism)** 입니다. 이전 단원에서 본 `virtual`/`override` 가 그 핵심 도구였고, 이 단원에서는 업/다운캐스팅, `is` 패턴, `abstract` 클래스까지 묶어 본격적으로 다룹니다.

## 학습 목표
- 업캐스팅과 다운캐스팅의 차이를 이해한다
- `is` 패턴 매칭으로 안전한 다운캐스팅을 한다
- `abstract` 클래스와 `abstract` 메서드의 쓰임을 안다
- `List<Animal>` 같은 컬렉션에 자식 객체들을 담고 일괄 처리한다

## 핵심 개념

### 1) 업캐스팅(upcast)
자식 → 부모 방향. 항상 안전, 명시적 캐스트도 필요 없습니다.
```csharp
Animal a = new Dog();    // Dog 객체를 Animal 변수에 담음 (업캐스팅)
a.Speak();               // virtual/override 라면 Dog.Speak() 가 실행됨
```
변수의 타입은 `Animal` 이라도 실제 객체는 `Dog`. 가상 메서드는 실제 타입에 맞춰 호출됩니다.

### 2) 다운캐스팅(downcast)
부모 → 자식 방향. 실제 객체가 그 자식이 맞을 때만 성공.
```csharp
Animal a = new Dog();
Dog d = (Dog)a;          // 실제로 Dog 라서 OK
d.Bark();

Animal a2 = new Animal();
Dog d2 = (Dog)a2;        // 런타임 InvalidCastException 발생
```

### 3) `is` 패턴 매칭으로 안전하게
```csharp
if (animal is Dog dog)
{
    dog.Bark();          // animal 이 Dog 일 때만 들어옴, dog 는 이미 캐스팅됨
}
```
조건 검사와 변수 선언을 한 번에. 캐스트 실패 위험이 없습니다.

`as` 키워드도 비슷하게 쓸 수 있습니다.
```csharp
Dog? d = animal as Dog;  // 실패 시 null
```

### 4) `abstract` 클래스
"이 클래스만으로는 객체를 만들지 못한다. 자식이 반드시 구체화하라"는 의미.
```csharp
abstract class Shape
{
    public abstract double Area();   // 본문 없음, 자식이 반드시 구현
    public void Print() => Console.WriteLine($"넓이={Area()}");
}

class Circle : Shape
{
    public double Radius;
    public Circle(double r) => Radius = r;
    public override double Area() => Math.PI * Radius * Radius;
}

// var s = new Shape();   // 컴파일 에러: abstract 는 직접 인스턴스화 못함
```
`abstract` 메서드는 자동으로 `virtual` 이며, 자식은 반드시 `override` 해야 합니다.

### 5) 가상 디스패치(virtual dispatch)
변수의 정적 타입이 아니라 **실제 객체의 동적 타입** 으로 메서드를 찾아 호출하는 것. 이것이 다형성의 작동 원리입니다.

```csharp
List<Animal> zoo = [new Dog(), new Cat(), new Cow()];
foreach (var a in zoo)
{
    a.Speak();   // 각각 다른 결과: 멍멍/야옹/음매
}
```
호출하는 코드는 단 한 줄(`a.Speak()`)인데, 객체마다 다른 동작이 나옵니다. 이것이 OOP의 큰 장점.

## 예제로 보기

### 예제 1 — `UpcastDowncast` : 캐스팅 방향
```csharp
// Program.cs
using CodingNow.Lecture.Oop09;

// 업캐스팅: 자식 → 부모 (안전, 자동)
Animal a = new Dog();
a.Speak();   // 가상 디스패치 → Dog.Speak()

// 다운캐스팅: 부모 → 자식 (실제 타입이 맞아야 함)
Dog d = (Dog)a;
d.Bark();
```
```csharp
// Animal.cs
namespace CodingNow.Lecture.Oop09;

internal class Animal
{
    public virtual void Speak() => Console.WriteLine("동물 소리");
}

// Dog.cs
internal class Dog : Animal
{
    public override void Speak() => Console.WriteLine("멍멍!");
    public void Bark() => Console.WriteLine("왕!왕!");
}
```
**실행 결과**
```
멍멍!
왕!왕!
```
**메모:** 변수 타입은 `Animal` 인데도 `Dog.Speak()` 가 호출되는 게 다형성의 본질.

### 예제 2 — `IsPattern` : 안전한 타입 검사
```csharp
// Program.cs
using CodingNow.Lecture.Oop09;

Animal[] animals = [new Dog(), new Cat(), new Animal()];

foreach (var a in animals)
{
    a.Speak();

    if (a is Dog dog)
        dog.Bark();
    else if (a is Cat cat)
        cat.Purr();
}
```
```csharp
// Animal.cs
namespace CodingNow.Lecture.Oop09;

internal class Animal
{
    public virtual void Speak() => Console.WriteLine("동물 소리");
}

internal class Dog : Animal
{
    public override void Speak() => Console.WriteLine("멍멍!");
    public void Bark() => Console.WriteLine("왕!왕!");
}

internal class Cat : Animal
{
    public override void Speak() => Console.WriteLine("야옹~");
    public void Purr() => Console.WriteLine("그르릉~");
}
```
**실행 결과**
```
멍멍!
왕!왕!
야옹~
그르릉~
동물 소리
```
**메모:** `if (a is Dog dog)` 안에서 `dog` 변수는 이미 `Dog` 타입으로 사용 가능. 캐스트 실패 위험이 없습니다.

### 예제 3 — `AbstractShape` : 추상 클래스
```csharp
// Program.cs
using CodingNow.Lecture.Oop09;

Shape s1 = new Circle(5);
Shape s2 = new Rectangle(3, 4);

s1.Print();
s2.Print();

// var s = new Shape();   // 컴파일 에러: abstract 직접 인스턴스화 불가
```
```csharp
// Shape.cs
namespace CodingNow.Lecture.Oop09;

internal abstract class Shape
{
    public abstract double Area();   // 본문 없음

    public void Print() => Console.WriteLine($"넓이 = {Area():F2}");
}

// Circle.cs
internal class Circle : Shape
{
    public double Radius;
    public Circle(double r) => Radius = r;
    public override double Area() => Math.PI * Radius * Radius;
}

// Rectangle.cs
internal class Rectangle : Shape
{
    public double Width;
    public double Height;
    public Rectangle(double w, double h) { Width = w; Height = h; }
    public override double Area() => Width * Height;
}
```
**실행 결과**
```
넓이 = 78.54
넓이 = 12.00
```
**메모:** `Shape` 는 추상이라 직접 만들 수 없지만, 공통 코드(`Print`)는 한 군데 모아 둘 수 있습니다.

### 예제 4 — `PolymorphismList` : 컬렉션 + 일괄 처리
```csharp
// Program.cs
using CodingNow.Lecture.Oop09;

List<Animal> zoo = [new Dog(), new Cat(), new Cow()];

foreach (var a in zoo)
{
    a.Speak();   // 같은 호출인데 객체마다 다른 결과
}
```
```csharp
// Animals.cs
namespace CodingNow.Lecture.Oop09;

internal class Animal
{
    public virtual void Speak() => Console.WriteLine("...");
}

internal class Dog : Animal
{
    public override void Speak() => Console.WriteLine("멍멍!");
}

internal class Cat : Animal
{
    public override void Speak() => Console.WriteLine("야옹~");
}

internal class Cow : Animal
{
    public override void Speak() => Console.WriteLine("음매~");
}
```
**실행 결과**
```
멍멍!
야옹~
음매~
```
**메모:** 새로운 동물을 추가해도 `zoo` 를 순회하는 코드는 그대로 — 다형성의 진정한 가치는 "추가에 닫혀 있고, 확장에 열려 있다"는 것.

## 자주 하는 실수
1. 다운캐스팅에 `(Dog)a` 만 쓰고 안전 검사를 빼서 `InvalidCastException` 발생 → `is` 또는 `as` 사용.
2. `abstract` 클래스를 `new` 로 직접 만들려 한다 — 컴파일 에러.
3. `abstract` 메서드를 자식이 `override` 안 하면 자식도 abstract 처리 안 해 둬서 컴파일 에러.
4. 가상 메서드를 호출했는데 부모 버전이 나오는 줄 안다. 변수 타입과 무관하게 **실제 객체** 의 메서드가 호출됨.
5. `is` 패턴 안에서 선언한 변수를 `if` 바깥에서 쓰려 한다 — 스코프는 보통 `if` 블록 내부.

## 정리
- 다형성 = "같은 호출, 다른 동작". `virtual`/`override` 와 가상 디스패치가 그 엔진.
- 업캐스팅은 자동, 다운캐스팅은 `is` 패턴 또는 `as` 로 안전하게.
- `abstract` 클래스는 공통 부분만 정의하고 구체적인 부분은 자식에게 위임할 때 쓴다.
- 컬렉션과 만나면 위력 폭발 — 자식 종류가 늘어도 처리 코드는 그대로.

## 직접 해 보기
```bash
cd src/UpcastDowncast
dotnet run

cd ../IsPattern
dotnet run

cd ../AbstractShape
dotnet run

cd ../PolymorphismList
dotnet run
```

## 다음 단원
[10_인터페이스](../10_인터페이스/) — 상속 없이 "할 수 있는 능력"만 약속하는 인터페이스를 배웁니다.
