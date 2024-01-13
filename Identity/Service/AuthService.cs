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
        public AuthService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, AppDbContext db, IJwtTokenGenerator jwtTokenGenerator)
        {
            _roleManager = roleManager;
            _db = db;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string matricNo, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.MatricNo.ToLower() == matricNo.ToLower());
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
                UserName = registrationRequestDTO.MatricNo,
                Email = registrationRequestDTO.Email,
                NormalizedEmail = registrationRequestDTO.Email.ToUpper(),
                FirstName = registrationRequestDTO.FirstName,
                LastName = registrationRequestDTO.LastName, 
                MatricNo = registrationRequestDTO.MatricNo,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDTO.Password);

                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDTO.MatricNo);
                    StudentDto userDTO = new()
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        FirstName = userToReturn.FirstName,
                        LastName = userToReturn.LastName,
                        MatricNo =  userToReturn.MatricNo
                    };

                    return ("", userDTO);
                }
                else
                {
                    return (result.Errors.FirstOrDefault().Description, new StudentDto());
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return ("Error Encountered", new StudentDto());
        }
    }
    
}
