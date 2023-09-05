using Core.DTO;
using Microsoft.AspNetCore.Identity;

namespace Core.Models;


/* IdentityUser<TKey> field:
    TKey Id	
    AccessFailedCount	
    ConcurrencyStamp	
    Email	
    EmailConfirmed	
    LockoutEnabled	
    LockoutEnd	
    NormalizedEmail	
    NormalizedUserName	
    PasswordHash	
    PhoneNumber	
    PhoneNumberConfirmed	
    SecurityStamp	
    TwoFactorEnabled	
    UserName	
*/

public class AppUser : IdentityUser<Guid>
{
    public bool IsDeleted { get; set; }
    public string PhotoSrc { get; set; }

    public AppUser(string UserName, string Email, string PhotoSrc) : base(UserName){
        this.IsDeleted = false;
        
        this.Email = Email;
        this.NormalizedEmail = Email.ToLower();
        this.PhotoSrc = PhotoSrc;
    }

    public AppUser(SignUpModel Model) : base(Model.UserName){
        this.IsDeleted = false;

        this.Email = Model.Email;
        this.NormalizedEmail = Model.Email.ToLower();
        this.PhotoSrc = Model.PhotoSrc;
    }

    public AppUser() : base("default") {
        this.IsDeleted = false;
        this.PhotoSrc = "";
    }
}

    