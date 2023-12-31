using Identity.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.DAL.Context;

public class AppIdentityContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public AppIdentityContext()
    { }

    public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options)
    { }
}
