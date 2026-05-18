using CodingNow.Lecture.Oop06;

var acc = new BankAccount(1000);

acc.Deposit(500);
Console.WriteLine($"잔액: {acc.GetBalance()}");

acc.Withdraw(2000);   // 잔액(1500)보다 큼 → 잔액 부족 출력
acc.Withdraw(1000);
Console.WriteLine($"잔액: {acc.GetBalance()}");
