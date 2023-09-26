using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.JWT;
using StudentManagement.Entities;
using StudentManagement.Enums;
using System.Net;
using System.Security.Claims;

namespace StudentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public UserManagementController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] ApplicationUser applicationUser)
        {
            IdentityResult? _identityResult = null;
            var _applicationUser = new ApplicationUser();
            var _message = applicationUser.Verify(applicationUser);
            if (!string.IsNullOrWhiteSpace(_message))
                return BadRequest(_message);

            _message = "UserName already Exist";
            _applicationUser = await _userManager.FindByNameAsync(applicationUser.UserName);
            if (_applicationUser is not null)
                return BadRequest(_message);

            _message = "Email already Exist";
            _applicationUser = await _userManager.FindByEmailAsync(applicationUser.Email);
            if (_applicationUser is not null)
                return BadRequest(_message);

            _applicationUser = new ApplicationUser
            {
                UserName = applicationUser.UserName,
                Email = applicationUser.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            _identityResult = await _userManager.CreateAsync(_applicationUser, applicationUser.Password);
            _message = "User Not Created Successfully";
            if (!_identityResult.Succeeded)
                return BadRequest(_message);

            if (!await _roleManager.RoleExistsAsync(EDefaultRoles.Student.ToString()))
                await _roleManager.CreateAsync(new IdentityRole(EDefaultRoles.Student.ToString()));

            _identityResult = await _userManager.AddToRoleAsync(_applicationUser,EDefaultRoles.Student.ToString());
            _message = "Role not assigned to User";
            if (!_identityResult.Succeeded)
                return BadRequest(_message);
            _message = $"User Created Successfully with UserName = {applicationUser.UserName}, Email={applicationUser.Email}, Passwork={applicationUser.Password}";
            return Ok(_message);
        }        

        [HttpPost]
        [Route("Get")]
        public async Task<IActionResult> Get(ApplicationUser applicationUser)
        {
            var _applicationUser = await GetApplicationUser(applicationUser);
            return Ok(_applicationUser);
        }

        private async Task<ApplicationUser> GetApplicationUser(ApplicationUser applicationUser)
        {
            ApplicationUser _applicationUser = null;
            if (!string.IsNullOrWhiteSpace(applicationUser.Id))
                _applicationUser = await _userManager.FindByIdAsync(applicationUser.Id);
            else if (!string.IsNullOrWhiteSpace(applicationUser.UserName))
                _applicationUser = await _userManager.FindByNameAsync(applicationUser.UserName);
            else if (!string.IsNullOrWhiteSpace(applicationUser.Email))
                _applicationUser = await _userManager.FindByEmailAsync(applicationUser.Email);
            return _applicationUser;
        }
        
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            var _applicationUsers = await _userManager.Users.ToListAsync();
            return Ok(_applicationUsers);            
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ApplicationUser applicationUser)
        {
            if (string.IsNullOrWhiteSpace(applicationUser.Password) && string.IsNullOrWhiteSpace(applicationUser.ConfirmPassword))
                return BadRequest("Password or Confirm Password is Empty");
            if(applicationUser.Password != applicationUser.ConfirmPassword) 
                return BadRequest("Password or Confirm Password does not mathc");

            var _applicationUser = await _userManager.FindByNameAsync(applicationUser?.UserName);
            if (_applicationUser is null && !string.IsNullOrWhiteSpace(applicationUser.Email))
                _applicationUser = await _userManager.FindByEmailAsync(applicationUser?.Email);
            if(_applicationUser is null)
                return BadRequest("User Not Found");

            // Removing Password from User
            var _identityResult = await _userManager.RemovePasswordAsync(_applicationUser);
            if (!_identityResult.Succeeded)
                return BadRequest("Unable to Remove Password from  User");

            //Assigning New Password to User
            _identityResult = await _userManager.AddPasswordAsync(_applicationUser, applicationUser.Password);
            if (!_identityResult.Succeeded)
                return BadRequest("Previous Password is Removed but Unable to Assign New Password, Pleased Try Again");
            return Ok("Previous Password is Removed and New Password is Assigned Successfully");
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] ApplicationUser applicationUser)
        {
            var _applicationUser = await GetApplicationUser(applicationUser);
            if (_applicationUser is null)
                return BadRequest("User Not Found");

            var _identityResult = await _userManager.DeleteAsync(_applicationUser);
            if (!_identityResult.Succeeded)
                return BadRequest("Unable to Delete User");
            return BadRequest("User Deleted Successfully");
        }
    }
}
