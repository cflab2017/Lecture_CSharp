// .NET 8 의 top-level statement 는 await 를 그대로 쓸 수 있다.
Console.WriteLine("작업 시작");
int result = await DoWorkAsync();
Console.WriteLine($"결과: {result}");
Console.WriteLine("작업 종료");

// async 메서드는 Task / Task<T> 를 반환한다.
// await 만난 시점에서 호출자에게 반환했다가, 작업이 끝나면 이어서 실행한다.
static async Task<int> DoWorkAsync()
{
    Console.WriteLine("  ...1초 대기");
    await Task.Delay(1000);
    Console.WriteLine("  ...1초 더 대기");
    await Task.Delay(1000);
    return 42;
}
