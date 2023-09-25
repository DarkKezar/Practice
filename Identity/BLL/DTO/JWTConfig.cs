namespace BLL.DTO;

public class JWTConfig
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }

    public JWTConfig(string issuer, string audience, string key)
    {
        Issuer = issuer;
        Audience = audience;
        Key = key;
    }
}