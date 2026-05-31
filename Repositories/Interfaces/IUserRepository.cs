using ServiceOrders.Api.Models;

namespace ServiceOrders.Api.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
}