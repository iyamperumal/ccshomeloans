namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class Realtor
    {
        [ForeignKey("AddressID")]
        public virtual CcsData.Models.Address Address { get; set; }

        public virtual int? AddressID { get; set; }

        [MaxLength(15), Display(Name="Cell Number")]
        public string CellNumber { get; set; }

        [MaxLength(50), Display(Name="Company Name")]
        public string CompanyName { get; set; }

        [Required, EmailAddress, Display(Name="Email")]
        public string Email { get; set; }

        [MaxLength(15), Display(Name="Fax Number")]
        public string FaxNumber { get; set; }

        [MaxLength(50), Display(Name="First Name")]
        public string FirstName { get; set; }

        [MaxLength(50), Display(Name="Image")]
        public string ImageFile { get; set; }

        [MaxLength(50), Display(Name="Last Name")]
        public string LastName { get; set; }

        [MaxLength(15), Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }

        [Key]
        public int Realtor_Id { get; set; }

        [MaxLength(15), Display(Name="Work Number")]
        public string WorkNumber { get; set; }
    }
}

