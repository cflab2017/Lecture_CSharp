namespace CodingNow.Lecture.Oop08;

internal class Manager : Employee
{
    public int Bonus;

    // 부모 생성자에 name, salary 를 넘긴다.
    public Manager(string name, int salary, int bonus) : base(name, salary)
    {
        Bonus = bonus;
    }

    public override void PrintInfo()
    {
        base.PrintInfo();   // 부모 버전 호출
        Console.WriteLine($"보너스: {Bonus}");
    }
}
