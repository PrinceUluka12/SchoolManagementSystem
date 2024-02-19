using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class TwoFactorCode
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public DateTime ExpiresAt { get; set; }
    }
}
