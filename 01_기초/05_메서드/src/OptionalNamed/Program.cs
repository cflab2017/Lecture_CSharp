// 선택적 매개변수 + 명명된 인수
static void Order(string item, int qty = 1, bool gift = false)
{
    string wrap = gift ? " (선물 포장)" : "";
    Console.WriteLine($"{item} × {qty}{wrap}");
}

Order("사과");                       // 기본값
Order("배", 3);                      // qty 지정
Order(item: "포도", gift: true);     // 이름으로
Order("귤", qty: 5, gift: true);
