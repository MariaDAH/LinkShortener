using System.Text.Json.Serialization;
using LinkShortener.Application.Models.Dtos;

namespace LinkShortener.Application.Services;

[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
[JsonDerivedType(typeof(Link))]
public interface ILink
{
    public Task<LinkDto> ConvertLink();
    
}