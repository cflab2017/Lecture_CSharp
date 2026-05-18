namespace CodingNow.Lecture.Oop08;

internal class Employee
{
    public string Name;
    public int Salary;

    public Employee(string name, int salary)
    {
        Name = name;
        Salary = salary;
    }

    public virtual void PrintInfo()
    {
        Console.WriteLine($"이름: {Name}, 급여: {Salary}");
    }
}
