namespace BLL.DTO;

public class SignUpModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhotoSrc { get; set; }
    public string Password { get; set; }

    public SignUpModel()
    { }
}
