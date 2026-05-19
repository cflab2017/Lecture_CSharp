// StreamReader: 한 줄씩 스트리밍 (큰 파일 친화)
string path = Path.GetTempFileName();
File.WriteAllLines(path, ["사과", "바나나", "체리"]);

// using 블록으로 sr 의 수명을 명시적으로 한정 → 블록을 빠져나오는 순간 Dispose.
// `using var` 로 쓰면 파일 메서드 범위 끝까지 핸들이 유지돼서
// Windows 에서는 아래 File.Delete 가 파일 잠금에 걸린다.
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

// sr 이 Dispose 된 뒤라 파일을 안전하게 지울 수 있다.
File.Delete(path);
