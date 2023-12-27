using Identity.DLL.Proto;

namespace Identity.BLL.DTO;

public class SignUpModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhotoSrc { get; set; }
    public string Password { get; set; }
    public string Biography { get; set; }
    public double Salary { get; set; }
}
