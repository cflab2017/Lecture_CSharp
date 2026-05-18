# 과제 - 21. 패턴 매칭

## 문제 1 — 도형 넓이 계산기
- 프로젝트 폴더: `Homework01/`
- 핵심 개념: 타입 패턴, `switch` 식, `record`

### 요구사항
- 다음 도형을 `record` 로 정의한다.
  - `Circle(double Radius)`
  - `Rectangle(double Width, double Height)`
  - `Triangle(double Base, double Height)`
  - 공통 추상: `abstract record Shape;`
- `static double Area(Shape s)` 메서드를 **`switch` 식 + 타입 패턴** 으로 작성한다.
- 다음 배열에 대해 결과를 출력한다.
  ```csharp
  Shape[] shapes = [new Circle(2), new Rectangle(3, 4), new Triangle(5, 6)];
  ```

### 예상 출력
```
Circle(2) 넓이: 12.57
Rectangle(3, 4) 넓이: 12.00
Triangle(5, 6) 넓이: 15.00
```

### 힌트
- 원 넓이 = `Math.PI * r * r`.
- 삼각형 = `0.5 * base * height`.
- 출력은 `넓이.ToString("F2")` 또는 `$"{넓이:F2}"`.

## 문제 2 — 좌표 분류기
- 프로젝트 폴더: `Homework02/`
- 핵심 개념: 속성 패턴, `when` 절

### 요구사항
- `record Point(int X, int Y);` 를 정의한다.
- `static string Classify(Point p)` 를 `switch` 식 + 속성 패턴으로 작성한다.
- 분류 규칙:
  - `(0, 0)` → `"원점"`
  - `X == 0` 이고 `Y != 0` → `"Y축"`
  - `Y == 0` 이고 `X != 0` → `"X축"`
  - `X == Y` (그리고 0,0 아님) → `"y=x 직선"`
  - 그 외 양수만 → `"1사분면"`
  - 그 외 음수만 → `"3사분면"`
  - 그 외 → `"기타 사분면"`
- 다음 배열로 호출:
  ```csharp
  Point[] points = [new(0,0), new(0,5), new(7,0), new(3,3), new(2,8), new(-1,-5), new(-3,4)];
  ```

### 예상 출력
```
(0,0)  → 원점
(0,5)  → Y축
(7,0)  → X축
(3,3)  → y=x 직선
(2,8)  → 1사분면
(-1,-5) → 3사분면
(-3,4) → 기타 사분면
```

### 힌트
- 분기 순서가 중요합니다 — `(0,0)` 을 가장 먼저.
- `{ X: var x, Y: var y } when x == y` 처럼 변수를 끄집어내 비교.
- `{ X: > 0, Y: > 0 }` 으로 사분면 한 번에.

## 정답 확인
직접 풀어 본 후 [`answer/`](./answer/) 폴더의 정답과 비교해 보세요.
