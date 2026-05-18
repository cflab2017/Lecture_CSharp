// finally 는 예외 여부와 무관하게 항상 실행된다
try
{
    Console.WriteLine("try 진입");
    throw new InvalidOperationException("일부러 던짐");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"catch: {ex.Message}");
}
finally
{
    Console.WriteLine("finally: 항상 실행됨");
}
