#nullable enable

string[] input = ["사과", "배", "사과", "감", "배"];
HashSet<string> unique = new(input);
Console.WriteLine($"중복 제거: [{string.Join(", ", unique)}]");

HashSet<string> a = ["사과", "배", "감"];
HashSet<string> b = ["배", "감", "포도"];

// 원본을 보존하기 위해 복사한 사본에 집합 연산을 호출한다
HashSet<string> intersection = new(a);
intersection.IntersectWith(b);
Console.WriteLine($"교집합: [{string.Join(", ", intersection)}]");

HashSet<string> union = new(a);
union.UnionWith(b);
Console.WriteLine($"합집합: [{string.Join(", ", union)}]");
