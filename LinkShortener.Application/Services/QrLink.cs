using System.Text.Json.Serialization;

namespace LinkShortener.Application.Services;

[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType)]
public class QrLink(string link) : ILink
{
    public async Task<ILink> ConvertLink()
    {
        Console.WriteLine($"Link {link} converted to QR.");
        return new QrLink(link);
    }
}