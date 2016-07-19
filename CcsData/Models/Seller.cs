namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class Seller
    {
        [ForeignKey("AddressID")]
        public virtual CcsData.Models.Address Address { get; set; }

        public virtual int? AddressID { get; set; }

        [Display(Name="Cell Number"), MaxLength(15)]
        public string CellNumber { get; set; }

        [MaxLength(50), Display(Name="CompanyName")]
        public string CompanyName { get; set; }

        [Display(Name="Fax Number"), MaxLength(15)]
        public string FaxNumber { get; set; }

        [Display(Name="First Name"), StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name="Last Name"), StringLength(50)]
        public string LastName { get; set; }

        [Display(Name="Phone Number"), MaxLength(15)]
        public string PhoneNumber { get; set; }

        [Key]
        public int Seller_Id { get; set; }

        [MaxLength(15), Display(Name="Work Number")]
        public string WorkNumber { get; set; }
    }
}

