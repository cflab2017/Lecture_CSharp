// is / as 타입 검사·캐스팅
object[] items = ["hello", 42, 3.14, true];

foreach (object item in items)
{
    if (item is string s)
        Console.WriteLine($"문자열: '{s}' (길이 {s.Length})");
    else if (item is int n)
        Console.WriteLine($"정수: {n}");
    else
        Console.WriteLine($"기타: {item} ({item.GetType().Name})");
}

object boxed = 100;
string? wrong = boxed as string;      // 캐스팅 실패 → null
Console.WriteLine($"as 결과: {wrong ?? "(null)"}");
