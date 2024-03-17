using LinkShortener.Application.Models;

namespace LinkShortener.Infrastructure.Services;

public interface IUserService
{
    public Task<User> GetUserByNameOrEmail(string? name, string? email);

}