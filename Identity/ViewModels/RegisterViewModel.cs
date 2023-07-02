namespace Identity.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        public string Role { get; set; } = string.Empty;
        [MinLength(5), MaxLength(20)]
        public string UserName { get; set; } = string.Empty;
        [MinLength(5), MaxLength(20)]
        public string Password { get; set; } = string.Empty;
        public string ReturnUrl { get; set; } = string.Empty;
    }
}
