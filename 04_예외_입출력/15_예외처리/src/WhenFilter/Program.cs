// when 절로 특정 조건의 예외만 골라 잡는다
try
{
    throw new InvalidOperationException("NotFound: 사용자 없음");
}
catch (InvalidOperationException ex) when (ex.Message.Contains("NotFound"))
{
    Console.WriteLine("404 와 비슷한 상황으로 처리");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"기타 오류: {ex.Message}");
}
