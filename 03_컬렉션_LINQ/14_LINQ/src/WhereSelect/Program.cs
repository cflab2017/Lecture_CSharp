#nullable enable

int[] nums = [1, 2, 3, 4, 5, 6, 7, 8];

// Where: 조건 만족하는 것만 통과, Select: 각각을 변환
var squaresOfBig = nums.Where(n => n > 4).Select(n => n * n);

foreach (int x in squaresOfBig)
{
    Console.WriteLine(x);
}
