# 과제 - 12. List·Dictionary·HashSet

## 문제 1 — 이름 정렬 + 중복 제거
- 프로젝트 폴더: `Homework01/`
- 핵심 개념: `HashSet<T>`, `List<T>`, `Sort`

### 요구사항
- 다음 배열을 입력으로 사용한다.
  ```csharp
  string[] names = ["민호", "지수", "민호", "서연", "지수", "윤재"];
  ```
- `HashSet<string>` 으로 중복을 제거한다.
- 그 결과를 `List<string>` 으로 옮긴 뒤 알파벳(가나다) 순으로 정렬해 출력한다.

### 예상 출력
```
중복 제거 후 정렬:
민호
서연
윤재
지수
```

### 힌트
- `new List<string>(hashSet)` 로 변환 가능.
- 한국어는 `Sort()` 만 호출해도 가나다 순.

---

## 문제 2 — 단어 카운터 (Top 3)
- 프로젝트 폴더: `Homework02/`
- 핵심 개념: `Dictionary<string, int>`, 정렬

### 요구사항
- 문장 `"the quick brown fox jumps over the lazy dog the fox is quick"` 의 각 단어 등장 횟수를 센다.
- 빈도 내림차순으로 정렬해 **상위 3개** 만 출력한다.

### 예상 출력
```
the: 3
quick: 2
fox: 2
```

### 힌트
- `Split(' ')` 로 단어 분리.
- `dict.OrderByDescending(kv => kv.Value).Take(3)` 로 상위 3개 (LINQ 미리 맛보기) — 또는 `List<KeyValuePair<string,int>>` 로 변환 후 `Sort` 람다 사용.

## 정답 확인
직접 풀어 본 후 [`answer/`](./answer/) 폴더의 정답과 비교해 보세요.
