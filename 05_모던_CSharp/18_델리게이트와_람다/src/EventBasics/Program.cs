// event 는 "여러 구독자에게 알림을 발행" 하는 델리게이트 멤버.
Sensor sensor = new();

// 구독: += 로 핸들러를 더한다.
sensor.OnAlert += msg => Console.WriteLine($"[로그] {msg}");
sensor.OnAlert += msg => Console.WriteLine($"[화면] !! {msg} !!");

sensor.CheckTemperature(80);    // 정상
sensor.CheckTemperature(120);   // 알림 발행

internal sealed class Sensor
{
    // event 키워드 : 외부에서 = 로 통째로 갈아끼우기 불가, += / -= 만 허용.
    public event Action<string>? OnAlert;

    public void CheckTemperature(int t)
    {
        if (t >= 100)
        {
            OnAlert?.Invoke($"고온 경고: {t}도");   // ?. 로 구독자 없을 때 안전 호출
        }
    }
}
