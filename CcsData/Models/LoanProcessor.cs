namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class LoanProcessor
    {
        [ForeignKey("AddressID")]
        public virtual CcsData.Models.Address Address { get; set; }

        public virtual int? AddressID { get; set; }

        [MaxLength(15), Display(Name="Cell Number")]
        public string CellNumber { get; set; }

        [Required, EmailAddress, Display(Name="Email")]
        public string Email { get; set; }

        [Display(Name="Fax Number"), MaxLength(15)]
        public string FaxNumber { get; set; }

        [MaxLength(50), Display(Name="First Name")]
        public string FirstName { get; set; }

        [Display(Name="Image"), MaxLength(50)]
        public string ImageFile { get; set; }

        [MaxLength(50), Display(Name="Last Name")]
        public string LastName { get; set; }

        [Key]
        public int LoanProcessor_Id { get; set; }

        [Display(Name="Phone Number"), MaxLength(15)]
        public string PhoneNumber { get; set; }

        [MaxLength(15), Display(Name="Work Number")]
        public string WorkNumber { get; set; }
    }
}

