namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class Contact
    {
        [Key]
        public virtual int Contact_Id { get; set; }

        [Required, StringLength(100), Display(Name="Email Address"), EmailAddress]
        public string EmailAddress { get; set; }

        [StringLength(100), Required, Display(Name="Full Name")]
        public string FullName { get; set; }

        [Required]
        public string Message { get; set; }

        [StringLength(250), Required]
        public string Subject { get; set; }
    }
}

