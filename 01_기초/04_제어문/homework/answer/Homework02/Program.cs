// FizzBuzz 1 ~ 30
for (int i = 1; i <= 30; i++)
{
    string line = i switch
    {
        _ when i % 15 == 0 => "FizzBuzz",
        _ when i % 3 == 0  => "Fizz",
        _ when i % 5 == 0  => "Buzz",
        _ => i.ToString()
    };
    Console.WriteLine(line);
}
