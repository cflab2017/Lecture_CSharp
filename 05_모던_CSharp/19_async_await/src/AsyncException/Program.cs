// async 메서드에서 던진 예외는 Task 안에 "포장" 되어 있다가, await 시점에 다시 던져진다.
try
{
    await FailingAsync();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"잡았다: {ex.Message}");
}

// Task.WhenAll 의 경우 첫 예외만 throw 되고, 나머지 예외는 Task.Exception 안에 모인다.
Task t1 = FailingAsync();
Task t2 = FailingAsync();
try
{
    await Task.WhenAll(t1, t2);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"WhenAll 에서 잡음: {ex.Message}");
    Console.WriteLine($"  t1 상태: {t1.Status}, t2 상태: {t2.Status}");
}

static async Task FailingAsync()
{
    await Task.Delay(200);
    throw new InvalidOperationException("의도된 실패");
}
