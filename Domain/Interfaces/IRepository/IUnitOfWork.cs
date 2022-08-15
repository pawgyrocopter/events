namespace Domain.Interfaces.IRepository;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    ITopingRepository TopingRepository { get; }
    IPizzaRepository PizzaRepository { get; }
    IOrderRepository OrderRepository { get; }
    Task<bool> Complete();
}