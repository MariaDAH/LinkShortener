using LinkShortener.Application.Services;

namespace LinkShortener.Infrastructure.Services;

public class ConverterService(ILink link) : Link(link)
{
    private readonly ILink _link = link;

    public override async Task<ILink> ConvertLink()
    {
        var linkConverted = await _link.ConvertLink();
        return await ShareLink(linkConverted);
    }

    private async Task<ILink> ShareLink(ILink link)
    {
        return link;
    }
}