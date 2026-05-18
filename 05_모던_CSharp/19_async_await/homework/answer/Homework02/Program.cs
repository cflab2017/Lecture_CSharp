using System.Diagnostics;

Stopwatch sw = Stopwatch.StartNew();

// 동시에 시작 — 가장 긴 작업(1200ms) 만큼만 걸린다.
Task<int> t1 = LoadAsync("A", 700);
Task<int> t2 = LoadAsync("B", 1200);
Task<int> t3 = LoadAsync("C", 900);

int[] results = await Task.WhenAll(t1, t2, t3);
sw.Stop();

Console.WriteLine($"합계: {results.Sum()}");
Console.WriteLine($"소요: {sw.ElapsedMilliseconds}ms 근처");

static async Task<int> LoadAsync(string name, int delayMs)
{
    Console.WriteLine($"[{name}] 시작");
    await Task.Delay(delayMs);
    Console.WriteLine($"[{name}] 완료 ({delayMs}ms)");
    return delayMs;
}
