namespace server.DTOs;

public class UpdateBusinessDto
{
    public string BusinessName { get; set;} = string.Empty;

    public string OwnerName { get; set;} = string.Empty;

    public string Email { get; set;} = string.Empty;

    public string PhoneNumber { get; set;} = string.Empty;

    public string Address { get; set;} = string.Empty;
}