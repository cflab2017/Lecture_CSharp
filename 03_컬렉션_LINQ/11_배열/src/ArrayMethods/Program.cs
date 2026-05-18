#nullable enable

int[] nums = [4, 2, 9, 1, 7];

Array.Sort(nums);                  // 제자리 정렬
Console.WriteLine($"정렬: [{string.Join(", ", nums)}]");

Array.Reverse(nums);               // 순서 뒤집기
Console.WriteLine($"역순: [{string.Join(", ", nums)}]");

int idx = Array.IndexOf(nums, 7);  // 값의 위치 찾기
Console.WriteLine($"7의 위치: {idx}");

int[] copy = new int[3];
Array.Copy(nums, copy, 3);         // 앞에서 3개 복사
Console.WriteLine($"앞 3개 복사: [{string.Join(", ", copy)}]");
