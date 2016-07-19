namespace CcsWeb.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class ManageUserViewModel
    {
        [Display(Name="Confirm new password"), Compare("NewPassword", ErrorMessage="The new password and confirmation password do not match."), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name="New password"), Required, StringLength(100, ErrorMessage="The {0} must be at least {2} characters long.", MinimumLength=6), DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password), Display(Name="Current password")]
        public string OldPassword { get; set; }
    }
}

