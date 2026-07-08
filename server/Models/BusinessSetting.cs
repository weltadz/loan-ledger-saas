using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class BusinessSetting
{
    public Guid BusinessSettingId { get; set; }

    public Guid BusinessId { get; set; }

    public decimal DefaultInterestRate { get; set; }

    public decimal DefaultPenaltyRate { get; set; }

    public int DefaultLoanTermDays { get; set; }

    public string Currency { get; set; } = "PHP";

    public bool AllowPartialPayments { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    //Navigation
    public Business Business { get; set; } = null!;
}