using CodingNow.Lecture.Oop07;

// required 프로퍼티는 객체 초기화 시 반드시 값을 줘야 한다.
var u = new User { Email = "alice@example.com" };
Console.WriteLine($"{u.Email} / {u.DisplayName}");

// var u2 = new User();   // Email 누락 → 컴파일 에러
