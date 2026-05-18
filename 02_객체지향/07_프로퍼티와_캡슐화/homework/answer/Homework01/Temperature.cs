namespace CodingNow.Lecture.Oop07;

internal class Temperature
{
    private double celsius;

    public double Celsius
    {
        get => celsius;
        set
        {
            if (value < -273.15)
                throw new ArgumentException("절대영도(-273.15°C) 아래로 내려갈 수 없습니다.");
            celsius = value;
        }
    }

    // 읽기 전용 계산 프로퍼티
    public double Fahrenheit => celsius * 9 / 5 + 32;
}
