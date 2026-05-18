// BMI 계산기
Console.Write("키(cm): ");
double heightCm = double.Parse(Console.ReadLine()!);

Console.Write("몸무게(kg): ");
double weightKg = double.Parse(Console.ReadLine()!);

double heightM = heightCm / 100.0;
double bmi = weightKg / (heightM * heightM);

Console.WriteLine($"BMI: {bmi:F2}");
