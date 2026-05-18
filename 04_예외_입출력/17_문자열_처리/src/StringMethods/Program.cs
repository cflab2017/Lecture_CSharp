// 자주 쓰는 string 메서드 모음
string s = "  Hello, C# World  ";

Console.WriteLine($"원본    : [{s}]");
Console.WriteLine($"Trim    : [{s.Trim()}]");
Console.WriteLine($"Upper   : [{s.Trim().ToUpper()}]");
Console.WriteLine($"Replace : [{s.Trim().Replace("C#", "DotNet")}]");

string[] parts = "사과,바나나,체리".Split(',');
Console.WriteLine($"Split   : {parts.Length}개 → {string.Join(" | ", parts)}");

Console.WriteLine($"Contains 'C#'   : {s.Contains("C#")}");
Console.WriteLine($"StartsWith ' ' : {s.StartsWith(" ")}");
