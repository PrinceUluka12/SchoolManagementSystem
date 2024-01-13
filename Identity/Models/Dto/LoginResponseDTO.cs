namespace Identity.Models.Dto
{
    public class LoginResponseDTO
    {
        public StudentDto User { get; set; }
        public string Token { get; set; }
    }
}
