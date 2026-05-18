// 보간 문자열과 형식 지정자
double pi = 3.14159265;
int score = 87;
DateTime now = new DateTime(2025, 5, 18, 14, 30, 0);

Console.WriteLine($"파이      : {pi:F2}");           // 소수점 2자리
Console.WriteLine($"점수      : {score,5}점");        // 폭 5, 우측 정렬
Console.WriteLine($"점수(좌) : {score,-5}|");         // 음수 → 좌측 정렬
Console.WriteLine($"날짜      : {now:yyyy-MM-dd HH:mm}");
Console.WriteLine($"퍼센트    : {0.873:P1}");          // 1자리 백분율
