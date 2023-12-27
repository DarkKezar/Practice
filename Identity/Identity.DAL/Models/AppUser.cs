using Microsoft.AspNetCore.Identity;

namespace Identity.DAL.Models;

public class AppUser : IdentityUser<Guid>
{
    public bool IsDeleted { get; set; }
    public string PhotoSrc { get; set; }

    public AppUser(string UserName, string Email, string PhotoSrc) : base(UserName)
    {
        this.IsDeleted = false;      
        this.Email = Email;
        this.NormalizedEmail = Email.ToLower();
        this.PhotoSrc = PhotoSrc;
    }
}

    