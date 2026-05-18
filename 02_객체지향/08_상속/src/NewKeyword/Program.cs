using CodingNow.Lecture.Oop08;

// new 키워드로 hide 한 경우: 변수 타입에 따라 어떤 메서드가 호출되는지 달라진다.
Animal a = new Cat();
a.Speak();      // Animal 타입으로 호출 → "Animal"

Cat c = new Cat();
c.Speak();      // Cat 타입으로 호출 → "Cat"
