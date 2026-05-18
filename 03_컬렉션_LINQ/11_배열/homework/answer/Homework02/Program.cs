#nullable enable

int[,] original = { { 1, 2, 3 }, { 4, 5, 6 } };

int rows = original.GetLength(0);
int cols = original.GetLength(1);

int[,] transposed = new int[cols, rows];
for (int r = 0; r < rows; r++)
{
    for (int c = 0; c < cols; c++)
    {
        transposed[c, r] = original[r, c];
    }
}

Print("원본 (2x3):", original);
Console.WriteLine();
Print("전치 (3x2):", transposed);

static void Print(string title, int[,] m)
{
    Console.WriteLine(title);
    for (int r = 0; r < m.GetLength(0); r++)
    {
        for (int c = 0; c < m.GetLength(1); c++)
        {
            Console.Write($"{m[r, c]} ");
        }
        Console.WriteLine();
    }
}
