namespace Identity.Models
{

    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Role { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string ReturnUrl { get; set; } = string.Empty;
    }
}
