// 같은 문자열이라도 culture 에 따라 결과가 다르다
using System.Globalization;

string s = "3,14";

decimal de = decimal.Parse(s, CultureInfo.GetCultureInfo("de-DE"));
bool ok = decimal.TryParse(
    s, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal inv);

Console.WriteLine($"de-DE 해석     : {de}");                  // 3.14
Console.WriteLine($"Invariant 해석 : ok={ok}, 값={inv}");      // ','를 천 단위로 → 314

// 기계가 읽는 포맷은 항상 InvariantCulture 명시
decimal price = 1234.56m;
Console.WriteLine($"기계 포맷: {price.ToString(CultureInfo.InvariantCulture)}");
