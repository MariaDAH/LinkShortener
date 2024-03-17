namespace LinkShortener.Web.Services;

public interface ILinkConverterCommand
{
    public Task ShareLink(string inputYrl, int format);
}