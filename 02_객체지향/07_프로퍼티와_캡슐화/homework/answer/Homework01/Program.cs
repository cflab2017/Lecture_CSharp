using CodingNow.Lecture.Oop07;

var t = new Temperature();

t.Celsius = 0;
Console.WriteLine($"{t.Celsius}°C = {t.Fahrenheit}°F");

t.Celsius = 100;
Console.WriteLine($"{t.Celsius}°C = {t.Fahrenheit}°F");

try
{
    t.Celsius = -300;
}
catch (ArgumentException ex)
{
    Console.WriteLine($"에러: {ex.Message}");
}
