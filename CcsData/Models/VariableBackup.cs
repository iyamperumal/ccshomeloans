namespace CcsData.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class VariableBackup
    {
        [UIHint("EnumCheck"), Display(Name="Active:")]
        public virtual YesNoAns Active { get; set; }

        [StringLength(150), Display(Name="ARM Terms (Index, Margin,Cap,F_indexRate)")]
        public string AdjustableTerms { get; set; }

        [DataType(DataType.Currency), Display(Name="Appraisal Fee: ")]
        public virtual decimal AppraisalFee { get; set; }

        [UIHint("Int"), Display(Name="Bankruptcy")]
        public int? Bankruptcy { get; set; }

        [DataType(DataType.Currency), Display(Name="Closing Escrow Fee: ")]
        public virtual decimal ClosingEscrowFee { get; set; }

        [Display(Name="CLTV")]
        public double CLTV { get; set; }

        [DefaultValue(true)]
        public bool Condo { get; set; }

        [DefaultValue("0.29|0.44|0.59|0.95"), Display(Name="Conventional PMI Factor : ")]
        public virtual string ConventionalPmiFactor { get; set; }

        [MaxLength(50)]
        public string County { get; set; }

        [DataType(DataType.Currency), Display(Name="Credit Report Fee: ")]
        public virtual decimal CreditReportFee { get; set; }

        public string CreditScoreRange { get; set; }

        [DataType(DataType.Currency), Display(Name="Daily Interest x 5: ")]
        public virtual decimal DailyInterestCalculation { get; set; }

        [Display(Name="Deed Stamp Percent: "), DefaultValue((double) 0.007)]
        public virtual double DeedStampPercent { get; set; }

        [Display(Name="Discount Percent: ")]
        public virtual double DiscountPercent { get; set; }

        [DataType(DataType.Currency), Display(Name="Endorsements Reconveyance Fee: ")]
        public virtual decimal EndorsementsReconveyanceFee { get; set; }

        [DefaultValue((double) 0.55), Display(Name="FHA Monthly MIP Refi percent Before June1_09  : ")]
        public virtual double FHA_Monthly_MIP_Refi_percent_BeforeJune1_2009 { get; set; }

        [DefaultValue((double) 1.35), Display(Name="FHA Monthly MIP Refi/Purchase Percent After May 31-09  : ")]
        public virtual double FHA_Monthly_MIP_RefiOrPurchase_percent_AfterMay31_2009 { get; set; }

        [DefaultValue((double) 0.01), Display(Name="FHA Up-Front Mortgage Insurance before June 1st, 2009  : ")]
        public virtual double FHA_Upfront_MIP_Refi_percent_beforeJune1_2009 { get; set; }

        [DefaultValue((double) 1.75), Display(Name="FHA Up-Front Mortgage Insurance After May 31st, 2009  : ")]
        public virtual double FHA_Upfront_MIP_RefiOrPurchase_percent_AfterMay31_2009 { get; set; }

        [DataType(DataType.Currency), Display(Name="Flood Certification Fee: "), DefaultValue((double) 0.0)]
        public virtual decimal FloodCertificationFee { get; set; }

        [Display(Name="Flood Insurance Percent: ")]
        public virtual double FloodInsurancePercent { get; set; }

        [UIHint("Int")]
        public int? Foreclosure { get; set; }

        [Display(Name="Hazard Insurance Percent: ")]
        public virtual double HazardInsurancePercent { get; set; }

        [Display(Name="Intangible Tax Percent: ")]
        public virtual double IntangibleTaxPercent { get; set; }

        public bool Investment { get; set; }

        [Display(Name="Max Judgment: "), DataType(DataType.Currency)]
        public virtual decimal Judgment { get; set; }

        [StringLength(50), Display(Name="Lender:")]
        public string Lender { get; set; }

        [Display(Name="Lender Credit Percent: ")]
        public virtual double LenderCreditPercent { get; set; }

        [Display(Name="Lender Logo:"), StringLength(50)]
        public string LenderLogo { get; set; }

        [DefaultValue((double) 0.0275)]
        public double LenderPaidComp { get; set; }

        [DataType(DataType.Currency), Display(Name="Lender's Title Insurance Fee: "), DefaultValue((double) 0.0)]
        public virtual decimal LenderTitleInsuranceFee { get; set; }

        [UIHint("EnumCheck"), Display(Name="Loan Type:")]
        public LoanTypeEnum LoanType { get; set; }

        [Display(Name="LTV Range")]
        public string LTV_Range { get; set; }

        [DefaultValue(true)]
        public bool Manufactured { get; set; }

        [UIHint("Double"), Display(Name="Max Back DTI")]
        public double MaxBacktDTI { get; set; }

        public double MaxCashOut { get; set; }

        [Display(Name="Max Front DTI"), UIHint("Double")]
        public double MaxfrontDTI { get; set; }

        public double MaxLoanAmount { get; set; }

        [Display(Name="Max LTV")]
        public double MaxLTV { get; set; }

        [Display(Name="Max Number Of Units: "), UIHint("Int")]
        public int? MaxNumberOfUnits { get; set; }

        [UIHint("Int"), Display(Name="MI Duration (Years)")]
        public int MiDurationYears { get; set; }

        public double MiFactor { get; set; }

        [Display(Name="Min LTV")]
        public double MinLTV { get; set; }

        [DefaultValue(true)]
        public bool MobileHome { get; set; }

        [Display(Name="Program Option:"), StringLength(50)]
        public string MortgageProgramOption { get; set; }

        [DataType(DataType.Currency), Display(Name="Mortgage Recording fee: ")]
        public virtual decimal MortgageRecordingfee { get; set; }

        [DefaultValue(true)]
        public bool MultiUnits { get; set; }

        [Display(Name="Interest Rate: ")]
        public virtual double NewInterestRate { get; set; }

        [UIHint("Int"), Display(Name="new Term In Years: ")]
        public virtual int newTermInYears { get; set; }

        [UIHint("Int"), Display(Name="Num Of 30 Late 12mo")]
        public int NumOf30LateAllowedIn12Mo { get; set; }

        [UIHint("Int"), Display(Name="Num Of 30 Late 24 mo")]
        public int NumOf30LateAllowedIn24Mo { get; set; }

        [Display(Name="Num of Months to Escrow Flood Insurance: "), UIHint("Int")]
        public virtual int NumofMonthstoEscrowFloodInsurance { get; set; }

        [Display(Name="Num of Months to Escrow Hazard Insurance: "), UIHint("Int")]
        public virtual int NumofMonthstoEscrowHazardInsurance { get; set; }

        [UIHint("Int"), Display(Name=" Num of Months to Escrow Taxes: ")]
        public virtual int NumofMonthstoEscrowTaxes { get; set; }

        [Display(Name="Option Number: "), UIHint("Int")]
        public virtual int? OptionNumber { get; set; }

        [Display(Name="Origination Percent: ")]
        public virtual double OriginationPercent { get; set; }

        [DataType(DataType.Currency), Display(Name="Pest Inspection Fee: "), DefaultValue((double) 0.0)]
        public virtual decimal PestInspectionFee { get; set; }

        public bool PrimaryResidence { get; set; }

        [Display(Name="Processing Fee: "), DataType(DataType.Currency)]
        public virtual decimal ProcessingFee { get; set; }

        [Display(Name="Property Tax Percent: ")]
        public virtual double PropertyTaxPercent { get; set; }

        [Display(Name="Property Type")]
        public string PropertyType { get; set; }

        [DefaultValue(true)]
        public bool Purchase { get; set; }

        [DefaultValue(true)]
        public bool QuoteBorrowerPaid { get; set; }

        [DefaultValue(true)]
        public bool QuoteLenderPaid { get; set; }

        [DefaultValue(true)]
        public bool RateTerm { get; set; }

        [Display(Name="Rate Type:"), UIHint("EnumCheck")]
        public InterestRateTypeEnum RateType { get; set; }

        [DefaultValue(true)]
        public bool Refi { get; set; }

        [DefaultValue(true)]
        public bool RefiCashout { get; set; }

        [Display(Name="Schedule Name: "), StringLength(50)]
        public virtual string ScheduleName { get; set; }

        public bool SecondHome { get; set; }

        [DefaultValue(true)]
        public bool SFR { get; set; }

        [MaxLength(50)]
        public string State { get; set; }

        [Display(Name="State Tax Percent: ")]
        public virtual double StateTaxPercent { get; set; }

        [DefaultValue((double) 0.0), Display(Name="Survey Fee: "), DataType(DataType.Currency)]
        public virtual decimal SurveyFee { get; set; }

        [Display(Name="Tax Service Fee: "), DataType(DataType.Currency), DefaultValue((double) 0.0)]
        public virtual decimal TaxServiceFee { get; set; }

        [Display(Name="Title Inusrance Percent: ")]
        public virtual double TitleInusrancePercent { get; set; }

        [DefaultValue(true)]
        public bool TownHome { get; set; }

        [Display(Name="Underwriting Fee: "), DataType(DataType.Currency)]
        public virtual decimal UnderwritingFee { get; set; }

        public double UpfrontMI { get; set; }

        [Display(Name="VA Funding Fee factor 10% + Down: "), DefaultValue((double) 1.25)]
        public virtual double VaFundingFeeFactor10PlusDown { get; set; }

        [DefaultValue((double) 1.5), Display(Name="VA Funding Fee factor 5% to 10% Down: ")]
        public virtual double VaFundingFeeFactor5to10Down { get; set; }

        [Display(Name="VA Funding Fee factor Mobile Home : "), DefaultValue((double) 1.0)]
        public virtual double VaFundingFeeFactorMobileHomeRefiNoCashout { get; set; }

        [DefaultValue((double) 0.5), Display(Name="VA Funding Fee factor Refi No Cashout: ")]
        public virtual double VaFundingFeeFactorRefiNoCashout { get; set; }

        [Display(Name="VA Funding Fee factor with Cashout : "), DefaultValue((double) 3.3)]
        public virtual double VaFundingFeeFactorWithCashout { get; set; }

        [DefaultValue((double) 2.15), Display(Name="VA Funding Fee factor zoro Down: ")]
        public virtual double VaFundingFeeFactorZeroDown { get; set; }

        [Display(Name="Date")]
        private DateTime VarDate { get; set; }

        [Key]
        public virtual int VariableBackup_Id { get; set; }
    }
}

