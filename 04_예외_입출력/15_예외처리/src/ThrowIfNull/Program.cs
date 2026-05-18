// .NET 6+ 의 한 줄 가드 메서드
try
{
    PrintLength("hello");           // 정상
    PrintLength(null);              // ArgumentNullException
}
catch (ArgumentNullException ex)
{
    Console.WriteLine($"잡힘: {ex.Message}");
}

static void PrintLength(string? text)
{
    ArgumentNullException.ThrowIfNull(text);
    Console.WriteLine($"길이: {text.Length}");
}
