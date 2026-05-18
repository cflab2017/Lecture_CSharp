# 15. 예외 처리

프로그램은 늘 실패 가능성과 함께 살아갑니다. 0 으로 나누는 입력, 없는 파일, 끊긴 네트워크 — 이런 일이 생겨도 **앱이 죽지 않고 우아하게 회복**하도록 만드는 도구가 예외 처리입니다. C# 은 `try`/`catch`/`finally` 와 `throw` 로 이를 다룹니다.

## 학습 목표
- `try`/`catch`/`finally` 의 실행 흐름을 이해한다
- 여러 `catch` 블록과 `when` 절을 활용해 예외를 골라 잡는다
- 사용자 정의 예외 클래스를 만든다
- `using` 으로 리소스를 자동 해제한다
- `ArgumentNullException.ThrowIfNull` 같은 가드 메서드를 활용한다

## 핵심 개념

### 1) 예외란?
런타임에 발생하는 "정상적이지 않은 사건"을 객체로 표현한 것입니다. 모든 예외는 `System.Exception` 을 상속합니다.

```csharp
try
{
    int x = int.Parse("abc");   // FormatException 발생
}
catch (FormatException ex)
{
    Console.WriteLine($"잘못된 형식: {ex.Message}");
}
```

`catch` 가 없으면 호출 스택을 거슬러 올라가며 던져지고, 끝까지 못 잡으면 프로세스가 종료됩니다.

### 2) `try` / `catch` / `finally`
- `try` : 위험한 코드를 감싼다
- `catch` : 예외를 잡아 처리한다 (타입별로 여러 개 가능)
- `finally` : **예외 발생 여부와 무관하게 항상 실행**된다 — 리소스 정리에 쓴다

```csharp
try { /* 작업 */ }
catch (IOException ex) { /* 파일 오류만 */ }
catch (Exception ex)   { /* 그 외 모두 */ }
finally { /* 정리 */ }
```

좁은 타입의 `catch` 를 먼저, 넓은 타입(`Exception`)을 나중에 적습니다.

### 3) `when` 절로 조건 필터
`catch` 에 조건식을 붙여 **특정 조건일 때만** 잡습니다.

```csharp
try { /* ... */ }
catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
{
    // 404 만 여기서 처리
}
```

조건이 거짓이면 예외가 다시 위로 던져집니다.

### 4) `throw` 로 예외 던지기
잘못된 인자나 상태에서 직접 예외를 만들어 던질 수 있습니다.

```csharp
if (age < 0)
    throw new ArgumentException("나이는 0 이상이어야 합니다.", nameof(age));
```

이미 잡은 예외를 다시 던지고 싶을 땐 **`throw;`** (스택 트레이스 유지). `throw ex;` 는 스택 트레이스가 초기화되니 피합니다.

### 5) 사용자 정의 예외
도메인 의미를 살리려면 `Exception` 을 상속해 만듭니다.

```csharp
public class InvalidAgeException : Exception
{
    public InvalidAgeException(string message) : base(message) { }
}
```

### 6) `using` 과 `IDisposable`
파일·네트워크 같은 외부 리소스는 `IDisposable` 을 구현합니다. `using` 으로 감싸면 **스코프를 벗어날 때 자동으로 `Dispose()`** 가 호출됩니다 — `finally` 를 직접 쓸 필요가 없습니다.

```csharp
using var sr = new StreamReader("a.txt");
// 이 블록을 벗어나면 sr.Dispose() 자동 호출
```

### 7) `ArgumentNullException.ThrowIfNull` (.NET 6+)
인자 검증 한 줄 패턴입니다.

```csharp
public void Save(string name)
{
    ArgumentNullException.ThrowIfNull(name);   // null 이면 즉시 예외
    // ...
}
```

## 예제로 보기

### 예제 1 — `TryCatch` : 0 으로 나누기 잡기
```csharp
try
{
    int a = 10;
    int b = 0;
    int c = a / b;          // DivideByZeroException
    Console.WriteLine(c);
}
catch (DivideByZeroException ex)
{
    Console.WriteLine($"0 으로 나눌 수 없습니다: {ex.Message}");
}

Console.WriteLine("프로그램 계속 진행");
```
**실행 결과**
```
0 으로 나눌 수 없습니다: Attempted to divide by zero.
프로그램 계속 진행
```
**메모:** 예외를 잡았기 때문에 뒤 코드가 정상 실행됩니다.

### 예제 2 — `Finally` : `finally` 는 무조건 실행
```csharp
try
{
    Console.WriteLine("try 진입");
    throw new InvalidOperationException("일부러 던짐");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"catch: {ex.Message}");
}
finally
{
    Console.WriteLine("finally: 항상 실행됨");
}
```
**실행 결과**
```
try 진입
catch: 일부러 던짐
finally: 항상 실행됨
```
**메모:** `return` 이나 다른 예외가 나도 `finally` 는 실행됩니다.

### 예제 3 — `WhenFilter` : `when` 으로 메시지 필터
```csharp
try
{
    throw new InvalidOperationException("NotFound: 사용자 없음");
}
catch (InvalidOperationException ex) when (ex.Message.Contains("NotFound"))
{
    Console.WriteLine("404 와 비슷한 상황으로 처리");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"기타 오류: {ex.Message}");
}
```
**실행 결과**
```
404 와 비슷한 상황으로 처리
```
**메모:** `when` 조건이 거짓이었다면 두 번째 `catch` 가 잡았을 것입니다.

### 예제 4 — `CustomException` : 사용자 정의 예외
```csharp
try
{
    Register(-1);
}
catch (InvalidAgeException ex)
{
    Console.WriteLine($"등록 실패: {ex.Message}");
}

static void Register(int age)
{
    if (age < 0)
        throw new InvalidAgeException($"나이가 음수입니다: {age}");
    Console.WriteLine($"{age}세 등록 완료");
}

public class InvalidAgeException : Exception
{
    public InvalidAgeException(string message) : base(message) { }
}
```
**실행 결과**
```
등록 실패: 나이가 음수입니다: -1
```
**메모:** 도메인 의미를 살린 예외 타입은 호출 측에서 잡기 쉽습니다.

### 예제 5 — `UsingDispose` : `using` 으로 자동 해제
```csharp
// 파일 대신 메모리 reader 로 시연 (16편에서 진짜 파일을 다룸)
using var sr = new System.IO.StringReader("첫 줄\n둘째 줄\n셋째 줄");

string? line;
while ((line = sr.ReadLine()) is not null)
    Console.WriteLine($"> {line}");

// 스코프 종료 시 sr.Dispose() 자동 호출
```
**실행 결과**
```
> 첫 줄
> 둘째 줄
> 셋째 줄
```
**메모:** `using var` 는 변수 스코프(블록 끝)에 맞춰 자동 해제됩니다.

### 예제 6 — `ThrowIfNull` : 한 줄 가드
```csharp
PrintLength("hello");   // 정상
PrintLength(null);      // ArgumentNullException

static void PrintLength(string? text)
{
    ArgumentNullException.ThrowIfNull(text);
    Console.WriteLine($"길이: {text.Length}");
}
```
**실행 결과**
```
길이: 5
Unhandled exception. System.ArgumentNullException: Value cannot be null. (Parameter 'text')
```
**메모:** `.NET 6+` 의 가드 메서드. 매개변수 이름이 자동으로 `Parameter 'text'` 로 들어갑니다.

## 자주 하는 실수
1. `catch (Exception)` 만 두고 모든 예외를 묻어버리기 — 원인을 못 찾는다.
2. 좁은 타입의 `catch` 를 넓은 타입 뒤에 두기 — 컴파일 에러(도달 불가).
3. `throw ex;` 로 다시 던지기 — 스택 트레이스가 사라진다. **`throw;`** 를 쓴다.
4. `finally` 에서 또 예외 던지기 — 원래 예외가 묻힌다.
5. 정상 흐름에 예외를 쓰기 — 예외는 비싸다. `TryParse` 같은 패턴을 우선 고려.

## 정리
- `try`/`catch`/`finally` 로 위험 코드와 정리 코드를 분리한다
- 좁은 타입부터 잡고, `when` 으로 조건도 걸 수 있다
- 도메인 예외는 직접 만들어 의미를 살린다
- `using` 으로 `IDisposable` 리소스는 자동 해제
- `ArgumentNullException.ThrowIfNull` 로 인자 검증을 한 줄에

## 직접 해 보기
```bash
cd src/TryCatch && dotnet run
cd ../Finally && dotnet run
cd ../WhenFilter && dotnet run
cd ../CustomException && dotnet run
cd ../UsingDispose && dotnet run
cd ../ThrowIfNull && dotnet run
```

## 다음 단원
[16_파일_IO](../16_파일_IO/) — 진짜 파일을 열고 닫는 안전한 패턴을 배웁니다.
