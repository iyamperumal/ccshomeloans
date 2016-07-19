namespace CcsData.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class App5
    {
        [Required(ErrorMessage="Enter your Mailing Address"), Display(Name="Address")]
        public string Address { get; set; }

        [Required(ErrorMessage="Enter you City"), Display(Name="City")]
        public string City { get; set; }

        [UIHint("Password"), DataType(DataType.Password), Display(Name="Confirm password"), Compare("Passwordr", ErrorMessage="The password and confirmation password do not match.")]
        public string ConfirmPasswordr { get; set; }

        [Display(Name="Email"), Required(ErrorMessage="* Email Address is required"), EmailAddress, DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Display(Name="First Name"), Required]
        public string FirstName { get; set; }

        [Required, Display(Name="Last Name")]
        public string LastName { get; set; }

        [Required, DataType(DataType.Password), Display(Name="Password"), UIHint("password"), StringLength(100, ErrorMessage="The {0} must be at least {2} characters long.", MinimumLength=6)]
        public string Passwordr { get; set; }

        [Required(ErrorMessage="Enter you Phone Number "), Display(Name="Phone")]
        public string Phone { get; set; }

        [Display(Name="State"), Required(ErrorMessage="Enter your State")]
        public string State { get; set; }

        [Required(ErrorMessage="Enter your Zipcode"), Display(Name="Zip/Postal Code")]
        public string ZipCode { get; set; }
    }
}

