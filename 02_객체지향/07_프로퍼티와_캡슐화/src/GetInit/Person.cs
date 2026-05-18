namespace CodingNow.Lecture.Oop07;

internal class Person
{
    // init: 객체 초기화 시점이나 생성자 안에서만 설정 가능. 이후엔 읽기 전용.
    public string Name { get; init; } = "";
    public int Age { get; init; }
}
