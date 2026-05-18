namespace CodingNow.Lecture.Oop08;

internal class Cat : Animal
{
    // override 가 아니라 new — 다형성이 작동하지 않는다.
    // 같은 이름의 "별도 메서드" 가 정의됐을 뿐.
    public new void Speak() => Console.WriteLine("Cat");
}
