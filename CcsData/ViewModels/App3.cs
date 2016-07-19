namespace CcsData.ViewModels
{
    using CcsData.Models;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class App3
    {
        [Range(1.0, double.MaxValue, ErrorMessage="Enter your current interest Rate"), Display(Name="Current Interest Rate"), UIHint("percent"), DefaultValue((double) 0.0)]
        public double CurrentInterestRate { get; set; }

        [Display(Name="Estimated Home Value"), DataType(DataType.Currency), Required, Range(1.0, double.MaxValue, ErrorMessage="Enter your home value estimate")]
        public virtual decimal EstimatedHomeValue { get; set; }

        [Range(1.0, double.MaxValue, ErrorMessage="* 1st Mortgage Ballance"), Display(Name="First Mortgage Balance"), Required, DataType(DataType.Currency)]
        public virtual decimal FirstMortgageBalance { get; set; }

        [Display(Name="Do you have a 2nd Mortgage"), Range(1, 3, ErrorMessage="* Have A 2nd Mortgage?")]
        public HaveSecondMortgageEnum Has2ndMortgage { get; set; }

        [Display(Name="Rate Type"), Required, UIHint("EnumCheck"), Range(1, 8, ErrorMessage="* Select your interest Rate Type")]
        public InterestRateTypeEnum InterestRateType { get; set; }

        [Range(1, 30, ErrorMessage="* Select Mortgage Type"), Display(Name="Loan type"), Required, UIHint("EnumCheck")]
        public LoanTypeEnum LoanType { get; set; }

        [Range(1, 30, ErrorMessage="* Select your mortgage term"), Display(Name="Term"), Required, UIHint("EnumCheck")]
        public MortgageTermEnum MortgageTerm { get; set; }

        [Display(Name="Pay Off The 2nd Mortgage"), UIHint("EnumCheck")]
        public YesNoAns? PayOff2ndMortgage { get; set; }

        [DefaultValue((double) 0.0), Display(Name="2nd Mortgage Balance"), DataType(DataType.Currency)]
        public virtual decimal? SecondMortgageBalance { get; set; }

        [Display(Name="2nd Mortgage Interst rate"), UIHint("percent")]
        public double? SecondMortgageInterestRate { get; set; }

        [Display(Name="Started 2nd Mortgage On"), DataType(DataType.Date)]
        public DateTime? SecondMortgageOriginationDate { get; set; }

        [DataType(DataType.Currency), Display(Name="2nd Mortgage Payment"), DefaultValue((double) 0.0)]
        public virtual decimal? SecondMortgagePayment { get; set; }

        [Range(1, 10, ErrorMessage="* Select your interest Rate Type"), Display(Name="2nd Mortgage Rate Type"), UIHint("EnumCheck")]
        public SecondMortgageRateTypeEnum? SecondMortgageRateType { get; set; }

        [Display(Name="2nd Mortgage Term"), UIHint("EnumCheck"), Range(1, 30, ErrorMessage="* Select Mortgage term"), UIHint("EnumCheck")]
        public SecondMortgageTermEnum? SecondMortgageTerm { get; set; }
    }
}

