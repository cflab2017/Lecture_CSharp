using CodingNow.Lecture.Oop07;

var apple = new Product { Name = "사과" };
apple.Price = 1500;
Console.WriteLine($"{apple.Name} / {apple.Price}원");

var pear = new Product { Name = "배" };
Console.WriteLine($"{pear.Name} / {pear.Price}원");

try
{
    pear.Price = -100;
}
catch (ArgumentException ex)
{
    Console.WriteLine($"에러: {ex.Message}");
}
