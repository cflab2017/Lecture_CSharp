# 과제 - 04. 제어문

## 문제 1 — 1부터 N까지의 합
- 프로젝트 폴더: `Homework01/`
- 핵심 개념: `for` 반복, 누적 합

### 요구사항
- 정수 `N` 을 입력받는다
- `1 + 2 + ... + N` 을 계산해 출력한다
- `N` 이 0 이하면 "0 이하 입력은 처리할 수 없습니다." 를 출력하고 종료

### 예상 출력
```
N: 10
1 ~ 10 의 합 = 55
```

### 힌트
- `for (int i = 1; i <= n; i++) sum += i;`
- 입력 검증을 먼저 한 뒤 `return;` 으로 일찍 종료할 수 있다 (top-level statements 에서도 가능)

## 문제 2 — FizzBuzz (1~30)
- 프로젝트 폴더: `Homework02/`
- 핵심 개념: `for`, `%`, `if`/`switch` 표현식

### 요구사항
- 1부터 30까지 출력하되,
  - 3의 배수이면 `Fizz`
  - 5의 배수이면 `Buzz`
  - 둘 다이면 `FizzBuzz`
  - 그 외는 숫자 그대로

### 예상 출력
```
1
2
Fizz
4
Buzz
Fizz
7
8
Fizz
Buzz
11
Fizz
13
14
FizzBuzz
... (30까지)
```

### 힌트
- `i % 15 == 0` 을 먼저 확인하거나, `switch` 표현식에서 패턴으로 처리
- `switch` 표현식 + when 절을 쓰면 한 식으로 깔끔하게 표현 가능:
  ```csharp
  string line = i switch
  {
      _ when i % 15 == 0 => "FizzBuzz",
      _ when i % 3 == 0  => "Fizz",
      _ when i % 5 == 0  => "Buzz",
      _ => i.ToString()
  };
  ```

## 정답 확인
직접 풀어 본 후 [`answer/`](./answer/) 폴더의 정답과 비교해 보세요.
