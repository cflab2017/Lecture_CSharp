# 과제 - 13. 제네릭

## 문제 1 — 제네릭 스택 직접 구현
- 프로젝트 폴더: `Homework01/`
- 핵심 개념: 제네릭 클래스, 내부에 `List<T>` 사용

### 요구사항
- `MyStack<T>` 클래스를 정의한다. 내부에 `List<T>` 를 두고 다음을 제공:
  - `void Push(T item)`
  - `T Pop()` — 비어 있으면 `InvalidOperationException` 던지기
  - `T Peek()` — 비어 있으면 `InvalidOperationException`
  - `int Count` (프로퍼티)
- `MyStack<int>` 와 `MyStack<string>` 두 가지를 만들어 동작을 확인한다.

### 예상 출력
```
=== int 스택 ===
Push: 1, 2, 3
Peek: 3
Pop: 3
Pop: 2
남은 개수: 1
=== string 스택 ===
Push: A, B
Pop: B
Pop: A
```

### 힌트
- `List<T>` 의 마지막 원소를 사용/제거하면 스택이 된다.
- `list[^1]` 로 마지막 원소 접근.

---

## 문제 2 — 제네릭 `Swap<T>` 메서드
- 프로젝트 폴더: `Homework02/`
- 핵심 개념: 제네릭 메서드, `ref` 매개변수

### 요구사항
- `static void Swap<T>(ref T a, ref T b)` 를 작성한다.
- `int` 두 변수와 `string` 두 변수에 대해 호출해 결과를 출력한다.

### 예상 출력
```
교환 전: a=10, b=20
교환 후: a=20, b=10
교환 전: s1=hello, s2=world
교환 후: s1=world, s2=hello
```

### 힌트
- `ref` 매개변수는 호출 측도 `ref` 키워드를 붙여 전달.
- 한 줄 임시 변수면 충분: `T tmp = a; a = b; b = tmp;`

## 정답 확인
직접 풀어 본 후 [`answer/`](./answer/) 폴더의 정답과 비교해 보세요.
