#nullable enable

// 한 번 정의한 Box<T> 로 어떤 타입이든 담을 수 있다
var intBox = new Box<int> { Value = 42 };
var strBox = new Box<string> { Value = "안녕" };

Console.WriteLine($"intBox: {intBox.Value}");
Console.WriteLine($"strBox: {strBox.Value}");

intBox.Show();
strBox.Show();

public class Box<T>
{
    public T? Value { get; set; }

    public void Show() => Console.WriteLine($"Box 안의 값: {Value}");
}
