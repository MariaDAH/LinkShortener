using LinkShortener.Application.Models;
using LinkShortener.Application.Services;

namespace LinkShortener.Infrastructure.Services;

public class ShortenerService(IUnitOfWork unitOfWork, IUserService userService) : IShortenerService
{
    public async Task<Url?> GetLink(string url, CancellationToken cancellationToken)
    {
        var key = url.Split("/").ToList().Last();
        var uow = unitOfWork.GetRepository<Url>();
        var link = await uow.GetByIdAsync(key);
        if (link is null)
        {
            return null;
        }

        return link;
    }

    public async Task<string> AddShortLink(string originalUrl, string userName, CancellationToken cancellationToken)
    {
        var user = await userService.GetUserByNameOrEmail(userName, null);
        if (user is null) throw new Exception("Non recognised user"); //ToDo: Consider creating custom exceptions.
        var resource = Url.Create(originalUrl, user);
        var uow = unitOfWork.GetRepository<Url>();
        await uow.AddAsync(resource);
        
        return resource.Hash;
    }

    public Task UpdateLink(string token, string url, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveLink(string url, CancellationToken cancellationToken)
    {
        var key = url.Split("/").ToList().Last();
        var uow = unitOfWork.GetRepository<Url>();
        var link = await uow.GetByIdAsync(key);
        if (link is not null )
        {
            var user = await userService.GetUserByNameOrEmail(link.User.Name, null);
            var resource = Url.Create(link.OriginalUrl, user);
            await uow.RemoveAsync(resource);
        }
    }
}