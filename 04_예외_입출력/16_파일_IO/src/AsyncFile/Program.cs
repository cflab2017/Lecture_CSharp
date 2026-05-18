// 비동기 파일 I/O (top-level await — C# 9 / .NET 5+)
// 본격적인 async/await 는 19편에서 다룬다
string path = Path.GetTempFileName();

await File.WriteAllTextAsync(path, "비동기로 저장!");
string text = await File.ReadAllTextAsync(path);

Console.WriteLine($"읽음: {text}");

File.Delete(path);
