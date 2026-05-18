// .csproj 의 <Nullable>enable</Nullable> 덕분에 NRT 가 켜진 상태.
// 또는 파일 상단에 #nullable enable 을 써도 같은 효과.

string nonNullable = "hello";
string? nullable = null;

// 컴파일러 경고: nonNullable 에 null 대입 안 됨.
// nonNullable = null;   // CS8600

// ? 가 붙은 변수는 사용 전에 null 인지 확인해야 한다.
// Console.WriteLine(nullable.Length);   // CS8602: 가능한 null 역참조

if (nullable is not null)
{
    Console.WriteLine($"길이: {nullable.Length}");
}

Console.WriteLine($"non = {nonNullable}");
Console.WriteLine($"null = {nullable ?? "(없음)"}");
