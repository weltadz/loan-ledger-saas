using server.DTOs;

namespace server.Interface;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);

    Task<AuthResponseDto> LoginAsync(LoginRequestDto request);

    Task<AuthResponseDto> RefreshAccessTokenAsync(RefreshTokenRequestDto request);

    Task LogoutAsync(RefreshTokenRequestDto request);
}