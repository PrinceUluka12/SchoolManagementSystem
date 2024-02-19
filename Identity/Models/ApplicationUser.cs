using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<TwoFactorCode> TwoFactorCodes { get; set; } = new List<TwoFactorCode>();

    }
}
