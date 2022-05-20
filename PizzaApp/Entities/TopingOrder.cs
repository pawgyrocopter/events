namespace PizzaApp.Entities;


public class TopingOrder
{
    public int Id { get; set; }
    
    public Toping Toping { get; set; }
    public int TopingId { get; set; }
    public int Counter { get; set; }

    public PizzaOrder PizzaOrder { get; set; }
    public int PizzaOrderId { get; set; }
}