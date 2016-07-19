namespace CcsWeb.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class RegisterViewModel
    {
        [Display(Name="Confirm password"), DataType(DataType.Password), Compare("Passwordr", ErrorMessage="The password and confirmation password do not match.")]
        public string ConfirmPasswordReg { get; set; }

        [Required, EmailAddress, Display(Name="Email")]
        public string EmailReg { get; set; }

        [Display(Name="First Name"), Required]
        public string FirstNameReg { get; set; }

        [Required, Display(Name="Last Name")]
        public string LastNameReg { get; set; }

        [StringLength(100, ErrorMessage="The {0} must be at least {2} characters long.", MinimumLength=6), DataType(DataType.Password), Display(Name="Password"), Required]
        public string PasswordReg { get; set; }
    }
}

