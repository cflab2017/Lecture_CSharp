# 과제 - 19. async / await

## 문제 1 — 취소 가능한 카운트다운
- 프로젝트 폴더: `Homework01/`
- 핵심 개념: `async Task`, `Task.Delay`, `CancellationTokenSource`

### 요구사항
- `CountdownAsync(int seconds, CancellationToken token)` 메서드를 만든다.
- 1초 간격으로 남은 초를 출력한다 ("5초 남음", "4초 남음", ...).
- 0 에 도달하면 "발사!" 를 출력한다.
- 메인에서 `CancellationTokenSource` 를 만들고 2.5초 후 취소 신호를 보내, 카운트다운이 도중에 멈추도록 한다.
- 취소되면 "취소되었습니다" 를 출력한다.

### 예상 출력
```
5초 남음
4초 남음
3초 남음
취소되었습니다
```

### 힌트
- `await Task.Delay(1000, token);` 처럼 토큰을 같이 넘기면 자동으로 `OperationCanceledException` 이 던져진다.
- `cts.CancelAfter(2500);` 로 지정 시간 후 취소.
- 카운트다운은 `Console.WriteLine($"{i}초 남음");` → `await Task.Delay(1000, token);` 순서.

## 문제 2 — 세 작업 병렬 처리
- 프로젝트 폴더: `Homework02/`
- 핵심 개념: `Task.WhenAll`, `async Task<T>`

### 요구사항
- 3 개의 비동기 작업 `LoadAsync(string name, int delayMs)` 를 만든다 — 지정 시간만큼 대기 후 `delayMs` 를 그대로 반환한다.
- 메인에서 3 개의 작업을 동시에 시작 (예: 700ms / 1200ms / 900ms).
- `Task.WhenAll` 로 모두 기다린 뒤, 반환된 값들의 합을 출력한다.
- 전체 소요 시간도 `Stopwatch` 로 측정해서 출력한다 (가장 긴 작업과 비슷해야 함).

### 예상 출력 (수치 예시)
```
[A] 시작
[B] 시작
[C] 시작
[A] 완료 (700ms)
[C] 완료 (900ms)
[B] 완료 (1200ms)
합계: 2800
소요: 1200ms 근처
```

### 힌트
- `System.Diagnostics.Stopwatch` 사용.
- `int[] results = await Task.WhenAll(t1, t2, t3);` → `results.Sum()`.
- 출력 순서는 작업 완료 순서대로(짧은 것부터) 나옵니다.

## 정답 확인
직접 풀어 본 후 [`answer/`](./answer/) 폴더의 정답과 비교해 보세요.
