using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthProject.Domain
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "The Email field is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The NewPassword field is required.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "The ConfirmPassword field is required.")]
        public string ConfirmPassword { get; set; }
    }
}
