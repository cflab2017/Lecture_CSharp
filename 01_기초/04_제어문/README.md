# 04. 제어문

프로그램의 흐름은 위에서 아래로 흐르되, **조건**으로 분기하고 **반복**으로 되풀이합니다. C# 8 이후 추가된 `switch` 표현식은 함수형 스타일의 깔끔한 패턴 매칭을 제공합니다.

## 학습 목표
- `if` / `else if` / `else` 로 조건 분기를 한다
- `switch` 문과 **`switch` 표현식**(`=>`)의 차이를 안다
- `for` / `while` / `do-while` / `foreach` 의 쓰임을 구분한다
- `break` 와 `continue` 로 반복을 제어한다

## 핵심 개념

### 1) `if` / `else if` / `else`
```csharp
int score = 85;
if (score >= 90)        Console.WriteLine("A");
else if (score >= 80)   Console.WriteLine("B");
else if (score >= 70)   Console.WriteLine("C");
else                    Console.WriteLine("F");
```
조건이 위에서부터 차례로 평가되어, **처음 참인 가지** 하나만 실행됩니다.

### 2) `switch` 문 vs `switch` 표현식
**문(statement)** 형:
```csharp
switch (day)
{
    case 1: Console.WriteLine("월"); break;
    case 2: Console.WriteLine("화"); break;
    default: Console.WriteLine("?"); break;
}
```

**표현식(expression)** 형 — C# 8+:
```csharp
string name = day switch
{
    1 => "월",
    2 => "화",
    _ => "?"        // default 역할
};
```
표현식형은 **값을 반환**하므로 변수에 바로 대입할 수 있고, `break` 가 필요 없습니다. `_` 는 "어떤 값과도 매칭" 되는 와일드카드입니다.

### 3) `for` / `while` / `do-while`
- `for` : 횟수가 정해진 반복
- `while` : 조건이 참인 동안 반복 (0회 가능)
- `do-while` : 일단 한 번 실행 후 조건 검사 (최소 1회)

```csharp
for (int i = 0; i < 3; i++)  Console.WriteLine(i);  // 0 1 2

int n = 3;
while (n > 0) { Console.WriteLine(n); n--; }        // 3 2 1

int m = 0;
do { Console.WriteLine("once"); } while (m > 0);    // 한 번만
```

### 4) `foreach`
배열·리스트 등 **`IEnumerable`** 을 순회합니다. 인덱스가 필요 없는 단순 순회에 적합.

```csharp
int[] nums = [10, 20, 30];
foreach (int x in nums)
    Console.WriteLine(x);
```

### 5) `break` / `continue`
- `break` : 현재 반복문을 **즉시 종료**
- `continue` : 현재 반복을 건너뛰고 **다음 반복으로**

```csharp
for (int i = 1; i <= 10; i++)
{
    if (i == 5) break;       // i==5 에서 탈출
    if (i % 2 == 0) continue;// 짝수는 건너뜀
    Console.WriteLine(i);    // 1, 3 출력
}
```

## 예제로 보기

### 예제 1 — `IfElse/Program.cs` : 성적 등급 판별
```csharp
Console.Write("점수: ");
int score = int.Parse(Console.ReadLine()!);

string grade;
if (score >= 90)      grade = "A";
else if (score >= 80) grade = "B";
else if (score >= 70) grade = "C";
else if (score >= 60) grade = "D";
else                  grade = "F";

Console.WriteLine($"등급: {grade}");
```
**실행 결과**
```
점수: 85
등급: B
```
**메모:** 위에서 아래로 평가되니, **좁은 조건부터** 또는 **큰 값부터** 정렬해야 의도대로 동작합니다.

### 예제 2 — `SwitchExpression/Program.cs` : 요일을 한글로
```csharp
Console.Write("요일 번호 (1~7): ");
int day = int.Parse(Console.ReadLine()!);

string name = day switch
{
    1 => "월요일",
    2 => "화요일",
    3 => "수요일",
    4 => "목요일",
    5 => "금요일",
    6 => "토요일",
    7 => "일요일",
    _ => "잘못된 입력"
};

Console.WriteLine(name);
```
**실행 결과**
```
요일 번호 (1~7): 3
수요일
```
**메모:** `switch` 표현식은 모든 경우를 다루지 않으면 경고가 납니다. `_` 로 기본값을 꼭 둡시다.

### 예제 3 — `ForWhile/Program.cs` : `for` 합계, `while` 카운트다운
```csharp
// 1부터 10까지의 합
int sum = 0;
for (int i = 1; i <= 10; i++)
    sum += i;
Console.WriteLine($"1~10 합 = {sum}");

// 5부터 1까지 카운트다운
int n = 5;
while (n > 0)
{
    Console.Write($"{n} ");
    n--;
}
Console.WriteLine("발사!");
```
**실행 결과**
```
1~10 합 = 55
5 4 3 2 1 발사!
```
**메모:** 누적 변수는 **루프 밖**에서 초기화해야 결과가 유지됩니다.

### 예제 4 — `Foreach/Program.cs` : 배열 합계
```csharp
int[] scores = [78, 92, 64, 85, 100];

int total = 0;
foreach (int s in scores)
    total += s;

double avg = (double)total / scores.Length;
Console.WriteLine($"총점: {total}, 평균: {avg:F2}");
```
**실행 결과**
```
총점: 419, 평균: 83.80
```
**메모:** 배열의 자세한 사용법은 11편에서 다룹니다. 여기서는 "묶음을 순회한다" 정도면 충분합니다.

## 자주 하는 실수
1. `for (int i = 0; i <= n; i++)` 처럼 `<=` 와 `<` 헷갈리기 — **off-by-one** 버그.
2. `switch` 문에서 `break` 누락 — 표현식형엔 없지만 문형은 필수.
3. 무한 루프 — `while (true)` 안에 `break` 조건 빠짐.
4. `foreach` 중 컬렉션 수정 — 예외 발생. 수정하려면 `for` 사용.
5. `if (a == 0 || 1)` 같이 잘못 쓰기 — `if (a == 0 || a == 1)` 이 맞다.

## 정리
- 분기: `if`/`else` 와 `switch`(표현식형 권장)
- 반복: 횟수 — `for`, 조건 — `while`, 최소 1회 — `do-while`, 컬렉션 — `foreach`
- `break` 로 탈출, `continue` 로 건너뛰기
- `switch` 표현식 + `_` 패턴이 현대 C# 의 권장 스타일

## 직접 해 보기
```bash
cd src/IfElse && dotnet run
cd ../SwitchExpression && dotnet run
cd ../ForWhile && dotnet run
cd ../Foreach && dotnet run
```

## 다음 단원
[05_메서드](../05_메서드/) — 코드를 메서드로 묶어 재사용합니다.
