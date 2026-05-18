// 잔액 부족 예외 시나리오
var account = new BankAccount(1000m);

try
{
    Console.WriteLine("출금 시도: 1500");
    account.Withdraw(1500m);
}
catch (InsufficientBalanceException ex)
{
    Console.WriteLine($"실패: {ex.Message}");
}

public class InsufficientBalanceException : Exception
{
    public InsufficientBalanceException(string message) : base(message) { }
}

public class BankAccount
{
    public decimal Balance { get; private set; }

    public BankAccount(decimal initial)
    {
        Balance = initial;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "금액은 0보다 커야 합니다.");

        if (amount > Balance)
            throw new InsufficientBalanceException($"잔액 부족 (잔액 {Balance}, 요청 {amount})");

        Balance -= amount;
    }
}
