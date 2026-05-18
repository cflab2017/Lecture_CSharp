# 13. 제네릭

`List<int>`, `Dictionary<string, int>` 처럼 `<T>` 가 붙은 타입은 모두 **제네릭(generic)** 입니다. 제네릭은 "어떤 타입에도 동작하지만, 사용 시에는 한 가지 타입으로 고정"되는 강력한 메커니즘입니다. 코드 재사용과 타입 안전성을 동시에 얻을 수 있어요.

## 학습 목표
- 제네릭 클래스와 제네릭 메서드를 정의·사용한다
- 타입 매개변수에 **제약(`where`)** 을 거는 이유와 방법을 안다
- 공변성(`out`) / 반변성(`in`) 의 개념을 한 줄로 이해한다

## 핵심 개념

### 1) 제네릭 클래스
`<T>` 는 "아직 정하지 않은 타입"을 뜻하는 자리표시자(placeholder)입니다. 사용 시점에 결정됩니다.

```csharp
public class Box<T>
{
    public T? Value { get; set; }
}

var intBox = new Box<int> { Value = 42 };
var strBox = new Box<string> { Value = "안녕" };
```

`object` 로 받는 대신 `T` 를 쓰면 **박싱/캐스팅이 필요 없고 컴파일 시점에 타입이 검사**됩니다.

### 2) 제네릭 메서드
```csharp
public static T Max<T>(T a, T b) where T : IComparable<T>
{
    return a.CompareTo(b) >= 0 ? a : b;
}

int m = Max(3, 7);          // T 가 int 로 추론됨
string s = Max("a", "z");
```

대부분 컴파일러가 `T` 를 추론합니다. 명시할 땐 `Max<int>(3, 7)`.

### 3) 제약 (`where`)
| 제약 | 의미 |
|---|---|
| `where T : class` | 참조 타입만 (`string`, 클래스) |
| `where T : struct` | 값 타입만 (`int`, `bool`, 구조체) |
| `where T : new()` | 매개변수 없는 생성자 필요 |
| `where T : IComparable<T>` | 해당 인터페이스 구현 |
| `where T : SomeBase` | `SomeBase` 또는 그 자식 |

여러 개 결합: `where T : class, new()` (참조 타입 + 기본 생성자).

### 4) 공변성(`out`) / 반변성(`in`) 개요
간단히 말해 **"제네릭 타입 사이의 형변환을 허용"** 하는 옵션입니다.

- `IEnumerable<out T>`: `IEnumerable<Dog>` 를 `IEnumerable<Animal>` 로 사용 가능 (**공변**, 출력만 하는 경우).
- `Action<in T>`: `Action<Animal>` 을 `Action<Dog>` 로 사용 가능 (**반변**, 입력만 받는 경우).

지금은 "이런 게 있다" 정도만 기억하세요. 자세한 활용은 고급 주제입니다.

## 예제로 보기

### 예제 1 — `GenericClass` : `Box<T>` 정의·사용
```csharp
var intBox = new Box<int> { Value = 42 };
var strBox = new Box<string> { Value = "안녕" };

Console.WriteLine($"intBox: {intBox.Value}");
Console.WriteLine($"strBox: {strBox.Value}");

public class Box<T>
{
    public T? Value { get; set; }

    public void Show() => Console.WriteLine($"Box 안의 값: {Value}");
}
```
**실행 결과**
```
intBox: 42
strBox: 안녕
```
**메모:** 한 번 정의한 `Box<T>` 로 모든 타입을 담을 수 있어 코드 중복이 사라집니다. `T?` 의 `?` 는 `Value` 가 초기에 `null` 일 수 있음을 표시.

### 예제 2 — `GenericMethod` : `Max<T>` 와 타입 추론
```csharp
Console.WriteLine(Max(3, 7));
Console.WriteLine(Max("apple", "banana"));
Console.WriteLine(Max(3.14, 2.71));

static T Max<T>(T a, T b) where T : IComparable<T>
{
    return a.CompareTo(b) >= 0 ? a : b;
}
```
**실행 결과**
```
7
banana
3.14
```
**메모:** `int`, `string`, `double` 은 모두 `IComparable<T>` 를 구현하므로 제약을 만족합니다. `CompareTo` 가 양수면 a 가 더 큼.

### 예제 3 — `Constraint` : `where` 제약 비교
```csharp
// class + new() 제약 → 새 인스턴스 생성 가능
static T Create<T>() where T : class, new() => new T();

// struct 제약 → 값 타입만
static T DoubleIt<T>(T x) where T : struct
{
    Console.WriteLine($"받은 값: {x}");
    return x;
}

Person p = Create<Person>();
p.Name = "지수";
Console.WriteLine($"새로 만든 사람: {p.Name}");

DoubleIt(42);
DoubleIt(3.14);
// DoubleIt("문자열");  ← 컴파일 에러: string 은 struct 아님

public class Person
{
    public string Name { get; set; } = "";
}
```
**실행 결과**
```
새로 만든 사람: 지수
받은 값: 42
받은 값: 3.14
```
**메모:** `new()` 제약 덕분에 `new T()` 호출이 가능해집니다. `struct` 제약은 컴파일 시점에 잘못된 타입을 차단합니다.

### 예제 4 — `Variance` : 공변성·반변성 한 눈 보기
```csharp
// IEnumerable<out T> — 공변 (Dog 의 컬렉션을 Animal 컬렉션으로 사용)
IEnumerable<Dog> dogs = [new Dog("바둑이"), new Dog("뽀삐")];
IEnumerable<Animal> animals = dogs;   // OK : out 덕분
foreach (Animal a in animals)
{
    Console.WriteLine($"동물: {a.Name}");
}

// Action<in T> — 반변 (Animal 처리 함수를 Dog 에 사용 가능)
Action<Animal> describe = a => Console.WriteLine($"이름은 {a.Name}");
Action<Dog> describeDog = describe;   // OK : in 덕분
describeDog(new Dog("초코"));

public class Animal(string name)
{
    public string Name { get; } = name;
}

public class Dog(string name) : Animal(name);
```
**실행 결과**
```
동물: 바둑이
동물: 뽀삐
이름은 초코
```
**메모:** 출력 전용(`out`)은 더 일반적인 타입으로, 입력 전용(`in`)은 더 구체적인 타입으로 안전하게 바꿀 수 있습니다. 이 예제의 클래스들은 **기본 생성자(primary constructor)** 문법을 사용했어요.

## 자주 하는 실수
1. `new T()` 를 호출하려고 했는데 `new()` 제약이 없어 컴파일 에러.
2. `where T : IComparable` (제네릭 없음) 과 `where T : IComparable<T>` 를 혼동.
3. 제네릭 클래스의 정적 필드는 **타입별로 따로** 존재 — `Box<int>.X` 와 `Box<string>.X` 는 다른 변수.
4. 모든 곳에 `<T>` 를 남발 — 한 가지 타입만 쓰는 곳은 그냥 구체 타입이 더 읽기 좋음.
5. `out`/`in` 키워드를 클래스에 붙이려 함 — 변성은 **인터페이스와 델리게이트에만** 적용됩니다.

## 정리
- 제네릭은 타입을 매개변수처럼 받아 **재사용 가능한 타입 안전 코드**를 만든다.
- `where` 제약으로 `T` 의 능력을 제한·보장할 수 있다.
- `out` 은 공변(출력), `in` 은 반변(입력) — 인터페이스/델리게이트 전용.
- `List<T>`, `Dictionary<K,V>` 등 표준 컬렉션이 모두 제네릭의 결실이다.

## 직접 해 보기
```bash
cd src/GenericClass
dotnet run

cd ../GenericMethod
dotnet run

cd ../Constraint
dotnet run

cd ../Variance
dotnet run
```

## 다음 단원
[14_LINQ](../14_LINQ/) — 제네릭 컬렉션을 우아하게 다루는 쿼리 도구, LINQ 를 만납니다.
