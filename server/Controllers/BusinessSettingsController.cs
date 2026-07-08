using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Services.Interfaces;
using server.DTOs;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BusinessSettingsController : ControllerBase
{
    private readonly IBusinessSettingsService _businessSettings;

    public BusinessSettingsController(IBusinessSettingsService businessSettings)
    {
        _businessSettings = businessSettings;
    }

    [HttpGet("get")]
    [Authorize]
    public async Task<IActionResult> GetSettings()
    {
        var claimsBusinessId = User.FindFirst("businessId")?.Value;
        var businessId = Guid.Parse(claimsBusinessId!);

        var response = await _businessSettings.GetSettingsAsync(businessId);

        return Ok(response);
    }

    [HttpPut("update")]
    [Authorize]
    public async Task<IActionResult> UpdateSettings(UpdateBusinessSettingsDto request)
    {
        var claimsBusinessId = User.FindFirst("businessId")?.Value;
        var businessId = Guid.Parse(claimsBusinessId!);

        await _businessSettings.UpdateSettingsAsync(businessId, request);

        return Ok(new{message = "Settings updated successfully"});
    }
}