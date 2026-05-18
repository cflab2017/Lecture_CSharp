namespace CodingNow.Lecture.Oop10;

internal class ConsoleLogger : ILogger
{
    public void Log(string msg) => Console.WriteLine(msg);

    // Warn 은 구현하지 않음 → 인터페이스의 기본 구현이 사용된다.
}
