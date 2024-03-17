using LinkShortener.Application.Services;

namespace LinkShortener.Infrastructure.Services;

public interface IShortenerService
{
    public Task<string> GetShortLink(string url, CancellationToken cancellationToken);
    
    public Task AddShortLink(string url, string userName, CancellationToken cancellationToken);

    public Task UpdateLink(string token, string url, CancellationToken cancellationToken);

    public Task RemoveLink(string url, CancellationToken cancellationToken);
}