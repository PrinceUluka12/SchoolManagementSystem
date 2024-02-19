namespace Identity.Service.IService
{
    public interface IPasswordResetService
    {
        Task<bool> RequestPasswordResetAsync(string email);
        Task<bool> VerifyPasswordResetCodeAsync(string userId, string code, string newPassword);
    }
}
