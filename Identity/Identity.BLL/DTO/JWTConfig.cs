namespace Identity.BLL.DTO;

public class JWTConfig
{
    public string Issuer { get; set; } = null;
    public string Audience { get; set; } = null;
    public string Key { get; set; } = null;
}