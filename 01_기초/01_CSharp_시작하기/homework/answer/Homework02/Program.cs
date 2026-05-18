// 현재 시각과 함께 인사
Console.Write("이름: ");
string? name = Console.ReadLine();

Console.WriteLine($"{name}님, 안녕하세요!");
Console.WriteLine($"지금 시각은 {DateTime.Now:yyyy-MM-dd HH:mm:ss} 입니다.");
