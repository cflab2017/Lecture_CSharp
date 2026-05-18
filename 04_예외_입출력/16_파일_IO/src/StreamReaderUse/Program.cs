// StreamReader: 한 줄씩 스트리밍 (큰 파일 친화)
string path = Path.GetTempFileName();
File.WriteAllLines(path, ["사과", "바나나", "체리"]);

using var sr = new StreamReader(path);

int lineNo = 1;
string? line;
while ((line = sr.ReadLine()) is not null)
{
    Console.WriteLine($"{lineNo}: {line}");
    lineNo++;
}

// using 으로 sr 자동 Dispose
File.Delete(path);
