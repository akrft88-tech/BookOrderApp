namespace BookOrder.Models;

public class Book
{
    public int Id { get; set; }
    public string BookCode { get; set; } = "";
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";

    public List<OrderBook> OrderBooks { get; set; } = new();
}
