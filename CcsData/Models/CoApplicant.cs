namespace CcsData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class CoApplicant
    {
        [StringLength(50), Display(Name="Care Of Name: ")]
        public virtual string CareOfName { get; set; }

        [StringLength(50), Display(Name="Cell Phone: ")]
        public virtual string CellPhone { get; set; }

        [Key]
        public virtual int CoApplicant_Id { get; set; }

        [DataType(DataType.Date)]
        public virtual DateTime? DateOfBirth { get; set; }

        [Display(Name="Email Address: "), StringLength(80)]
        public virtual string EmailAddress { get; set; }

        public virtual List<Employment> Employments { get; set; }

        [Display(Name="Fax #"), DataType(DataType.PhoneNumber), StringLength(50)]
        public virtual string Fax { get; set; }

        [StringLength(50), Display(Name="First Name: "), Required]
        public virtual string FirstName { get; set; }

        [StringLength(100), Display(Name="Full Name: ")]
        public virtual string FullName { get; set; }

        [MaxLength(50)]
        public virtual string HomePhone { get; set; }

        [Display(Name="Last Name: "), Required, StringLength(50)]
        public virtual string LastName { get; set; }

        [Display(Name="Middle Name: "), StringLength(50)]
        public virtual string MiddleName { get; set; }

        public virtual List<AdditionalIncome> OtherIncomes { get; set; }

        [MaxLength(11), Display(Name="Last 4 SSN")]
        public virtual string SocialSecurity4 { get; set; }

        [Display(Name="Social Security Number: "), MaxLength(11)]
        public virtual string SocialSecurityNumber { get; set; }

        [StringLength(50)]
        public virtual string Suffix { get; set; }

        [StringLength(50), Display(Name="Work Phone: ")]
        public virtual string WorkPhone { get; set; }

        [Display(Name="Cell Phone: ")]
        public virtual int YearsInSchool { get; set; }
    }
}

