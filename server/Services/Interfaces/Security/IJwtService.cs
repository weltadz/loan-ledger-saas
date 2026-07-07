using server.Models;

namespace server.Services.Interfaces.Security;

public interface IJwtService
{
    public string GenerateAccessToken(User user);
    public string GenerateRefreshToken();
}