using server.DTOs;

namespace server.Services.Interfaces;

public interface IBusinessService
{
    Task<BusinessResponseDto> GetBusinessProfileAsync(Guid businessId);

    Task UpdateBusinessProfileAsync(Guid businessId, UpdateBusinessDto request);
}