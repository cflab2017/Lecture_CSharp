namespace CodingNow.Lecture.Oop10;

internal class Invoice : IPrintable
{
    public int Amount;

    public Invoice(int amount) => Amount = amount;

    public void Print() => Console.WriteLine($"청구서: {Amount}원");
}
