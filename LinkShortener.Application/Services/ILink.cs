using System.Text.Json.Serialization;

namespace LinkShortener.Application.Services;

[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
[JsonDerivedType(typeof(Link))]
public interface ILink
{
    public Task<ILink> ConvertLink();
    
}