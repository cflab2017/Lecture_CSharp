namespace CodingNow.Lecture.Oop07;

internal class Product
{
    // required: 객체 생성 시 반드시 값을 줘야 함
    public required string Name { get; init; }

    private int price;
    public int Price
    {
        get => price;
        set
        {
            if (value < 0)
                throw new ArgumentException("가격은 0 이상이어야 합니다.");
            price = value;
        }
    }
}
