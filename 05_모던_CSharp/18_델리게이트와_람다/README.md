# 18. 델리게이트와 람다

C# 에서 **메서드 자체를 값처럼 다루는 도구**가 델리게이트(`delegate`)와 람다 식입니다. LINQ, async, 이벤트, 콜백 — 모던 C# 의 거의 모든 기능이 이 위에 서 있습니다. 이 단원에서 그 토대를 다집니다.

## 학습 목표
- `delegate` 타입을 직접 선언하고 사용한다
- 표준 델리게이트 `Action<>`/`Func<>`/`Predicate<>` 를 구분해서 쓴다
- 람다 식 `=>` 의 단문/블록 형태를 자유롭게 작성한다
- **메서드 그룹 변환** — 메서드 이름만으로 델리게이트를 채울 수 있음
- `event` 의 기본 모델(발행/구독) 을 이해한다

## 핵심 개념

### 1) 델리게이트는 "메서드를 가리키는 타입"
함수 포인터에 타입 안전성을 더한 개념입니다. 시그니처(매개변수·반환 타입)가 같으면 어떤 메서드든 담을 수 있습니다.

```csharp
delegate int BinaryOp(int a, int b);

BinaryOp add = (a, b) => a + b;
int r = add(3, 4);   // 7
```

### 2) 표준 델리게이트 — 직접 선언하지 말 것
.NET 이 제공하는 일반 델리게이트만 알아도 99% 충분합니다.

| 표준 타입 | 의미 | 예 |
|---|---|---|
| `Action` / `Action<T>` / `Action<T1,T2>` … | **반환값 없음** | `Action<string> log = s => Console.WriteLine(s);` |
| `Func<TResult>` / `Func<T,TResult>` … | **마지막이 반환 타입** | `Func<int,int,int> add = (a,b) => a+b;` |
| `Predicate<T>` | `bool` 반환 (1-인자) | `Predicate<int> pos = n => n > 0;` |

> `Predicate<T>` 는 사실상 `Func<T, bool>` 과 같지만, `Array.Find` / `List<T>.FindAll` 처럼 BCL 의 일부 메서드 시그니처가 `Predicate<T>` 를 요구합니다.

### 3) 람다 식 `=>`
한 줄짜리 익명 함수 표기. 본문이 길면 중괄호로 블록을 만들 수 있습니다.

```csharp
Func<int,int> square = n => n * n;            // 식 본문
Func<int,string> classify = n =>              // 블록 본문
{
    if (n > 0) return "양수";
    return n < 0 ? "음수" : "0";
};
```

### 4) 메서드 그룹 변환
시그니처가 맞으면 **람다 없이 메서드 이름만으로도** 델리게이트를 채울 수 있습니다.

```csharp
list.ForEach(Console.WriteLine);   // Action<string> 자리에 메서드 이름만!
```

### 5) `event` — 발행/구독 모델
`event` 키워드는 델리게이트에 두 가지 제약을 추가합니다.

- 외부에서 `obj.OnAlert = ...` 처럼 **통째로 교체 불가** (`+=`, `-=` 만 허용)
- 외부에서 호출 불가 — **선언한 클래스 내부에서만 발행**

```csharp
public event Action<string>? OnAlert;
OnAlert?.Invoke("위험!");   // 구독자가 없을 수 있으니 ?. 로 안전 호출
```

## 예제로 보기

### 예제 1 — `DelegateBasics` : 델리게이트 타입 직접 선언
```csharp
BinaryOp add = (a, b) => a + b;
BinaryOp mul = (a, b) => a * b;

int Apply(int x, int y, BinaryOp op) => op(x, y);

Console.WriteLine($"Apply(10, 5, add) = {Apply(10, 5, add)}");
Console.WriteLine($"Apply(10, 5, mul) = {Apply(10, 5, mul)}");

// top-level statements 파일에서는 타입 선언이 뒤로 와야 한다.
delegate int BinaryOp(int a, int b);
```
**실행 결과**
```
add(3, 4) = 7
mul(3, 4) = 12
Apply(10, 5, add) = 15
Apply(10, 5, mul) = 50
```
**메모:** 델리게이트는 변수 · 매개변수 · 반환값 어디에든 쓸 수 있는 일급 시민(first-class)입니다.

### 예제 2 — `ActionFunc` : 표준 델리게이트
```csharp
Action<string> greet = name => Console.WriteLine($"안녕, {name}!");
Func<int, int, int> add = (a, b) => a + b;
Func<int, int> square = n => n * n;

greet("Alice");
Console.WriteLine($"add(2, 3)  = {add(2, 3)}");
Console.WriteLine($"square(7)  = {square(7)}");
```
**실행 결과**
```
안녕, Alice!
안녕, Bob!
add(2, 3)  = 5
square(7)  = 49
음수
```
**메모:** `Action` 은 "행동만", `Func` 는 "값 돌려줌". 매개변수가 늘어나도 같은 타입을 그대로 확장합니다.

### 예제 3 — `Predicate` : `Array.Find` / `FindAll`
```csharp
Predicate<int> isPositive = n => n > 0;
Predicate<int> isEven = n => n % 2 == 0;

int[] numbers = [-3, -2, -1, 0, 1, 2, 3];
int firstPositive = Array.Find(numbers, isPositive);
List<int> evens = [.. numbers].FindAll(isEven);
```
**실행 결과**
```
첫 양수: 1
짝수들: -2, 0, 2
3 보다 큰 첫 값: 0
```
**메모:** 찾는 값이 없으면 `default(T)` 가 반환됩니다. `int` 의 default 는 0 이므로 진짜 0 과 구별하려면 `FindIndex` 등을 쓰세요.

### 예제 4 — `MethodGroup` : 메서드 그룹 변환
```csharp
List<string> names = ["Alice", "Bob", "Charlie"];
names.ForEach(n => Console.WriteLine(n));   // 람다
names.ForEach(Console.WriteLine);           // 메서드 그룹 (더 간결)
```
**실행 결과**
```
Alice
Bob
Charlie
Alice
Bob
Charlie
길이: 7
```
**메모:** 메서드 그룹은 람다보다 살짝 빠르고(.NET 7+ 에서 캐싱) 읽기도 좋습니다. 단, 시그니처가 정확히 맞아야 합니다.

### 예제 5 — `EventBasics` : `event` 발행/구독
```csharp
internal sealed class Sensor
{
    public event Action<string>? OnAlert;
    public void CheckTemperature(int t)
    {
        if (t >= 100) OnAlert?.Invoke($"고온 경고: {t}도");
    }
}
```
**실행 결과**
```
[로그] 고온 경고: 120도
[화면] !! 고온 경고: 120도 !!
```
**메모:** `+=` 로 핸들러를 여러 개 붙이면 **등록 순서대로** 모두 호출됩니다. 메모리 누수가 걱정된다면 더 이상 안 쓸 때 `-=` 로 해제하세요.

## 자주 하는 실수
1. `delegate` 타입을 직접 선언 — 대부분 `Action`/`Func` 으로 충분합니다.
2. `event` 를 외부에서 `=` 로 덮어쓰려 함 — 컴파일 에러. `+=` / `-=` 만 가능.
3. 람다 안에서 외부 변수를 캡처한 채 비동기로 넘기고, 원본 변수를 바꾸기 — "**클로저 캡처**" 라 의도치 않은 값이 보입니다.
4. `Func<int>` 처럼 매개변수가 없는데 `()` 를 빠뜨림 — 호출은 `f()`, 변수 참조는 `f`.
5. `Predicate<T>` 와 `Func<T,bool>` 을 서로 대입 — **타입이 달라 직접 대입 불가**. 람다로 다시 만들면 됩니다.

## 정리
- 메서드를 값처럼 다루려면 델리게이트, 즉석에서 만들려면 람다
- 직접 `delegate` 선언보다 `Action` / `Func` / `Predicate` 우선
- 메서드 그룹 변환으로 코드를 한 단계 더 짧게
- `event` 는 발행/구독 모델 — `+=`/`-=` 만 허용, 발행은 클래스 내부에서만

## 직접 해 보기
```bash
cd src/DelegateBasics && dotnet run
cd ../ActionFunc && dotnet run
cd ../Predicate && dotnet run
cd ../MethodGroup && dotnet run
cd ../EventBasics && dotnet run
```

## 다음 단원
[19_async_await](../19_async_await/) — 람다와 델리게이트가 비동기 세상에서 어떻게 빛나는지 봅니다.
