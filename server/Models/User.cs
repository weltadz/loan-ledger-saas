using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class User
{
    [Key]
    public Guid UserId {get; set;}

    [Required]
    public Guid BusinessId {get; set;}

    [Required]
    [MaxLength(155)]
    public string FullName {get; set;} = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(60)]
    public string Email {get; set;} = string.Empty;

    [Required]
    public string PasswordHash {get; set;} = string.Empty;

    public bool IsActive {get; set;} = true;

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;

    //Navigation
    public Business Business {get; set;} = null!;

    public ICollection<RefreshToken> RefreshTokens {get; set;} = new List<RefreshToken>();
}