using Edblock.IdentityServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Edblock.Library.Data;

public class UsersDbContext(DbContextOptions<UsersDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder) =>
        base.OnModelCreating(builder);
}