// 가장 먼저 완료된 작업만 처리한다 — "어느 서버가 먼저 응답하는가" 패턴.
Task<string> mirrorA = MirrorAsync("Seoul", 1500);
Task<string> mirrorB = MirrorAsync("Tokyo", 800);
Task<string> mirrorC = MirrorAsync("Singapore", 1200);

Task<string> winner = await Task.WhenAny(mirrorA, mirrorB, mirrorC);
string result = await winner;   // 한 번 더 await 해서 값을 꺼낸다.
Console.WriteLine($"가장 먼저 응답: {result}");

// 나머지는 그대로 백그라운드에서 끝나도록 두거나, 취소하면 된다(다음 예제 참고).
static async Task<string> MirrorAsync(string region, int delayMs)
{
    await Task.Delay(delayMs);
    return $"{region}({delayMs}ms)";
}
