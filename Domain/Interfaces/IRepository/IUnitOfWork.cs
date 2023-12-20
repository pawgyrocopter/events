namespace Domain.Interfaces.IRepository;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    Task<bool> Complete();
}