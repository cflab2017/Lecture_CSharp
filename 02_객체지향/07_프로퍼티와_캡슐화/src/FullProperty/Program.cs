using CodingNow.Lecture.Oop07;

var t = new Temperature();
t.Celsius = 25;
Console.WriteLine($"{t.Celsius}°C");

try
{
    t.Celsius = -500;   // 절대영도 아래 → 예외 발생
}
catch (ArgumentException ex)
{
    Console.WriteLine($"에러: {ex.Message}");
}
