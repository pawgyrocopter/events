namespace PizzaApp.Entities;


public class Order
{
    public int Id { get; set; }
    public List<PizzaOrder> Pizzas { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
}