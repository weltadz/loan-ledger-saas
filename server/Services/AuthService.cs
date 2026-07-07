using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTOs;
using server.Interface;
using server.Models;
using server.Services.Interfaces;
using server.Services.Interfaces.Security;


namespace server.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public AuthService(ApplicationDbContext dbContext,IPasswordHasher passwordHasher, IJwtService jwtService)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
    {
        var existingUser = await _dbContext.Users
        .FirstOrDefaultAsync(u => u.Email == request.Email);

        if(existingUser != null)
        {
            throw new InvalidOperationException("Email is already registered");
        }

        var business = new Business
        {
            BusinessId = Guid.NewGuid(),
            BusinessName = request.BusinessName,
            OwnerName = request.FullName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var user = new User
        {
            UserId = Guid.NewGuid(),
            BusinessId = business.BusinessId,
            FullName = request.FullName,
            Email = request.Email,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        user.PasswordHash = _passwordHasher.HashPassword(user,request.Password);

        var refreshToken = _jwtService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            RefreshTokenId = Guid.NewGuid(),
            UserId = user.UserId,
            Token = refreshToken,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        _dbContext.Businesses.Add(business);
        _dbContext.Users.Add(user);
        _dbContext.RefreshTokens.Add(refreshTokenEntity);

        await _dbContext.SaveChangesAsync();

        var accessToken = _jwtService.GenerateAccessToken(user);

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            FullName = user.FullName,
            Email =  user.Email
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _dbContext.Users
        .FirstOrDefaultAsync(u => u.Email == request.Email);

        if(user == null)
        {
            throw new KeyNotFoundException("Invalid Credentials");
        }

        var verifyPassword = _passwordHasher.VerifyPassword(user, user.PasswordHash, request.Password);

        if(verifyPassword == PasswordVerificationResult.Failed)
        {
            throw new InvalidOperationException("Invalid Credentials");
        }

        var accessToken = _jwtService.GenerateAccessToken(user);

        var refreshToken = _jwtService.GenerateRefreshToken();

        var refreshTokenEntity = await _dbContext.RefreshTokens
        .FirstOrDefaultAsync(r => r.UserId == user.UserId);

        if(refreshTokenEntity != null)
        {
            refreshTokenEntity.Token = refreshToken;
            refreshTokenEntity.CreatedAt = DateTime.UtcNow;
            refreshTokenEntity.ExpiresAt = DateTime.UtcNow.AddDays(7);
            refreshTokenEntity.IsRevoked = false;

        }
        else
        {
            refreshTokenEntity = new RefreshToken
            {
                RefreshTokenId = Guid.NewGuid(),
                UserId = user.UserId,
                Token = refreshToken,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            _dbContext.RefreshTokens.Add(refreshTokenEntity);
        }

        await _dbContext.SaveChangesAsync();

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            FullName = user.FullName,
            Email = user.Email
        };
    }
}