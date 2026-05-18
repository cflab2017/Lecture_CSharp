# 12. List·Dictionary·HashSet·Queue·Stack

배열은 크기가 고정이지만 실제 프로그램에서는 **크기가 변하는 컬렉션**이 훨씬 자주 필요합니다. C#은 `System.Collections.Generic` 네임스페이스에 다양한 자료구조를 미리 만들어 두었습니다.

## 학습 목표
- `List<T>` 의 추가·삭제·검색·정렬 연산을 사용한다
- `Dictionary<TKey, TValue>` 로 키-값 쌍을 저장하고 조회한다
- `HashSet<T>` 로 중복 제거와 집합 연산(합/교집합)을 수행한다
- `Queue<T>` (FIFO) 와 `Stack<T>` (LIFO) 의 차이를 안다

## 핵심 개념

### 1) `List<T>` — 가변 길이 배열
```csharp
List<int> nums = [10, 20, 30];        // 컬렉션 식 OK
nums.Add(40);
nums.Remove(20);     // 값으로 첫 번째 제거
nums.RemoveAt(0);    // 인덱스로 제거
bool has = nums.Contains(30);
nums.Sort();
```
- 인덱서 `nums[i]` 로 배열처럼 접근 가능.
- 내부적으로 배열을 자동 확장해 줍니다.

### 2) `Dictionary<TKey, TValue>` — 키-값 매핑
```csharp
Dictionary<string, int> ages = new()
{
    ["지수"] = 20,
    ["민호"] = 25
};

ages["서연"] = 22;                 // 추가 또는 갱신
if (ages.TryGetValue("민호", out int age))
{
    Console.WriteLine(age);
}
```
- 키는 **유일**해야 하고, 동일 키로 두 번 `Add` 하면 예외가 납니다.
- `TryGetValue` 가 안전한 조회 패턴입니다.

### 3) `HashSet<T>` — 중복 없는 집합
```csharp
HashSet<string> a = ["사과", "배", "감"];
HashSet<string> b = ["배", "감", "포도"];

a.UnionWith(b);      // 합집합 (a 가 바뀜)
a.IntersectWith(b);  // 교집합
```
- 추가/검색이 평균 **O(1)** 로 매우 빠릅니다.
- 순서가 보장되지 않습니다.

### 4) `Queue<T>` 와 `Stack<T>`
- **`Queue<T>` (선입선출, FIFO)**: `Enqueue` 로 넣고 `Dequeue` 로 꺼냄. 줄 서는 사람들 비유.
- **`Stack<T>` (후입선출, LIFO)**: `Push` 로 넣고 `Pop` 으로 꺼냄. 책 더미 비유.
- 둘 다 `Peek` 로 다음에 꺼낼 값만 미리 볼 수 있습니다.

## 예제로 보기

### 예제 1 — `ListBasics` : `List<T>` 의 기본
```csharp
List<int> nums = [10, 20, 30];

nums.Add(40);
nums.Remove(20);

Console.WriteLine($"포함 30? {nums.Contains(30)}");
Console.WriteLine($"개수: {nums.Count}");

nums.Sort();
Console.WriteLine($"정렬: [{string.Join(", ", nums)}]");
```
**실행 결과**
```
포함 30? True
개수: 3
정렬: [10, 30, 40]
```
**메모:** `List<T>.Count` 는 배열의 `Length` 와 같은 역할. `Remove` 는 값으로, `RemoveAt` 는 인덱스로 지웁니다.

### 예제 2 — `DictBasics` : 단어 카운터
```csharp
string text = "apple banana apple cherry banana apple";
Dictionary<string, int> counts = new();

foreach (string word in text.Split(' '))
{
    counts[word] = counts.GetValueOrDefault(word, 0) + 1;
}

foreach (var (word, count) in counts)
{
    Console.WriteLine($"{word}: {count}");
}
```
**실행 결과**
```
apple: 3
banana: 2
cherry: 1
```
**메모:** `GetValueOrDefault(key, 기본값)` 은 키가 없으면 기본값을 돌려주는 편리한 메서드. `foreach` 의 `var (k, v)` 분해 구문은 .NET 8에서 자연스럽게 동작합니다.

### 예제 3 — `HashSet` : 중복 제거와 집합 연산
```csharp
string[] input = ["사과", "배", "사과", "감", "배"];
HashSet<string> unique = new(input);
Console.WriteLine($"중복 제거: [{string.Join(", ", unique)}]");

HashSet<string> a = ["사과", "배", "감"];
HashSet<string> b = ["배", "감", "포도"];

HashSet<string> intersection = new(a);
intersection.IntersectWith(b);
Console.WriteLine($"교집합: [{string.Join(", ", intersection)}]");

HashSet<string> union = new(a);
union.UnionWith(b);
Console.WriteLine($"합집합: [{string.Join(", ", union)}]");
```
**실행 결과**
```
중복 제거: [사과, 배, 감]
교집합: [배, 감]
합집합: [사과, 배, 감, 포도]
```
**메모:** 원본을 보존하려면 `new HashSet<T>(원본)` 으로 복사한 뒤 집합 연산을 호출합니다. `IntersectWith` 는 호출한 쪽을 직접 수정해요.

### 예제 4 — `QueueStack` : FIFO 와 LIFO
```csharp
Queue<string> queue = new();
queue.Enqueue("첫번째");
queue.Enqueue("두번째");
queue.Enqueue("세번째");

Console.WriteLine("=== Queue (FIFO) ===");
while (queue.Count > 0)
{
    Console.WriteLine(queue.Dequeue());
}

Stack<string> stack = new();
stack.Push("첫번째");
stack.Push("두번째");
stack.Push("세번째");

Console.WriteLine("=== Stack (LIFO) ===");
while (stack.Count > 0)
{
    Console.WriteLine(stack.Pop());
}
```
**실행 결과**
```
=== Queue (FIFO) ===
첫번째
두번째
세번째
=== Stack (LIFO) ===
세번째
두번째
첫번째
```
**메모:** `Queue` 는 들어온 순서대로, `Stack` 은 거꾸로 나옵니다. 웹 브라우저의 뒤로가기 = Stack, 프린터 작업 대기열 = Queue.

## 자주 하는 실수
1. `Dictionary` 의 같은 키에 `Add` 를 두 번 호출 → `ArgumentException`. 갱신은 `dict[key] = value` 로.
2. 빈 `Queue`/`Stack` 에서 `Dequeue`/`Pop` 호출 → `InvalidOperationException`. 미리 `Count > 0` 체크.
3. `List<T>` 를 `foreach` 로 순회하면서 `Add`/`Remove` → 컬렉션 변경 예외. `for` 역순 순회 또는 새 리스트 사용.
4. `HashSet<T>` 가 순서를 지킨다고 착각 — 순서 보장 안 됨. 정렬이 필요하면 `SortedSet<T>`.
5. `List<T>` 의 `Remove(value)` 는 **첫 번째** 일치만 제거. 모두 지우려면 `RemoveAll(predicate)`.

## 정리
- `List<T>` 는 가변 길이 배열, 대부분의 "여러 개" 상황에서 첫 선택지.
- `Dictionary<K,V>` 는 키-값 매핑, 조회 평균 O(1).
- `HashSet<T>` 는 중복 제거와 집합 연산.
- `Queue<T>`(FIFO), `Stack<T>`(LIFO) 는 처리 순서가 중요한 상황에 사용.

## 직접 해 보기
```bash
cd src/ListBasics
dotnet run

cd ../DictBasics
dotnet run

cd ../HashSet
dotnet run

cd ../QueueStack
dotnet run
```

## 다음 단원
[13_제네릭](../13_제네릭/) — 컬렉션이 어떻게 모든 타입을 받을 수 있는지, 그 뒤의 제네릭 문법을 배웁니다.
