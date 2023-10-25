using System.ComponentModel.DataAnnotations;

namespace TexCode.ViewModels
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}