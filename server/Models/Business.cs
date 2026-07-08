using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class Business
{
    public Guid BusinessId { get; set;}

    [Required]
    [MaxLength(100)]
    public string BusinessName { get; set;} = string.Empty;

    [Required]
    [MaxLength(100)]
    public string OwnerName { get; set;} = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set;} = string.Empty;

    [MaxLength(11)]
    public string? PhoneNumber { get; set;}

    [MaxLength(255)]
    public string? Address { get; set;}

    public DateTime CreatedAt { get; set;} = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set;} = DateTime.UtcNow;

    //Navigation
    public ICollection<User> Users { get; set;} = new List<User>();

    public BusinessSetting BusinessSetting { get; set;} = null!;
}