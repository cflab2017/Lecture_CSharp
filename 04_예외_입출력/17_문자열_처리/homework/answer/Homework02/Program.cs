// 본문에서 이메일을 모두 추출
using System.Text.RegularExpressions;

string text = "연락처: alice@example.com, bob.smith@test.io.\n"
            + "문의는 admin+help@foo.co.kr 로 보내주세요.";

string pattern = @"[\w\.\+-]+@[\w\.-]+\.\w+";

foreach (Match m in Regex.Matches(text, pattern))
{
    Console.WriteLine(m.Value);
}
