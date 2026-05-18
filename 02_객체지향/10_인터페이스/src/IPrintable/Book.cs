namespace CodingNow.Lecture.Oop10;

internal class Book : IPrintable
{
    public string Title;

    public Book(string title) => Title = title;

    public void Print() => Console.WriteLine($"책: {Title}");
}
