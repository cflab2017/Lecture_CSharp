namespace CodingNow.Lecture.Oop08;

internal class Dog : Animal
{
    public string Breed;

    // 부모 생성자에 name 을 넘겨준다. 자식 생성자 본문보다 base(...) 가 먼저 실행됨.
    public Dog(string name, string breed) : base(name)
    {
        Breed = breed;
    }

    // 부모의 Introduce 를 가리고(new), 안에서 base.Introduce() 로 부모 버전을 호출한다.
    public new void Introduce()
    {
        base.Introduce();
        Console.WriteLine($"종은 {Breed}야.");
    }
}
