// using var: 스코프 종료 시 자동으로 Dispose() 호출
// 파일 대신 메모리 reader 로 동작을 시연한다 (16편에서 진짜 파일을 다룬다)
using var sr = new System.IO.StringReader("첫 줄\n둘째 줄\n셋째 줄");

string? line;
while ((line = sr.ReadLine()) is not null)
{
    Console.WriteLine($"> {line}");
}

// 여기서 sr.Dispose() 가 자동 호출된다
