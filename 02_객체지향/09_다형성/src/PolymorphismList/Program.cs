using CodingNow.Lecture.Oop09;

// 자식 타입 객체들을 부모 타입 컬렉션 하나에 모두 담을 수 있다.
List<Animal> zoo = [new Dog(), new Cat(), new Cow()];

foreach (var a in zoo)
{
    a.Speak();   // 같은 호출인데 객체마다 다른 결과 — 이것이 다형성
}
