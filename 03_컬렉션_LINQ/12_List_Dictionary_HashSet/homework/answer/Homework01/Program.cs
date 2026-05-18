#nullable enable

string[] names = ["민호", "지수", "민호", "서연", "지수", "윤재"];

HashSet<string> unique = new(names);
List<string> sorted = new(unique);
sorted.Sort();

Console.WriteLine("중복 제거 후 정렬:");
foreach (string name in sorted)
{
    Console.WriteLine(name);
}
