// 여러 줄 출력 + 문자열 보간
string product = "사과";
int count = 3;
int price = 1500;

Console.WriteLine("=== 구매 내역 ===");
Console.WriteLine($"상품: {product}");
Console.WriteLine($"수량: {count}개");
Console.WriteLine($"합계: {count * price}원");
