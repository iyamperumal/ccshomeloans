namespace CcsData.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class SecondMortgage
    {
        public SecondMortgage()
        {
            this.EntryDate = DateTime.Now;
        }

        [Display(Name="Balance:")]
        public virtual decimal? Balance { get; set; }

        [Display(Name="Balloon Payment:")]
        public YesNoAns? BalloonPayment { get; set; }

        [Display(Name="Balloon Due Date")]
        public DateTime? BalloonPaymentDueDate { get; set; }

        public DateTime EntryDate { get; set; }

        [DefaultValue(0), Display(Name="Interest Rate")]
        public double InterestRate { get; set; }

        [StringLength(50), Display(Name="LenderName")]
        public string LanderName { get; set; }

        public LoanTypeEnum? LoanType { get; set; }

        [DataType(DataType.Date), Display(Name="Maturity Date")]
        public DateTime? MaturityDate { get; set; }

        [Display(Name="Monthly Payment:")]
        public virtual decimal? MonthlyPayment { get; set; }

        [Display(Name="Property Taxes (Monthly):")]
        public virtual decimal? MonthlyPropertyTaxes { get; set; }

        [DefaultValue((double) 0.0), Display(Name="Mortage Type")]
        public virtual MortgageProgramOptionsEnum? MortageType { get; set; }

        [DefaultValue("Current Morthgage"), StringLength(50), Display(Name="Mortgage Name")]
        public string MortgageName { get; set; }

        [Display(Name="Original Amount")]
        public decimal? OriginalAmount { get; set; }

        [Display(Name="Origination Date")]
        public DateTime? OriginationDate { get; set; }

        public int? Position { get; set; }

        [Display(Name="PrePayment Penalty:")]
        public PrepaymentEnum? PrePaymentPenalty { get; set; }

        [Key]
        public int SecondMortgage_Id { get; set; }

        [Display(Name="2nd Mortgae Rate Type"), UIHint("EnumCheck")]
        public SecondMortgageRateTypeEnum? SecondMortgageRateType { get; set; }

        [UIHint("EnumCheck"), Display(Name="2nd Mortgage Term"), UIHint("EnumCheck")]
        public SecondMortgageTermEnum? SecondMortgageTerm { get; set; }
    }
}

