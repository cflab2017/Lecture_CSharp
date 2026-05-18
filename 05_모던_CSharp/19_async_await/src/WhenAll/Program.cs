using System.Diagnostics;

// 3 개의 작업을 동시에 시작하고 모두 끝나길 기다린다.
Stopwatch sw = Stopwatch.StartNew();

Task<int> t1 = FetchAsync("A", 1000);
Task<int> t2 = FetchAsync("B", 1500);
Task<int> t3 = FetchAsync("C", 800);

int[] results = await Task.WhenAll(t1, t2, t3);

sw.Stop();
Console.WriteLine($"결과 합계: {results.Sum()}");
Console.WriteLine($"총 시간: {sw.ElapsedMilliseconds} ms  (순차였다면 3300ms 정도)");

// 네트워크 호출을 흉내내는 비동기 메서드 — 실제로는 HttpClient 등을 await 한다.
static async Task<int> FetchAsync(string name, int delayMs)
{
    Console.WriteLine($"  [{name}] 시작 ({delayMs}ms)");
    await Task.Delay(delayMs);
    Console.WriteLine($"  [{name}] 완료");
    return delayMs;
}
