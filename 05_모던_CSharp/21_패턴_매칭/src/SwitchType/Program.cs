// switch 식 + 타입 패턴 — 상속 트리에 한 번씩 분기하는 데 적합.
Animal[] zoo = [new Dog("바둑이"), new Cat("나비"), new Bird("짹짹"), new Dog("쫑")];

foreach (Animal a in zoo)
{
    string sound = Describe(a);
    Console.WriteLine(sound);
}

static string Describe(Animal a) => a switch
{
    Dog d  => $"{d.Name}: 멍멍",
    Cat c  => $"{c.Name}: 야옹",
    Bird b => $"{b.Name}: 짹짹",
    _      => "알 수 없는 동물",   // discard 패턴 — 그 외 전부
};

internal abstract record Animal(string Name);
internal sealed record Dog(string Name) : Animal(Name);
internal sealed record Cat(string Name) : Animal(Name);
internal sealed record Bird(string Name) : Animal(Name);
