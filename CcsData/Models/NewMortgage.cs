namespace CcsData.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class NewMortgage
    {
        public virtual int? Applicant_ID { get; set; }

        [Display(Name="Current Ballance:")]
        public virtual decimal? Balance { get; set; }

        [Display(Name="Current Balloon Payment:")]
        public YesNoAns? BalloonPayment { get; set; }

        [Display(Name="Balloon Due Date")]
        public DateTime? BalloonPaymentDueDate { get; set; }

        public virtual decimal? CashOutAmountRequested { get; set; }

        public virtual string CashOutType { get; set; }

        public virtual decimal? ComputedCashOutAmountAvailable { get; set; }

        [Display(Name="Interest Rate")]
        public float InterestRate { get; set; }

        [StringLength(50), Display(Name="Current LenderName")]
        public string LanderName { get; set; }

        [DataType(DataType.Date), Display(Name="Current Maturity Date")]
        public DateTime? MaturityDate { get; set; }

        [DefaultValue((double) 0.0), Display(Name="Mtg Insurance (Monthly):")]
        public virtual decimal? MonthlyMortgageInsurance { get; set; }

        [Display(Name="Monthly Payment:")]
        public virtual decimal? MonthlyPayment { get; set; }

        [Display(Name="Property Taxes (monthly):")]
        public virtual decimal? MonthlyPropertyTacxes { get; set; }

        [DefaultValue((double) 0.0), Display(Name="Mortage Type")]
        public virtual MortgageProgramOptionsEnum? MortageType { get; set; }

        [ForeignKey("Applicant_ID")]
        public Applicant MortgageApplicant { get; set; }

        [DefaultValue("Current Morthgage"), Display(Name="Mortgage Name"), StringLength(50)]
        public string MortgageName { get; set; }

        public Property MortgageProperty { get; set; }

        [Key]
        public int NewMortgage_Id { get; set; }

        [Display(Name="Current Original Amount")]
        public decimal? OriginalAmount { get; set; }

        [DataType("Date"), Display(Name="Current Origination Date")]
        public DateTime? OriginationDate { get; set; }

        public int? position { get; set; }

        [Display(Name="Current PrePayment Penalty:")]
        public PrepaymentEnum? PrePaymentPenalty { get; set; }

        public LoanTypeEnum PrimaryLoanType { get; set; }

        [Display(Name="Pymt Includes Insurance?")]
        public virtual YesNoAns PymtIncludesInsurance { get; set; }

        [Display(Name="Pymt Includes Taxes")]
        public virtual YesNoAns PymtIncludesTaxes { get; set; }

        [Display(Name="New Rate Type")]
        public InterestRateTypeEnum RateType { get; set; }

        public double SavingsPerMnt { get; set; }

        public double SavingsPerYear { get; set; }

        [Display(Name="Current Term")]
        public int? Term { get; set; }

        public virtual decimal? TotalBalanceOfDebtToConsolidate { get; set; }

        public virtual decimal? TotalMonthlyAmountOfDebtPaymentsToConsolidate { get; set; }

        [Display(Name="Home Insurance Payment (Yearly):")]
        public virtual decimal? YearlyHomeInsurancePayment { get; set; }

        [Display(Name="Current Mortgage Insurance (Yearly):")]
        public virtual decimal? YearlyMortgageInsurance { get; set; }

        [Display(Name="Property Taxes (Yearly):")]
        public virtual decimal? YearlyPropertyTacxes { get; set; }

        [Display(Name="Property Taxes (Yearly):")]
        public virtual decimal? YearlyPropertyTaxes { get; set; }
    }
}

