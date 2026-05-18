namespace CodingNow.Lecture.Oop07;

internal class User
{
    // required: 객체 생성 시 반드시 값을 줘야 하는 프로퍼티 (C# 11+)
    public required string Email { get; init; }

    // 기본값이 있어 선택적으로 세팅 가능
    public string DisplayName { get; init; } = "익명";
}
