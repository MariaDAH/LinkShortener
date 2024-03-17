namespace LinkShortener.Web.Services;

public class ApiKeyValidation(IConfiguration configuration) : IApiKeyValidation
{
    public bool IsValidApiKey(string userApiKey)
    {
        if (string.IsNullOrWhiteSpace(userApiKey))
            return false;
        string? apiKey = configuration.GetValue<string>(Constants.ApiKeyHeaderName);
        if (apiKey == null || apiKey != userApiKey)
            return false;
        return true;
    }
}