namespace Core.DTO;

public class SignUpModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhotoSrc { get; set; }
    public string Password { get; set; }

    public SignUpModel(string UserName, 
                        string Email,
                        string PhotoSrc, 
                        string Password){
        this.UserName = UserName;
        this.Email = Email;
        this.PhotoSrc = PhotoSrc;
        this.Password = Password;
    }
}
