using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    DbSet<Business> Businesses => Set<Business>();
    DbSet<User> Users => Set<User>();
    DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
}