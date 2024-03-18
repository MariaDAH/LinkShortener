using System.Text.Json.Serialization;
using LinkShortener.Application.Models;
using LinkShortener.Application.Models.Dtos;

namespace LinkShortener.Application.Services;

[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType)]
public class TextLink(Url url) : ILink
{
    private const string URLDOMAIN = "http://shorturl/";
    
    public async Task<LinkDto> ConvertLink()
    {
        return await new TextLink(url).ReturnLink(url);
    }

    private async Task<LinkDto> ReturnLink(Url url)
    {
        var shortUrl = string.Concat(URLDOMAIN, url.Hash);
        Console.WriteLine($"Link {url.OriginalUrl} converted to text: {shortUrl}");
        
        return new LinkDto
        {
            ShortUrl = shortUrl,
            Hash = url.Hash,
            Location = url.OriginalUrl
        };
    }
}