using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.DTOs;
using server.Services.Interfaces;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BusinessController : ControllerBase
{
    private readonly IBusinessService _businessService;

    public BusinessController(IBusinessService businessService)
    {
        _businessService = businessService;
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetBusinessProfile()
    {
        var claimsBusinessId = User.FindFirst("businessId")?.Value;
        var businessId = Guid.Parse(claimsBusinessId!);

        var response = await _businessService.GetBusinessProfileAsync(businessId);
        return Ok(response);
    }

    [HttpPut("update")]
    [Authorize]
    public async Task<IActionResult> UpdateBusinessProfile(UpdateBusinessDto request)
    {
        var claimsBusinessId = User.FindFirst("businessId")?.Value;
        var businessId = Guid.Parse(claimsBusinessId!);

        await _businessService.UpdateBusinessProfileAsync(businessId, request);

        return Ok(new{message = "Update successfully"});
    }
}