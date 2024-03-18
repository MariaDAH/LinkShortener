using LinkShortener.Application.Models;
using LinkShortener.Application.Services;

namespace LinkShortener.Infrastructure.Services;

public interface IShortenerService
{
    public Task<Url?> GetLink(string url, CancellationToken cancellationToken);
    
    public Task<string> AddShortLink(string url, string userName, CancellationToken cancellationToken);

    public Task UpdateLink(string token, string url, CancellationToken cancellationToken);

    public Task RemoveLink(string url, CancellationToken cancellationToken);
}