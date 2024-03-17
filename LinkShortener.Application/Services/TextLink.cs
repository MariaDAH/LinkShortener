using System.Text.Json.Serialization;

namespace LinkShortener.Application.Services;

[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType)]
public class TextLink(string link) : ILink
{
    public async Task<ILink> ConvertLink()
    {
        Console.WriteLine($"Link {link} converted to text.");
        return new TextLink(link);
    }

    /*private TestLink()
    {
         string text = File.ReadAllText(@"./person.json");
         var person = JsonSerializer.Deserialize<>(text);
    }*/
}