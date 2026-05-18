// 기본 타입과 크기
int i = 100;
long l = 10_000_000_000L;
double d = 3.14;
decimal m = 1500.99m;
bool b = true;
char c = '가';
string s = "안녕";

Console.WriteLine($"int    : {i} ({sizeof(int)} B)");
Console.WriteLine($"long   : {l} ({sizeof(long)} B)");
Console.WriteLine($"double : {d} ({sizeof(double)} B)");
Console.WriteLine($"decimal: {m}");
Console.WriteLine($"bool   : {b}, char: {c}, string: {s}");
