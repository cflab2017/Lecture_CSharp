// 성적 등급 판별
Console.Write("점수: ");
int score = int.Parse(Console.ReadLine()!);

string grade;
if (score >= 90)      grade = "A";
else if (score >= 80) grade = "B";
else if (score >= 70) grade = "C";
else if (score >= 60) grade = "D";
else                  grade = "F";

Console.WriteLine($"등급: {grade}");
