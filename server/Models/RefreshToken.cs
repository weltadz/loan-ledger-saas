using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class RefreshToken
{
    [Key]
    public Guid RefreshTokenId { get; set;}

    [Required]
    public Guid UserId { get; set;}

    [Required]
    public string Token { get; set;} = string.Empty;

    public DateTime ExpiresAt { get; set;}

    public DateTime CreatedAt { get; set;} = DateTime.UtcNow;

    public DateTime? RevokedAt { get; set;}

    public bool IsRevoked { get; set;} = false;

    //Navigation
    public User User { get; set;} = null!;
}