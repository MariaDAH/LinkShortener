namespace LinkShortener.Application.Services;

public interface IQRGeneratorService
{
    public Task<string> GenerateQRService(string url, CancellationToken cancellationToken = default);
}