# 08. 상속

이미 잘 만들어진 클래스가 있을 때, 비슷한 클래스를 처음부터 다시 짜는 건 낭비입니다. **상속(inheritance)** 으로 부모 클래스의 멤버를 그대로 물려받고 필요한 부분만 덧붙이거나 바꿀 수 있습니다.

## 학습 목표
- `: base` 표기로 클래스를 상속할 수 있다
- 자식 생성자에서 `: base(...)` 로 부모 생성자를 호출할 수 있다
- `virtual` / `override` / `sealed override` 의 차이를 안다
- `new` 키워드로 메서드를 가린(hide) 결과가 어떻게 다른지 안다

## 핵심 개념

### 1) `: base` 표기
```csharp
class Animal
{
    public string Name = "";
    public void Eat() => Console.WriteLine($"{Name} 먹는다");
}

class Dog : Animal       // Animal 을 상속
{
    public void Bark() => Console.WriteLine($"{Name} 멍멍!");
}
```
`Dog` 는 `Animal` 의 `Name`, `Eat()` 을 그대로 쓸 수 있습니다. C#은 **단일 상속**만 허용 (한 부모만).

### 2) 자식 생성자에서 `: base(...)` 호출
부모에 매개변수 있는 생성자가 있다면 자식이 직접 호출해 줘야 합니다.
```csharp
class Animal
{
    public string Name;
    public Animal(string name) => Name = name;
}

class Dog : Animal
{
    public Dog(string name) : base(name) { }   // 부모 생성자 호출
}
```

### 3) `virtual` 과 `override`
부모가 `virtual` 로 표시한 메서드를 자식이 `override` 로 다시 정의(재정의)할 수 있습니다.
```csharp
class Animal
{
    public virtual void Speak() => Console.WriteLine("...");
}

class Dog : Animal
{
    public override void Speak() => Console.WriteLine("멍멍!");
}
```
변수의 타입이 `Animal` 이라도, 실제 객체가 `Dog` 면 `Dog.Speak()` 가 실행됩니다(=다형성, 다음 단원).

### 4) `sealed override`
"이 메서드는 더 이상 자식이 override 할 수 없다"는 표시.
```csharp
class Puppy : Dog
{
    public sealed override void Speak() => Console.WriteLine("깨갱!");
}
// Puppy 를 상속한 클래스는 Speak 를 또 override 못 함
```

### 5) `new` 키워드로 hide
`override` 가 아니라 `new` 를 쓰면 "부모 메서드를 가린다" 는 의미가 됩니다. 다형성이 작동하지 않으니 주의.
```csharp
class Animal { public virtual void Speak() => Console.WriteLine("Animal"); }
class Cat : Animal { public new void Speak() => Console.WriteLine("Cat"); }

Animal a = new Cat();
a.Speak();   // "Animal" 이 나옴 — Cat 의 new 메서드는 무시됨
```
`override` 는 가상 디스패치 테이블을 갈아끼우지만, `new` 는 단순히 같은 이름을 새로 만든 별도 메서드입니다.

### 6) `protected`
부모의 멤버에 자식만 접근하게 하고 싶을 때 쓰는 접근 제한자.
```csharp
class Animal { protected int HungerLevel = 0; }
class Dog : Animal { public void Feed() => HungerLevel--; }
```

## 예제로 보기

### 예제 1 — `AnimalDog` : 단순 상속
```csharp
// Program.cs
using CodingNow.Lecture.Oop08;

var d = new Dog();
d.Name = "초코";
d.Eat();    // 부모에게서 물려받음
d.Bark();   // 자식 고유
```
```csharp
// Animal.cs
namespace CodingNow.Lecture.Oop08;

internal class Animal
{
    public string Name = "";
    public void Eat() => Console.WriteLine($"{Name} 먹는다");
}

// Dog.cs
internal class Dog : Animal
{
    public void Bark() => Console.WriteLine($"{Name} 멍멍!");
}
```
**실행 결과**
```
초코 먹는다
초코 멍멍!
```
**메모:** 자식은 부모의 `Name`, `Eat()` 를 따로 작성하지 않고도 그대로 씁니다.

### 예제 2 — `BaseCall` : 자식 생성자 + `base.Method()`
```csharp
// Program.cs
using CodingNow.Lecture.Oop08;

var d = new Dog("초코", "푸들");
d.Introduce();
```
```csharp
// Animal.cs
namespace CodingNow.Lecture.Oop08;

internal class Animal
{
    public string Name;

    public Animal(string name)
    {
        Name = name;
    }

    public void Introduce()
    {
        Console.WriteLine($"나는 동물 {Name}.");
    }
}

// Dog.cs
internal class Dog : Animal
{
    public string Breed;

    public Dog(string name, string breed) : base(name)   // 부모 생성자 먼저 호출
    {
        Breed = breed;
    }

    public new void Introduce()
    {
        base.Introduce();   // 부모 버전을 먼저 호출
        Console.WriteLine($"종은 {Breed}야.");
    }
}
```
**실행 결과**
```
나는 동물 초코.
종은 푸들이야.
```
**메모:** `base.Method()` 는 부모 클래스의 같은 이름 메서드를 그대로 부르는 문법입니다.

### 예제 3 — `VirtualOverride` : 다형성의 출발점
```csharp
// Program.cs
using CodingNow.Lecture.Oop08;

Animal a1 = new Animal();
Animal a2 = new Dog();
Animal a3 = new Puppy();

a1.Speak();
a2.Speak();
a3.Speak();
```
```csharp
// Animal.cs
namespace CodingNow.Lecture.Oop08;

internal class Animal
{
    public virtual void Speak() => Console.WriteLine("...");
}

// Dog.cs
internal class Dog : Animal
{
    public override void Speak() => Console.WriteLine("멍멍!");
}

// Puppy.cs
internal class Puppy : Dog
{
    public sealed override void Speak() => Console.WriteLine("깨갱!");
}
```
**실행 결과**
```
...
멍멍!
깨갱!
```
**메모:** 변수의 타입은 모두 `Animal` 이지만, 실제 객체에 맞는 `Speak()` 가 호출됩니다. `Puppy.Speak()` 는 `sealed` 라서 그 아래에서는 더 못 바꿉니다.

### 예제 4 — `NewKeyword` : `override` vs `new` 차이
```csharp
// Program.cs
using CodingNow.Lecture.Oop08;

Animal a = new Cat();   // 변수 타입은 Animal, 실제 객체는 Cat
a.Speak();              // new 라서 부모 버전("Animal")이 호출됨

Cat c = new Cat();
c.Speak();              // 이건 Cat 버전 호출
```
```csharp
// Animal.cs
namespace CodingNow.Lecture.Oop08;

internal class Animal
{
    public virtual void Speak() => Console.WriteLine("Animal");
}

// Cat.cs
internal class Cat : Animal
{
    // override 가 아니라 new — 단순히 같은 이름의 새 메서드를 정의한다.
    public new void Speak() => Console.WriteLine("Cat");
}
```
**실행 결과**
```
Animal
Cat
```
**메모:** `new` 는 "부모 메서드와 무관한 새 메서드"로 동작합니다. 다형성이 필요한 경우는 거의 항상 `override` 가 정답.

## 자주 하는 실수
1. 부모 생성자에 인자가 있는데 자식이 `: base(...)` 호출을 안 한다 — 컴파일 에러.
2. `virtual` 안 붙은 메서드를 자식에서 `override` 하려고 한다 — 컴파일 에러.
3. `override` 자리에 무심코 `new` 를 써서 다형성이 안 먹는다 — 디버깅 어려움.
4. `private` 멤버를 자식이 직접 쓰려고 한다 — 막힘. 자식에게 보여주려면 `protected`.
5. 단일 상속이라는 사실을 잊고 `class A : B, C` 처럼 부모 둘을 적는다 — 컴파일 에러 (다중 상속 X). 인터페이스는 여러 개 OK (10편).

## 정리
- `class Child : Parent` 로 단일 상속. `base(...)`/`base.M()` 으로 부모를 호출.
- `virtual` + `override` 가 다형성의 기본 골조. `new` 로 hide 는 가능하나 권장 X.
- `sealed override` 로 추가 재정의 차단, `protected` 로 자식에게만 노출.

## 직접 해 보기
```bash
cd src/AnimalDog
dotnet run

cd ../BaseCall
dotnet run

cd ../VirtualOverride
dotnet run

cd ../NewKeyword
dotnet run
```

## 다음 단원
[09_다형성](../09_다형성/) — 상속을 활용해 같은 호출이 다른 동작으로 이어지는 다형성을 본격적으로 다룹니다.
