using System.Text.Json.Serialization;
using LinkShortener.Application.Models;
using LinkShortener.Application.Models.Dtos;

namespace LinkShortener.Application.Services;

[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType)]
public class QrLink(Url url) : ILink //, IOtherService
{
    public async Task<LinkDto> ConvertLink()
    {
        Console.WriteLine($"Link {url.OriginalUrl} converted to QR.");
        return await new QrLink(url).ReturnLink(url);
    }
    
    private async Task<LinkDto> ReturnLink(Url url)
    {
        return new LinkDto();
    }
    
    //Implement a method of the OtherService, which is a client for a third party system that generates the QR??
}