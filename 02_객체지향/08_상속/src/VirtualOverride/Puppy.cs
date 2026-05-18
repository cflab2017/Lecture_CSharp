namespace CodingNow.Lecture.Oop08;

internal class Puppy : Dog
{
    // sealed override: 더 이상 자식이 Speak 를 override 할 수 없게 막는다.
    public sealed override void Speak() => Console.WriteLine("깨갱!");
}
