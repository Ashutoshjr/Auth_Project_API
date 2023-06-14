using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthProject.Application.DTOs
{
    public record RegisterUserDto
    {
        [Required(ErrorMessage = "Email field is required.")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Email field is required.")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Email field is required.")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Email field is required.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one letter and one digit.")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
    }


    public record UpdateUserDto 
    {
        [Required(ErrorMessage = "Id field is required.")]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

    }
}
