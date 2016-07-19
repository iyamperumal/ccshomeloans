namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class LoanOfficer
    {
        [ForeignKey("AddressID")]
        public virtual CcsData.Models.Address Address { get; set; }

        public virtual int? AddressID { get; set; }

        [Display(Name="Cell Number"), MaxLength(15)]
        public string CellNumber { get; set; }

        [Required, EmailAddress, Display(Name="Email")]
        public string Email { get; set; }

        [Display(Name="Fax Number"), MaxLength(15)]
        public string FaxNumber { get; set; }

        [Display(Name="First Name"), MaxLength(50)]
        public string FirstName { get; set; }

        [Display(Name="Image"), MaxLength(50)]
        public string ImageFile { get; set; }

        [MaxLength(50), Display(Name="Last Name")]
        public string LastName { get; set; }

        [Key]
        public int LoanOfficer_Id { get; set; }

        [Display(Name="Phone Number"), MaxLength(15)]
        public string PhoneNumber { get; set; }

        [Display(Name="Work Number"), MaxLength(15)]
        public string WorkNumber { get; set; }
    }
}

