namespace Core.DTO;

public class AuthModel
{
    public string Email { get; set; }
    public string Password { get; set; }

    public AuthModel(string Email, string Password){
        this.Email = Email;
        this.Password = Password;
    }
}
