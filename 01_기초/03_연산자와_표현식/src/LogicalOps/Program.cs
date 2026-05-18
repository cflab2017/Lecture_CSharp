// && / || 의 단락 평가 시연
static bool Log(string label, bool value)
{
    Console.WriteLine($"  [{label} 평가됨 → {value}]");
    return value;
}

Console.WriteLine("(false && X) 결과:");
bool r1 = Log("A", false) && Log("B", true);
Console.WriteLine($"  => {r1}");

Console.WriteLine("(true || X) 결과:");
bool r2 = Log("C", true) || Log("D", false);
Console.WriteLine($"  => {r2}");
