namespace CcsData.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class App4
    {
        [DataType(DataType.Currency), Display(Name="Annual Homeowners Assoc. Dues"), Required(ErrorMessage="HOA ? enter 0 if N/A")]
        public virtual decimal AnnualHomeownersAssocDues { get; set; }

        [Required(ErrorMessage="Annual Homeowner's insurance "), Display(Name="Annual Homeowners Insur"), DataType(DataType.Currency), Range(1.0, double.MaxValue, ErrorMessage="* Enter at least $1")]
        public virtual decimal AnnualHomeownersInsur { get; set; }

        [Display(Name="Annual Property Taxes"), Required(ErrorMessage="Annual Property Taxes"), DataType(DataType.Currency), Range(1.0, double.MaxValue, ErrorMessage="* property taxes must be at least $1")]
        public virtual decimal AnnualPropertyTaxes { get; set; }

        [DataType(DataType.Date), Required(ErrorMessage="Enter The date you started this Mortgage"), Display(Name="Started 1st Mortgage On")]
        public DateTime? FirstMortgageOriginationDate { get; set; }

        [DefaultValue((double) 0.0), Display(Name="1st Mortgage Payment"), DataType(DataType.Currency), Required(ErrorMessage="Enter your 1st Mortgage payment ")]
        public virtual decimal FirstMortgagePayment { get; set; }

        [Display(Name="HOA Dues/Fees"), UIHint("Bool"), DefaultValue(false)]
        public bool HoaDuesFees { get; set; }

        [Display(Name="Monthly Mortgage Insurance"), DataType(DataType.Currency)]
        public virtual decimal? MonthlyMortgageInsur { get; set; }

        [Display(Name="Homeowners Insurance"), DefaultValue(false), UIHint("Bool")]
        public bool PymtIncludesHomeownersInsurance { get; set; }

        [DefaultValue(false), Display(Name="Mortgage Insurance"), UIHint("Bool")]
        public bool PymtIncludesMI { get; set; }

        [DefaultValue(false), Display(Name="None"), UIHint("Bool")]
        public bool PymtIncludesMone { get; set; }

        [DefaultValue(false), Display(Name="Property taxes"), UIHint("Bool")]
        public bool PymtIncludesPropTaxes { get; set; }
    }
}

