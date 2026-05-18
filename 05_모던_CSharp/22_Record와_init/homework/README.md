# 과제 - 22. Record 와 init

## 문제 1 — Book 레코드
- 프로젝트 폴더: `Homework01/`
- 핵심 개념: `record` 선언, `with` 식, 값 동등성

### 요구사항
- `record Book(string Title, string Author, int Pages);` 를 정의한다.
- `Book original = new("CLR via C#", "Jeffrey Richter", 1100);` 을 만든다.
- `with` 식을 사용해 다음을 만든다.
  - `revised` : `original` 에서 `Pages` 만 1200 으로 바꾼 새 인스턴스.
  - `translated` : `original` 에서 `Title` 을 "CLR via C# (한국어판)" 으로 바꾼 새 인스턴스.
- 세 인스턴스를 모두 출력한다 (record 의 자동 `ToString` 사용).
- `original == revised`, `original == new("CLR via C#", "Jeffrey Richter", 1100)` 두 값을 출력한다 (각각 False, True 가 나와야 함).

### 예상 출력
```
original  : Book { Title = CLR via C#, Author = Jeffrey Richter, Pages = 1100 }
revised   : Book { Title = CLR via C#, Author = Jeffrey Richter, Pages = 1200 }
translated: Book { Title = CLR via C# (한국어판), Author = Jeffrey Richter, Pages = 1100 }

original == revised        ? False
original == 같은 값의 새 객체 ? True
```

### 힌트
- `record Book(string Title, string Author, int Pages);` 만으로 충분.
- `var revised = original with { Pages = 1200 };`
- 두 record 인스턴스의 `==` 는 값 비교.

## 문제 2 — Vector 레코드 구조체
- 프로젝트 폴더: `Homework02/`
- 핵심 개념: `readonly record struct`, 메서드 추가, 연산자 오버로드는 선택

### 요구사항
- `readonly record struct Vector(double X, double Y)` 를 정의한다.
- 같은 record 안에 인스턴스 메서드 `Vector Add(Vector other)` 를 추가한다 — 두 벡터를 더한 새 `Vector` 반환.
- 같은 record 안에 인스턴스 메서드 `double Length()` 를 추가한다 — `sqrt(X*X + Y*Y)`.
- 다음을 출력한다.
  - `v1 = (3, 4)`, `v2 = (1, 2)`.
  - `v1.Add(v2)` 결과.
  - `v1.Length()` 값 (5).
  - `v1 == new Vector(3, 4)` (True).

### 예상 출력
```
v1 = Vector { X = 3, Y = 4 }
v2 = Vector { X = 1, Y = 2 }
v1 + v2 = Vector { X = 4, Y = 6 }
|v1| = 5
v1 == new Vector(3, 4) ? True
```

### 힌트
- record 본문은 `record struct Vector(double X, double Y) { ... 메서드 ... }` 처럼 중괄호로 확장 가능.
- `Math.Sqrt`.
- `readonly record struct` 이면 메서드도 자동으로 readonly 컨텍스트.

## 정답 확인
직접 풀어 본 후 [`answer/`](./answer/) 폴더의 정답과 비교해 보세요.
