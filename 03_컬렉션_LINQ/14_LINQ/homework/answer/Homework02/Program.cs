#nullable enable

Order[] orders =
[
    new("사과", 3, 1500),
    new("바나나", 5, 800),
    new("사과", 2, 1500),
    new("귤", 10, 500),
    new("바나나", 3, 800),
];

int total = orders.Sum(o => o.Quantity * o.UnitPrice);
Console.WriteLine($"전체 매출: {total:N0}원");

Console.WriteLine("=== 상품별 매출 ===");
var byProduct = orders
    .GroupBy(o => o.Product)
    .Select(g => new { Name = g.Key, Total = g.Sum(o => o.Quantity * o.UnitPrice) })
    .OrderByDescending(x => x.Total);

foreach (var p in byProduct)
{
    Console.WriteLine($"{p.Name}: {p.Total:N0}원");
}

public class Order(string product, int quantity, int unitPrice)
{
    public string Product { get; } = product;
    public int Quantity { get; } = quantity;
    public int UnitPrice { get; } = unitPrice;
}
