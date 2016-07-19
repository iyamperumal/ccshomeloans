namespace CcsData.ViewModels
{
    using CcsData.Models;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class MortgageVM
    {
        public void Mortgage()
        {
            this.CashOutAmountRequested = 0.00M;
        }

        [UIHint("Currency"), Display(Name="Balance"), DataType(DataType.Currency)]
        public virtual decimal? Balance { get; set; }

        [Display(Name="Cashout Requested ?"), DataType(DataType.Currency), DefaultValue((double) 0.0)]
        public virtual decimal? CashOutAmountRequested { get; set; }

        [DataType(DataType.Currency), DefaultValue((double) 0.0), Display(Name="What Is Your Estimated Property Value ?")]
        public virtual decimal? EstimatedMarketValue { get; set; }

        [Display(Name="What Is Your Current Interest Rate?")]
        public double InterestRate { get; set; }

        [UIHint("EnumCheck"), Display(Name="Select Your Rate Type?")]
        public InterestRateTypeEnum InterestRateType { get; set; }

        [Display(Name="What Is Your Monthly Mortgage Insurance?"), DataType(DataType.Currency), DefaultValue((double) 0.0)]
        public virtual decimal? MonthlyMortgageInsurance { get; set; }

        [Display(Name="What Is Your Current Mortgage Payment?"), DataType(DataType.Currency)]
        public virtual decimal? MonthlyPayment { get; set; }

        [Key]
        public int Mortgage_Id { get; set; }

        [DataType(DataType.Date), Required, Display(Name="Origination Date")]
        public DateTime? OriginationDate { get; set; }

        [Display(Name="Does Your Payment Includes Home Owners Insurance?"), UIHint("EnumCheck")]
        public virtual YesNoAns PymtIncludesInsurance { get; set; }

        [Display(Name="Does Your Payment Includes Property Taxes?"), UIHint("EnumCheck")]
        public virtual YesNoAns PymtIncludesTaxes { get; set; }

        [Display(Name="What Is You Current Mortgage Term?"), UIHint("EnumCheck"), Required]
        public MortgageTermEnum Term { get; set; }

        [Display(Name="What Is Your Home Insurance Payment (Yearly)?"), DataType(DataType.Currency)]
        public virtual decimal YearlyHomeInsurancePayment { get; set; }

        [Display(Name="What Are Your Property Taxes (Yearly)?"), DataType(DataType.Currency)]
        public virtual decimal YearlyPropertyTaxes { get; set; }
    }
}

