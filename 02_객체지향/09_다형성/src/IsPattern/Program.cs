using CodingNow.Lecture.Oop09;

Animal[] animals = [new Dog(), new Cat(), new Animal()];

foreach (var a in animals)
{
    a.Speak();   // 가상 디스패치

    // is 패턴: 타입 검사 + 변수 선언을 한 번에. 안에서는 이미 캐스팅된 변수 사용.
    if (a is Dog dog)
        dog.Bark();
    else if (a is Cat cat)
        cat.Purr();
}
