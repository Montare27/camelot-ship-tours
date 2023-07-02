namespace Identity.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class AuthenticationRequest 
    {
        [MinLength(5), MaxLength(20)]
        public string UserName { get; set; } = string.Empty;
        [MinLength(5), MaxLength(20)]
        public string Password { get; set; } = string.Empty;
    }
}
