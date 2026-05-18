# 과제 - 16. 파일 I/O

## 문제 1 — 번호 매겨 출력하기
- 프로젝트 폴더: `Homework01/`
- 핵심 개념: `Path.GetTempFileName`, `File.WriteAllLines`, `File.ReadAllLines`

### 요구사항
- 임시 파일을 만든다 (`Path.GetTempFileName()`)
- `["서울", "부산", "대구", "광주"]` 를 임시 파일에 줄 단위로 쓴다
- 그 파일을 읽어 `[1] 서울` 처럼 번호와 함께 한 줄씩 출력한다
- 종료 전 파일을 삭제한다

### 예상 출력
```
[1] 서울
[2] 부산
[3] 대구
[4] 광주
```

### 힌트
- `File.WriteAllLines(path, lines)` 와 `File.ReadAllLines(path)` 를 그대로 사용
- 인덱스는 `for (int i = 0; i < lines.Length; i++)` 또는 `Enumerate` 패턴

## 문제 2 — CSV 컬럼 합계
- 프로젝트 폴더: `Homework02/`
- 핵심 개념: 줄/필드 파싱, 숫자 변환, 누적 합

### 요구사항
- 임시 파일에 다음과 같은 CSV 를 쓴다 (헤더 + 3 행):
  ```
  name,score
  Alice,80
  Bob,95
  Charlie,72
  ```
- 파일을 한 줄씩 읽으며 헤더는 건너뛰고, 두 번째 컬럼(`score`)을 더한다
- 합계와 평균(정수 나누기로 OK)을 출력한다

### 예상 출력
```
합계: 247
평균: 82
```

### 힌트
- `string.Split(',')` 로 컬럼을 자른다
- `int.Parse(parts[1])` 로 숫자 변환
- 헤더는 첫 줄(`i == 0`)을 그냥 `continue;` 로 건너뛴다

## 정답 확인
직접 풀어 본 후 [`answer/`](./answer/) 폴더의 정답과 비교해 보세요.
