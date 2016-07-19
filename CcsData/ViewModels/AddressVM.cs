namespace CcsData.ViewModels
{
    using Models;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class AddressVM
    {
        [StringLength(50)]
        public virtual string CareOfName { get; set; }

        [StringLength(50)]
        public virtual string CarrierRoute { get; set; }

        [StringLength(50)]
        public virtual string City { get; set; }

        [StringLength(50)]
        public virtual string Country { get; set; }

        [StringLength(50)]
        public virtual string County { get; set; }

        public virtual int Id { get; set; }

        [Display(Name="Is a mail box"), DefaultValue(1)]
        public YesNoAns? IsMailBox { get; set; }

        [UIHint("CheckBox"), Display(Name="Is mailing Address")]
        public YesNoAns? IsMailingAddress { get; set; }

        [StringLength(50), Required]
        public string State { get; set; }

        [Display(Name="Street Address"), StringLength(0xff)]
        public virtual string StreetAddress { get; set; }

        [Display(Name="Years at this address")]
        public virtual int YearAtThisAddress { get; set; }

        [StringLength(20)]
        public virtual string Zip4 { get; set; }

        [Required, StringLength(20), Display(Name="Zip Code")]
        public virtual string ZipCode { get; set; }

        [StringLength(50)]
        public virtual string ZipPlusZip4 { get; set; }
    }
}

