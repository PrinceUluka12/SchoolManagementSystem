using Identity.Data;
using Identity.Models;
using Identity.Models.Dto;
using Identity.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Identity.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IEmailService _emailService;
        public AuthService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, AppDbContext db, IJwtTokenGenerator jwtTokenGenerator, IEmailService emailService)
        {
            _roleManager = roleManager;
            _db = db;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _emailService = emailService;
        }

        public async Task<bool> AssignRole(string username, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    // create role if it does not exist
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.Username.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDTO()
                {
                    User = null,
                    Token = ""
                };
            }
            else
            {
                // Generate Token Here
                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtTokenGenerator.GenerateToken(user, roles);

                StudentDto studentDto = new()
                {
                    Email = user.Email,
                    ID = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };

                LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
                {
                    User = studentDto,
                    Token = token
                };
                return loginResponseDTO;

            }
        }

        public async Task<(string, StudentDto)> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDTO.Username,
                Email = registrationRequestDTO.Email,
                NormalizedEmail = registrationRequestDTO.Email.ToUpper(),
                FirstName = registrationRequestDTO.FirstName,
                LastName = registrationRequestDTO.LastName
            };

            try
            {
                var usernameExists = await _userManager.FindByNameAsync(registrationRequestDTO.Username) != null;
                if (usernameExists)
                {
                    return ("Username is already taken.",new StudentDto());
                }
                else
                {
                    var result = await _userManager.CreateAsync(user, registrationRequestDTO.Password);

                    if (result.Succeeded)
                    {
                        var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDTO.Username);
                        StudentDto userDTO = new()
                        {
                            Email = userToReturn.Email,
                            ID = userToReturn.Id,
                            FirstName = userToReturn.FirstName,
                            LastName = userToReturn.LastName,
                            MatricNo = userToReturn.UserName
                        };

                        return ("", userDTO);
                    }
                    else
                    {
                        return (result.Errors.FirstOrDefault().Description, new StudentDto());
                    }
                }
                
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return (ex.Message, new StudentDto());
            }
            return ("Error Encountered", new StudentDto());
        }

        
    }
    
}
