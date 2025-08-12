namespace User.Application.DTOs
{
    public class RegisterDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string PhoneNumber { get; set; }
        public List<string> Roles { get; set; } = new List<string>();   
    }
}
