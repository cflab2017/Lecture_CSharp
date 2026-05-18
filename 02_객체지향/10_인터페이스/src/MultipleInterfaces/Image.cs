namespace CodingNow.Lecture.Oop10;

// 클래스 하나가 여러 인터페이스를 동시에 구현할 수 있다.
internal class Image : IPrintable, IResizable
{
    public void Print() => Console.WriteLine("이미지를 인쇄합니다");

    public void Resize(int width, int height)
        => Console.WriteLine($"이미지 크기를 {width}x{height} 로 변경");
}
