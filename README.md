# C# 입문 (.NET 8 LTS) — 22편

> 🌐 **전체 22편 강의 보기** — [https://coding-now.com/blog/csharp](https://coding-now.com/blog/csharp)
>
> 한국어 무료 코딩 강의 264편 + 개발자 도구 모음 · Coding Now

C# 과 .NET 의 핵심 기능을 짧고 실용적인 22단원으로 정리한 강의 트랙입니다.
프로그래밍 입문자도 따라올 수 있도록 구성했고, 다른 OOP 언어(Java/C++/Python) 경험자라면
빠르게 C# 특유의 모던 기능(records, pattern matching, nullable reference type, async/await)까지
훑어볼 수 있습니다.

> 이 트랙은 **언어 자체와 표준 라이브러리**에 집중합니다.
> ASP.NET Core, Unity, .NET MAUI, Entity Framework Core 등 프레임워크는 별도 후속 트랙입니다.

## 학습 목표

- C# 문법과 타입 시스템(value/reference, nullable) 이해
- 객체지향 4대 원칙(캡슐화·상속·다형성·추상화)을 C# 로 자유롭게 구현
- 컬렉션·제네릭·LINQ 로 데이터 변환 파이프라인 작성
- 예외 처리·파일 I/O·문자열 처리 같은 실전 기능 사용
- 델리게이트·람다·`async/await`·NRT·pattern matching·record 등 모던 C# 자유롭게 활용

## 개발 환경

| 항목 | 권장 |
|---|---|
| 런타임/SDK | **.NET 8 SDK LTS** |
| IDE | Visual Studio 2022 Community / VS Code + **C# Dev Kit** / JetBrains Rider Community |
| 빌드 도구 | `dotnet` CLI (모든 예제가 CLI 만으로 동작) |
| OS | Windows / macOS / Linux 모두 동일하게 동작 |

### OS 별 설치

```bash
# Windows (PowerShell, 관리자 권한)
winget install Microsoft.DotNet.SDK.8

# macOS (Homebrew)
brew install --cask dotnet-sdk

# Ubuntu / Debian
sudo apt update
sudo apt install -y dotnet-sdk-8.0

# Fedora
sudo dnf install -y dotnet-sdk-8.0
```

설치 후 확인:

```bash
dotnet --version
# 8.0.x 가 출력되면 OK
```

### 첫 실행 (Hello, World)

```bash
dotnet new console -n HelloApp
cd HelloApp
dotnet run
# Hello, World!
```

## 디렉토리 구조

```
Lecture_CSharp/
├── 01_기초/
│   ├── 01_CSharp_시작하기/
│   ├── 02_변수와_타입/
│   ├── 03_연산자와_표현식/
│   ├── 04_제어문/
│   └── 05_메서드/
├── 02_객체지향/
│   ├── 06_클래스와_객체/
│   ├── 07_프로퍼티와_캡슐화/
│   ├── 08_상속/
│   ├── 09_다형성/
│   └── 10_인터페이스/
├── 03_컬렉션_LINQ/
│   ├── 11_배열/
│   ├── 12_List_Dictionary_HashSet/
│   ├── 13_제네릭/
│   └── 14_LINQ/
├── 04_예외_입출력/
│   ├── 15_예외처리/
│   ├── 16_파일_IO/
│   └── 17_문자열_처리/
└── 05_모던_CSharp/
    ├── 18_델리게이트와_람다/
    ├── 19_async_await/
    ├── 20_Nullable_참조_타입/
    ├── 21_패턴_매칭/
    └── 22_Record와_init/
```

각 단원 폴더는 다음 구조를 따릅니다.

```
NN_단원명/
├── README.md            본문 (학습 목표 · 핵심 개념 · 예제 · 자주 하는 실수 · 정리)
├── src/                 강의 본문에 등장하는 예제 (각자 dotnet 프로젝트)
│   └── ExampleApp/
│       ├── ExampleApp.csproj
│       └── Program.cs
└── homework/
    ├── README.md        과제 요구사항·예상 출력·힌트
    └── answer/          정답 (HomeworkNN 프로젝트들)
```

## 22단원 인덱스

### Part 1. 기초

| #  | 폴더 | 주제 |
|----|---|---|
| 01 | [01_CSharp_시작하기](01_기초/01_CSharp_시작하기/) | .NET 8 SDK · IDE · `dotnet new console` · `dotnet run` |
| 02 | [02_변수와_타입](01_기초/02_변수와_타입/) | value vs reference · 기본 타입 8종 · `var`/`const` · boxing |
| 03 | [03_연산자와_표현식](01_기초/03_연산자와_표현식/) | 산술·비교·논리 · `?.` · `??` / `??=` · `is`/`as` · `?:` |
| 04 | [04_제어문](01_기초/04_제어문/) | `if` · `switch` expression · `for` / `while` / `foreach` · `break`/`continue` |
| 05 | [05_메서드](01_기초/05_메서드/) | 선언 · 오버로딩 · `out` / `ref` / `in` · optional / named arg |

### Part 2. 객체지향

| #  | 폴더 | 주제 |
|----|---|---|
| 06 | [06_클래스와_객체](02_객체지향/06_클래스와_객체/) | `class` · 필드 · 생성자 · `this` · 접근 제한자 |
| 07 | [07_프로퍼티와_캡슐화](02_객체지향/07_프로퍼티와_캡슐화/) | auto-property · `init` · `required` · full property |
| 08 | [08_상속](02_객체지향/08_상속/) | `: base` · `base()` · `override`/`virtual`/`sealed` · `new` |
| 09 | [09_다형성](02_객체지향/09_다형성/) | 업/다운캐스팅 · `is` 패턴 · `abstract` 클래스 |
| 10 | [10_인터페이스](02_객체지향/10_인터페이스/) | `interface` · default interface member · explicit 구현 |

### Part 3. 컬렉션 · LINQ

| #  | 폴더 | 주제 |
|----|---|---|
| 11 | [11_배열](03_컬렉션_LINQ/11_배열/) | 1차원 · 2차원 · `Array.Sort`/`IndexOf` · `Span<T>`/`Memory<T>` 입문 |
| 12 | [12_List_Dictionary_HashSet](03_컬렉션_LINQ/12_List_Dictionary_HashSet/) | `List<T>` · `Dictionary<TK,TV>` · `HashSet<T>` · `Queue/Stack` |
| 13 | [13_제네릭](03_컬렉션_LINQ/13_제네릭/) | 제네릭 클래스/메서드 · 제약(`where`) · 공/반변성 개요 |
| 14 | [14_LINQ](03_컬렉션_LINQ/14_LINQ/) | `Where`/`Select`/`OrderBy`/`GroupBy`/`Aggregate`/`Join` · 지연 실행 |

### Part 4. 예외 · 입출력

| #  | 폴더 | 주제 |
|----|---|---|
| 15 | [15_예외처리](04_예외_입출력/15_예외처리/) | `try`/`catch`/`finally` · `throw` · `when` · `using` · custom exception |
| 16 | [16_파일_IO](04_예외_입출력/16_파일_IO/) | `File.*` · `StreamReader` · `Path` · async file I/O |
| 17 | [17_문자열_처리](04_예외_입출력/17_문자열_처리/) | `string` 메서드 · `StringBuilder` · 보간 문자열 · `Regex` · culture |

### Part 5. 모던 C#

| #  | 폴더 | 주제 |
|----|---|---|
| 18 | [18_델리게이트와_람다](05_모던_CSharp/18_델리게이트와_람다/) | `delegate` · `Action`/`Func`/`Predicate` · 람다 · `event` 개요 |
| 19 | [19_async_await](05_모던_CSharp/19_async_await/) | `Task`/`Task<T>` · `async`/`await` · `WhenAll`/`WhenAny` · cancellation |
| 20 | [20_Nullable_참조_타입](05_모던_CSharp/20_Nullable_참조_타입/) | `string?` · `!`/`!.`/`?.` · `#nullable enable` · null-aware 패턴 |
| 21 | [21_패턴_매칭](05_모던_CSharp/21_패턴_매칭/) | `is` · `switch` 패턴(constant/type/property/tuple/positional) · `when` |
| 22 | [22_Record와_init](05_모던_CSharp/22_Record와_init/) | `record` · `record struct` · `with` 식 · `init` setter · value equality |

## 학습 순서 & 진도 가이드

- **1주 2~3편** 권장 — 약 **8~11주** 완주
- 단원 순서대로 따라가되, Part 2 이후는 관심 있는 단원만 골라 봐도 충분합니다.
- 각 단원은 다음 순서로 학습하세요.
  1. README 본문 읽기 (15~25분)
  2. `src/` 의 예제를 직접 타이핑 + `dotnet run`
  3. `homework/README.md` 의 과제 풀기
  4. `homework/answer/` 와 비교 (정답은 정답일 뿐, 다른 풀이도 OK)

## 미니 프로젝트 워크북

각 Part 가 끝나면 작은 실습으로 통합해 보세요.

| Part | 추천 미니 프로젝트 |
|---|---|
| 1. 기초 | **숫자 맞히기 게임** — `Random` · `if`/`while` · 메서드 분리 |
| 2. 객체지향 | **간단 주소록(Console)** — `Contact` 클래스 · 상속(`Friend`/`Coworker`) · 인터페이스 `IPrintable` |
| 3. 컬렉션 · LINQ | **도서 관리 + LINQ 쿼리** — `Book[]` 또는 `List<Book>` → 장르별 그룹/평점 평균 |
| 4. 예외 · 입출력 | **CSV 가계부** — 파일 읽기 → 항목 파싱(예외 처리) → `string` 정리 |
| 5. 모던 C# | **비동기 다운로더 + record 결과** — `HttpClient` · `WhenAll` · 결과를 `record` 로 반환 |

## 컨벤션

- **.NET 8 LTS** 기준 — top-level statements, primary constructors, collection expressions, list/array patterns 자유롭게 사용
- namespace: `CodingNow.Lecture.PartNN` (예: 02편 → `CodingNow.Lecture.Basics02`)
- 클래스/메서드 `PascalCase`, 지역변수/매개변수 `camelCase`, 상수 `PascalCase`
- 들여쓰기 4 스페이스 · Allman 중괄호
- 파일 인코딩 UTF-8 (BOM 없음), 줄바꿈 LF
- `#nullable enable` 기본 — null 안전성을 컴파일러로 강제
- public API 에는 1~2줄 XML doc 주석

## 라이선스

MIT License. 자유롭게 학습·복제·재배포해도 좋습니다. 출처(이 저장소) 표기는 환영합니다.
