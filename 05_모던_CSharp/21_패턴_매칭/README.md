# 21. 패턴 매칭

C# 의 **패턴 매칭(pattern matching)** 은 단순한 `if/else` 사다리를 압축해 주는 동시에, 데이터의 **모양(shape)** 을 코드에 그대로 드러나게 합니다. .NET 8 에는 타입·속성·튜플·위치·리스트까지 거의 모든 형태의 패턴이 있습니다.

## 학습 목표
- `is` 패턴으로 **타입 검사 + 변수 선언** 을 한 번에 한다
- `switch` 식의 **타입 패턴 / 속성 패턴 / 튜플 패턴 / 리스트 패턴** 을 자유롭게 쓴다
- `when` 절로 추가 조건을 단다
- 패턴 매칭이 어떻게 가독성과 안전성을 같이 끌어올리는지 본다

## 핵심 개념

### 1) `is` 패턴 — 타입 + 변수
```csharp
if (obj is string s)
{
    Console.WriteLine(s.Length);   // s 는 이미 string 으로 캐스팅되어 있음
}
```

이전 스타일 `if (obj is string) { string s = (string)obj; ... }` 의 보일러플레이트가 사라집니다. `is not`, `and`, `or` 와 결합할 수도 있습니다.

```csharp
if (n is int x and > 0) ...
if (c is 'a' or 'A') ...
```

### 2) `switch` 식 — 값을 돌려주는 분기
표현식이라 변수에 그대로 대입할 수 있습니다. `default` 자리는 `_` (discard).

```csharp
string label = day switch
{
    1 => "월", 2 => "화", 3 => "수",
    _ => "기타",
};
```

### 3) 타입 패턴 — 상속/인터페이스 분기
```csharp
string sound = animal switch
{
    Dog d  => $"{d.Name} 멍멍",
    Cat c  => $"{c.Name} 야옹",
    _      => "조용...",
};
```

### 4) 속성 패턴 — 객체 속성 검사
```csharp
string zone = point switch
{
    { X: 0, Y: 0 }     => "원점",
    { X: 0 }           => "Y축 위",
    { Y: 0 }           => "X축 위",
    { X: > 0, Y: > 0 } => "1사분면",
    _                  => "기타",
};
```

비교 연산자 (`>`, `<=` ...) 도 그대로 쓸 수 있습니다.

### 5) 튜플 패턴 — 여러 값 동시에
```csharp
(int x, int y) p = (1, 2);
string s = (x, y) switch
{
    (0, 0)                     => "원점",
    var (a, b) when a == b     => "대각선",
    _                          => "기타",
};
```

### 6) 리스트 패턴 (.NET 7+) — 길이 + 위치
```csharp
int[] arr = [1, 2, 3];
string s = arr switch
{
    []                       => "비어 있음",
    [var only]               => $"하나만: {only}",
    [1, .., 3]               => "1로 시작 3으로 끝",
    [_, _, _]                => "정확히 세 개",
    [var first, .. var rest] => $"맨 앞 {first}, 나머지 {rest.Length}개",
};
```

`..` 은 "그 사이는 뭐든 OK", `var name` 은 슬라이스/요소를 변수로 받기.

### 7) `when` 절 — 추가 조건
```csharp
string s = age switch
{
    int a when a < 0   => "잘못된 값",
    int a when a < 20  => "청소년",
    _                  => "성인",
};
```

## 예제로 보기

### 예제 1 — `IsPattern` : `is` 패턴
```csharp
foreach (object item in items)
{
    if (item is string s)        Console.WriteLine($"문자열: {s}");
    else if (item is int n and > 0) Console.WriteLine($"양의 정수: {n}");
    else if (item is double d)   Console.WriteLine($"실수: {d}");
    else                         Console.WriteLine($"기타: {item}");
}
```
**실행 결과**
```
문자열(5자): hello
양의 정수: 42
실수: 3.14
기타: True
문자열(5자): world
```
**메모:** `is int n and > 0` 처럼 타입과 값 조건을 결합할 수 있어서 한 줄에 많은 의미를 담습니다.

### 예제 2 — `SwitchType` : 타입 패턴 + `switch` 식
```csharp
static string Describe(Animal a) => a switch
{
    Dog d  => $"{d.Name}: 멍멍",
    Cat c  => $"{c.Name}: 야옹",
    Bird b => $"{b.Name}: 짹짹",
    _      => "알 수 없는 동물",
};
```
**실행 결과**
```
바둑이: 멍멍
나비: 야옹
짹짹: 짹짹
쫑: 멍멍
```
**메모:** 옛 다형성 `virtual Sound()` 와 비교해 보세요. 동물 종류 추가가 적고 분기 로직이 한 곳에 모이면 패턴 매칭이 더 깔끔합니다.

### 예제 3 — `PropertyPattern` : 속성 패턴
```csharp
string zone = p switch
{
    { X: 0, Y: 0 }     => "원점",
    { X: 0 }           => "Y축 위",
    { Y: 0 }           => "X축 위",
    { X: > 0, Y: > 0 } => "1사분면",
    { X: < 0, Y: < 0 } => "3사분면",
    _                  => "기타 사분면",
};
```
**실행 결과**
```
(0, 0) → 원점
(0, 5) → Y축 위
(7, 0) → X축 위
(3, 4) → 1사분면
(-1, -2) → 3사분면
```
**메모:** `switch` 식의 분기는 **위에서 아래로** 먼저 매칭되는 항목을 고릅니다. 더 구체적인 조건을 위에 두세요.

### 예제 4 — `TuplePattern` : 가위바위보
```csharp
string result = (p1, p2) switch
{
    ("rock",     "scissors") => "P1 승",
    ("paper",    "rock")     => "P1 승",
    ("scissors", "paper")    => "P1 승",
    var (a, b) when a == b   => "비김",
    _                        => "P2 승",
};
```
**실행 결과**
```
rock     vs scissors : P1 승
paper    vs rock     : P1 승
scissors vs scissors : 비김
rock     vs paper    : P2 승
```
**메모:** 룰 테이블을 코드로 그대로 표현할 수 있습니다. `var (a, b) when ...` 으로 변수 분해 + 조건 부착.

### 예제 5 — `ListPattern` : 리스트 패턴
```csharp
string desc = arr switch
{
    []              => "비어 있음",
    [var only]      => $"원소 한 개 ({only})",
    [1, .., 3]      => "1로 시작 3으로 끝",
    [_, _, _]       => "정확히 세 개",
    [var first, .. var rest] => $"맨 앞 {first}, 나머지 {rest.Length}개",
};
```
**실행 결과**
```
[] → 비어 있음
[42] → 원소 한 개 (42)
[1,2,3] → 1로 시작 3으로 끝
[1,99,3] → 1로 시작 3으로 끝
[1,2,3,4,5] → 1로 시작 3으로 끝
[9,8,7] → 정확히 세 개
```
**메모:** `[1, .., 3]` 가 `[_, _, _]` 보다 위에 있으므로 `[1,2,3]` 도 첫 번째 분기에서 잡힙니다. 우선순위를 항상 의식하세요.

## 자주 하는 실수
1. `switch` 식에서 **모든 경로 커버 안 함** — `default` (`_`) 가 없으면 `MatchFailureException` 가능. 컴파일러도 경고.
2. 더 일반적인 패턴을 위에 두는 바람에 아래 더 구체적인 패턴이 영영 안 닿는 코드.
3. 속성 패턴에서 null 체크 빠뜨림 — `obj switch { { Prop: ... } => ... }` 는 obj 가 null 이면 매칭 안 됨. 의도라면 OK, 아니면 별도 분기.
4. `when` 절을 남용해 분기가 사실상 `if/else if` 가 되어 가독성이 떨어짐 — 그럴 땐 그냥 `if` 가 낫습니다.
5. 리스트 패턴을 큰 컬렉션에 사용 — 패턴은 길이를 통해 인덱싱하므로 `IEnumerable` 보다는 배열/리스트에 적합.

## 정리
- `is` 패턴 = 타입 검사 + 변수 선언 + 추가 조건
- `switch` 식은 값을 돌려주므로 `var x = obj switch { ... };` 형태가 자연스럽다
- 타입/속성/튜플/리스트 패턴을 조합하면 분기 로직이 데이터 모양을 그대로 그린다
- 분기 순서가 의미를 가진다 — 더 구체적인 패턴을 위에
- `when` 절은 마지막 보루, 너무 많이 쓰면 차라리 `if` 가 가독성 좋다

## 직접 해 보기
```bash
cd src/IsPattern && dotnet run
cd ../SwitchType && dotnet run
cd ../PropertyPattern && dotnet run
cd ../TuplePattern && dotnet run
cd ../ListPattern && dotnet run
```

## 다음 단원
[22_Record와_init](../22_Record와_init/) — 패턴 매칭과 가장 잘 어울리는 `record` 타입을 익힙니다.
