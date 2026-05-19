// null-forgiving 연산자 ! : "내가 보장하니까 컴파일러는 null 경고 꺼" 라는 표시.

// Console.ReadLine() 의 반환은 string? — EOF 시 null 일 수 있다.
// 콘솔 입력에서 null 이 안 나온다고 확신하면 ! 로 꺼 줄 수 있다.
Console.Write("이름을 입력하세요: ");
string name = Console.ReadLine()!;     // ! 로 null 가능성 무시
Console.WriteLine($"안녕, {name}!");

// 다른 자주 쓰는 곳: 늦은 초기화 (예: 단위 테스트의 SetUp)
Person p = GetUser();
Console.WriteLine($"Name = {p.Name}");

// top-level statements 다음에 로컬 함수 → 그 뒤에 타입 선언 순서를 지킨다.
static Person GetUser()
{
    return new Person { Name = "Alice" };
}

internal sealed class Person
{
    // 생성 직후 곧장 채울 거지만, 컴파일러는 그걸 모른다 → null! 로 초기화 의도 표시.
    public string Name { get; set; } = null!;
}
