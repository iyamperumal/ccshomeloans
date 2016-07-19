namespace CcsData.ViewModels
{
    using CcsData.Models;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class App1
    {
        [DefaultValue((double) 0.0), DataType(DataType.Currency), Display(Name="Additional Cash Out Requested")]
        public decimal? AdditionalCashOutRequested { get; set; }

        [Display(Name="Cash Out Requested"), DefaultValue((double) 0.0), DataType(DataType.Currency)]
        public virtual decimal? CashOutRequested { get; set; }

        [Display(Name="Property County")]
        public string County { get; set; }

        [Required, Display(Name="Property County")]
        public int CountyFips { get; set; }

        [DataType(DataType.Currency), DefaultValue((double) 0.0), Display(Name="Down Payment Amount")]
        public virtual decimal? DownPaymentAmount { get; set; }

        [DataType(DataType.Currency), Display(Name="Estimated Homeowners Association Fees (Annual)"), DefaultValue((double) 0.0)]
        public virtual decimal? EstimatedHomeownersAssociationFeesAnnual { get; set; }

        [DataType(DataType.Currency), Display(Name="Estimate Total Debt to Pay Off"), DefaultValue((double) 0.0)]
        public virtual decimal? EstimateTotalDebtToPayOff { get; set; }

        [Display(Name="Loan Type Requested"), Required(ErrorMessage="select one"), Range(1, 6, ErrorMessage="Select loan type")]
        public LoanTypeRequestedEnum LoanTypeRequested { get; set; }

        [Range(1, 0x3e8, ErrorMessage="* Select property ownership"), UIHint("EnumCheck"), Required, Display(Name="This Property is my")]
        public OwnershipTypeEnum OwnerShipType { get; set; }

        [Display(Name="Property Type is"), Required, UIHint("EnumCheck"), Range(1, 0x3e8, ErrorMessage="* Select your property type")]
        public PropertyTypeEnum PropertyType { get; set; }

        [DataType(DataType.Currency), Display(Name="Purchase Price")]
        public virtual decimal? PurchasePrice { get; set; }

        [ForeignKey("RealtorID")]
        public virtual CcsData.Models.Realtor Realtor { get; set; }

        [Display(Name="Realtor"), Range(1, 0x3e8, ErrorMessage="* Please Select your Realtor")]
        public virtual int? RealtorID { get; set; }

        [DataType(DataType.Currency), DefaultValue((double) 0.0), Display(Name="Seller Paid Credit for Closing Cost")]
        public virtual decimal? SellerPaidCreditClosingCost { get; set; }

        [DefaultValue((double) 0.0), DataType(DataType.Currency), Display(Name="Total of Monthly Payments on Debt to Pay Off")]
        public virtual decimal? TotalOfMonthlyPaymentsOnDebtToPayOff { get; set; }

        [Range(1, 60, ErrorMessage="Select A State"), Display(Name="Property State"), Required(ErrorMessage="select s state")]
        public UsStateEnum UsState { get; set; }
    }
}

