using LinkShortener.Application.Services;
using LinkShortener.Infrastructure.Services;

namespace LinkShortener.Web.Services;

public class LinkConverterCommand(): ILinkConverterCommand
{
    public async Task ShareLink(string inputUrl, int format)
    {
        switch (format)
        {
            case 1:
                var qrCode = new ConverterService(new QrLink(inputUrl));
                await qrCode.ConvertLink();
                break;
            case 2:
                var testCode = new ConverterService(new TextLink(inputUrl));
                await testCode.ConvertLink();
                break;
            default:
                Console.WriteLine("Sharing raw");
                break;
        }
    }
}