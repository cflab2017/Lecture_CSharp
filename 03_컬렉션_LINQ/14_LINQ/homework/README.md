# 과제 - 14. LINQ

## 문제 1 — 학생 성적 분석
- 프로젝트 폴더: `Homework01/`
- 핵심 개념: `Average`, `OrderByDescending`, `First`, `GroupBy`

### 요구사항
다음 학생 데이터를 사용한다.
```csharp
Student[] students =
[
    new("지수", "A반", 88),
    new("민호", "B반", 72),
    new("서연", "A반", 95),
    new("윤재", "B반", 65),
    new("하늘", "A반", 78),
    new("도윤", "B반", 90),
];
```
LINQ 만 사용해 다음을 모두 출력한다.
1. 전체 평균 점수 (소수점 첫째 자리)
2. 최고 점수 학생의 이름과 점수
3. **반(`Class`) 별 평균 점수**

### 예상 출력
```
전체 평균: 81.3
최고 점수: 서연 (95점)
=== 반별 평균 ===
A반: 87.0
B반: 75.7
```

### 힌트
- `students.Average(s => s.Score)`
- `students.OrderByDescending(s => s.Score).First()`
- `students.GroupBy(s => s.Class)` → 각 그룹에서 `Average` 호출

---

## 문제 2 — 주문 매출 통계
- 프로젝트 폴더: `Homework02/`
- 핵심 개념: `Sum`, `GroupBy`, `Select`, 익명 형식 또는 튜플

### 요구사항
다음 주문 데이터를 사용한다.
```csharp
Order[] orders =
[
    new("사과", 3, 1500),
    new("바나나", 5, 800),
    new("사과", 2, 1500),
    new("귤", 10, 500),
    new("바나나", 3, 800),
];
```
LINQ 로 다음을 출력한다.
1. 전체 매출 합계 (`수량 * 단가` 의 합)
2. **상품별 매출 합계** — 매출 내림차순 정렬

### 예상 출력
```
전체 매출: 18,900원
=== 상품별 매출 ===
사과: 7,500원
바나나: 6,400원
귤: 5,000원
```

### 힌트
- `orders.Sum(o => o.Quantity * o.UnitPrice)` 로 전체 매출.
- `GroupBy(o => o.Product)` → `Select(g => new { Name = g.Key, Total = g.Sum(o => o.Quantity * o.UnitPrice) })` → `OrderByDescending`.
- 천 단위 구분: `$"{value:N0}원"`.

## 정답 확인
직접 풀어 본 후 [`answer/`](./answer/) 폴더의 정답과 비교해 보세요.
