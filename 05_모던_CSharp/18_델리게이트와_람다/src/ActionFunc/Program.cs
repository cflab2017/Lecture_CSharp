// Action<T...> : 반환값이 없는 메서드를 가리키는 표준 델리게이트
Action<string> greet = name => Console.WriteLine($"안녕, {name}!");
greet("Alice");
greet("Bob");

// Func<...,TResult> : 마지막 타입이 반환 타입
Func<int, int, int> add = (a, b) => a + b;
Func<int, int> square = n => n * n;

Console.WriteLine($"add(2, 3)  = {add(2, 3)}");
Console.WriteLine($"square(7)  = {square(7)}");

// 메서드 본문이 한 줄이면 람다, 여러 줄이면 중괄호 블록을 쓸 수도 있다.
Func<int, string> classify = n =>
{
    if (n > 0) return "양수";
    if (n < 0) return "음수";
    return "0";
};

Console.WriteLine(classify(-5));
