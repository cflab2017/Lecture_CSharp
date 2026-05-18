// 임시 파일에 쓰고, 번호 매겨 출력
string path = Path.GetTempFileName();

string[] cities = ["서울", "부산", "대구", "광주"];
File.WriteAllLines(path, cities);

string[] loaded = File.ReadAllLines(path);
for (int i = 0; i < loaded.Length; i++)
{
    Console.WriteLine($"[{i + 1}] {loaded[i]}");
}

File.Delete(path);
