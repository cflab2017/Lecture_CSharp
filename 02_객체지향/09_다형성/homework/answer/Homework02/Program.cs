using CodingNow.Lecture.Oop09;

Animal[] zoo = [new Dog(), new Cat(), new Cow()];

foreach (var a in zoo)
{
    a.Speak();

    if (a is Dog dog)
        dog.Bark();
}
