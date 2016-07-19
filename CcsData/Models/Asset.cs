namespace CcsData.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class Asset
    {
        [Key]
        public virtual int Asset_Id { get; set; }

        [Display(Name="Asset Type"), MaxLength(50)]
        public virtual string AssetType { get; set; }

        public virtual float? DTI { get; set; }

        public Loan LoanInfo { get; set; }

        public virtual float? LTV { get; set; }

        [DefaultValue(1), Display(Name="Is Paid off")]
        public YesNoAns PaidOff { get; set; }

        [StringLength(250)]
        public string Remarks { get; set; }

        [Display(Name="Estimated Value"), DataType(DataType.Currency)]
        public virtual decimal? Value { get; set; }
    }
}

