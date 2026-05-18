#nullable enable

// 3행 4열 직사각형 배열
int[,] grid = new int[3, 4];

for (int r = 0; r < grid.GetLength(0); r++)
{
    for (int c = 0; c < grid.GetLength(1); c++)
    {
        grid[r, c] = r * 10 + c;
    }
}

// 표 형태로 출력
for (int r = 0; r < grid.GetLength(0); r++)
{
    for (int c = 0; c < grid.GetLength(1); c++)
    {
        Console.Write($"{grid[r, c],3} ");
    }
    Console.WriteLine();
}
