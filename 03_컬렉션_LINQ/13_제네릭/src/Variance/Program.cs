#nullable enable

// IEnumerable<out T> 는 공변 → Dog 컬렉션을 Animal 컬렉션으로 사용 가능
IEnumerable<Dog> dogs = [new Dog("바둑이"), new Dog("뽀삐")];
IEnumerable<Animal> animals = dogs;     // out 덕분에 OK
foreach (Animal a in animals)
{
    Console.WriteLine($"동물: {a.Name}");
}

// Action<in T> 는 반변 → Animal 처리 함수를 Dog 에 사용 가능
Action<Animal> describe = a => Console.WriteLine($"이름은 {a.Name}");
Action<Dog> describeDog = describe;     // in 덕분에 OK
describeDog(new Dog("초코"));

public class Animal(string name)
{
    public string Name { get; } = name;
}

public class Dog(string name) : Animal(name);
