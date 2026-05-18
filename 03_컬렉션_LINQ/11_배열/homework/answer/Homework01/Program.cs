#nullable enable

int[] data = [42, 17, 89, 23, 65, 8, 50];

int max = data[0];
int min = data[0];
int sum = 0;

foreach (int n in data)
{
    max = Math.Max(max, n);
    min = Math.Min(min, n);
    sum += n;
}

double avg = (double)sum / data.Length;

Console.WriteLine($"배열: [{string.Join(", ", data)}]");
Console.WriteLine($"최댓값: {max}");
Console.WriteLine($"최솟값: {min}");
Console.WriteLine($"평균: {avg:F1}");
