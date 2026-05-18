// CSV 임시 파일을 만들고, 점수 컬럼 합계/평균 계산
string path = Path.GetTempFileName();

string[] csv =
[
    "name,score",
    "Alice,80",
    "Bob,95",
    "Charlie,72",
];
File.WriteAllLines(path, csv);

string[] lines = File.ReadAllLines(path);
int total = 0;
int count = 0;

for (int i = 0; i < lines.Length; i++)
{
    if (i == 0) continue;                       // 헤더 건너뛰기
    string[] parts = lines[i].Split(',');
    total += int.Parse(parts[1]);
    count++;
}

Console.WriteLine($"합계: {total}");
Console.WriteLine($"평균: {total / count}");

File.Delete(path);
