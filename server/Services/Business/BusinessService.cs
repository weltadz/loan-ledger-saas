using System.Drawing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using server.Data;
using server.DTOs;
using server.Services.Interfaces;

namespace server.Services;

public class BusinessService : IBusinessService
{
    private readonly ApplicationDbContext _dbContext;

    public BusinessService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BusinessResponseDto> GetBusinessProfileAsync(Guid businessId)
    {
        var business = await _dbContext.Businesses
        .FirstOrDefaultAsync(b => b.BusinessId == businessId);

        if(business == null)
        {
            throw new KeyNotFoundException("Business not found");
        }

        return new BusinessResponseDto
        {
            BusinessName = business.BusinessName,
            OwnerName = business.OwnerName,
            Email = business.Email,
            PhoneNumber = business.PhoneNumber,
            Address = business.Address,
            CreatedAt = business.CreatedAt,
            UpdatedAt = business.UpdatedAt
        };
    }

    public async Task UpdateBusinessProfileAsync(Guid businessId, UpdateBusinessDto request)
    {
        var business = await _dbContext.Businesses
        .FirstOrDefaultAsync(b => b.BusinessId == businessId);

        if(business == null)
        {
            throw new KeyNotFoundException("Business not found");
        }

        business.BusinessName = request.BusinessName;
        business.OwnerName = request.OwnerName;
        business.Email = request.Email;
        business.PhoneNumber = request.PhoneNumber;
        business.Address = request.Address;
        business.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
    }
}