namespace CcsData.ViewModels
{
    using CcsData.Models;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class App2
    {
        [Display(Name="Date of Bankruptcy Discharge"), DataType(DataType.Date)]
        public DateTime? BankruptcyDischargeDate { get; set; }

        [DataType(DataType.Date), Display(Name="Chapter 13 Date of Filing")]
        public DateTime? Chapter13FilingDate { get; set; }

        [Required, UIHint("EnumCheck"), Range(500, 850, ErrorMessage="* Please estimate you Credit score"), Display(Name="Estimated Credit Score")]
        public CreditScoreEstimateEnum CreditScoreEstimate { get; set; }

        [Required, UIHint("EnumCheck"), Range(1, 10, ErrorMessage="* Select 0 x 30 if none"), Display(Name="In Last 12 months, # of over 30 days late on Mortgage or Rental history")]
        public DaysLateEnum DaysLate { get; set; }

        [Display(Name="Have you ever filed Bankruptcy in last 4 yrs"), Range(1, 5, ErrorMessage="* Filed Bankruptcy? "), Required, UIHint("EnumCheck")]
        public FiledBankruptcyTypeEnum FiledBankruptcyType { get; set; }

        [Display(Name="Date of Foreclosure, Short Sale, Deed in Lieu"), DataType(DataType.Date)]
        public DateTime? ForeclosureShortSaleDeedinLieuDate { get; set; }

        [Display(Name="Any Foreclosures, Short Sale, or Deed in Lieu"), UIHint("EnumCheck"), Range(1, 4, ErrorMessage="Foreclosures Short Sale Deed in Lieu?"), Required]
        public ForeclosuresShortSaleDeedinLieuEnum ForeclosuresShortSaleDeedinLieu { get; set; }

        [Required, DataType(DataType.Currency), Range(1.0, double.MaxValue, ErrorMessage="* Annual Gross Income"), Display(Name="Gross Annual Income"), DefaultValue((double) 0.0)]
        public virtual decimal GrossAnnualIncome { get; set; }

        [Display(Name="How Long Will You Own"), Range(1, 7, ErrorMessage="* How long will you one this property"), Required, UIHint("EnumCheck")]
        public OwnerShipLongevityEnum OwnerShipLongevity { get; set; }

        [Range(1, 2, ErrorMessage="* Is the property Rural"), Display(Name="Is this a Rural Property"), UIHint("EnumCheck")]
        public YesNoAns RuralProperty { get; set; }

        [Display(Name="Total of all monthly payments (Credit cards, car loans ...etc"), DefaultValue((double) 0.0), DataType(DataType.Currency), Range(1.0, double.MaxValue, ErrorMessage="Total Montly Payment")]
        public virtual decimal TotalMontlyPayments { get; set; }

        [UIHint("EnumCheck"), Range(1, 2, ErrorMessage="* Are you a Veteran"), Display(Name="Are you a Veteran")]
        public YesNoAns Veteran { get; set; }
    }
}

