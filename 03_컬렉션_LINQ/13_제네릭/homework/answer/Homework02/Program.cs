#nullable enable

int a = 10, b = 20;
Console.WriteLine($"교환 전: a={a}, b={b}");
Swap(ref a, ref b);
Console.WriteLine($"교환 후: a={a}, b={b}");

string s1 = "hello", s2 = "world";
Console.WriteLine($"교환 전: s1={s1}, s2={s2}");
Swap(ref s1, ref s2);
Console.WriteLine($"교환 후: s1={s1}, s2={s2}");

static void Swap<T>(ref T a, ref T b)
{
    T tmp = a;
    a = b;
    b = tmp;
}
