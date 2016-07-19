namespace CcsData.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class ApplicantVm
    {
        [DataType(DataType.Time), Display(Name="Call Back Date & Time: ")]
        public virtual DateTime? CallBackTime { get; set; }

        [DataType(DataType.PhoneNumber), StringLength(50), Display(Name="Cell Phone: ")]
        public virtual string CellPhone { get; set; }

        [DataType(DataType.EmailAddress), MaxLength(50)]
        public virtual string EmailAddress { get; set; }

        [StringLength(100), Display(Name="Full Name: ")]
        public virtual string FullName { get; set; }

        [Display(Name="Home Phone: "), DataType(DataType.PhoneNumber), MaxLength(50)]
        public virtual string HomePhone { get; set; }
    }
}

