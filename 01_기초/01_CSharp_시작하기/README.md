# 01. C# 시작하기

C#은 마이크로소프트가 만든 정적 타입 언어로, 게임(Unity), 웹(ASP.NET), 데스크톱, 모바일까지 두루 쓰입니다. 이 단원에서는 .NET 8 SDK를 설치하고, 콘솔 프로젝트를 만들어 직접 실행해 봅니다.

## 학습 목표
- .NET 8 LTS의 의미와 SDK/Runtime의 차이를 안다
- IDE(Visual Studio / VS Code / Rider) 중 하나를 골라 설치한다
- `dotnet new console`로 첫 프로젝트를 만든다
- `dotnet run`으로 실행하고 결과를 확인한다
- `Console.WriteLine` / `Console.ReadLine` 으로 간단한 입출력을 한다

## 핵심 개념

### 1) .NET 이란?
**.NET**은 C#, F#, VB.NET 같은 언어를 실행해 주는 런타임 + 표준 라이브러리 + SDK의 묶음입니다. 한 번 작성한 코드를 Windows/macOS/Linux에서 모두 돌릴 수 있습니다.

- **.NET 8 LTS**: 2023년 11월 공개된 장기 지원(Long Term Support) 버전. 2026년 11월까지 공식 지원.
- **SDK**: 개발에 필요한 컴파일러·도구 모음 (`dotnet` CLI 포함)
- **Runtime**: 만든 프로그램을 실행만 할 때 필요한 최소 구성요소

### 2) SDK 설치 확인
설치 후 터미널에서 아래 명령어로 확인합니다.

```bash
dotnet --version
# 출력 예: 8.0.xxx
```

`8.0` 으로 시작하면 OK. 만약 명령어가 없다고 나오면 [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download) 에서 **.NET 8 SDK** 를 받아 설치하세요.

### 3) IDE 선택
- **Visual Studio 2022 (Windows)** — 무거우나 통합 환경 최강
- **VS Code + C# Dev Kit (크로스 플랫폼)** — 가볍고 빠름, 추천
- **JetBrains Rider** — 유료지만 강력한 리팩토링

이 강의는 IDE에 구애받지 않습니다. 어떤 것이든 `dotnet` CLI 로 동작은 같습니다.

### 4) 첫 프로젝트 만들기
```bash
dotnet new console -o HelloApp
cd HelloApp
dotnet run
```
- `dotnet new console` : 콘솔 앱 템플릿 생성
- `-o HelloApp` : `HelloApp` 폴더에 생성
- `dotnet run` : 빌드(필요 시 `restore` 자동) → 실행

처음 실행 시 `Hello, World!` 가 출력됩니다.

### 5) Top-level statements
.NET 6 이후 `Main` 메서드를 생략하고 곧바로 코드를 쓸 수 있습니다.

```csharp
// Program.cs
Console.WriteLine("안녕하세요!");
```

`Console.WriteLine` 은 줄바꿈 포함 출력, `Console.Write` 는 줄바꿈 없이 출력합니다.

## 예제로 보기

### 예제 1 — `HelloApp/Program.cs` : 가장 기본
```csharp
// 첫 C# 프로그램
Console.WriteLine("Hello, C# !");
```
**실행 결과**
```
Hello, C# !
```
**메모:** 세미콜론(`;`)을 빼먹지 마세요. C#은 줄바꿈이 아니라 `;`로 문장을 구분합니다.

### 예제 2 — `Echo/Program.cs` : 입력 받아 인사
```csharp
Console.Write("이름을 입력하세요: ");
string? name = Console.ReadLine();
Console.WriteLine($"안녕하세요, {name}님!");
```
**실행 결과**
```
이름을 입력하세요: 지수
안녕하세요, 지수님!
```
**메모:** `Console.ReadLine()` 은 입력이 없을 경우 `null` 을 반환할 수 있어 타입이 `string?` 입니다. (자세한 내용은 2단원에서)

### 예제 3 — `MultiPrint/Program.cs` : 여러 줄 + 문자열 보간
```csharp
string product = "사과";
int count = 3;
int price = 1500;

Console.WriteLine("=== 구매 내역 ===");
Console.WriteLine($"상품: {product}");
Console.WriteLine($"수량: {count}개");
Console.WriteLine($"합계: {count * price}원");
```
**실행 결과**
```
=== 구매 내역 ===
상품: 사과
수량: 3개
합계: 4500원
```
**메모:** `$"..."` 안의 `{ }` 는 표현식을 그대로 넣을 수 있습니다. 이를 **문자열 보간(string interpolation)** 이라 부릅니다.

## 자주 하는 실수
1. `Console.Writeline` 처럼 대소문자를 틀린다 — C#은 **대소문자 구분**.
2. 문장 끝 `;` 누락 — 컴파일 에러.
3. `$` 없이 `"{name}"` 만 써서 `{name}` 그대로 출력됨.
4. `dotnet run` 을 프로젝트 폴더 밖에서 실행해 "프로젝트를 찾을 수 없다" 에러가 남.
5. `.NET Framework 4.x` 와 `.NET 8` 을 혼동 — 이 강의는 **.NET 8** (Core 계열) 기준.

## 정리
- .NET 8 SDK 설치 후 `dotnet --version` 으로 확인한다
- `dotnet new console -o 이름` → `dotnet run` 이 기본 흐름이다
- `Console.WriteLine` 으로 출력, `Console.ReadLine` 으로 입력 받는다
- `$"..."` 문자열 보간으로 변수를 깔끔하게 끼워 넣는다

## 직접 해 보기
```bash
cd src/HelloApp
dotnet run

cd ../Echo
dotnet run

cd ../MultiPrint
dotnet run
```

## 다음 단원
[02_변수와_타입](../02_변수와_타입/) — 값을 담는 그릇, 변수와 타입을 익힙니다.
