namespace CodingNow.Lecture.Oop06;

internal class Book
{
    public string Title;
    public string Author;
    public int Pages;

    public Book(string title, string author, int pages)
    {
        this.Title = title;
        this.Author = author;
        this.Pages = pages;
    }

    public void Describe()
    {
        Console.WriteLine($"{Title} - {Author} 지음 ({Pages}쪽)");
    }
}
