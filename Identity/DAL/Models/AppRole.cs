using Microsoft.AspNetCore.Identity;

namespace DAL.Models;


/* IdentityRole<TKey> fields:
    TKey Id
    ConcurrencyStamp	
    Name	
    NormalizedName	
*/

public class AppRole : IdentityRole<Guid>
{
    public AppRole(string RoleName) : base(RoleName) {}
    public AppRole() : base("default") {}
}
