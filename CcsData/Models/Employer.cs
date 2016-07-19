namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class Employer
    {
        public virtual int Address_ID { get; set; }

        [Key]
        public virtual int Employer_Id { get; set; }

        [ForeignKey("Address_ID")]
        public virtual Address EmployerAddress { get; set; }

        [Display(Name=" Employer Name"), MaxLength(50)]
        public virtual string Name { get; set; }

        [MaxLength(20), Display(Name="Phone Number")]
        public virtual string PhoneNumber { get; set; }

        [MaxLength(50), Display(Name=" Supervisor Name")]
        public virtual string SupervisorName { get; set; }

        [Display(Name="Business Type"), MaxLength(50)]
        public virtual string TypeOfBusiness { get; set; }
    }
}

