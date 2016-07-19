namespace CcsWeb.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class ResetPasswordViewModel
    {
        public string Code { get; set; }

        [Display(Name="Confirm password"), DataType(DataType.Password), Compare("Password", ErrorMessage="The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required, Display(Name="Email"), EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(100, ErrorMessage="The {0} must be at least {2} characters long.", MinimumLength=6), DataType(DataType.Password), Display(Name="Password")]
        public string Password { get; set; }
    }
}

