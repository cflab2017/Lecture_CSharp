// 정수 입력 재시도 루프
while (true)
{
    Console.Write("정수를 입력하세요: ");
    string input = Console.ReadLine() ?? "";

    try
    {
        int value = int.Parse(input);
        Console.WriteLine($"입력한 값: {value}");
        break;
    }
    catch (FormatException)
    {
        Console.WriteLine("정수가 아닙니다. 다시 입력하세요.");
    }
}
