# 11. 배열

배열은 같은 타입의 값을 **고정된 크기**로 줄 세워 저장하는 가장 기본적인 자료구조입니다. C#에서는 1차원·2차원 배열을 모두 지원하며, `Array` 클래스의 정적 메서드와 `Span<T>` 같은 현대적 도구로 다양한 작업을 효율적으로 처리할 수 있습니다.

## 학습 목표
- 1차원 배열을 선언·초기화하고 인덱스로 접근한다
- 2차원 배열(`int[,]`)을 만들고 `GetLength` 로 크기를 구한다
- `Array.Sort`, `IndexOf`, `Reverse`, `Copy` 같은 정적 메서드를 사용한다
- `Span<T>` / `ReadOnlySpan<T>` 가 무엇이며 왜 필요한지 한 문장으로 이해한다

## 핵심 개념

### 1) 배열 선언과 컬렉션 식
.NET 8에서는 **컬렉션 식(collection expression)** `[...]` 으로 간결하게 초기화할 수 있습니다.

```csharp
int[] a = [1, 2, 3, 4, 5];   // 컬렉션 식 (.NET 8+)
int[] b = { 1, 2, 3 };       // 전통 방식
int[] c = new int[5];        // 0 으로 채워진 길이 5 배열
```

- 인덱스는 **0부터 시작**, 마지막 인덱스는 `Length - 1`.
- `^1` 은 끝에서 첫 번째(= `Length - 1`).

### 2) 2차원 배열
```csharp
int[,] grid = new int[3, 4];          // 3행 4열
grid[1, 2] = 99;
int rows = grid.GetLength(0);          // 3
int cols = grid.GetLength(1);          // 4
```

`int[][]` 는 **들쭉날쭉 배열(jagged array)** 로, 행마다 길이가 다를 수 있어요. `int[,]` 는 직사각형 배열입니다.

### 3) `Array` 정적 메서드
| 메서드 | 설명 |
|---|---|
| `Array.Sort(arr)` | 오름차순 정렬 (제자리) |
| `Array.Reverse(arr)` | 순서 뒤집기 |
| `Array.IndexOf(arr, value)` | 첫 등장 인덱스, 없으면 `-1` |
| `Array.Copy(src, dst, length)` | 앞에서 `length` 개 복사 |

### 4) `Span<T>` / `Memory<T>` 입문
`Span<T>` 는 배열·문자열의 **일부 구간을 복사 없이 가리키는 얇은 래퍼**입니다. 새 배열을 만들지 않아 메모리·속도에 유리합니다.

```csharp
ReadOnlySpan<char> slice = "hello".AsSpan(1, 3);   // "ell"
```

지금은 "효율적 슬라이싱"만 기억하면 됩니다. 자세한 활용은 고급 주제로 미룹니다.

## 예제로 보기

### 예제 1 — `ArrayBasics` : 1차원 배열의 기본
```csharp
int[] scores = [85, 92, 78, 90, 88];

Console.WriteLine($"길이: {scores.Length}");
Console.WriteLine($"첫 번째: {scores[0]}");
Console.WriteLine($"마지막: {scores[^1]}");

int sum = 0;
foreach (int s in scores)
{
    sum += s;
}
Console.WriteLine($"합계: {sum}");
```
**실행 결과**
```
길이: 5
첫 번째: 85
마지막: 88
합계: 433
```
**메모:** `^1` 은 "끝에서 1번째" 인덱스. C# 8에서 도입된 인덱스 연산자입니다.

### 예제 2 — `Array2D` : 2차원 배열 다루기
```csharp
int[,] grid = new int[3, 4];

for (int r = 0; r < grid.GetLength(0); r++)
{
    for (int c = 0; c < grid.GetLength(1); c++)
    {
        grid[r, c] = r * 10 + c;
    }
}

for (int r = 0; r < grid.GetLength(0); r++)
{
    for (int c = 0; c < grid.GetLength(1); c++)
    {
        Console.Write($"{grid[r, c],3} ");
    }
    Console.WriteLine();
}
```
**실행 결과**
```
  0   1   2   3
 10  11  12  13
 20  21  22  23
```
**메모:** `GetLength(0)` 은 행 수, `GetLength(1)` 은 열 수. `{value,3}` 은 너비 3칸 오른쪽 정렬 서식.

### 예제 3 — `ArrayMethods` : `Array` 정적 메서드
```csharp
int[] nums = [4, 2, 9, 1, 7];

Array.Sort(nums);
Console.WriteLine($"정렬: [{string.Join(", ", nums)}]");

Array.Reverse(nums);
Console.WriteLine($"역순: [{string.Join(", ", nums)}]");

int idx = Array.IndexOf(nums, 7);
Console.WriteLine($"7의 위치: {idx}");

int[] copy = new int[3];
Array.Copy(nums, copy, 3);
Console.WriteLine($"앞 3개 복사: [{string.Join(", ", copy)}]");
```
**실행 결과**
```
정렬: [1, 2, 4, 7, 9]
역순: [9, 7, 4, 2, 1]
7의 위치: 1
앞 3개 복사: [9, 7, 4]
```
**메모:** `Array.Sort` 는 원본을 **그 자리에서** 바꿉니다(반환값 없음). `string.Join` 으로 배열을 보기 좋게 출력할 수 있어요.

### 예제 4 — `SpanIntro` : `ReadOnlySpan<char>` 으로 슬라이싱
```csharp
ReadOnlySpan<char> text = "Hello, World!".AsSpan();

ReadOnlySpan<char> hello = text.Slice(0, 5);
ReadOnlySpan<char> world = text.Slice(7, 5);

Console.WriteLine(hello.ToString());
Console.WriteLine(world.ToString());

// Substring 과 달리 새 문자열을 만들지 않고 원본을 가리킨다.
Console.WriteLine($"원본 길이: {text.Length}, 슬라이스 길이: {hello.Length}");
```
**실행 결과**
```
Hello
World
원본 길이: 13, 슬라이스 길이: 5
```
**메모:** `string.Substring` 은 매번 새 문자열을 할당하지만 `Span` 은 메모리를 공유합니다. 대량 텍스트 파싱에서 유용해요.

## 자주 하는 실수
1. `arr[arr.Length]` 처럼 마지막 + 1 인덱스에 접근 → `IndexOutOfRangeException`.
2. 2차원 배열을 `grid[1][2]` 로 접근 — `int[,]` 는 `grid[1, 2]` 가 맞습니다. (`grid[1][2]` 는 `int[][]` 용).
3. `Array.Sort` 가 새 배열을 반환한다고 착각 — **원본을 바꿉니다**(`void`).
4. `int[]` 의 크기는 한 번 정해지면 못 늘림 — 가변 길이가 필요하면 다음 단원의 `List<T>` 사용.
5. `Span<T>` 를 비동기 메서드의 `await` 너머로 들고 갈 수 없음 — 쓰임이 제한됨에 유의.

## 정리
- 배열은 **같은 타입, 고정 길이** 자료구조이며 컬렉션 식 `[1, 2, 3]` 으로 간결하게 만든다.
- 2차원 배열은 `int[,]`, `GetLength(0/1)` 로 크기를 구한다.
- 자주 쓰는 동작은 `Array` 정적 메서드 (`Sort`, `IndexOf`, `Reverse`, `Copy`) 로 해결한다.
- `Span<T>` 는 복사 없이 부분을 가리키는 현대적 도구다.

## 직접 해 보기
```bash
cd src/ArrayBasics
dotnet run

cd ../Array2D
dotnet run

cd ../ArrayMethods
dotnet run

cd ../SpanIntro
dotnet run
```

## 다음 단원
[12_List_Dictionary_HashSet](../12_List_Dictionary_HashSet/) — 길이가 변하는 컬렉션과 키-값 저장소를 다룹니다.
