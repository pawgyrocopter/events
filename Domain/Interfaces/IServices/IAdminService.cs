namespace Domain.Interfaces.IServices;

public interface IAdminService
{
   // Task<object> GetUsersWithRoles();
    Task<object> EditRoles(string userName, string roles);
}