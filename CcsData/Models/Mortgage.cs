namespace CcsData.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class Mortgage
    {
        public Mortgage()
        {
            this.EntryDate = DateTime.Now;
        }

        public virtual int? Applicant_ID { get; set; }

        [Display(Name="Balance:")]
        public virtual decimal? Balance { get; set; }

        [Display(Name="Balloon Payment:")]
        public YesNoAns? BalloonPayment { get; set; }

        [Display(Name="Balloon Due Date")]
        public DateTime? BalloonPaymentDueDate { get; set; }

        [Display(Name="DownPayment"), DefaultValue(0)]
        public decimal? DownPayment { get; set; }

        public DateTime EntryDate { get; set; }

        [DataType(DataType.Currency), Display(Name="Estimated Homeowners Association Fees (Annual)"), DefaultValue((double) 0.0)]
        public virtual decimal? EstimatedHomeownersAssociationFeesAnnual { get; set; }

        [Display(Name="Interest Rate"), DefaultValue(0)]
        public double InterestRate { get; set; }

        [Display(Name="Rate Type")]
        public InterestRateTypeEnum? InterestRateType { get; set; }

        [StringLength(50), Display(Name="LenderName")]
        public string LanderName { get; set; }

        public LoanTypeEnum? LoanType { get; set; }

        [DataType(DataType.Date), Display(Name="Maturity Date")]
        public DateTime? MaturityDate { get; set; }

        [DefaultValue((double) 0.0), Display(Name="Mtg Insurance (Monthly):")]
        public virtual decimal? MonthlyMortgageInsurance { get; set; }

        [Display(Name="Monthly Payment:")]
        public virtual decimal? MonthlyPayment { get; set; }

        [Display(Name="Property Taxes (Monthly):")]
        public virtual decimal? MonthlyPropertyTaxes { get; set; }

        [Display(Name="Mortage Type"), DefaultValue((double) 0.0)]
        public virtual MortgageProgramOptionsEnum? MortageType { get; set; }

        [Key]
        public int Mortgage_Id { get; set; }

        [ForeignKey("Applicant_ID")]
        public Applicant MortgageApplicant { get; set; }

        public Property MortgagedProperty { get; set; }

        [StringLength(50), DefaultValue("Current Morthgage"), Display(Name="Mortgage Name")]
        public string MortgageName { get; set; }

        [Display(Name="Original Amount")]
        public decimal? OriginalAmount { get; set; }

        [Display(Name="Origination Date")]
        public DateTime? OriginationDate { get; set; }

        public int? Position { get; set; }

        [Display(Name="PrePayment Penalty:")]
        public PrepaymentEnum? PrePaymentPenalty { get; set; }

        public decimal? PurchasePrice { get; set; }

        [Display(Name="Pymt Includes Insurance?")]
        public virtual YesNoAns PymtIncludesHomeownersInsurance { get; set; }

        [Display(Name="Pymt Includes Taxes")]
        public virtual YesNoAns PymtIncludesTaxes { get; set; }

        [Display(Name="2nd Mortgae Rate Type"), UIHint("EnumCheck")]
        public SecondMortgageRateTypeEnum? SecondMortgageRateType { get; set; }

        [UIHint("EnumCheck"), UIHint("EnumCheck"), Display(Name="2nd Mortgage Term")]
        public SecondMortgageTermEnum? SecondMortgageTerm { get; set; }

        [Display(Name="Seller Paid Credit for Closing Cost"), DefaultValue((double) 0.0), DataType(DataType.Currency)]
        public virtual decimal? SellerPaidCreditClosingCost { get; set; }

        [DefaultValue(30), UIHint("EnumCheck"), Display(Name="Mortgage Term")]
        public MortgageTermEnum? Term { get; set; }

        [Display(Name="Home Insurance Payment (Yearly):")]
        public virtual decimal? YearlyHomeInsurancePayment { get; set; }

        [Display(Name="Mtg Insurance (Yearly):")]
        public virtual decimal? YearlyMortgageInsurance { get; set; }

        [Display(Name="Property Taxes (Yearly):")]
        public virtual decimal? YearlyPropertyTaxes { get; set; }
    }
}

