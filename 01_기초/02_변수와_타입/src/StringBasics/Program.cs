// 문자열은 불변(immutable)
string a = "Hello";
string b = a;
a += ", World!";          // a 만 새 객체를 가리킴

Console.WriteLine($"a = {a}");
Console.WriteLine($"b = {b}");

// 연결 방법 비교
string c1 = string.Concat("값:", 42);
string c2 = $"값:{42}";   // 문자열 보간 (권장)
Console.WriteLine(c1);
Console.WriteLine(c2);
