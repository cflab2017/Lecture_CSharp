// Regex 의 IsMatch / Match / Matches
using System.Text.RegularExpressions;

string text = "문의 메일은 alice@example.com 또는 bob@test.io 로 보내세요.";

// 1) 포함 여부
bool hasDigit = Regex.IsMatch("주문 #42", @"\d+");
Console.WriteLine($"숫자 포함? {hasDigit}");

// 2) 첫 매치
Match first = Regex.Match("가격 1500원", @"\d+");
Console.WriteLine($"첫 숫자: {first.Value}");

// 3) 모든 이메일 추출 (학습용 단순 패턴)
foreach (Match m in Regex.Matches(text, @"[\w\.-]+@[\w\.-]+\.\w+"))
{
    Console.WriteLine($"메일: {m.Value}");
}
