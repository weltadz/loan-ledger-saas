using server.Services.Interfaces;
using server.Data;
using server.DTOs;
using Microsoft.EntityFrameworkCore;

namespace server.Services;

public class BusinessSettingsService : IBusinessSettingsService
{
    private readonly  ApplicationDbContext _dbContext;

    public BusinessSettingsService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BusinessSettingsResponseDto> GetSettingsAsync(Guid businessId)
    {
        var businessSetting = await _dbContext.BusinessSettings
        .FirstOrDefaultAsync(b => b.BusinessId == businessId);

        if(businessSetting == null)
        {
            throw new KeyNotFoundException("Business not found");
        }

        return new BusinessSettingsResponseDto
        {
            DefaultInterestRate = businessSetting.DefaultInterestRate,
            DefaultPenaltyRate = businessSetting.DefaultPenaltyRate,
            DefaultLoanTermDays = businessSetting.DefaultLoanTermDays,
            Currency = businessSetting.Currency,
            AllowPartialPayments = businessSetting.AllowPartialPayments
        };
    }

    public async Task UpdateSettingsAsync(Guid businessId, UpdateBusinessSettingsDto request)
    {
        var businessSetting = await _dbContext.BusinessSettings
        .FirstOrDefaultAsync(b => b.BusinessId == businessId);

        if(businessSetting == null)
        {
            throw new KeyNotFoundException("Business not found");
        }

        businessSetting.DefaultInterestRate = request.DefaultInterestRate;
        businessSetting.DefaultPenaltyRate = request.DefaultPenaltyRate;
        businessSetting.DefaultLoanTermDays = request.DefaultLoanTermDays;
        businessSetting.Currency = request.Currency;
        businessSetting.AllowPartialPayments = request.AllowPartialPayments;

        await _dbContext.SaveChangesAsync();
    }
}