// Predicate<T> : bool 을 반환하는 1-인자 델리게이트 (Func<T, bool> 과 사실상 동일)
Predicate<int> isPositive = n => n > 0;
Predicate<int> isEven = n => n % 2 == 0;

int[] numbers = [-3, -2, -1, 0, 1, 2, 3];

// Array.Find — 조건을 만족하는 첫 요소를 찾는다.
int firstPositive = Array.Find(numbers, isPositive);
Console.WriteLine($"첫 양수: {firstPositive}");

// List<T>.FindAll — 조건을 만족하는 모든 요소를 새 리스트로 반환.
List<int> list = [.. numbers];
List<int> evens = list.FindAll(isEven);
Console.WriteLine($"짝수들: {string.Join(", ", evens)}");

// 람다는 즉시 만들어 넘길 수도 있다.
Console.WriteLine($"3 보다 큰 첫 값: {Array.Find(numbers, n => n > 3)}"); // 없으면 0 (default)
