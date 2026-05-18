int[] nums = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

Console.WriteLine("짝수: " + string.Join(" ", Filter(nums, n => n % 2 == 0)));
Console.WriteLine("5 이상: " + string.Join(" ", Filter(nums, n => n >= 5)));
Console.WriteLine("소수: " + string.Join(" ", Filter(nums, IsPrime)));

// Func<int,bool> 을 받아 조건에 맞는 요소만 돌려준다.
static IEnumerable<int> Filter(IEnumerable<int> source, Func<int, bool> predicate)
{
    foreach (int n in source)
    {
        if (predicate(n)) yield return n;
    }
}

// 소수 판정 — 메서드 그룹으로 Func<int,bool> 자리에 그대로 넘긴다.
static bool IsPrime(int n)
{
    if (n < 2) return false;
    for (int i = 2; i < n; i++)
    {
        if (n % i == 0) return false;
    }
    return true;
}
