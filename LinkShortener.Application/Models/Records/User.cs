using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkShortener.Application.Models;

[Table("User")]
public record User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }
    public string? Name { get; private set; }
    public string? Email { get; private set; }
    
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedDate { get; init; }
    
    //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? LastLogin { get; private set; }

    public static User Create(string name, string email)
    {
        return new User()
        {
            Name = name,
            Email = email,
            CreatedDate = DateTime.Now
        };
    }

    public void Update(User user)
    {
        Name = user.Name;
        Email = user.Email;
        LastLogin = DateTime.Now;
    }
}