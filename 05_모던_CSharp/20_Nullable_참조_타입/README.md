# 20. Nullable 참조 타입 (NRT)

C# 8 부터 **참조 타입에도 `?` 를 붙여서 "null 가능 여부"를 타입으로 표현**할 수 있게 되었습니다. `.NET 8` 의 새 프로젝트는 기본적으로 NRT 가 켜져 있어, NullReferenceException 의 대부분이 컴파일 타임에 잡힙니다.

## 학습 목표
- `string` 과 `string?` 의 차이를 안다
- `#nullable enable` / `<Nullable>enable</Nullable>` 의 효과를 이해한다
- `?.`, `??`, `??=` 로 null 을 안전하게 다룬다
- `!` (null-forgiving) 을 언제 쓰고 언제 피해야 하는지 안다
- `[NotNullWhen(true)]` 같은 attribute 의 큰 그림을 본다

## 핵심 개념

### 1) `<Nullable>enable</Nullable>` — 프로젝트 단위 스위치
이 한 줄이 NRT 를 켜는 핵심입니다. 파일 단위로는 `#nullable enable` / `#nullable disable` 를 쓸 수 있습니다.

```xml
<PropertyGroup>
  <Nullable>enable</Nullable>
</PropertyGroup>
```

켜져 있으면:
- `string x = null;` → 경고 (`CS8600`)
- `string? x = null;` → 정상
- `string? x = ...; x.Length;` → 경고 (`CS8602`)

### 2) `string` vs `string?`
| 표기 | 의미 | null 대입 | 멤버 접근 시 경고 |
|---|---|---|---|
| `string` | non-nullable | 경고 | 없음 |
| `string?` | nullable | OK | null 체크 안 하면 경고 |

타입 정보를 보고 **"이 변수는 null 이 들어올 수 있는가"** 를 즉시 알 수 있게 만들자는 취지입니다.

### 3) null 안전 연산자들
이미 03단원에서 잠깐 봤지만, 여기서 한 번에 정리합니다.

```csharp
string? name = null;

string display = name ?? "(없음)";    // null이면 기본값
name ??= "Alice";                     // null일 때만 대입
int? length = name?.Length;           // null이면 통째로 null
char? first = name?[0];               // 인덱싱도 가능
```

### 4) `!` — null-forgiving 연산자
"내가 책임지고 보장하니까, 컴파일러는 경고 꺼" 라는 표현입니다.

```csharp
string s = Console.ReadLine()!;       // 입력이 null 이 아니라고 단언
Person p = null!;                     // 곧 초기화될 거라는 자리표시
```

남용하면 NRT 의 의미가 사라집니다. **정말 확실할 때만**, 그리고 가능한 작은 범위에 쓰세요.

### 5) `is null` vs `== null`
권장은 `is null`. 이유는 `==` 은 사용자 정의 오버로딩이 끼어들 수 있지만, `is` 는 언어 차원의 검사라 항상 동일하게 동작합니다.

```csharp
if (x is null) ...
if (x is not null) ...
```

### 6) Nullable Attribute (라이브러리용)
`[NotNullWhen(true)]` 같은 attribute 로 컴파일러의 흐름 분석을 보강합니다. `Dictionary.TryGetValue` 가 `out` 매개변수의 null 여부를 보고하는 것도 같은 원리입니다.

```csharp
bool TryParseName(string raw, [NotNullWhen(true)] out string? value);
// true 를 돌려주면 → value 는 null 이 아님을 컴파일러에 알린다
```

## 예제로 보기

### 예제 1 — `EnableNrt` : `string` vs `string?`
```csharp
string nonNullable = "hello";
string? nullable = null;
// nonNullable = null;            // CS8600 경고
// Console.WriteLine(nullable.Length);   // CS8602 경고

if (nullable is not null)
{
    Console.WriteLine($"길이: {nullable.Length}");
}
```
**실행 결과**
```
non = hello
null = (없음)
```
**메모:** `.csproj` 의 `<Nullable>enable</Nullable>` 한 줄이 모든 차이를 만듭니다. 신규 .NET 8 프로젝트는 기본 켜짐.

### 예제 2 — `NullForgiving` : `!` 연산자
```csharp
string name = Console.ReadLine()!;     // 입력이 null 아니라고 단언
public string Name { get; set; } = null!;  // 곧 채울 거니 경고 꺼 두기
```
**실행 결과(예시 입력 "지수")**
```
이름을 입력하세요: 지수
안녕, 지수!
Name = Alice
```
**메모:** `null!` 은 ORM 의 엔티티나 의존성 주입 컨테이너가 곧 채워 줄 필드에 자주 등장합니다. 일반 비즈니스 코드에서 남발하면 NRT 의 의미가 무력화됩니다.

### 예제 3 — `NullPattern` : `is null` 과 `??`/`??=`/`?.`
```csharp
string? a = null;
if (a is null) Console.WriteLine("a 는 null");
string display = a ?? "(이름 없음)";
a ??= "기본값";
string? upper = b?.ToUpper();
```
**실행 결과**
```
a 는 null
b 는 hello
display = (이름 없음)
a = 기본값
upper = HELLO
first = h
```
**메모:** 이 네 가지 연산자(`is null`, `??`, `??=`, `?.`)만 잘 써도 NullReferenceException 의 95% 가 사라집니다.

### 예제 4 — `NullableAnnotation` : `[NotNullWhen(true)]`
```csharp
static bool TryParseName(string raw, [NotNullWhen(true)] out string? value)
{
    if (string.IsNullOrWhiteSpace(raw)) { value = null; return false; }
    value = raw.Trim();
    return true;
}

if (TryParseName("Alice", out string? name))
{
    Console.WriteLine(name.Length);   // 여기선 name 이 not null 로 추론됨
}
```
**실행 결과**
```
이름 길이: 5
실패: empty == null ? True
```
**메모:** 이 attribute 덕분에 호출자는 `if` 안에서 `name!` 을 쓸 필요가 없습니다. 라이브러리 작성 시 API 정확도를 크게 높여 줍니다.

### 예제 5 — `MaybeNullPattern` : "찾는" 메서드의 `T?` 반환
```csharp
static Person? FindByName(IEnumerable<Person> people, string name)
{
    foreach (Person p in people) if (p.Name == name) return p;
    return null;
}

Person? found = FindByName(people, "Alice");
int age = found?.Age ?? -1;
if (found is { Age: > 18 } adult) Console.WriteLine($"{adult.Name} 는 성인");
```
**실행 결과**
```
Alice 나이: 30
Zoe 나이:   
Zoe 나이 (기본 -1): -1
Alice 는 성인
```
**메모:** "결과가 없을 수 있다"를 타입으로 분명히 드러내는 패턴입니다. 호출 쪽은 `?.`/`??`/패턴 매칭으로 다루면 깔끔합니다.

## 자주 하는 실수
1. 경고를 죽이려고 `!` 를 남발 — 정작 null 이 흘러 들어와도 못 잡습니다.
2. `string` (non-nullable) 멤버를 생성자에서 초기화하지 않음 — CS8618 경고. `required` (07편) 나 `= ""` 로 해결.
3. `is null` 대신 `Equals(null)`/`== null` 사용 — 동작은 같지만 일관성을 위해 `is null` 권장.
4. NRT 가 꺼진 옛 라이브러리에서 받은 값을 그대로 non-nullable 변수에 대입 — 런타임에 null 일 수 있음. `?` 로 받아 검사.
5. `Dictionary.TryGetValue` 의 `out` 매개변수를 null 체크 없이 사용 — 컴파일러는 이미 알고 있으니, `if (dict.TryGetValue(...))` 분기 안에서 안전합니다.

## 정리
- NRT 는 "참조 타입의 null 가능성"을 타입 수준으로 표현하는 기능
- `<Nullable>enable</Nullable>` 또는 `#nullable enable` 로 켠다
- `?` 가 없는 참조 타입에는 null 을 넣지 않는다 (컴파일러가 막아 줌)
- `?.`, `??`, `??=`, `is null` 네 연산자로 null 을 우아하게 다룬다
- `!` 는 최후의 수단 — 정말 책임질 수 있을 때만

## 직접 해 보기
```bash
cd src/EnableNrt && dotnet run
cd ../NullForgiving && dotnet run
cd ../NullPattern && dotnet run
cd ../NullableAnnotation && dotnet run
cd ../MaybeNullPattern && dotnet run
```

## 다음 단원
[21_패턴_매칭](../21_패턴_매칭/) — `is`/`switch` 패턴으로 분기를 한 단계 우아하게 만듭니다.
