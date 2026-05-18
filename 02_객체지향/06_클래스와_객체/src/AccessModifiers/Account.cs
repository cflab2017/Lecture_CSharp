namespace CodingNow.Lecture.Oop06;

internal class Account
{
    // private: 같은 클래스 안에서만 접근. 외부에서 직접 바꾸지 못하게 막는다.
    private int balance;

    public Account(int initial)
    {
        balance = initial;
    }

    // public: 외부에서 사용할 동작
    public void Deposit(int amount)
    {
        if (amount <= 0) return;   // 잘못된 입력은 무시
        balance += amount;
    }

    // public: 잔액을 조회하는 안전한 통로
    public int GetBalance() => balance;

    // internal: 같은 어셈블리(프로젝트) 안에서만 보임
    internal void LogInternal()
    {
        Console.WriteLine($"[내부 로그] 현재 잔액 = {balance}");
    }
}
