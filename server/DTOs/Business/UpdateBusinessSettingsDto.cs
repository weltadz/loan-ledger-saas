namespace server.DTOs;  

public class UpdateBusinessSettingsDto
{
    public decimal DefaultInterestRate { get; set; }

    public decimal DefaultPenaltyRate { get; set; }

    public int DefaultLoanTermDays { get; set; }

    public string Currency { get; set; } = string.Empty;

    public bool AllowPartialPayments { get; set; }
}