# 과제 - 08. 상속

## 문제 1 — `Shape` → `Circle`/`Rectangle`
- 프로젝트 폴더: `Homework01/`
- 핵심 개념: 상속, `virtual` / `override`

### 요구사항
- 부모 `Shape` 클래스에 `virtual double Area()` 메서드를 만든다(기본 반환 0).
- 자식 `Circle` 은 반지름을 받아 `Area()` 를 `π × r²` 로 override.
- 자식 `Rectangle` 은 가로/세로를 받아 `Area()` 를 `w × h` 로 override.
- `Program.cs` 에서 `Circle`, `Rectangle` 객체를 만들고 `Area()` 출력.

### 예상 출력
```
원 넓이: 78.54
사각형 넓이: 12
```

### 힌트
- `Math.PI` 를 그대로 쓰면 된다.
- 반올림은 `Math.Round(value, 2)` 또는 형식 지정자 `:F2` 사용.

## 문제 2 — `Employee` → `Manager`
- 프로젝트 폴더: `Homework02/`
- 핵심 개념: 자식 생성자에서 `: base(...)` 호출

### 요구사항
- `Employee` 클래스: 필드 `Name`(string), `Salary`(int). 생성자에서 둘 다 받음. `PrintInfo()` 로 정보 출력.
- `Manager : Employee`: 추가 필드 `Bonus`(int). 생성자에서 부모의 두 값 + bonus 를 받아 `: base(name, salary)` 로 부모 생성자 호출.
- `Manager.PrintInfo()` 는 부모의 PrintInfo 를 호출한 뒤 보너스도 출력.
- `Program.cs` 에서 두 객체를 만들고 정보 출력.

### 예상 출력
```
이름: 영희, 급여: 3000000
이름: 철수, 급여: 5000000
보너스: 1000000
```

### 힌트
- 자식 `PrintInfo` 안에서 `base.PrintInfo()` 호출.
- 자식 메서드를 부모와 같은 이름으로 쓸 땐 `virtual`/`override` 또는 `new` 중 선택.

## 정답 확인
직접 풀어 본 후 [`answer/`](./answer/) 폴더의 정답과 비교해 보세요.
