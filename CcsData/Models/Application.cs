namespace CcsData.Models
{
    using CreditPull;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class Application
    {
        public int setFips()
        {
            int num = -1;
            if (this.UsState.HasValue)
            {
                int countyFips = this.CountyFips;
                int num2 = ((int) this.UsState.Value) * (int)((UsStateEnum) 0x3e8);
                num2 += this.CountyFips;
                num = num2;
                this.Fips = num;
            }
            return num;
        }

        [DefaultValue((double) 0.0), DataType(DataType.Currency), Display(Name="Additional Cash Out Requested")]
        public decimal? AdditionalCashOutRequested { get; set; }

        [Display(Name="Address")]
        public string Address { get; set; }

        [DataType(DataType.Currency), Display(Name="Annual Homeowners Assoc. Dues"), DefaultValue((double) 0.0)]
        public virtual decimal? AnnualHomeownersAssocDues { get; set; }

        [DataType(DataType.Currency), Display(Name="Annual Homeowners Insur"), DefaultValue((double) 0.0)]
        public virtual decimal? AnnualHomeownersInsur { get; set; }

        [Display(Name="Annual Property Taxes"), DataType(DataType.Currency), DefaultValue((double) 0.0)]
        public virtual decimal? AnnualPropertyTaxes { get; set; }

        [ForeignKey("ApplicantID")]
        public virtual CcsData.Models.Applicant Applicant { get; set; }

        public virtual int? ApplicantID { get; set; }

        [Key]
        public int Application_Id { get; set; }

        [Display(Name="Application Date")]
        public DateTime ApplicationDate { get; set; }

        [Display(Name="BackDTI Credit")]
        public virtual double? BackDTI_Credit { get; set; }

        [Display(Name="Date of Bankruptcy Discharge"), DataType(DataType.Date)]
        public DateTime? BankruptcyDischargeDate { get; set; }

        [UIHint("EnumCheck"), Display(Name="Month")]
        public MonthsEnum BankruptcyDischargeMonth { get; set; }

        [UIHint("EnumCheck"), Display(Name="Year")]
        public YearsEnum BankruptcyDischargeYear { get; set; }

        [DataType(DataType.Currency), Display(Name="Cash Out Requested"), DefaultValue((double) 0.0)]
        public virtual decimal? CashOutRequested { get; set; }

        [DataType(DataType.Date), Display(Name="Chapter 13 Date of Filing")]
        public DateTime? Chapter13FilingDate { get; set; }

        [UIHint("EnumCheck"), Display(Name="Month")]
        public MonthsEnum Chapter13FilingMonth { get; set; }

        [UIHint("EnumCheck"), Display(Name="Year")]
        public YearsEnum Chapter13FilingYear { get; set; }

        [Display(Name="City")]
        public string City { get; set; }

        [Display(Name="Property County")]
        public string County { get; set; }

        [Required, Display(Name="Property County")]
        public int CountyFips { get; set; }

        [DefaultValue(false), Display(Name="credit Pulled")]
        public virtual bool creditPulled { get; set; }

        [DefaultValue(0), Display(Name="Credit Score")]
        public virtual int CreditScore { get; set; }

        [Required, UIHint("EnumCheck"), Display(Name="Estimated Credit Score")]
        public CreditScoreEstimateEnum CreditScoreEstimate { get; set; }

        [Display(Name="Current Interest Rate"), DefaultValue((double) 0.0)]
        public double? CurrentInterestRate { get; set; }

        [Display(Name="Current LTV"), DefaultValue((double) 0.0), DataType(DataType.Currency)]
        public virtual double CurrentLTV { get; set; }

        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true), Display(Name="Date Of Birth"), DataType(DataType.Date)]
        public virtual DateTime? DateOfBirth { get; set; }

        [Display(Name="In Last 12 months, # of over 30 days late on Mortgage"), UIHint("EnumCheck")]
        public DaysLateEnum DaysLate { get; set; }

        [DataType(DataType.Currency), Display(Name="Down Payment Amount"), DefaultValue((double) 0.0)]
        public virtual decimal? DownPaymentAmount { get; set; }

        [Display(Name="Email"), DataType(DataType.EmailAddress), EmailAddress]
        public string EmailAddress { get; set; }

        [DefaultValue((double) 0.0), DataType(DataType.Currency), Display(Name="Estimated Homeowners Association Fees (Annual)")]
        public virtual decimal? EstimatedHomeownersAssociationFeesAnnual { get; set; }

        [DefaultValue((double) 0.0), Display(Name="Estimated Home Value"), DataType(DataType.Currency)]
        public virtual decimal? EstimatedHomeValue { get; set; }

        [Display(Name="Estimate Total Debt to Pay Off"), DefaultValue((double) 0.0), DataType(DataType.Currency)]
        public virtual decimal? EstimateTotalDebtToPayOff { get; set; }

        [Required, UIHint("EnumCheck"), Display(Name="filed Bankruptcy")]
        public FiledBankruptcyTypeEnum FiledBankruptcyType { get; set; }

        [Display(Name="Property County"), Required]
        public int Fips { get; set; }

        [DataType(DataType.Currency), Display(Name="First Mortgage Balance"), DefaultValue((double) 0.0)]
        public virtual decimal? FirstMortgageBalance { get; set; }

        [Display(Name="Started 1st Mortgage On"), DataType(DataType.Date)]
        public DateTime? FirstMortgageOriginationDate { get; set; }

        [Display(Name="1st Mortgage Payment"), DefaultValue((double) 0.0), DataType(DataType.Currency)]
        public virtual decimal? FirstMortgagePayment { get; set; }

        [Required, Display(Name="First name")]
        public string FirstName { get; set; }

        [DataType(DataType.Date), Display(Name="Date of Foreclosure, Short Sale, Deed in Lieu")]
        public DateTime? ForeclosureShortSaleDeedinLieuDate { get; set; }

        [UIHint("EnumCheck"), Display(Name="Month")]
        public MonthsEnum ForeclosureShortSaleDeedinLieuMonth { get; set; }

        [UIHint("EnumCheck"), Display(Name="Year")]
        public YearsEnum ForeclosureShortSaleDeedinLieuYear { get; set; }

        [UIHint("EnumCheck"), Display(Name="Any Foreclosures, Short Sale, or Deed in Lieu")]
        public ForeclosuresShortSaleDeedinLieuEnum ForeclosuresShortSaleDeedinLieu { get; set; }

        [Display(Name="Gross Annual Income"), DefaultValue((double) 0.0), DataType(DataType.Currency)]
        public virtual decimal GrossAnnualIncome { get; set; }

        [UIHint("EnumCheck"), Display(Name="Do you have a 2nd Mortgage")]
        public HaveSecondMortgageEnum Has2ndMortgage { get; set; }

        [Display(Name="Do you have a 2nd Mortgage"), UIHint("EnumCheck")]
        public YesNoAns? Have2ndMortgage { get; set; }

        [Display(Name="HOA Dues/Fees"), UIHint("Bool"), DefaultValue(false)]
        public bool HoaDuesFees { get; set; }

        [Display(Name="Rate Type"), Required, UIHint("EnumCheck")]
        public InterestRateTypeEnum InterestRateType { get; set; }

        [DefaultValue(true)]
        public bool IsQualifiedChapter13 { get; set; }

        [DefaultValue(true)]
        public bool IsQualifiedChapter7 { get; set; }

        [DefaultValue(true)]
        public bool IsQualifiedforeclosure { get; set; }

        [DefaultValue(true)]
        public bool IsQualifiedJudgment { get; set; }

        [Required, Display(Name="Last name")]
        public string LastName { get; set; }

        [DefaultValue(0), Display(Name="Lates12 Cred")]
        public virtual int lates12Credit { get; set; }

        [Display(Name="Lates24 Cre"), DefaultValue(0)]
        public virtual int lates24Credit { get; set; }

        [ForeignKey("LoanOfficerID")]
        public virtual CcsData.Models.LoanOfficer LoanOfficer { get; set; }

        [Display(Name="Loan Officer")]
        public virtual int? LoanOfficerID { get; set; }

        [ForeignKey("LoanProcessorID")]
        public virtual CcsData.Models.LoanProcessor LoanProcessor { get; set; }

        [Display(Name="Loan Processor")]
        public virtual int? LoanProcessorID { get; set; }

        [Display(Name="Loan type"), UIHint("EnumCheck"), Required]
        public LoanTypeEnum LoanType { get; set; }

        [UIHint("EnumCheck"), DefaultValue(0), Display(Name="Loan Type Requested"), Required]
        public LoanTypeRequestedEnum LoanTypeRequested { get; set; }

        [DataType(DataType.Currency), Display(Name="Mothly Mortgage Insurance")]
        public virtual decimal? MonthlyMortgageInsur { get; set; }

        [Required, Display(Name="Term"), UIHint("EnumCheck")]
        public MortgageTermEnum MortgageTerm { get; set; }

        [DefaultValue(false)]
        public bool NoMorgagesOnCredit { get; set; }

        [ForeignKey("OptionSelectedID")]
        public virtual CcsData.Models.OptionSelected OptionSelected { get; set; }

        public virtual int? OptionSelectedID { get; set; }

        [Display(Name="How Long Will You Own"), UIHint("EnumCheck")]
        public OwnerShipLongevityEnum OwnerShipLongevity { get; set; }

        [UIHint("EnumCheck"), Display(Name="This Property is my"), Required]
        public OwnershipTypeEnum OwnerShipType { get; set; }

        [Display(Name="Pay Off The 2nd Mortgage"), UIHint("EnumCheck")]
        public YesNoAns? PayOff2ndMortgage { get; set; }

        [Display(Name="Phone")]
        public string Phone { get; set; }

        [ForeignKey("PropertyID")]
        public virtual CcsData.Models.Property Property { get; set; }

        public virtual int? PropertyID { get; set; }

        [Required, UIHint("EnumCheck"), Display(Name="Property Type is")]
        public PropertyTypeEnum PropertyType { get; set; }

        [DataType(DataType.Currency), Display(Name="Proposed Loan Amount"), DefaultValue((double) 0.0)]
        public virtual decimal ProposedLoanAmount { get; set; }

        public List<PublicRecord> publicRecords { get; set; }

        [Display(Name="PurchasePrice"), DefaultValue((double) 0.0), DataType(DataType.Currency)]
        public virtual decimal PurchasePrice { get; set; }

        [Display(Name="Homeowners Insurance"), UIHint("Bool"), DefaultValue(false)]
        public bool PymtIncludesHomeownersInsurance { get; set; }

        [DefaultValue(false), Display(Name="Mortgage Insurance"), UIHint("Bool")]
        public bool PymtIncludesMI { get; set; }

        [Display(Name="None"), UIHint("Bool"), DefaultValue(false)]
        public bool PymtIncludesMone { get; set; }

        [Display(Name="Property taxes"), DefaultValue(false), UIHint("Bool")]
        public bool PymtIncludesPropTaxes { get; set; }

        [ForeignKey("RealtorID")]
        public virtual CcsData.Models.Realtor Realtor { get; set; }

        [Display(Name="Realtor")]
        public virtual int? RealtorID { get; set; }

        [UIHint("EnumCheck"), Display(Name="Is this a Rural Property")]
        public YesNoAns RuralProperty { get; set; }

        [DefaultValue((double) 0.0), Display(Name="2nd Mortgage Balance"), DataType(DataType.Currency)]
        public virtual decimal? SecondMortgageBalance { get; set; }

        [DefaultValue((double) 0.0), Display(Name="2nd Mortgage Interst rate")]
        public double? SecondMortgageInterestRate { get; set; }

        [DataType(DataType.Date), Display(Name="Started this Mortgage On")]
        public DateTime? SecondMortgageOriginationDate { get; set; }

        [Display(Name="Month"), UIHint("EnumCheck")]
        public MonthsEnum SecondMortgageOriginationMonth { get; set; }

        [Display(Name="Year"), UIHint("EnumCheck")]
        public YearsEnum SecondMortgageOriginationYear { get; set; }

        [Display(Name="2nd Mortgage Payment"), DefaultValue((double) 0.0), DataType(DataType.Currency)]
        public virtual decimal? SecondMortgagePayment { get; set; }

        [Display(Name="2nd Mortgae Rate Type"), UIHint("EnumCheck")]
        public SecondMortgageRateTypeEnum SecondMortgageRateType { get; set; }

        [UIHint("EnumCheck"), UIHint("EnumCheck"), Display(Name="2nd Mortgage Term")]
        public SecondMortgageTermEnum? SecondMortgageTerm { get; set; }

        [Display(Name="Seller Paid Credit for Closing Cost"), DefaultValue((double) 0.0), DataType(DataType.Currency)]
        public virtual decimal? SellerPaidCreditClosingCost { get; set; }

        [MaxLength(11), Display(Name="SSN: ")]
        public virtual string SocialSecurityNumber { get; set; }

        [Display(Name="State")]
        public string State { get; set; }

        [Display(Name="TOT Bal Cred")]
        public virtual decimal? TotalBalanceCredit { get; set; }

        [DefaultValue((double) 0.0), Display(Name="Total of all monthly payments (Credit cards, car loans ...ect"), DataType(DataType.Currency)]
        public virtual decimal TotalMontlyPayments { get; set; }

        [Display(Name="Total of Monthly Payments on Debt to Pay Off"), DefaultValue((double) 0.0), DataType(DataType.Currency)]
        public virtual decimal? TotalOfMonthlyPaymentsOnDebtToPayOff { get; set; }

        [Display(Name="TOT Pmt Cred")]
        public virtual decimal? TotalPaymentCredit { get; set; }

        [Display(Name="Property State")]
        public UsStateEnum? UsState { get; set; }

        [Display(Name="Are you a Veteran"), UIHint("EnumCheck")]
        public YesNoAns Veteran { get; set; }

        [Display(Name="Zip/Postal Code")]
        public string ZipCode { get; set; }
    }
}

