using CodingNow.Lecture.Oop10;

var p = new MyPrinter();

// p.Print();   // 명시적 구현이라 클래스에서 직접 안 보임 (주석 풀면 컴파일 에러)

// 1) 인터페이스 변수로 받아서 호출
IPrintable pr = p;
pr.Print();

// 2) 즉석에서 인터페이스로 캐스팅해 호출
((IPrintable)p).Print();
