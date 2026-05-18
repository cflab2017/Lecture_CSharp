// 줄 단위로 한 번에 쓰고 읽기
string path = Path.GetTempFileName();

string[] names = ["Alice", "Bob", "Charlie"];
File.WriteAllLines(path, names);

string[] loaded = File.ReadAllLines(path);
foreach (var n in loaded)
{
    Console.WriteLine($"- {n}");
}

File.Delete(path);
