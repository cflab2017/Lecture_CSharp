namespace CodingNow.Lecture.Oop10;

internal class MyPrinter : IPrintable
{
    // 명시적 구현: 메서드 이름 앞에 "인터페이스.이름" 을 붙이고 접근 제한자를 쓰지 않는다.
    // 이렇게 하면 클래스의 일반 메서드로는 노출되지 않는다.
    void IPrintable.Print() => Console.WriteLine("(인터페이스로만 호출 가능) 출력 중");
}
