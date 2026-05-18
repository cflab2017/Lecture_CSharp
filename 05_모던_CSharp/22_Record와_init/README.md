# 22. Record 와 init

`record` 는 **"값 자체가 의미인" 데이터 타입을 한 줄로** 만들어 주는 키워드입니다. `init` only setter, value equality, `with` 식 같은 모던 기능을 묶어, DTO·이벤트·결과 객체 작성을 비약적으로 줄여 줍니다.

## 학습 목표
- `record` 의 자동 생성 멤버(생성자, init 프로퍼티, Equals, ToString, Deconstruct) 를 안다
- `record class` 와 `record struct` 의 차이를 안다
- `with` 식으로 **비파괴 복사(non-destructive mutation)** 를 한다
- `init` only setter 의 의미와 사용처를 안다
- **값 동등성(value equality)** 이 무엇이고 왜 유용한지 안다

## 핵심 개념

### 1) `record` 한 줄의 위력
```csharp
record Person(string Name, int Age);
```

이 한 줄이 자동으로 만들어 주는 것:
- `public Person(string name, int age)` 생성자
- `public string Name { get; init; }`, `public int Age { get; init; }` 프로퍼티 (둘 다 init only)
- `Equals`, `GetHashCode` — **값 기반**
- `ToString` — `Person { Name = Alice, Age = 30 }` 형식
- `==`, `!=` 연산자 — 값 비교
- `Deconstruct` — `var (n, a) = p;`

### 2) `record class` vs `record struct`
| | `record class` (기본) | `record struct` |
|---|---|---|
| 카테고리 | 참조 타입 (힙) | 값 타입 (스택/인라인) |
| 복사 | 참조 복사 | 값 복사 |
| 동등성 | 자동 값 동등성 | 자동 값 동등성 |
| 크기 | 큰 객체 OK | 작은 데이터 (16~24 B) 권장 |
| 예 | DTO, 메시지 | `Point`, `Vector` |

`record struct` 는 일반적으로 `readonly record struct` 로 선언해 불변성을 강제합니다.

```csharp
record class    Person(string Name, int Age);
readonly record struct Point(int X, int Y);
```

### 3) `with` 식 — 비파괴 복사
`with` 는 "원본은 그대로 두고, 일부 프로퍼티만 바꾼 새 인스턴스" 를 만듭니다.

```csharp
Person p1 = new("Alice", 30);
Person p2 = p1 with { Age = 31 };   // p1 은 그대로, p2 가 새 객체
```

`record` 가 컴파일러에 의해 자동 생성한 복제 메서드(`<Clone>$`) 를 사용합니다. 일반 `class` 에서는 `with` 를 못 씁니다.

### 4) `init` only setter
```csharp
class Person
{
    public string Name { get; init; } = "";
}
```

- **객체 초기화 시점에만** 대입 가능 (`new Person { Name = "Alice" }`)
- 객체가 만들어진 후엔 readonly
- record 가 아닌 일반 class 에서도 사용 가능

### 5) 값 동등성과 컬렉션
`record` 의 `Equals`/`GetHashCode` 가 자동으로 값 기반이라, **`HashSet<Person>` / `Dictionary<Person, …>` 키** 로 그대로 사용할 수 있습니다.

```csharp
HashSet<Coord> set = [new(1,2), new(1,2)];   // Count == 1
```

`class` 였다면 두 객체가 다른 참조라 둘 다 들어갑니다.

## 예제로 보기

### 예제 1 — `RecordBasics` : record 한 줄의 위력
```csharp
Person p1 = new("Alice", 30);
Person p2 = new("Alice", 30);
Console.WriteLine($"p1 == p2 ? {p1 == p2}");   // True
Console.WriteLine(p1);                          // ToString 자동
var (name, age) = p1;                           // 분해 자동
```
**실행 결과**
```
p1 == p2 ? True
p1 == p3 ? False
Person { Name = Alice, Age = 30 }
name=Alice, age=30
```
**메모:** 한 줄 선언으로 5가지 자동 멤버가 생깁니다. DTO/메시지 객체 작성이 진짜 짧아집니다.

### 예제 2 — `WithExpression` : 비파괴 복사
```csharp
Person original = new("Alice", 30, "Seoul");
Person aged     = original with { Age = 31 };
Person moved    = original with { City = "Busan", Age = 31 };
```
**실행 결과**
```
original: Person { Name = Alice, Age = 30, City = Seoul }
aged    : Person { Name = Alice, Age = 31, City = Seoul }
moved   : Person { Name = Alice, Age = 31, City = Busan }

original 은 여전히 30세, Seoul
```
**메모:** 함수형 스타일의 "상태 변경 = 새 객체 생성" 패턴. 동시성 코드와 LINQ 파이프라인에서 특히 빛납니다.

### 예제 3 — `RecordVsClass` : class 와 struct
```csharp
internal sealed record PointClass(int X, int Y);
internal readonly record struct PointStruct(int X, int Y);

Console.WriteLine($"record class:  c1 == c2 ? {c1 == c2}  (참조? {ReferenceEquals(c1, c2)})");
Console.WriteLine($"record struct: s1 == s2 ? {s1 == s2}");
```
**실행 결과**
```
record class:  c1 == c2 ? True  (참조? False)
record struct: s1 == s2 ? True
s1 = PointStruct { X = 1, Y = 2 }
shifted = PointStruct { X = 99, Y = 2 }, s1 = PointStruct { X = 1, Y = 2 }
```
**메모:** 둘 다 값 동등성이라 `==` 결과는 같습니다. 차이는 메모리 배치 — `struct` 는 스택/인라인에 통째 저장되므로 작은 데이터에 적합.

### 예제 4 — `InitOnly` : init only setter
```csharp
internal sealed class Person
{
    public string Name { get; init; } = "";
    public int Age { get; init; }
}

Person p = new() { Name = "Alice", Age = 30 };
// p.Name = "Bob";  // 컴파일 에러 — init 은 객체 초기화 후 변경 불가
```
**실행 결과**
```
Alice, 30세
C# 입문 (320p)
```
**메모:** `init` 은 record 의 전용 기능이 아닙니다. 일반 class 에서도 "생성 후 불변" 을 만들 때 쓸 수 있습니다. `required` 와 결합하면 "반드시 지정" 도 강제됩니다.

### 예제 5 — `ValueEquality` : 값 동등성 + 컬렉션
```csharp
internal sealed record Coord(int X, int Y);

Coord a = new(1, 2);
Coord b = new(1, 2);
HashSet<Coord> set = [a, b, new(3, 4)];   // a, b 는 같으니 중복 제거
```
**실행 결과**
```
a.Equals(b) = True
set.Count = 2
ToString: Coord { X = 1, Y = 2 }
a == b : True
a == c : False
```
**메모:** `Dictionary<Coord, ...>` 의 키로 써도 정확히 동작합니다. `class` 였다면 `Equals`/`GetHashCode` 를 직접 오버라이드해야 했을 일입니다.

## 자주 하는 실수
1. record 의 프로퍼티는 기본이 `init` only 라는 사실을 잊고, 만들고 나서 `p.Name = ...` 으로 바꾸려 함 — 컴파일 에러.
2. `with` 식을 일반 `class` 에 시도 — record 전용입니다.
3. `record struct` 를 `readonly` 없이 선언 — 변경 가능 struct 는 미묘한 버그의 원인. 기본적으로 `readonly record struct` 권장.
4. 큰 객체를 `record struct` 로 — 매번 통째 복사라 오히려 느려집니다. 보통 16바이트 이하만 struct.
5. record 안에 가변 컬렉션(`List<T>`) 을 그대로 두기 — 안의 리스트는 여전히 변경 가능. 정말 불변이 필요하면 `IReadOnlyList<T>` 노출.

## 정리
- `record` 한 줄로 생성자/프로퍼티/Equals/ToString/Deconstruct 가 자동 생성
- 값 동등성 덕분에 HashSet/Dictionary 키로 그대로 쓸 수 있음
- `with` 식으로 비파괴 복사 — 함수형 스타일에 적합
- `init` 은 record 전용이 아니라 일반 class 에서도 사용 가능
- 작고 불변인 값 묶음은 `readonly record struct`, 그 외는 `record class`

## 직접 해 보기
```bash
cd src/RecordBasics && dotnet run
cd ../WithExpression && dotnet run
cd ../RecordVsClass && dotnet run
cd ../InitOnly && dotnet run
cd ../ValueEquality && dotnet run
```

## 다음 단원

축하합니다 — **C# 입문 22편 트랙 완주**입니다.

이제 언어와 표준 라이브러리는 충분히 익혔으니, 만들고 싶은 분야에 따라 후속 트랙으로 이어가세요.

| 트랙 | 추천 시작점 |
|---|---|
| **ASP.NET Core 8** | 웹 API · MVC · Minimal API |
| **Entity Framework Core 8** | DB 매핑과 LINQ-to-SQL |
| **Unity** (C# 게임 개발) | 2D/3D 게임, MonoBehaviour 라이프사이클 |
| **.NET MAUI** | 모바일/데스크톱 크로스플랫폼 UI |
| **Blazor** | C# 으로 작성하는 웹 프론트엔드 |

[← 강의 메인으로 돌아가기](../../README.md)
