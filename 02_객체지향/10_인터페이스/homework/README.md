# 과제 - 10. 인터페이스

## 문제 1 — `IComparable<T>` 구현으로 정렬
- 프로젝트 폴더: `Homework01/`
- 핵심 개념: 표준 라이브러리 인터페이스 구현, 정렬 활용

### 요구사항
- `Student` 클래스를 만든다. 필드는 `Name`(string), `Score`(int).
- `IComparable<Student>` 를 구현해 `CompareTo` 가 **점수 내림차순** 으로 비교하도록 한다.
- `List<Student>` 에 학생 3~4명 담고 `Sort()` 호출 후 출력.

### 예상 출력
```
영희: 95
철수: 80
민수: 70
```

### 힌트
- `int.CompareTo(other)` 는 작으면 음수, 같으면 0, 크면 양수를 반환.
- 내림차순으로 정렬하려면 `other.Score.CompareTo(this.Score)` 처럼 비교 순서를 뒤집는다.
- `using System;` 는 `ImplicitUsings` 로 자동 포함되므로 별도 추가 불필요.

## 문제 2 — `IDrawable` + `IResizable` 두 인터페이스 구현
- 프로젝트 폴더: `Homework02/`
- 핵심 개념: 여러 인터페이스 구현, 인터페이스 변수로 호출

### 요구사항
- `IDrawable` 인터페이스: `void Draw()`.
- `IResizable` 인터페이스: `void Resize(int width, int height)`.
- `Rectangle` 클래스가 두 인터페이스를 모두 구현. 가로/세로 필드를 갖는다.
- `Draw()` 는 현재 크기 정보를 출력, `Resize` 는 크기를 변경한 뒤 메시지 출력.
- `Program.cs` 에서 `Rectangle` 객체를 만들고 두 메서드 호출.

### 예상 출력
```
사각형 그리기 (10x5)
사각형 크기 변경 → (20x10)
사각형 그리기 (20x10)
```

### 힌트
- `class Rectangle : IDrawable, IResizable` 처럼 콤마로 나열.
- 두 인터페이스 메서드를 모두 `public` 으로 구현해야 한다.

## 정답 확인
직접 풀어 본 후 [`answer/`](./answer/) 폴더의 정답과 비교해 보세요.
