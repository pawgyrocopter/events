namespace PizzaApp.SignalR;

public interface IOrderHub
{
    Task SendMessage();
}