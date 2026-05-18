#nullable enable

// T 는 호출 시 자동 추론된다
Console.WriteLine(Max(3, 7));
Console.WriteLine(Max("apple", "banana"));
Console.WriteLine(Max(3.14, 2.71));

static T Max<T>(T a, T b) where T : IComparable<T>
{
    return a.CompareTo(b) >= 0 ? a : b;
}
