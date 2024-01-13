using School.Models.DTO.IdentityDTO;

namespace School.Services.IdentityService
{
    public interface IIdentityService
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO request);
        Task<StudentDto> Register(RegistrationRequestDTO model);
    }
}
