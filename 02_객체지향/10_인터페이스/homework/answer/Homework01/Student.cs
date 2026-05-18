namespace CodingNow.Lecture.Oop10;

internal class Student : IComparable<Student>
{
    public string Name;
    public int Score;

    public Student(string name, int score)
    {
        Name = name;
        Score = score;
    }

    // 점수 내림차순 정렬: other 와 this 의 비교 순서를 뒤집는다.
    public int CompareTo(Student? other)
    {
        if (other is null) return 1;
        return other.Score.CompareTo(this.Score);
    }
}
