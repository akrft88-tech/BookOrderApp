namespace BookOrder.Models;

public class Order
{
    public int Id { get; set; }
    public string ReaderName { get; set; } = "";
    public string ReaderAddress { get; set; } = "";

    public List<OrderBook> OrderBooks { get; set; } = new();
}
