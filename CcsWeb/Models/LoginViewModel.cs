namespace CcsWeb.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class LoginViewModel
    {
        [DataType(DataType.Password), Required, Display(Name="Password")]
        public string Password { get; set; }

        [Display(Name="Remember me?")]
        public bool RememberMe { get; set; }

        [Required, Display(Name="User Name")]
        public string UserName { get; set; }
    }
}

