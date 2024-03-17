using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkShortener.Application.Models;

[Table("Url")]
public record Url
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string? Hash { get; set; }
    public string? OriginalUrl { get; private set; }

    //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreationDate { get; private set; } = DateTime.Now;

    //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime ExpirationDate { get; private set; } = DateTime.Now.AddMonths(1);
    public User? User { get; init; }

    public static Url Create(string originalUrl, User user)
    {
        return new()
        {
            OriginalUrl = originalUrl,
            User = user
        };
    }

    public void Update(string originalUrl)
    {
        OriginalUrl = originalUrl;
        CreationDate = DateTime.Now;
        ExpirationDate = DateTime.Now.AddMonths(1);
    }
}