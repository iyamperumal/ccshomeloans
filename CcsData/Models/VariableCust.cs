namespace CcsData.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class VariableCust
    {
        [Display(Name="Active:")]
        public virtual YesNoAns Active { get; set; }

        [Display(Name="ARM Terms (Index, Margin,Cap,F_indexRate)"), StringLength(150)]
        public string AdjustableTerms { get; set; }

        [Display(Name="Appraisal Fee: "), DataType(DataType.Currency)]
        public virtual decimal AppraisalFee { get; set; }

        public int? Bankruptcy { get; set; }

        [Display(Name="Closing Escrow Fee: "), DataType(DataType.Currency)]
        public virtual decimal ClosingEscrowFee { get; set; }

        [Display(Name="CLTV")]
        public double CLTV { get; set; }

        [DefaultValue("0.29|0.44|0.59|0.95"), Display(Name="Conventional PMI Factor : ")]
        public virtual string ConventionalPmiFactor { get; set; }

        [MaxLength(50)]
        public string County { get; set; }

        [Display(Name="Credit Report Fee: "), DataType(DataType.Currency)]
        public virtual decimal CreditReportFee { get; set; }

        public string CreditScoreRange { get; set; }

        [Display(Name="Daily Interest x 5: "), DataType(DataType.Currency)]
        public virtual decimal DailyInterestCalculation { get; set; }

        [DefaultValue((double) 0.007), Display(Name="Deed Stamp Percent: ")]
        public virtual double DeedStampPercent { get; set; }

        [Display(Name="Discount Percent: ")]
        public virtual double DiscountPercent { get; set; }

        [DataType(DataType.Currency), Display(Name="Endorsements Reconveyance Fee: ")]
        public virtual decimal EndorsementsReconveyanceFee { get; set; }

        [Display(Name="FHA Monthly MIP Refi percent Before June1_09  : "), DefaultValue((double) 0.55)]
        public virtual double FHA_Monthly_MIP_Refi_percent_BeforeJune1_2009 { get; set; }

        [Display(Name="FHA Monthly MIP Refi/Purchase Percent After May 31-09  : "), DefaultValue((double) 1.35)]
        public virtual double FHA_Monthly_MIP_RefiOrPurchase_percent_AfterMay31_2009 { get; set; }

        [Display(Name="FHA Up-Front Mortgage Insurance before June 1st, 2009  : "), DefaultValue((double) 0.01)]
        public virtual double FHA_Upfront_MIP_Refi_percent_beforeJune1_2009 { get; set; }

        [Display(Name="FHA Up-Front Mortgage Insurance After May 31st, 2009  : "), DefaultValue((double) 1.75)]
        public virtual double FHA_Upfront_MIP_RefiOrPurchase_percent_AfterMay31_2009 { get; set; }

        [DefaultValue((double) 0.0), Display(Name="Flood Certification Fee: ")]
        public virtual decimal FloodCertificationFee { get; set; }

        [Display(Name="Flood Insurance Percent: ")]
        public virtual double FloodInsurancePercent { get; set; }

        public int? Foreclosure { get; set; }

        [Display(Name="Hazard Insurance Percent: ")]
        public virtual double HazardInsurancePercent { get; set; }

        [Display(Name="Intangible Tax Percent: ")]
        public virtual double IntangibleTaxPercent { get; set; }

        [StringLength(50), Display(Name="Lender:")]
        public string Lender { get; set; }

        [Display(Name="Lender Credit Percent: ")]
        public virtual double LenderCreditPercent { get; set; }

        [StringLength(50), Display(Name="LenderLogo:")]
        public string LenderLogo { get; set; }

        [DefaultValue((double) 0.0275)]
        public double LenderPaidComp { get; set; }

        [DefaultValue((double) 0.0), Display(Name="Lender's Title Insurance Fee: ")]
        public virtual decimal LenderTitleInsuranceFee { get; set; }

        [Display(Name="Loan Type:")]
        public LoanTypeEnum LoanType { get; set; }

        [Display(Name="LTV Range")]
        public string LTV_Range { get; set; }

        public double MaxBacktDTI { get; set; }

        public double MaxCashOut { get; set; }

        public double MaxfrontDTI { get; set; }

        public double MaxLoanAmount { get; set; }

        [Display(Name="Max LTV")]
        public double MaxLTV { get; set; }

        [Display(Name="Max Number Of Units: ")]
        public int? MaxNumberOfUnits { get; set; }

        public int MiDurationYears { get; set; }

        public double MiFactor { get; set; }

        [StringLength(50), Display(Name="Program Option:")]
        public string MortgageProgramOption { get; set; }

        [Display(Name="Mortgage Recording fee: "), DataType(DataType.Currency)]
        public virtual decimal MortgageRecordingfee { get; set; }

        [Display(Name="Interest Rate: ")]
        public virtual double NewInterestRate { get; set; }

        [Display(Name="new Term In Years: ")]
        public virtual int newTermInYears { get; set; }

        public int NumOf30LateAllowedIn12Mo { get; set; }

        public int NumOf30LateAllowedIn24Mo { get; set; }

        [Display(Name="Num of Months to Escrow Flood Insurance: ")]
        public virtual int NumofMonthstoEscrowFloodInsurance { get; set; }

        [Display(Name="Num of Months to Escrow Hazard Insurance: ")]
        public virtual int NumofMonthstoEscrowHazardInsurance { get; set; }

        [Display(Name=" Num of Months to Escrow Taxes: ")]
        public virtual int NumofMonthstoEscrowTaxes { get; set; }

        [Display(Name="Option Number: ")]
        public virtual int? OptionNumber { get; set; }

        [Display(Name="Origination Percent: ")]
        public virtual double OriginationPercent { get; set; }

        [Display(Name="Ownershipe Type")]
        public string OwnershipType { get; set; }

        [DefaultValue((double) 0.0), Display(Name="Pest Inspection Fee: ")]
        public virtual decimal PestInspectionFee { get; set; }

        [Display(Name="Processing Fee: "), DataType(DataType.Currency)]
        public virtual decimal ProcessingFee { get; set; }

        [Display(Name="Property Tax Percent: ")]
        public virtual double PropertyTaxPercent { get; set; }

        [Display(Name="Property Type")]
        public string PropertyType { get; set; }

        [Display(Name="Rate Type:")]
        public InterestRateTypeEnum RateType { get; set; }

        [StringLength(50), Display(Name="Schedule Name: ")]
        public virtual string ScheduleName { get; set; }

        [MaxLength(50)]
        public string State { get; set; }

        [Display(Name="State Tax Percent: ")]
        public virtual double StateTaxPercent { get; set; }

        [Display(Name="Survey Fee: "), DefaultValue((double) 0.0)]
        public virtual decimal SurveyFee { get; set; }

        [DefaultValue((double) 0.0), Display(Name="Tax Service Fee: ")]
        public virtual decimal TaxServiceFee { get; set; }

        [Display(Name="Title Inusrance Percent: ")]
        public virtual double TitleInusrancePercent { get; set; }

        [DataType(DataType.Currency), Display(Name="Underwriting Fee: ")]
        public virtual decimal UnderwritingFee { get; set; }

        public double UpfrontMI { get; set; }

        [DefaultValue((double) 1.25), Display(Name="VA Funding Fee factor 10% + Down: ")]
        public virtual double VaFundingFeeFactor10PlusDown { get; set; }

        [DefaultValue((double) 1.5), Display(Name="VA Funding Fee factor 5% to 10% Down: ")]
        public virtual double VaFundingFeeFactor5to10Down { get; set; }

        [DefaultValue((double) 1.0), Display(Name="VA Funding Fee factor Mobil Home : ")]
        public virtual double VaFundingFeeFactorMobilHomeRefiNoCashout { get; set; }

        [DefaultValue((double) 0.5), Display(Name="VA Funding Fee factor Refi No Cashout: ")]
        public virtual double VaFundingFeeFactorRefiNoCashout { get; set; }

        [Display(Name="VA Funding Fee factor with Cashout : "), DefaultValue((double) 3.3)]
        public virtual double VaFundingFeeFactorWithCashout { get; set; }

        [DefaultValue((double) 2.15), Display(Name="VA Funding Fee factor zoro Down: ")]
        public virtual double VaFundingFeeFactorZeroDown { get; set; }

        [Display(Name="Date")]
        private DateTime VarDate { get; set; }

        [Key]
        public virtual int VariableCust_Id { get; set; }
    }
}

