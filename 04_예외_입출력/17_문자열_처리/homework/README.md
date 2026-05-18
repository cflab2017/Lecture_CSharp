# 과제 - 17. 문자열 처리

## 문제 1 — 단어 수와 가장 긴 단어
- 프로젝트 폴더: `Homework01/`
- 핵심 개념: `Split`, 반복, 비교

### 요구사항
- 사용자에게 한 줄 문장을 입력받는다 (`Console.ReadLine()`)
- 공백으로 단어를 나누되, 빈 항목은 제외한다
- 단어 수와 가장 긴 단어(공동 1위라면 첫 번째 것)를 출력한다

### 예상 출력
```
문장을 입력하세요: The quick brown fox jumps over the lazy dog
단어 수: 9
가장 긴 단어: quick
```

### 힌트
- `input.Split(' ', StringSplitOptions.RemoveEmptyEntries)` 로 빈 항목 제거
- 또는 `Split(new[]{' '}, StringSplitOptions.RemoveEmptyEntries)`
- 가장 긴 단어를 찾으려면 첫 단어로 초기화 후 더 긴 것이 나오면 교체

## 문제 2 — 이메일 모두 추출
- 프로젝트 폴더: `Homework02/`
- 핵심 개념: `Regex.Matches`, verbatim 문자열

### 요구사항
- 다음 본문에서 이메일을 모두 찾아 한 줄씩 출력한다:
  ```
  연락처: alice@example.com, bob.smith@test.io.
  문의는 admin+help@foo.co.kr 로 보내주세요.
  ```
- 정규식 패턴은 `[\w\.\+-]+@[\w\.-]+\.\w+` 정도면 충분 (학습용)

### 예상 출력
```
alice@example.com
bob.smith@test.io
admin+help@foo.co.kr
```

### 힌트
- `using System.Text.RegularExpressions;`
- `foreach (Match m in Regex.Matches(text, pattern))`
- 본문은 코드에 문자열 상수로 박아 두어도 됨

## 정답 확인
직접 풀어 본 후 [`answer/`](./answer/) 폴더의 정답과 비교해 보세요.
