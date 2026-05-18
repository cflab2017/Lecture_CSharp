// CancellationToken : 비동기 작업을 외부에서 중단할 수 있게 해 주는 신호.
using CancellationTokenSource cts = new();

// 1.5초 후에 취소 신호를 보낸다.
cts.CancelAfter(TimeSpan.FromMilliseconds(1500));

try
{
    await LongJobAsync(cts.Token);
    Console.WriteLine("정상 완료");
}
catch (OperationCanceledException)
{
    Console.WriteLine("작업이 취소되었습니다");
}

// 토큰을 매 단계마다 점검하거나, Task.Delay 에 그대로 넘겨 자동 취소시킨다.
static async Task LongJobAsync(CancellationToken token)
{
    for (int i = 1; i <= 5; i++)
    {
        await Task.Delay(500, token);   // 취소되면 OperationCanceledException 던짐
        Console.WriteLine($"  단계 {i} 완료");
    }
}
