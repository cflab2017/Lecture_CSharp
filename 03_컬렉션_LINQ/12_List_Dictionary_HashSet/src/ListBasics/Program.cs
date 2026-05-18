#nullable enable

List<int> nums = [10, 20, 30];

nums.Add(40);          // 끝에 추가
nums.Remove(20);       // 값으로 첫 번째 제거

Console.WriteLine($"포함 30? {nums.Contains(30)}");
Console.WriteLine($"개수: {nums.Count}");

nums.Sort();           // 오름차순 정렬
Console.WriteLine($"정렬: [{string.Join(", ", nums)}]");
