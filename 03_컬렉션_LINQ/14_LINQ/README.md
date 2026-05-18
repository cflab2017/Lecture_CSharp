# 14. LINQ

LINQ(Language Integrated Query)는 컬렉션·데이터베이스·XML 같은 데이터 소스에 대해 **SQL 처럼 선언적으로** 질의할 수 있게 해 주는 .NET 의 강력한 기능입니다. C# 안에서 자연스럽게 쓰이며, 메서드 체인 또는 쿼리식(query syntax) 두 가지 문법을 지원합니다.

## 학습 목표
- `Where`, `Select`, `OrderBy`, `GroupBy`, `Join` 등의 표준 LINQ 연산자를 사용한다
- 집계 연산자 `Sum`, `Average`, `Max`, `Aggregate` 를 안다
- 메서드 체인과 쿼리식의 차이를 이해한다
- **지연 실행(deferred execution)** 의 의미를 안다

## 핵심 개념

### 1) `Where` 와 `Select`
```csharp
int[] nums = [1, 2, 3, 4, 5, 6];
var result = nums.Where(n => n > 3).Select(n => n * n);
// 결과: 16, 25, 36
```
- `Where` 는 **걸러내기**, `Select` 는 **변환**.
- 람다(`=>`) 는 한 줄짜리 함수.

### 2) 정렬과 그룹화
```csharp
var sorted = people.OrderBy(p => p.Age).ThenBy(p => p.Name);
var byCity = people.GroupBy(p => p.City);

foreach (var group in byCity)
{
    Console.WriteLine($"{group.Key}: {group.Count()}명");
}
```
- 내림차순은 `OrderByDescending` / `ThenByDescending`.
- `GroupBy` 의 결과는 `IGrouping<TKey, TElement>` 시퀀스 — `Key` 와 그룹 내 원소를 함께 가진다.

### 3) 집계 연산자
| 연산자 | 설명 |
|---|---|
| `Sum`, `Average`, `Max`, `Min`, `Count` | 통상적인 통계 |
| `Aggregate(seed, (acc, x) => ...)` | 누적식 직접 정의 |
| `Any`, `All` | 조건을 만족하는 원소가 있나/모두인가 |

### 4) 메서드 체인 vs 쿼리식
같은 동작을 두 가지로 쓸 수 있습니다.
```csharp
// 메서드 체인
var a = nums.Where(n => n > 3).Select(n => n * n);

// 쿼리식 (SQL 유사)
var b = from n in nums
        where n > 3
        select n * n;
```
- 단순한 경우 메서드 체인이 짧고 명료.
- `join`, `let`, 복잡한 `group ... by ... into` 등은 쿼리식이 읽기 쉬울 때가 많음.

### 5) 지연 실행 (deferred execution)
LINQ 표현식은 **즉시 실행되지 않습니다**. `foreach` 로 순회하거나 `ToList`/`ToArray` 같은 종결 연산자가 호출될 때 비로소 평가됩니다.

```csharp
var q = list.Where(n => n > 0);
list.Add(99);
foreach (var n in q) { /* 99 도 포함된다 */ }
```

원본을 그 자리에서 "스냅샷" 하려면 `.ToList()` 로 강제 평가.

## 예제로 보기

### 예제 1 — `WhereSelect` : 걸러내고 변환하기
```csharp
int[] nums = [1, 2, 3, 4, 5, 6, 7, 8];

var squaresOfBig = nums.Where(n => n > 4).Select(n => n * n);

foreach (int x in squaresOfBig)
{
    Console.WriteLine(x);
}
```
**실행 결과**
```
25
36
49
64
```
**메모:** `Where` 는 조건이 `true` 인 것만 통과시키고, `Select` 는 각 원소를 다른 형태로 변환합니다. 순서를 바꾸면 결과도 달라질 수 있어요.

### 예제 2 — `OrderByGroup` : 정렬과 그룹화
```csharp
Person[] people =
[
    new("지수", "서울", 25),
    new("민호", "부산", 30),
    new("서연", "서울", 22),
    new("윤재", "부산", 28),
];

var sorted = people.OrderBy(p => p.City).ThenByDescending(p => p.Age);
foreach (Person p in sorted)
{
    Console.WriteLine($"{p.City} - {p.Name} ({p.Age})");
}

Console.WriteLine("---");

var groups = people.GroupBy(p => p.City);
foreach (var g in groups)
{
    Console.WriteLine($"[{g.Key}] {g.Count()}명: {string.Join(", ", g.Select(p => p.Name))}");
}

public class Person(string name, string city, int age)
{
    public string Name { get; } = name;
    public string City { get; } = city;
    public int Age { get; } = age;
}
```
**실행 결과**
```
부산 - 민호 (30)
부산 - 윤재 (28)
서울 - 지수 (25)
서울 - 서연 (22)
---
[서울] 2명: 지수, 서연
[부산] 2명: 민호, 윤재
```
**메모:** `ThenBy` 는 1차 정렬 기준이 같을 때 적용되는 2차 기준. `IGrouping<K,T>` 는 `Key` 프로퍼티와 원소 시퀀스를 모두 가집니다.

### 예제 3 — `Aggregation` : 집계 연산자
```csharp
int[] nums = [10, 20, 30, 40, 50];

Console.WriteLine($"합계: {nums.Sum()}");
Console.WriteLine($"평균: {nums.Average()}");
Console.WriteLine($"최댓값: {nums.Max()}");
Console.WriteLine($"최솟값: {nums.Min()}");
Console.WriteLine($"개수: {nums.Count()}");

// Aggregate: 직접 누적식 정의 (여기선 곱)
int product = nums.Aggregate(1, (acc, n) => acc * n);
Console.WriteLine($"곱: {product}");

bool allBigger = nums.All(n => n > 0);
bool anyEven = nums.Any(n => n % 2 == 0);
Console.WriteLine($"모두 양수? {allBigger}, 짝수 존재? {anyEven}");
```
**실행 결과**
```
합계: 150
평균: 30
최댓값: 50
최솟값: 10
개수: 5
곱: 12000000
모두 양수? True, 짝수 존재? True
```
**메모:** `Aggregate(seed, func)` 는 `seed` 부터 시작해 모든 원소에 `func` 를 누적 적용합니다. `Sum`/`Average` 등은 `Aggregate` 의 특수 사례라고 볼 수 있어요.

### 예제 4 — `QuerySyntax` : 메서드 체인 vs 쿼리식
```csharp
int[] nums = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

// 메서드 체인
var a = nums.Where(n => n % 2 == 0).Select(n => n * n);

// 동일한 의미의 쿼리식
var b = from n in nums
        where n % 2 == 0
        select n * n;

Console.WriteLine($"메서드 체인: [{string.Join(", ", a)}]");
Console.WriteLine($"쿼리식    : [{string.Join(", ", b)}]");
```
**실행 결과**
```
메서드 체인: [4, 16, 36, 64, 100]
쿼리식    : [4, 16, 36, 64, 100]
```
**메모:** 두 문법은 컴파일되면 동일한 코드가 됩니다. 취향과 가독성에 따라 골라 쓰면 돼요. 쿼리식 내부에서도 `join`, `group ... by ... into` 등을 자유롭게 결합할 수 있습니다.

### 예제 5 — `DeferredExec` : 지연 실행 시연
```csharp
List<int> nums = [1, 2, 3];

// 이 시점에 실행되지 않는다 — 단지 "어떻게 할지" 만 기억해 둠
var query = nums.Where(n => n > 1);

nums.Add(99);   // 쿼리 정의 이후에 원본을 수정

Console.Write("지연 실행 결과: ");
foreach (int n in query)
{
    Console.Write($"{n} ");
}
Console.WriteLine();

// 즉시 평가하려면 ToList 로 스냅샷을 만든다
var snapshot = nums.Where(n => n > 1).ToList();
nums.Add(100);
Console.WriteLine($"스냅샷 결과: [{string.Join(", ", snapshot)}]");
```
**실행 결과**
```
지연 실행 결과: 2 3 99 
스냅샷 결과: [2, 3, 99]
```
**메모:** 첫 번째 결과에 `99` 가 포함된 이유는 `foreach` 가 실제로 실행되는 순간에 `nums` 를 다시 보기 때문입니다. `ToList()`/`ToArray()` 로 평가 시점을 강제할 수 있어요.

## 자주 하는 실수
1. `Where` 다음에 `Select`, `Select` 다음에 `Where` — 순서에 따라 람다 매개변수 타입이 달라집니다.
2. 지연 실행을 잊고 `foreach` 를 여러 번 돌려 매번 다시 평가 → 성능 손해. 결과를 다시 쓸 거면 `.ToList()`.
3. `Count` 메서드 vs `Count` 프로퍼티 혼동: `IEnumerable<T>` 는 메서드, `List<T>` 는 프로퍼티(`Count`).
4. 빈 시퀀스에 `First()` / `Single()` 호출 → 예외. 안전하게 쓰려면 `FirstOrDefault()`.
5. `GroupBy` 결과를 `Dictionary` 처럼 인덱싱하려고 함 — `ToDictionary(g => g.Key)` 로 변환 필요.

## 정리
- LINQ 는 컬렉션에 대해 SQL 스타일로 질의하는 표준 도구다.
- `Where`/`Select`/`OrderBy`/`GroupBy`/`Aggregate` 가 가장 자주 쓰인다.
- 메서드 체인과 쿼리식은 표현만 다른 같은 기능이다.
- 지연 실행 덕분에 효율적이지만, 예상치 못한 결과를 막으려면 `.ToList()` 로 평가 시점을 명시할 수 있다.

## 직접 해 보기
```bash
cd src/WhereSelect
dotnet run

cd ../OrderByGroup
dotnet run

cd ../Aggregation
dotnet run

cd ../QuerySyntax
dotnet run

cd ../DeferredExec
dotnet run
```

## 다음 단원
[15_예외처리](../../04_예외_입출력/15_예외처리/) — 프로그램 실행 중 발생하는 오류를 안전하게 다루는 방법을 배웁니다.
