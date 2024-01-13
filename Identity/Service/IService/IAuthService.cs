using Identity.Models.Dto;

namespace Identity.Service.IService
{
    public interface IAuthService
    {
        Task<(string, StudentDto)> Register(RegistrationRequestDTO registrationRequestDTO);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);

        Task<bool> AssignRole(string email, string roleName);
    }
}
