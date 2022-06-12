using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Entities;


public class Order
{
    [Key]
    public int Id { get; set; }
    public List<PizzaOrder> Pizzas{get;set;}   
    public int UserId { get; set; }
    public User User { get; set; }
    
    public OrderState OrderState { get; set; }
}

public enum OrderState
{
    Making,
    Ready
}