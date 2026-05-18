# 19. async / await

I/O 를 기다리는 동안 스레드를 점유하지 않게 해 주는 것이 **비동기 프로그래밍**입니다. C# 의 `async`/`await` 는 콜백 지옥 없이 동기 코드처럼 비동기를 쓰게 해 줍니다. 이 단원에서는 .NET 의 `Task` 모델과 함께 핵심 패턴을 익힙니다.

## 학습 목표
- `Task`/`Task<T>` 와 `async`/`await` 의 관계를 이해한다
- `Task.WhenAll` 로 병렬 대기, `Task.WhenAny` 로 첫 완료 처리
- `CancellationToken` 으로 비동기 작업을 안전하게 취소한다
- async 메서드의 예외가 `await` 시점에 던져진다는 사실을 안다
- `ConfigureAwait(false)` 의 용도를 큰 그림으로 파악한다

## 핵심 개념

### 1) `Task` 는 "미래의 결과"
`Task` 는 진행 중인 작업, `Task<T>` 는 끝나면 `T` 를 돌려줄 작업입니다. `await` 는 그 결과를 기다렸다가 꺼내 줍니다.

```csharp
async Task<int> GetAsync()
{
    await Task.Delay(1000);   // 1초간 스레드를 놓아준다
    return 42;
}

int n = await GetAsync();   // 비동기처럼 보이지만 동기처럼 읽힌다
```

### 2) `async` 가 붙은 메서드의 시그니처
- 반환 타입은 `Task`, `Task<T>`, 또는 `ValueTask`/`ValueTask<T>`
- 이벤트 핸들러 한정 예외로 `async void` — 일반 코드에서는 **쓰지 말 것** (예외를 잡을 수 없음)
- 관례적으로 메서드 이름 끝에 `Async` 를 붙임 (`SaveAsync`, `FetchAsync`)

### 3) 병렬 대기 — `Task.WhenAll` / `Task.WhenAny`
`await` 를 연달아 쓰면 순차 대기지만, `Task` 를 모아서 한 번에 `await` 하면 **동시에** 진행됩니다.

```csharp
Task<int> a = FetchAsync("A");
Task<int> b = FetchAsync("B");
int[] all = await Task.WhenAll(a, b);   // 모두 끝날 때까지
Task<int> first = await Task.WhenAny(a, b);   // 하나만 끝나도 됨
```

### 4) 취소 — `CancellationToken`
협동적 취소(cooperative cancellation) 모델입니다. **토큰이 받아 주는 쪽에서 확인** 해야 실제로 중단됩니다.

```csharp
using CancellationTokenSource cts = new();
cts.CancelAfter(2000);
await Task.Delay(5000, cts.Token);   // 2초에 OperationCanceledException
```

`Task.Delay`, `HttpClient.GetAsync`, `Stream.ReadAsync` 등 BCL 메서드는 대부분 `CancellationToken` 매개변수를 받습니다.

### 5) 예외는 `await` 에서 다시 던져진다
async 메서드의 예외는 `Task` 안에 보관되었다가, **`await` 하는 시점에** 다시 던져집니다. 따라서 `try`/`catch` 는 호출 쪽에 두면 됩니다.

```csharp
try { await DoAsync(); }
catch (InvalidOperationException ex) { /* ... */ }
```

`Task.WhenAll` 에서 여러 예외가 나면 첫 번째만 throw, 나머지는 `Task.Exception` (`AggregateException`) 에 모입니다.

### 6) `ConfigureAwait(false)` — 라이브러리 코드에서만
WinForms/WPF/ASP.NET Framework 처럼 **UI/요청 컨텍스트가 있는** 환경에서 await 후 같은 컨텍스트로 돌아오는 비용을 피하고 싶을 때 씁니다. 콘솔 앱이나 ASP.NET Core 에서는 거의 무의미합니다.

```csharp
await stream.ReadAsync(buf).ConfigureAwait(false);
```

라이브러리 작성자라면 반복적으로 붙이는 게 안전, 앱 코드는 신경 쓰지 않아도 OK.

## 예제로 보기

### 예제 1 — `AsyncBasics` : 기본 흐름
```csharp
Console.WriteLine("작업 시작");
int result = await DoWorkAsync();
Console.WriteLine($"결과: {result}");

static async Task<int> DoWorkAsync()
{
    await Task.Delay(1000);
    await Task.Delay(1000);
    return 42;
}
```
**실행 결과**
```
작업 시작
  ...1초 대기
  ...1초 더 대기
결과: 42
작업 종료
```
**메모:** `await Task.Delay` 동안 스레드는 다른 일을 할 수 있습니다. UI 가 멈추지 않는 핵심 이유입니다.

### 예제 2 — `WhenAll` : 동시에 시작, 한 번에 기다리기
```csharp
Task<int> t1 = FetchAsync("A", 1000);
Task<int> t2 = FetchAsync("B", 1500);
Task<int> t3 = FetchAsync("C", 800);
int[] results = await Task.WhenAll(t1, t2, t3);
```
**실행 결과**
```
  [A] 시작 (1000ms)
  [B] 시작 (1500ms)
  [C] 시작 (800ms)
  [C] 완료
  [A] 완료
  [B] 완료
결과 합계: 3300
총 시간: 1500 ms  (순차였다면 3300ms 정도)
```
**메모:** 가장 오래 걸린 작업의 시간만큼만 걸립니다. I/O 호출 여러 건을 묶을 때 최고의 패턴.

### 예제 3 — `WhenAny` : 가장 빠른 응답만 채택
```csharp
Task<string> winner = await Task.WhenAny(mirrorA, mirrorB, mirrorC);
string result = await winner;
```
**실행 결과**
```
가장 먼저 응답: Tokyo(800ms)
```
**메모:** `WhenAny` 는 가장 먼저 끝난 `Task` 자체를 돌려주므로, 값을 꺼내려면 **한 번 더 `await`** 해야 합니다.

### 예제 4 — `Cancellation` : 외부에서 중단하기
```csharp
using CancellationTokenSource cts = new();
cts.CancelAfter(TimeSpan.FromMilliseconds(1500));

try { await LongJobAsync(cts.Token); }
catch (OperationCanceledException) { Console.WriteLine("작업이 취소되었습니다"); }
```
**실행 결과**
```
  단계 1 완료
  단계 2 완료
작업이 취소되었습니다
```
**메모:** 토큰을 전달받은 메서드 안쪽에서 `Task.Delay(ms, token)` 처럼 토큰을 함께 넘겨야 합니다. 토큰을 무시하면 절대 안 멈춥니다.

### 예제 5 — `AsyncException` : 예외 흐름
```csharp
try { await FailingAsync(); }
catch (InvalidOperationException ex) { Console.WriteLine($"잡았다: {ex.Message}"); }
```
**실행 결과**
```
잡았다: 의도된 실패
WhenAll 에서 잡음: 의도된 실패
  t1 상태: Faulted, t2 상태: Faulted
```
**메모:** async 메서드 안에서 던진 예외는 Task 객체에 담겨 있다가, await 하는 순간 그대로 throw 됩니다. 평소 동기 코드처럼 try/catch 로 잡으면 됩니다.

## 자주 하는 실수
1. `async void` 사용 — 호출자가 예외를 못 잡고, 앱이 죽습니다. 이벤트 핸들러 외에는 쓰지 말 것.
2. `task.Result` / `task.Wait()` 호출 — UI 컨텍스트에서 데드락의 주범. 항상 `await`.
3. `Task.Run(async () => ...)` 으로 단순 I/O 를 감싸기 — I/O 는 이미 비동기라 스레드 풀에 던질 필요 없습니다.
4. `WhenAll` 의 결과를 모은 뒤에야 첫 예외만 보고 끝내기 — 다른 작업의 예외도 검사해야 누락 안 됨.
5. `CancellationToken` 을 받기만 하고 다음 호출에 전달하지 않기 — 취소가 전파되지 않습니다.

## 정리
- `async` 가 붙은 메서드는 `Task`/`Task<T>` 를 반환하고, 호출자는 `await` 로 결과를 꺼낸다
- 여러 작업을 묶을 땐 `Task.WhenAll`, 가장 빠른 하나만 쓸 땐 `Task.WhenAny`
- 외부 취소는 `CancellationToken` 으로, 호출 체인을 따라 끝까지 전파한다
- 예외는 `await` 시점에 던져지므로 평소처럼 `try`/`catch` 로 다루면 된다
- 콘솔/서버 앱은 `ConfigureAwait` 거의 신경 안 써도 됨, UI 라이브러리는 챙기자

## 직접 해 보기
```bash
cd src/AsyncBasics && dotnet run
cd ../WhenAll && dotnet run
cd ../WhenAny && dotnet run
cd ../Cancellation && dotnet run
cd ../AsyncException && dotnet run
```

## 다음 단원
[20_Nullable_참조_타입](../20_Nullable_참조_타입/) — `null` 의 정체를 컴파일 타임에 잡아 줍니다.
