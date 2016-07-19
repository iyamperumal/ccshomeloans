namespace CcsData.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class Address
    {
        public Address()
        {
            this.AddressDate = DateTime.Now;
        }

        [Key]
        public virtual int Address_Id { get; set; }

        public virtual DateTime AddressDate { get; set; }

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

        [Display(Name="Is a mail box"), DefaultValue(1)]
        public YesNoAns? IsMailBox { get; set; }

        [Display(Name="Is mailing Address"), UIHint("CheckBox")]
        public YesNoAns? IsMailingAddress { get; set; }

        [StringLength(50), Required]
        public string State { get; set; }

        [StringLength(0xff), Display(Name="Street Address")]
        public virtual string StreetAddress { get; set; }

        [StringLength(50), Display(Name="Unit Number")]
        public virtual string UnitNumber { get; set; }

        [DefaultValue(0), Display(Name="Years at this address")]
        public virtual int YearsAtThisAddress { get; set; }

        [StringLength(20)]
        public virtual string Zip4 { get; set; }

        [Display(Name="Zip Code"), StringLength(20), Required]
        public virtual string ZipCode { get; set; }

        [StringLength(50)]
        public virtual string ZipPlusZip4 { get; set; }
    }
}

