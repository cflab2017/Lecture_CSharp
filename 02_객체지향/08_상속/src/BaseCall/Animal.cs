namespace CodingNow.Lecture.Oop08;

internal class Animal
{
    public string Name;

    public Animal(string name)
    {
        Name = name;
    }

    public void Introduce()
    {
        Console.WriteLine($"나는 동물 {Name}.");
    }
}
