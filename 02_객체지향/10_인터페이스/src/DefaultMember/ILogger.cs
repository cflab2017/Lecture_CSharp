namespace CodingNow.Lecture.Oop10;

internal interface ILogger
{
    void Log(string msg);

    // 기본 구현(default interface member, C# 8+):
    // 구현 클래스가 별도로 정의하지 않으면 이 동작이 그대로 쓰인다.
    void Warn(string msg) => Log($"[WARN] {msg}");
}
