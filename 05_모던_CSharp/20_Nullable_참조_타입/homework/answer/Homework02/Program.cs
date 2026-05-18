UserRepo repo = new();
int[] queries = [1, 2, 99];

List<User> adults = [];

foreach (int id in queries)
{
    User? u = repo.FindById(id);
    if (u is null)
    {
        Console.WriteLine($"#{id} (찾을 수 없음)");
    }
    else
    {
        Console.WriteLine($"#{u.Id} {u.Name} ({u.Age}세)");
    }

    // 패턴 매칭으로 성인만 골라 모은다.
    if (u is { Age: >= 18 })
    {
        adults.Add(u);   // 분기 안에서는 u 가 not null 로 추론됨
    }
}

Console.WriteLine("성인 회원: " + string.Join(", ", adults.Select(a => a.Name)));

internal sealed record User(int Id, string Name, int Age);

internal sealed class UserRepo
{
    private readonly List<User> _users =
    [
        new(1, "Alice", 30),
        new(2, "Bob", 16),
    ];

    public User? FindById(int id)
    {
        foreach (User u in _users)
        {
            if (u.Id == id) return u;
        }
        return null;
    }
}
