using CodingNow.Lecture.Oop10;

// 부모-자식 관계가 아니어도, 같은 인터페이스를 구현했다면 묶어 처리할 수 있다.
IPrintable[] items = [new Book("객체지향의 사실과 오해"), new Invoice(99000)];

foreach (var item in items)
{
    item.Print();
}
