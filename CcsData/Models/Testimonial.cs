namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class Testimonial
    {
        [ForeignKey("ApplicantID")]
        public virtual CcsData.Models.Applicant Applicant { get; set; }

        public virtual int? ApplicantID { get; set; }

        [Display(Name="Application Number: "), Required, StringLength(50)]
        public virtual string ApplicationNumber { get; set; }

        [Display(Name="City"), StringLength(50)]
        public string City { get; set; }

        [StringLength(0x400), Display(Name="Comment")]
        public string Comment { get; set; }

        [Display(Name="First Name: "), Required, StringLength(50)]
        public virtual string FirstName { get; set; }

        [Display(Name="Last Name: "), Required, StringLength(50)]
        public virtual string LastName { get; set; }

        [Display(Name="Middle Name: "), StringLength(50)]
        public virtual string MiddleName { get; set; }

        [StringLength(50), Display(Name="State")]
        public string State { get; set; }

        [Key]
        public virtual int Testimonial_Id { get; set; }
    }
}

