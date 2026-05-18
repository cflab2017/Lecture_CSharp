// 도메인 의미를 살린 사용자 정의 예외
try
{
    Register(-1);
}
catch (InvalidAgeException ex)
{
    Console.WriteLine($"등록 실패: {ex.Message}");
}

static void Register(int age)
{
    if (age < 0)
        throw new InvalidAgeException($"나이가 음수입니다: {age}");
    Console.WriteLine($"{age}세 등록 완료");
}

public class InvalidAgeException : Exception
{
    public InvalidAgeException(string message) : base(message) { }
}
