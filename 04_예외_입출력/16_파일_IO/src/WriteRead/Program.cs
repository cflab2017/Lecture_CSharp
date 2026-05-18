// 임시 파일에 전체 텍스트 쓰고 다시 읽기
string path = Path.GetTempFileName();

File.WriteAllText(path, "안녕, 파일!\n두 번째 줄");

string text = File.ReadAllText(path);
Console.WriteLine("== 읽은 내용 ==");
Console.WriteLine(text);

File.Delete(path);                          // 작업 디렉토리에 흔적 남기지 않기
