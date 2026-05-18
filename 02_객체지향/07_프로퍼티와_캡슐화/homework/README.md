# 과제 - 07. 프로퍼티와 캡슐화

## 문제 1 — `Temperature` (절대영도 검증)
- 프로젝트 폴더: `Homework01/`
- 핵심 개념: full property, backing field, `set` 안의 검증

### 요구사항
- `Temperature` 클래스에 `Celsius` 프로퍼티를 만든다.
- 값을 세팅할 때 `-273.15` 미만이면 `ArgumentException` 을 던진다.
- `Fahrenheit` 프로퍼티(읽기 전용)도 추가한다. 공식: `°F = °C × 9/5 + 32`.
- `Program.cs` 에서 정상 값과 비정상 값을 세팅해 출력한다.

### 예상 출력
```
0°C = 32°F
100°C = 212°F
에러: 절대영도(-273.15°C) 아래로 내려갈 수 없습니다.
```

### 힌트
- backing field 는 `private double celsius;` 로 둔다.
- `Fahrenheit` 는 `get => celsius * 9 / 5 + 32;` 처럼 계산식.

## 문제 2 — `Product` (가격 0 이상 검증)
- 프로젝트 폴더: `Homework02/`
- 핵심 개념: `required`, `init`, full property + 검증

### 요구사항
- `Product` 클래스를 만든다.
- `Name` 은 `required string ... { get; init; }` 로 만든다.
- `Price` 는 full property 로 만들고, 음수 값을 세팅하면 `ArgumentException`.
- 객체 초기화 구문 `new Product { Name = "...", Price = ... }` 으로 만든다.

### 예상 출력
```
사과 / 1500원
배 / 0원
에러: 가격은 0 이상이어야 합니다.
```

### 힌트
- `Price` 의 backing field 는 `private int price;`.
- 초기 가격을 0 으로 두려면 기본값을 따로 줄 필요 없음 (`int` 기본값 0).

## 정답 확인
직접 풀어 본 후 [`answer/`](./answer/) 폴더의 정답과 비교해 보세요.
