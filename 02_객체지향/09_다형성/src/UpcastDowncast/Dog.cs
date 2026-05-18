namespace CodingNow.Lecture.Oop09;

internal class Dog : Animal
{
    public override void Speak() => Console.WriteLine("멍멍!");
    public void Bark() => Console.WriteLine("왕!왕!");
}
