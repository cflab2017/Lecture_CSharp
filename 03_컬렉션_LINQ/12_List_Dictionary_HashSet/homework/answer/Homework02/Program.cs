#nullable enable

string sentence = "the quick brown fox jumps over the lazy dog the fox is quick";

Dictionary<string, int> counts = new();
foreach (string word in sentence.Split(' '))
{
    counts[word] = counts.GetValueOrDefault(word, 0) + 1;
}

// 빈도 내림차순으로 정렬 후 상위 3개 (LINQ 사용 — OrderBy 는 안정 정렬)
var top3 = counts.OrderByDescending(kv => kv.Value).Take(3);

foreach (var kv in top3)
{
    Console.WriteLine($"{kv.Key}: {kv.Value}");
}
