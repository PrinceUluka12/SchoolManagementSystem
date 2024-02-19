using Identity.Data;
using Identity.Models;
using Identity.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Identity.Service
{
    public class PasswordResetService : IPasswordResetService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _db;
        private readonly IEmailService _emailService;

        public PasswordResetService(UserManager<ApplicationUser> userManager, AppDbContext db, IEmailService emailService)
        {
            _userManager = userManager;
            _db = db;
            _emailService = emailService;
        }

        public async Task<bool> RequestPasswordResetAsync(string username)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                    return false;

                // Generate a unique code and store it in the database
                var code = Generate2faCode();
                var expiration = DateTime.UtcNow.AddMinutes(15); // Adjust expiration as needed

                user.TwoFactorCodes.Add(new TwoFactorCode { Code = code, ExpiresAt = expiration, UserName = username});

                await _userManager.UpdateAsync(user);
                string body = $"Dear {user.LastName} ,\r\n\r\nYour Two-Factor Authentication (2FA) code is: {code}\r\n\r\nPlease use this code to complete the password reset process. Note that this code is only valid for 15 minutes.\r\n\r\nIf you didn't request this code, please ignore this email.\r\n\r\nThank you\r\n";

                Notification notification = new Notification()
                {
                    Body = body,
                    To = user.Email,
                    CreatedDate = DateTime.Now,
                    Subject = "Your Two-Factor Authentication Code",
                    NotificationType = 1

                };
                _emailService.SendEmail(notification);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public async Task<bool> VerifyPasswordResetCodeAsync(string username, string code, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return false;

            // Retrieve the code from the database
            var storedCode = user.TwoFactorCodes
                .FirstOrDefault(c => c.Code == code && c.ExpiresAt >= DateTime.UtcNow);

            if (storedCode == null)
                return false;

            // Reset the password
            var result = await _userManager.ResetPasswordAsync(user, storedCode.Code, newPassword);
            if (result.Succeeded)
            {
                // Remove the used code
                user.TwoFactorCodes.Remove(storedCode);
                await _userManager.UpdateAsync(user);
            }

            return result.Succeeded;
        }
        private string Generate2faCode()
        {
            Random random = new Random();
            const int codeLength = 6;

            // Generate a random 6-digit numeric code
            string code = "";
            for (int i = 0; i < codeLength; i++)
            {
                code += random.Next(0, 10).ToString();
            }

            return code;
        }
    }
}
