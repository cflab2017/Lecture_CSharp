namespace CodingNow.Lecture.Oop09;

internal abstract class Animal
{
    // 자식이 반드시 구현해야 한다.
    public abstract void Speak();
}

internal class Dog : Animal
{
    public override void Speak() => Console.WriteLine("멍멍!");
    public void Bark() => Console.WriteLine("왕!왕!");
}

internal class Cat : Animal
{
    public override void Speak() => Console.WriteLine("야옹~");
}

internal class Cow : Animal
{
    public override void Speak() => Console.WriteLine("음매~");
}
