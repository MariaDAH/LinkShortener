namespace LinkShortener.Application.Models.Dtos;

public record LinkDto
{
    public string? Hash { get; set; }
    public string? Location { get; set; }
    public string? ShortUrl { get; set; }
    
    public byte[]? Image { get; set; }
}