namespace CodingNow.Lecture.Oop09;

internal class Animal
{
    public virtual void Speak() => Console.WriteLine("...");
}

internal class Dog : Animal
{
    public override void Speak() => Console.WriteLine("멍멍!");
}

internal class Cat : Animal
{
    public override void Speak() => Console.WriteLine("야옹~");
}

internal class Cow : Animal
{
    public override void Speak() => Console.WriteLine("음매~");
}
