# 과제 - 20. Nullable 참조 타입

## 문제 1 — null 가능 값 안전 조회
- 프로젝트 폴더: `Homework01/`
- 핵심 개념: `Dictionary<string, string?>`, `?.`, `??`

### 요구사항
- 다음과 같은 `Dictionary<string, string?>` 을 만든다.
  ```csharp
  Dictionary<string, string?> profile = new()
  {
      ["name"] = "Alice",
      ["email"] = null,
      ["city"] = "Seoul",
  };
  ```
- 키 목록 `["name", "email", "city", "phone"]` 을 순회하며 각 키의 길이를 출력한다.
  - 키가 없거나 값이 null 이면 길이는 `0` 으로 표시한다.
- 안전 연산자 `?.` 와 `??` 만 사용해 한 줄로 계산할 것.

### 예상 출력
```
name  : 5
email : 0
city  : 5
phone : 0
```

### 힌트
- `dict.TryGetValue(key, out string? value)` 로 값 추출.
- 길이 = `value?.Length ?? 0`.
- 출력은 `key.PadRight(6)` 등으로 정렬하면 깔끔합니다.

## 문제 2 — `User?` 를 반환하는 리포지토리
- 프로젝트 폴더: `Homework02/`
- 핵심 개념: `T?` 반환, `is { ... }` 패턴, `??`

### 요구사항
- `record User(int Id, string Name, int Age);` 를 정의한다.
- `UserRepo` 클래스에 다음 메서드를 만든다.
  - `User? FindById(int id)` — 없으면 null 반환.
- 메인에서 ID 1, 2, 99 를 조회하며 다음을 출력한다.
  - 찾으면 `"#1 Alice (30세)"` 형태.
  - 못 찾으면 `"#99 (찾을 수 없음)"` 형태.
- 추가로, 찾은 User 중 성인(Age >= 18)만 골라 "성인 회원: Alice, Bob" 형태로 한 줄 출력. (`is { Age: >= 18 }` 패턴 권장)

### 예상 출력
```
#1 Alice (30세)
#2 Bob (16세)
#99 (찾을 수 없음)
성인 회원: Alice
```

### 힌트
- 데이터 예시:
  ```csharp
  new User(1, "Alice", 30),
  new User(2, "Bob", 16),
  ```
- 사용처에서 `User? u = repo.FindById(id);` → `if (u is null) ... else ...`.
- 성인만 모을 때 `List<User?>` 보다는 `List<User>` 가 깔끔합니다.

## 정답 확인
직접 풀어 본 후 [`answer/`](./answer/) 폴더의 정답과 비교해 보세요.
