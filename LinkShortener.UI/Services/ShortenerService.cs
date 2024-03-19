using System.Net.Http.Headers;
using System.Text.Json;
using LinkShortener.UI.Pages;

namespace LinkShortener.UI.Services;

public class ShortenerService(HttpClient httpClient, IConfiguration configuration): IShortenerService
{
    public async Task<LinkShare.Link?> ShareLink(string originalUrl, bool format, CancellationToken cancellationToken = default)
    {
        var apiKey = configuration.GetValue<string>("ApiKey");
        var username = configuration.GetValue<string>("Username");

        var body = new LinkShare.Link
        {
            OriginalUrl = originalUrl,
            Username = username,
            Format = format
        };

        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"shortlink/link?originalUrl={body.OriginalUrl}&userName={body.Username}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("X-API-Key", apiKey);
        
            var response = await httpClient.SendAsync(request, cancellationToken); 
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken) ?? String.Empty;
            response.EnsureSuccessStatusCode();
            
            return await ConvertToFormatAsync(responseContent, Convert.ToInt32(format));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private async Task<LinkShare.Link?> ConvertToFormatAsync(string hash, int format)
    {
        var response = await httpClient.GetAsync($"shortlink/link?hash={hash}&option={format}"); ;
        var responseContent = response.Content.ReadAsStringAsync().Result ?? String.Empty;
        response.EnsureSuccessStatusCode();
        return JsonSerializer.Deserialize<LinkShare.Link>(responseContent);
    }
}