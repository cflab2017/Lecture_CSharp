# 16. 파일 I/O

설정 파일을 읽고, 로그를 쓰고, CSV 를 파싱하는 일은 거의 모든 앱이 합니다. C# 은 `System.IO` 네임스페이스로 파일을 다루는 풍부한 API 를 제공합니다. 짧은 작업은 `File` 정적 메서드로, 큰 파일은 `Stream` 계열로 다룹니다.

## 학습 목표
- `File.ReadAllText`/`WriteAllText`/`ReadAllLines`/`WriteAllLines` 사용법을 안다
- `StreamReader` 로 한 줄씩 읽는 패턴을 익힌다
- `Path` 클래스로 OS 독립적인 경로 조작을 한다
- `Path.GetTempFileName()` 으로 안전한 임시 파일을 만든다
- `async` 비동기 파일 I/O 의 기본을 맛본다

## 핵심 개념

### 1) `File` 정적 메서드 (소규모 파일용)
파일 전체를 한 번에 읽고 쓰는 가장 간단한 방법입니다.

```csharp
File.WriteAllText("a.txt", "안녕");           // 전체 문자열 쓰기
string text = File.ReadAllText("a.txt");      // 전체 읽기

File.WriteAllLines("b.txt", new[] {"a","b"}); // 한 줄씩 쓰기
string[] lines = File.ReadAllLines("b.txt");  // 한 줄씩 읽기 (배열)
```

수십 MB 이상이면 메모리를 다 먹으니 스트림 방식이 낫습니다.

### 2) `StreamReader` (큰 파일용)
한 줄씩 읽어 처리하면 메모리를 적게 씁니다. `IDisposable` 이므로 `using` 으로 감쌉니다.

```csharp
using var sr = new StreamReader(path);
string? line;
while ((line = sr.ReadLine()) is not null)
{
    // line 처리
}
```

### 3) `Path` — 경로 조작
직접 `"/"` 나 `"\\"` 로 잇지 말고 `Path.Combine` 을 씁니다. OS 마다 구분자가 다르기 때문입니다.

```csharp
string p = Path.Combine("logs", "2025", "app.log");
string name = Path.GetFileName(p);       // "app.log"
string ext  = Path.GetExtension(p);      // ".log"
string tmp  = Path.GetTempFileName();    // OS 임시 폴더에 빈 파일 생성
```

`Path.GetTempFileName()` 은 충돌 없는 파일을 자동으로 만들어 줍니다. 학습용 예제는 이걸 쓰는 게 안전합니다.

### 4) 예외 처리와 함께
파일 작업은 거의 항상 실패 가능성이 있습니다 (없는 파일, 권한 부족 등).

```csharp
try
{
    string s = File.ReadAllText(path);
}
catch (FileNotFoundException)   { /* ... */ }
catch (UnauthorizedAccessException) { /* ... */ }
catch (IOException) { /* 그 외 I/O 오류 */ }
```

### 5) 비동기 I/O (`async`/`await`)
디스크는 느립니다. UI 나 서버에서는 비동기로 처리해 다른 작업을 막지 않습니다. 19편에서 자세히 다루지만, 사용법은 단순합니다 — 메서드 이름 뒤에 `Async` 가 붙고 `await` 으로 받습니다.

```csharp
string s = await File.ReadAllTextAsync(path);
await File.WriteAllTextAsync(path, s);
```

> 본격적인 `async`/`await` 학습은 **19편**에서 다룹니다. 여기선 형태만 익혀 두세요.

## 예제로 보기

### 예제 1 — `WriteRead` : 전체 텍스트 한 번에 쓰고 읽기
```csharp
string path = Path.GetTempFileName();           // 임시 파일

File.WriteAllText(path, "안녕, 파일!\n두 번째 줄");

string text = File.ReadAllText(path);
Console.WriteLine("== 읽은 내용 ==");
Console.WriteLine(text);

File.Delete(path);                              // 정리
```
**실행 결과**
```
== 읽은 내용 ==
안녕, 파일!
두 번째 줄
```
**메모:** `Path.GetTempFileName()` 은 OS 임시 폴더에 충돌 없는 파일을 만들어 줍니다.

### 예제 2 — `WriteReadLines` : 줄 배열로 다루기
```csharp
string path = Path.GetTempFileName();

string[] names = ["Alice", "Bob", "Charlie"];
File.WriteAllLines(path, names);

string[] loaded = File.ReadAllLines(path);
foreach (var n in loaded)
    Console.WriteLine($"- {n}");

File.Delete(path);
```
**실행 결과**
```
- Alice
- Bob
- Charlie
```
**메모:** 줄 단위 데이터는 `WriteAllLines`/`ReadAllLines` 가 가장 깔끔합니다.

### 예제 3 — `StreamReaderUse` : 한 줄씩 스트리밍
```csharp
string path = Path.GetTempFileName();
File.WriteAllLines(path, ["사과", "바나나", "체리"]);

// using 블록으로 sr 의 수명을 명시적으로 한정.
using (var sr = new StreamReader(path))
{
    int lineNo = 1;
    string? line;
    while ((line = sr.ReadLine()) is not null)
    {
        Console.WriteLine($"{lineNo}: {line}");
        lineNo++;
    }
}

File.Delete(path);
```
**실행 결과**
```
1: 사과
2: 바나나
3: 체리
```
**메모:** 큰 파일에서도 메모리 사용이 일정합니다. `using` 블록을 빠져나오면 핸들이 즉시 닫혀, 바로 이어지는 `File.Delete` 가 Windows 의 파일 잠금에 걸리지 않습니다 (`using var` 였다면 메서드 범위 끝까지 핸들이 유지되어 충돌).

### 예제 4 — `PathHelpers` : 경로를 쪼개고 합치기
```csharp
string combined = Path.Combine("logs", "2025", "app.log");
Console.WriteLine($"Combine    : {combined}");
Console.WriteLine($"FileName   : {Path.GetFileName(combined)}");
Console.WriteLine($"Extension  : {Path.GetExtension(combined)}");
Console.WriteLine($"NoExt      : {Path.GetFileNameWithoutExtension(combined)}");

string temp = Path.GetTempFileName();
Console.WriteLine($"Temp       : {temp}");
File.Delete(temp);
```
**실행 결과**
```
Combine    : logs/2025/app.log
FileName   : app.log
Extension  : .log
NoExt      : app
Temp       : /tmp/tmpXXXXXX.tmp
```
**메모:** Windows 에서는 `\` 가 들어갑니다. **수동 연결 금지, `Path.Combine` 사용**.

### 예제 5 — `AsyncFile` : 비동기 읽고 쓰기
```csharp
// top-level await: C# 9 / .NET 5+ 부터 가능
string path = Path.GetTempFileName();

await File.WriteAllTextAsync(path, "비동기로 저장!");
string text = await File.ReadAllTextAsync(path);

Console.WriteLine($"읽음: {text}");

File.Delete(path);
```
**실행 결과**
```
읽음: 비동기로 저장!
```
**메모:** 동작은 동기 버전과 같지만, 디스크 대기 동안 다른 일을 할 수 있습니다. 19편에서 자세히 다룹니다.

## 자주 하는 실수
1. 경로를 `"a" + "/" + "b"` 로 직접 잇기 — OS 종속. `Path.Combine` 을 쓴다.
2. 큰 파일을 `ReadAllText` 로 읽기 — 메모리 폭발. 스트림으로 한 줄씩.
3. `using` 없이 `StreamReader` 사용 — 파일 핸들 누수.
4. 작업 디렉토리에 임시 파일 두기 — 환경을 더럽힘. **`Path.GetTempFileName()`** 사용.
5. 예외 처리 누락 — 없는 파일·권한·디스크 가득참 등을 대비한다.

## 정리
- 작은 텍스트: `File.ReadAllText`/`WriteAllText`
- 줄 단위: `File.ReadAllLines`/`WriteAllLines`
- 큰 파일: `StreamReader` + `using`
- 경로는 `Path.Combine` 으로, 임시 파일은 `Path.GetTempFileName()` 으로
- 디스크가 느린 곳에선 `*Async` 버전 + `await` (19편 예고)

## 직접 해 보기
```bash
cd src/WriteRead && dotnet run
cd ../WriteReadLines && dotnet run
cd ../StreamReaderUse && dotnet run
cd ../PathHelpers && dotnet run
cd ../AsyncFile && dotnet run
```

## 다음 단원
[17_문자열_처리](../17_문자열_처리/) — 읽어들인 텍스트를 가공하는 도구들을 익힙니다.
