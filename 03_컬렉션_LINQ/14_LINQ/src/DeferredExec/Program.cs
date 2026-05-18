#nullable enable

List<int> nums = [1, 2, 3];

// 이 시점에는 실행되지 않는다 — "어떻게 할지"만 기억해 둠
var query = nums.Where(n => n > 1);

nums.Add(99);   // 쿼리 정의 후에 원본을 변경

Console.Write("지연 실행 결과: ");
foreach (int n in query)
{
    Console.Write($"{n} ");
}
Console.WriteLine();

// 즉시 평가하려면 ToList 로 스냅샷
var snapshot = nums.Where(n => n > 1).ToList();
nums.Add(100);
Console.WriteLine($"스냅샷 결과: [{string.Join(", ", snapshot)}]");
