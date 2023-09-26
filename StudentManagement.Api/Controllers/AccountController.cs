using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.JWT;
using StudentManagement.Entities;
using System.Net;
using System.Security.Claims;

namespace StudentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private JwtManager _jwtManager;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private AuthenticationManager _authenticationManager;
        public AccountController(JwtManager jwtManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,AuthenticationManager authenticationManager)
        {
            this._jwtManager = jwtManager;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._authenticationManager = authenticationManager;
        }

        public async Task<IActionResult> Login(ApplicationUser applicationUser)
        {
            var _applicationUser = await _userManager.FindByNameAsync(applicationUser.UserName);
            if (_applicationUser == null) 
                _applicationUser = await _userManager.FindByEmailAsync(applicationUser.Email);
            if (_applicationUser == null)
                return BadRequest("User Record Not Found");
            _applicationUser.Role = _userManager.GetRolesAsync(_applicationUser).Result.FirstOrDefault();
            int _expirationInMinutes = 120;
            var _claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid,_applicationUser.Id),
                new Claim(ClaimTypes.Surname,_applicationUser.UserName),
                new Claim(ClaimTypes.Email,_applicationUser.Email),
                new Claim(ClaimTypes.Role,_applicationUser.Role),
                new Claim(ClaimTypes.Expiration, _expirationInMinutes.ToString()),
            };
            var _clamisIdentity = new ClaimsIdentity(_claims);
            var _token = _jwtManager.GenerateToken(_clamisIdentity, _expirationInMinutes);
            if (_token == null)
                return BadRequest("Unable To Generate Token");
            return Ok(_token);
        }        
    }
}
