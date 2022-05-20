namespace PizzaApp.Entities;


public class PizzaOrder
{
    public int Id { get; set; }
    public Pizza Pizza { get; set; }
    public int PizzaId { get; set; }
    
    public IEnumerable<TopingOrder> Topings { get; set; }
    public Order Order { get; set; }
    public int OrderId { get; set; }
}