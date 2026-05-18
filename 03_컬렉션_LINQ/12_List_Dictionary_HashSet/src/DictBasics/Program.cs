#nullable enable

string text = "apple banana apple cherry banana apple";
Dictionary<string, int> counts = new();

foreach (string word in text.Split(' '))
{
    // 없으면 0, 있으면 기존 값 → + 1
    counts[word] = counts.GetValueOrDefault(word, 0) + 1;
}

foreach (var (word, count) in counts)
{
    Console.WriteLine($"{word}: {count}");
}
