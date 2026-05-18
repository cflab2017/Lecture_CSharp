#nullable enable

int[] nums = [10, 20, 30, 40, 50];

Console.WriteLine($"합계: {nums.Sum()}");
Console.WriteLine($"평균: {nums.Average()}");
Console.WriteLine($"최댓값: {nums.Max()}");
Console.WriteLine($"최솟값: {nums.Min()}");
Console.WriteLine($"개수: {nums.Count()}");

// Aggregate: 직접 누적 함수 정의 (여기선 곱)
int product = nums.Aggregate(1, (acc, n) => acc * n);
Console.WriteLine($"곱: {product}");

bool allBigger = nums.All(n => n > 0);
bool anyEven = nums.Any(n => n % 2 == 0);
Console.WriteLine($"모두 양수? {allBigger}, 짝수 존재? {anyEven}");
