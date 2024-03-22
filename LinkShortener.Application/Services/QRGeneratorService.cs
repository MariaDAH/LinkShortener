namespace LinkShortener.Application.Services;

public class QRGeneratorService(HttpClient httpClient): IQRGeneratorService
{
    public async Task<string> GenerateQRService(string url, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"http://api.qrserver.com/v1/create-qr-code/?size=150x150&data={url}"); ;
        var responseContent = await response.Content.ReadAsByteArrayAsync(cancellationToken);
        var base64StringContent = Convert.ToBase64String(responseContent);
        return base64StringContent;
    }
}