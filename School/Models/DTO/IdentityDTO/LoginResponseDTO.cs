namespace School.Models.DTO.IdentityDTO
{
    public class LoginResponseDTO
    {
        public StudentDto User { get; set; }
        public string Token { get; set; }
    }
}
