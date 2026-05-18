#nullable enable

int[] nums = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

// 메서드 체인
var a = nums.Where(n => n % 2 == 0).Select(n => n * n);

// 동일한 의미의 쿼리식 (SQL 유사)
var b = from n in nums
        where n % 2 == 0
        select n * n;

Console.WriteLine($"메서드 체인: [{string.Join(", ", a)}]");
Console.WriteLine($"쿼리식    : [{string.Join(", ", b)}]");
