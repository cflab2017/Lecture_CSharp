// ?. ?? ??= 시연
string? name = null;

int len = name?.Length ?? 0;          // null 이면 0
Console.WriteLine($"길이: {len}");

name ??= "(이름 없음)";               // null 일 때만 대입
Console.WriteLine($"이름: {name}");

// 이미 값이 있으니 변하지 않음
name ??= "또 다른 기본값";
Console.WriteLine($"최종: {name}");
