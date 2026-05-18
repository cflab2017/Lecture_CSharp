// switch 표현식 - 요일 번호를 한글로
Console.Write("요일 번호 (1~7): ");
int day = int.Parse(Console.ReadLine()!);

string name = day switch
{
    1 => "월요일",
    2 => "화요일",
    3 => "수요일",
    4 => "목요일",
    5 => "금요일",
    6 => "토요일",
    7 => "일요일",
    _ => "잘못된 입력"
};

Console.WriteLine(name);
