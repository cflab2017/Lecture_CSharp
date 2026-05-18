using CodingNow.Lecture.Oop06;

// 같은 클래스에서 인자 개수가 다른 생성자를 골라 호출할 수 있다.
var p1 = new Point();          // (0, 0)
var p2 = new Point(5);         // (5, 5)
var p3 = new Point(3, 7);      // (3, 7)

p1.Print();
p2.Print();
p3.Print();
