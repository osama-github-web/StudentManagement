using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Entities
{
    public class ApplicationUser : IdentityUser
    {
        //Not Mapped
        [NotMapped]
        public string? Role { get; set; }
        [NotMapped]
        public string? Password { get; set; }
        [NotMapped]
        public string? ConfirmPassword { get; set; }

        public string Verify(ApplicationUser applicationUser)
        {
            if (applicationUser is null)
                return "All Fields are Empty";
            if (string.IsNullOrWhiteSpace(applicationUser.UserName))
                return "UserName Field is Empty";
            if (string.IsNullOrWhiteSpace(applicationUser.Email))
                return "Email Field is Empty";
            if (string.IsNullOrWhiteSpace(applicationUser.Password))
                return "Password Field is Empty";
            if (string.IsNullOrWhiteSpace(applicationUser.ConfirmPassword))
                return "ConfirmPassword Field is Empty";
            if (!applicationUser.ConfirmPassword.Equals(applicationUser.Password))
                return "Password and ConfirmPassword Does Not Match";
            return null;
        }
    }
}
