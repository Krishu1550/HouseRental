namespace User.Application.DTOs
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public int AccessTokenExpirationMinutes { get; set; }
    }

}
