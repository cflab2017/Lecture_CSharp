// 단어 수와 가장 긴 단어 찾기
Console.Write("문장을 입력하세요: ");
string input = Console.ReadLine() ?? "";

string[] words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

if (words.Length == 0)
{
    Console.WriteLine("입력이 비어 있습니다.");
    return;
}

string longest = words[0];
foreach (var w in words)
{
    if (w.Length > longest.Length)
        longest = w;
}

Console.WriteLine($"단어 수: {words.Length}");
Console.WriteLine($"가장 긴 단어: {longest}");
