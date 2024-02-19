using System.ComponentModel.DataAnnotations;

namespace Identity.Models.Dto
{
    public class PasswordResetVerificationDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\w\d\s]).{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string NewPassword { get; set; }
    }
}
