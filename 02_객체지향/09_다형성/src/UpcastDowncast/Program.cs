using CodingNow.Lecture.Oop09;

// 업캐스팅: 자식(Dog) → 부모(Animal). 안전하므로 명시적 캐스트 불필요.
Animal a = new Dog();
a.Speak();   // 가상 디스패치: 실제 객체가 Dog 이므로 Dog.Speak() 가 실행됨

// 다운캐스팅: 부모(Animal) → 자식(Dog). 실제 타입이 Dog 가 맞을 때만 OK.
Dog d = (Dog)a;
d.Bark();
