using System.Diagnostics.CodeAnalysis;

// [NotNullWhen(true)] : "이 메서드가 true 를 돌려줄 땐 out 매개변수가 null 이 아닙니다" 를
// 컴파일러에게 알려주는 attribute. 분석기가 이걸 보고 흐름을 더 똑똑하게 추적한다.

if (TryParseName("Alice", out string? name))
{
    // 여기 안에서는 name 이 not null 로 추론된다 → 경고 없이 Length 사용 OK.
    Console.WriteLine($"이름 길이: {name.Length}");
}

if (!TryParseName("", out string? empty))
{
    Console.WriteLine($"실패: empty == null ? {empty is null}");
}

// 비슷한 형제 attribute : [NotNull], [MaybeNull], [MemberNotNull] 등 — 라이브러리 작성 때 유용.
static bool TryParseName(string raw, [NotNullWhen(true)] out string? value)
{
    if (string.IsNullOrWhiteSpace(raw))
    {
        value = null;
        return false;
    }
    value = raw.Trim();
    return true;
}
