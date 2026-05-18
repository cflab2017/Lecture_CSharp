namespace CodingNow.Lecture.Oop08;

internal class Animal
{
    // virtual: 자식이 override 로 재정의할 수 있다.
    public virtual void Speak() => Console.WriteLine("...");
}
