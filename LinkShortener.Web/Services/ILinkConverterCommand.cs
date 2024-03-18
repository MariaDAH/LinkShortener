using LinkShortener.Application.Models;
using LinkShortener.Application.Models.Dtos;
using LinkShortener.Application.Services;

namespace LinkShortener.Web.Services;

public interface ILinkConverterCommand
{
    public Task<LinkDto> ShareLink(Url url, int format);
}