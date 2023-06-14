using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthProject.Application.DTOs
{
    public record AuthenticateUserDto
    {
        [Required(ErrorMessage = "Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; init; }

        [Required(ErrorMessage = "The Password field is required.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one letter and one digit.")]
        public string Password { get; init; }
    }
}
