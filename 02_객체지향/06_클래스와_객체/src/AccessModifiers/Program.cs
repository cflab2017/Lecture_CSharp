using CodingNow.Lecture.Oop06;

var account = new Account(1000);

account.Deposit(500);   // public — 어디서든 호출 가능
Console.WriteLine($"잔액: {account.GetBalance()}");

// account.balance = 9999;   // private 필드라서 외부 접근 불가 (주석 해제 시 컴파일 에러)
account.LogInternal();        // internal — 같은 프로젝트 안에서는 OK
