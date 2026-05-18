// 짝수/홀수 판별 - 삼항 연산자
Console.Write("정수: ");
int n = int.Parse(Console.ReadLine()!);

string kind = n % 2 == 0 ? "짝수" : "홀수";
Console.WriteLine($"{n} 은 {kind}입니다.");
