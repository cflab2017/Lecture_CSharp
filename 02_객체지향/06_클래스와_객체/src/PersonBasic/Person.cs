namespace CodingNow.Lecture.Oop06;

internal class Person
{
    // 필드: 객체가 가진 데이터
    public string Name;
    public int Age;

    // 생성자: 객체가 만들어질 때 한 번 호출
    public Person(string name, int age)
    {
        this.Name = name;
        this.Age = age;
    }

    // 메서드: 객체가 가진 동작
    public void Greet()
    {
        Console.WriteLine($"안녕, 나는 {Name}({Age}세).");
    }
}
