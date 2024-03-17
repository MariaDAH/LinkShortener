namespace LinkShortener.Web.Services;

public interface IApiKeyValidation
{
    bool IsValidApiKey(string userApiKey);
}