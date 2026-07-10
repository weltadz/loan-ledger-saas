namespace server.DTOs;

public class BusinessResponseDto
{
     public string BusinessName { get; set;} = string.Empty;

     public string OwnerName { get; set;} = string.Empty;

     public string Email { get; set;} = string.Empty;

     public string? PhoneNumber { get; set;}

     public string? Address { get; set;} = string.Empty;

     public DateTime CreatedAt { get; set;}

     public DateTime UpdatedAt { get; set;}
}