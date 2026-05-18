// 리스트/배열 패턴 (.NET 7+) — 길이와 위치를 함께 검사.
int[][] arrays =
[
    [],
    [42],
    [1, 2, 3],
    [1, 99, 3],
    [1, 2, 3, 4, 5],
    [9, 8, 7],
];

foreach (int[] arr in arrays)
{
    string desc = arr switch
    {
        []              => "비어 있음",
        [var only]      => $"원소 한 개 ({only})",
        [1, .., 3]      => "1로 시작 3으로 끝",
        [_, _, _]       => "정확히 세 개",
        [var first, .. var rest] => $"맨 앞 {first}, 나머지 {rest.Length}개",
    };
    Console.WriteLine($"[{string.Join(",", arr)}] → {desc}");
}
