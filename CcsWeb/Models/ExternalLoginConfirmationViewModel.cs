namespace CcsWeb.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class ExternalLoginConfirmationViewModel
    {
        [Display(Name="Email"), Required, EmailAddress]
        public string Email { get; set; }
    }
}

