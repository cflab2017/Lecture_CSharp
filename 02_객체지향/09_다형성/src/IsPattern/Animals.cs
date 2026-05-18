namespace CodingNow.Lecture.Oop09;

internal class Animal
{
    public virtual void Speak() => Console.WriteLine("동물 소리");
}

internal class Dog : Animal
{
    public override void Speak() => Console.WriteLine("멍멍!");
    public void Bark() => Console.WriteLine("왕!왕!");
}

internal class Cat : Animal
{
    public override void Speak() => Console.WriteLine("야옹~");
    public void Purr() => Console.WriteLine("그르릉~");
}
