namespace CodingNow.Lecture.Oop06;

internal class BankAccount
{
    private int balance;

    public BankAccount(int initial)
    {
        balance = initial;
    }

    public void Deposit(int amount)
    {
        if (amount <= 0) return;
        balance += amount;
    }

    public void Withdraw(int amount)
    {
        if (amount > balance)
        {
            Console.WriteLine("잔액 부족");
            return;
        }
        balance -= amount;
    }

    public int GetBalance() => balance;
}
