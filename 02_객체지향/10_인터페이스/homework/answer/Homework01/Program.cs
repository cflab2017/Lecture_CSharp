using CodingNow.Lecture.Oop10;

var students = new List<Student>
{
    new Student("철수", 80),
    new Student("영희", 95),
    new Student("민수", 70),
};

students.Sort();   // IComparable<Student> 의 CompareTo 가 호출됨 (점수 내림차순)

foreach (var s in students)
{
    Console.WriteLine($"{s.Name}: {s.Score}");
}
