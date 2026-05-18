#nullable enable

ReadOnlySpan<char> text = "Hello, World!".AsSpan();

// 새 문자열을 만들지 않고 원본의 일부를 가리킨다
ReadOnlySpan<char> hello = text.Slice(0, 5);
ReadOnlySpan<char> world = text.Slice(7, 5);

Console.WriteLine(hello.ToString());
Console.WriteLine(world.ToString());

Console.WriteLine($"원본 길이: {text.Length}, 슬라이스 길이: {hello.Length}");
