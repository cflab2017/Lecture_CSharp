// 튜플 패턴 — 여러 값을 동시에 매칭. 가위바위보 룰을 한눈에.
(string, string)[] games =
[
    ("rock", "scissors"),
    ("paper", "rock"),
    ("scissors", "scissors"),
    ("rock", "paper"),
];

foreach ((string p1, string p2) in games)
{
    string result = (p1, p2) switch
    {
        ("rock",     "scissors") => "P1 승",
        ("paper",    "rock")     => "P1 승",
        ("scissors", "paper")    => "P1 승",
        var (a, b) when a == b   => "비김",        // when 절로 추가 조건
        _                        => "P2 승",
    };
    Console.WriteLine($"{p1,-8} vs {p2,-8} : {result}");
}
