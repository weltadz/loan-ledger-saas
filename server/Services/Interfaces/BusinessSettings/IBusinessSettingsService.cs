using server.DTOs;

namespace server.Services.Interfaces;

public interface IBusinessSettingsService
{
    Task<BusinessSettingsResponseDto> GetSettingsAsync(Guid businessId);

    Task UpdateSettingsAsync(Guid businessId, UpdateBusinessSettingsDto request);   
}