#nullable enable

// 컬렉션 식으로 배열을 만든다 (.NET 8+)
int[] scores = [85, 92, 78, 90, 88];

Console.WriteLine($"길이: {scores.Length}");
Console.WriteLine($"첫 번째: {scores[0]}");
Console.WriteLine($"마지막: {scores[^1]}");   // 끝에서 1번째

int sum = 0;
foreach (int s in scores)
{
    sum += s;
}
Console.WriteLine($"합계: {sum}");
