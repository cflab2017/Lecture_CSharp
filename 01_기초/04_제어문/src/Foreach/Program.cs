// 배열을 foreach 로 순회해 합계·평균
int[] scores = [78, 92, 64, 85, 100];

int total = 0;
foreach (int s in scores)
    total += s;

double avg = (double)total / scores.Length;
Console.WriteLine($"총점: {total}, 평균: {avg:F2}");
