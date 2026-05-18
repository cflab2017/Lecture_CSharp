using CancellationTokenSource cts = new();
cts.CancelAfter(TimeSpan.FromMilliseconds(2500));   // 2.5초 후 자동 취소

try
{
    await CountdownAsync(5, cts.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("취소되었습니다");
}

static async Task CountdownAsync(int seconds, CancellationToken token)
{
    for (int i = seconds; i > 0; i--)
    {
        Console.WriteLine($"{i}초 남음");
        await Task.Delay(1000, token);   // 토큰 전달 — 취소되면 여기서 throw
    }
    Console.WriteLine("발사!");
}
