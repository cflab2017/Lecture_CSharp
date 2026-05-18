// Path 의 유틸리티 메서드들
string combined = Path.Combine("logs", "2025", "app.log");
Console.WriteLine($"Combine    : {combined}");
Console.WriteLine($"FileName   : {Path.GetFileName(combined)}");
Console.WriteLine($"Extension  : {Path.GetExtension(combined)}");
Console.WriteLine($"NoExt      : {Path.GetFileNameWithoutExtension(combined)}");

string temp = Path.GetTempFileName();
Console.WriteLine($"Temp       : {temp}");
File.Delete(temp);
