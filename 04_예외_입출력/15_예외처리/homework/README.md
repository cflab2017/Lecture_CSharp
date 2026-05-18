# 과제 - 15. 예외 처리

## 문제 1 — 안전한 숫자 입력
- 프로젝트 폴더: `Homework01/`
- 핵심 개념: `int.Parse`/`FormatException`, `try`/`catch`, 재입력 루프

### 요구사항
- 사용자에게 "정수를 입력하세요: " 라고 표시하고 한 줄을 받는다
- `int.Parse` 가 실패하면 "정수가 아닙니다. 다시 입력하세요." 라고 안내하고 다시 받는다
- 성공하면 "입력한 값: NN" 을 출력하고 종료한다

### 예상 출력
```
정수를 입력하세요: abc
정수가 아닙니다. 다시 입력하세요.
정수를 입력하세요: 12.3
정수가 아닙니다. 다시 입력하세요.
정수를 입력하세요: 42
입력한 값: 42
```

### 힌트
- `while (true)` 루프 안에서 `try`/`catch (FormatException)` 로 감싼다
- 성공하면 `break;`
- `Console.ReadLine()` 의 반환은 `string?` 이므로 `?? ""` 로 받거나 별도 처리

## 문제 2 — 잔액 부족 예외
- 프로젝트 폴더: `Homework02/`
- 핵심 개념: 사용자 정의 예외, `throw`, 메서드 가드

### 요구사항
- `InsufficientBalanceException : Exception` 을 정의한다
- `class BankAccount` 를 만들고 `decimal Balance` 와 `void Withdraw(decimal amount)` 를 둔다
- `Withdraw` 는 `amount` 가 `Balance` 보다 크면 `InsufficientBalanceException` 을 던진다
- `Main` 에서 잔액 1000 의 계좌에 1500 을 인출 시도해 예외를 잡아 메시지를 출력한다

### 예상 출력
```
출금 시도: 1500
실패: 잔액 부족 (잔액 1000, 요청 1500)
```

### 힌트
- 예외 메시지에 잔액과 요청 금액을 함께 담으면 디버깅에 좋다
- `amount` 가 0 이하인 경우도 `ArgumentOutOfRangeException` 으로 막아보자 (선택)

## 정답 확인
직접 풀어 본 후 [`answer/`](./answer/) 폴더의 정답과 비교해 보세요.
