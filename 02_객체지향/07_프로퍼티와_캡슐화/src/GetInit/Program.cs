using CodingNow.Lecture.Oop07;

// 객체 초기화 구문에서만 값을 세팅할 수 있다.
var alice = new Person { Name = "Alice", Age = 30 };
Console.WriteLine($"{alice.Name} / {alice.Age}");

// alice.Name = "Bob";   // init 이라 이후 변경 불가 (주석 풀면 컴파일 에러)
