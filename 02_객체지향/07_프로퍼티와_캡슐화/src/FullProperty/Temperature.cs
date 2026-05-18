namespace CodingNow.Lecture.Oop07;

internal class Temperature
{
    // backing field: 실제 데이터는 여기에 저장
    private double celsius;

    public double Celsius
    {
        get => celsius;
        set
        {
            // set 안의 value 는 "들어온 새 값" 을 가리키는 예약 매개변수
            if (value < -273.15)
                throw new ArgumentException("절대영도(-273.15°C) 아래로 내려갈 수 없습니다.");
            celsius = value;
        }
    }
}
