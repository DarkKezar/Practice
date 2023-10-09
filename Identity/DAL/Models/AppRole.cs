using Microsoft.AspNetCore.Identity;

namespace DAL.Models;

public class AppRole : IdentityRole<Guid>
{
    public AppRole(string RoleName) : base(RoleName) {}
    public AppRole() : base("default") {}
}
