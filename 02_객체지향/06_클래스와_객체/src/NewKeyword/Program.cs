using CodingNow.Lecture.Oop06;

var a = new Box(10);
var b = a;            // 복사가 아니라 "같은 객체"를 b 도 가리키게 한다.
b.Value = 99;

Console.WriteLine($"a.Value = {a.Value}");   // 99 (a 도 같이 변했다)
Console.WriteLine($"b.Value = {b.Value}");   // 99

var c = new Box(10);  // 완전히 별개인 새 객체
Console.WriteLine($"a == b ? {ReferenceEquals(a, b)}");   // True
Console.WriteLine($"a == c ? {ReferenceEquals(a, c)}");   // False
