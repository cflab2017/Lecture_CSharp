Dictionary<string, string?> profile = new()
{
    ["name"] = "Alice",
    ["email"] = null,
    ["city"] = "Seoul",
};

string[] keys = ["name", "email", "city", "phone"];

foreach (string key in keys)
{
    // 키가 없으면 value 는 null, 키가 있어도 값이 null 일 수 있다.
    profile.TryGetValue(key, out string? value);
    int length = value?.Length ?? 0;   // 두 가지 null 케이스를 한 번에 처리
    Console.WriteLine($"{key.PadRight(6)}: {length}");
}
