using LinkShortener.Application.Models;
using LinkShortener.Application.Models.Dtos;
using LinkShortener.Application.Services;
using LinkShortener.Infrastructure.Services;

namespace LinkShortener.Web.Services;

public class LinkConverterCommand(): ILinkConverterCommand
{
    public async Task<LinkDto> ShareLink(Url url, int format)
    {
        switch (format)
        {
            case 0:
                var testCode = new ConverterService(new TextLink(url));
                return await testCode.ConvertLink();
            case 1:
                var qrCode = new ConverterService(new QrLink(url));
                return await qrCode.ConvertLink();
            default:
                Console.WriteLine("Sharing raw");
                return null;
        }
    }
}