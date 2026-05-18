using CodingNow.Lecture.Oop08;

// 변수의 타입은 모두 Animal 이지만 실제 객체에 맞는 Speak() 가 호출된다.
Animal a1 = new Animal();
Animal a2 = new Dog();
Animal a3 = new Puppy();

a1.Speak();
a2.Speak();
a3.Speak();
