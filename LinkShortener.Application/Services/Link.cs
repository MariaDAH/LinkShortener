using System.Text.Json.Serialization;
using LinkShortener.Application.Models.Dtos;

namespace LinkShortener.Application.Services;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$link-type", UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
[JsonDerivedType(typeof(QrLink), typeDiscriminator:"qr")]
[JsonDerivedType(typeof(TextLink), typeDiscriminator:"text")]
public abstract class Link(ILink link): ILink
{
    public string? OriginalUrl { get; set; }

    public string? ShortUrl { get; set; }
    
    public bool Format { get; set; } = false;
    
    public string? Username { get; set; }
    
    public virtual async Task<LinkDto> ConvertLink()
    {
        return await link.ConvertLink();
    }
    
}