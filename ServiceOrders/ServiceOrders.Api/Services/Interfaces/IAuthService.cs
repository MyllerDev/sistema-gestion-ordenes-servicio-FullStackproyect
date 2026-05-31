using ServiceOrders.Api.DTOs;

namespace ServiceOrders.Api.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}