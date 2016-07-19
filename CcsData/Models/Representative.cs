namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class Representative
    {
        [StringLength(50)]
        public virtual string Email { get; set; }

        [Display(Name="Fire Date"), DataType(DataType.Date)]
        public virtual DateTime? FireDate { get; set; }

        [Display(Name="First Name"), StringLength(50)]
        public virtual string FirstName { get; set; }

        [Display(Name="Full Name"), MaxLength(100)]
        public virtual string FullName { get; set; }

        [DataType(DataType.Date), Display(Name="Hire Date")]
        public virtual DateTime? HireDate { get; set; }

        [StringLength(50), Display(Name="last Name")]
        public virtual string LastName { get; set; }

        [Key]
        public virtual int Representative_Id { get; set; }
    }
}

