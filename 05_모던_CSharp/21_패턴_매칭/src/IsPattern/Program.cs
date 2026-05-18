// is 패턴 — 타입 체크 + 변수 선언을 한 번에.
object[] items = ["hello", 42, 3.14, true, "world"];

foreach (object item in items)
{
    // declaration pattern : 타입에 맞으면 변수 s 에 캐스팅된 채로 담아 준다.
    if (item is string s)
    {
        Console.WriteLine($"문자열({s.Length}자): {s}");
    }
    // 결합도 가능
    else if (item is int n and > 0)
    {
        Console.WriteLine($"양의 정수: {n}");
    }
    else if (item is double d)
    {
        Console.WriteLine($"실수: {d}");
    }
    else
    {
        Console.WriteLine($"기타: {item}");
    }
}
