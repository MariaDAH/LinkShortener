using LinkShortener.Application.Models;
using LinkShortener.Application.Models.Dtos;

namespace LinkShortener.Web.Services;

public interface ILinkConverterCommand
{
    public Task<LinkDto> ShareLink(Url url, int format);
}