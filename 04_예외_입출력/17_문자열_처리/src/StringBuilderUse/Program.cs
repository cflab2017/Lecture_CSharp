// StringBuilder: 반복 합성에 강하다
using System.Text;

var sb = new StringBuilder();
sb.Append("[");
for (int i = 1; i <= 5; i++)
{
    if (i > 1) sb.Append(", ");
    sb.Append(i);
}
sb.Append("]");

Console.WriteLine(sb.ToString());
Console.WriteLine($"길이: {sb.Length}");
// 성능 메모: 루프가 수백 회 이상이면 += 대비 매우 빠르다
