using LinkShortener.Application.Models.Dtos;
using LinkShortener.Application.Services;

namespace LinkShortener.Infrastructure.Services;

public class ConverterService(ILink link) : Link(link)
{
    private readonly ILink _link = link;

    public override async Task<LinkDto> ConvertLink()
    {
        return await _link.ConvertLink();
    }
}