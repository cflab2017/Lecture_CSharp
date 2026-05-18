# 과제 - 18. 델리게이트와 람다

## 문제 1 — 숫자 필터
- 프로젝트 폴더: `Homework01/`
- 핵심 개념: `Func<int,bool>` 매개변수, 람다, `IEnumerable<T>`

### 요구사항
- `IEnumerable<int> Filter(IEnumerable<int> source, Func<int,bool> predicate)` 메서드를 만든다.
- `int[] nums = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];` 에 대해 다음을 람다로 호출해 출력한다.
  - 짝수만
  - 5 이상
  - 소수(2 이상이고 자기 자신 외 약수가 없음)

### 예상 출력
```
짝수: 2 4 6 8 10
5 이상: 5 6 7 8 9 10
소수: 2 3 5 7
```

### 힌트
- `Filter` 안은 `foreach` 와 `yield return` 으로 작성하거나, 그냥 `List<int>` 에 모아서 반환해도 OK.
- 소수 판정은 `n` 이 2 미만이면 false, 2 부터 `n-1` 까지 나눠 보면 됨 (간단 버전).

## 문제 2 — 사칙연산 계산기
- 프로젝트 폴더: `Homework02/`
- 핵심 개념: `Dictionary<string, Func<int,int,int>>`, 람다 등록, 메서드 그룹

### 요구사항
- `Calculator` 클래스에 `Dictionary<string, Func<int,int,int>> Ops` 를 두고 4 가지 연산("+", "-", "*", "/") 을 등록한다.
- `int Run(string op, int a, int b)` 메서드가 딕셔너리에서 람다를 찾아 실행한다.
- 등록되지 않은 연산자가 들어오면 `InvalidOperationException` 을 던진다.
- 다음을 호출해 결과를 출력한다.
  - `Run("+", 10, 3)`, `Run("-", 10, 3)`, `Run("*", 10, 3)`, `Run("/", 10, 3)`

### 예상 출력
```
10 + 3 = 13
10 - 3 = 7
10 * 3 = 30
10 / 3 = 3
```

### 힌트
- `Ops["+"] = (a, b) => a + b;` 처럼 등록.
- `if (!Ops.TryGetValue(op, out var fn)) throw new InvalidOperationException(...)` 패턴.
- `int` 끼리 `/` 는 몫만 반환합니다.

## 정답 확인
직접 풀어 본 후 [`answer/`](./answer/) 폴더의 정답과 비교해 보세요.
