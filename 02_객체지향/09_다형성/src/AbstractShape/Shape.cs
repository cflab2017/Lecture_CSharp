namespace CodingNow.Lecture.Oop09;

// abstract: 자체로는 인스턴스화 못 하고, 자식이 구체화해야 한다.
internal abstract class Shape
{
    // 본문 없는 abstract 메서드 — 자식이 반드시 override.
    public abstract double Area();

    // 추상 클래스에도 일반(구체) 메서드를 둘 수 있다.
    public void Print() => Console.WriteLine($"넓이 = {Area():F2}");
}
