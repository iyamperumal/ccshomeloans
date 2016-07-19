namespace CcsData.ViewModels
{
    using CcsData.Models;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class LeadSheetVM
    {
        [MaxLength(50)]
        public virtual string Ages { get; set; }

        [DataType(DataType.Currency), Display(Name="Amount (Gross Monthly)")]
        public virtual decimal? Amount { get; set; }

        [Display(Name="Any foreclosures?")]
        public virtual YesNoAns? AnyForclosure { get; set; }

        [Display(Name="Any Non Medical judgments or collections")]
        public virtual YesNoAns? AnyJudgments { get; set; }

        [Display(Name="Balance:")]
        public virtual decimal? Balance { get; set; }

        [Display(Name="Balloon Payment:")]
        public YesNoAns? BalloonPayment { get; set; }

        [Display(Name="Balloon Due Date")]
        public DateTime? BalloonPaymentDueDate { get; set; }

        public virtual BankruptcyChapterEnum? BankruptcyType { get; set; }

        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true), Display(Name="Date discharged"), DataType(DataType.Date)]
        public virtual DateTime? BankruptyDischargeDate { get; set; }

        [Display(Name="# of Bths")]
        public virtual float Bathrooms { get; set; }

        [Display(Name="# of Bdrms")]
        public virtual int Bedrooms { get; set; }

        [Display(Name="120 Days Late")]
        public virtual int? C120DaysLate { get; set; }

        [Display(Name="30 Days Late")]
        public virtual int? C30DaysLate { get; set; }

        [Display(Name="60 Days Late")]
        public virtual int? C60DaysLate { get; set; }

        [Display(Name="90 Days Late")]
        public virtual int? C90DaysLate { get; set; }

        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true), DataType(DataType.Date), Display(Name="Call Back Date ")]
        public virtual DateTime? CallBackDate { get; set; }

        [DataType(DataType.Time), Display(Name="Call Back Time ")]
        public virtual DateTime? CallBackTime { get; set; }

        public virtual decimal? CashOutAmountRequested { get; set; }

        [Display(Name="Cashout Purpose"), MaxLength(50)]
        public virtual string CashOutType { get; set; }

        [DataType(DataType.PhoneNumber), StringLength(50), Display(Name="Cell Phone: ")]
        public virtual string CellPhone { get; set; }

        public virtual string City { get; set; }

        [Display(Name="Date"), DataType(DataType.Date)]
        public virtual DateTime? ClientApplicationDate { get; set; }

        [Display(Name="Amount (Gross Monthly)"), DataType(DataType.Currency)]
        public virtual decimal? CoAmount { get; set; }

        [StringLength(50), Display(Name="Cell Phone: "), DataType(DataType.PhoneNumber)]
        public virtual string CoCellPhone { get; set; }

        [DataType(DataType.Date)]
        public virtual DateTime? CoDateOfBirth { get; set; }

        [Display(Name="Earning per pay period")]
        public virtual decimal? CoEarningsBeforeTax { get; set; }

        [MaxLength(50), DataType(DataType.EmailAddress), Display(Name="Email Address")]
        public virtual string CoEmailAddress { get; set; }

        [Display(Name="Employer Name"), MaxLength(50)]
        public virtual string CoEmployerName { get; set; }

        public virtual string CoEmploymentType { get; set; }

        [Display(Name="End Date"), DataType(DataType.Date)]
        public virtual DateTime? CoEndDate { get; set; }

        [DataType(DataType.PhoneNumber), StringLength(50), Display(Name="Fax #")]
        public virtual string CoFax { get; set; }

        [Display(Name="First Name: ")]
        public virtual string CoFirstName { get; set; }

        [Display(Name="Hire Date"), DataType(DataType.Date)]
        public virtual DateTime? CoHireDate { get; set; }

        [Display(Name="Home Phone: "), DataType(DataType.PhoneNumber), MaxLength(50)]
        public virtual string CoHomePhone { get; set; }

        [Display(Name="Type of Income"), StringLength(250)]
        public virtual string CoIncomeType { get; set; }

        [Display(Name="Last Name: ")]
        public virtual string CoLastName { get; set; }

        [Display(Name="Last Year's Adusted Gross Income Declared On Tax Returns? ")]
        public virtual decimal? CoLastYear_SE_EarningsReported_IRS { get; set; }

        [Display(Name="Last Year Depreciation As Reported on Tax Returns")]
        public virtual decimal? CoLastYearDepreciationAsReported { get; set; }

        [Display(Name="Middle Name: ")]
        public virtual string CoMiddleName { get; set; }

        [Display(Name="Computed Cashout Amount Available")]
        public virtual decimal? ComputedCashOutAmountAvailable { get; set; }

        [MaxLength(50), Display(Name="Condo Association Name")]
        public virtual string CondoAssociationName { get; set; }

        [Display(Name="Confirmed Value")]
        public virtual decimal? ConfirmedMarketValue { get; set; }

        [Display(Name="Confirmed Value Date"), DataType(DataType.Date)]
        public virtual DateTime? ConfirmedMarketValueDate { get; set; }

        [Display(Name="Future Years to Recieve")]
        public virtual int CoNumberFutureYearsToReceive { get; set; }

        [Display(Name="How Offten do you get Payed")]
        public virtual PayScheduleEnum? CoPayPeriod { get; set; }

        [Display(Name="Position"), MaxLength(50)]
        public virtual string CoPosition { get; set; }

        [Display(Name="Years Recieved")]
        public virtual int CoPreviousYearsReceived { get; set; }

        [StringLength(250), Display(Name="Remarks")]
        public string CoRemarks { get; set; }

        [Display(Name="Schedule Type"), MaxLength(50)]
        public virtual string CoScheduleType { get; set; }

        [MaxLength(11), Display(Name="Social Security Number: ")]
        public virtual string CoSocialSecurityNumber { get; set; }

        [MaxLength(250), Display(Name="Source")]
        public virtual string CoSource { get; set; }

        [DataType(DataType.PhoneNumber), MaxLength(50), Display(Name="Work Phone: ")]
        public virtual string CoWorkPhone { get; set; }

        [Display(Name="Year before Last: Adusted Gross Income Declared On Tax Returns? ")]
        public virtual decimal? CoYear2_SE_EarningsReported_IRS { get; set; }

        [Display(Name="Year Before Last: Depreciation As Reported on Tax Returns")]
        public virtual decimal? CoYear2DepreciationAsReported { get; set; }

        [Display(Name="Years In School: ")]
        public virtual int CoYearsInSchool { get; set; }

        [Display(Name="CurrentStanding:"), MaxLength(50)]
        public virtual string CurrentStanding { get; set; }

        [DataType(DataType.Date), Display(Name="Date Of Birth"), DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public virtual DateTime? DateOfBirth { get; set; }

        [Display(Name="Any Mainteneance Needed"), MaxLength(50)]
        public virtual string DefferedMaintenance { get; set; }

        [Display(Name="Earning Per Pay Period")]
        public virtual decimal? EarningsBeforeTax { get; set; }

        [MaxLength(50), DataType(DataType.EmailAddress)]
        public virtual string EmailAddress { get; set; }

        [Display(Name="Employer Name"), MaxLength(50)]
        public virtual string EmployerName { get; set; }

        [MaxLength(50), Display(Name="Employment Type")]
        public virtual string EmploymentType { get; set; }

        [Display(Name="End Date"), DataType(DataType.Date)]
        public virtual DateTime? EndDate { get; set; }

        [Display(Name="Estimated Value")]
        public virtual decimal? EstimatedMarketValue { get; set; }

        [Display(Name="Estimated Value Date"), DataType(DataType.Date)]
        public virtual DateTime? EstimatedMarketValueDate { get; set; }

        [Display(Name="Fax #"), StringLength(50), DataType(DataType.PhoneNumber)]
        public virtual string Fax { get; set; }

        [EnumDataType(typeof(YesNoAns)), Display(Name="Have you ever filed bankruptcy? ")]
        public virtual YesNoAns? FiledBankruptcy { get; set; }

        [Required, StringLength(50), Display(Name="First Name: ")]
        public virtual string FirstName { get; set; }

        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true), Display(Name="If so, What is the forclosure date?"), DataType(DataType.Date)]
        public virtual DateTime? ForclosureDate { get; set; }

        public virtual string Garage { get; set; }

        [Display(Name="Hire Date"), DataType(DataType.Date)]
        public virtual DateTime? HireDate { get; set; }

        [Display(Name="Condition Of Home compare to Neighbors")]
        public virtual string HomeCondition { get; set; }

        [DataType(DataType.PhoneNumber), MaxLength(50), Display(Name="Home Phone: ")]
        public virtual string HomePhone { get; set; }

        [Display(Name="How Old Are Judment And Collections?")]
        public string HowOldAreJudmentAndCollections { get; set; }

        [Display(Name="Type of Income"), StringLength(250)]
        public virtual string IncomeType { get; set; }

        [Display(Name="Interest Rate")]
        public float InterestRate { get; set; }

        [DataType(DataType.Currency), Display(Name="If so, What is the total balance? ")]
        public virtual decimal? JudgmentsBalance { get; set; }

        [Display(Name="LenderName"), StringLength(50)]
        public string LanderName { get; set; }

        [Display(Name="Last Known Credit Score")]
        public virtual int? LastKnownCreditScore { get; set; }

        [Display(Name="Last Name: "), StringLength(50), Required]
        public virtual string LastName { get; set; }

        [Display(Name="Date of Last Known Credit Score")]
        public virtual DateTime? LastScoreDate { get; set; }

        [Display(Name="Last Year's Adusted Gross Income Declared On Tax Returns? ")]
        public virtual decimal? LastYear_SE_EarningsReported_IRS { get; set; }

        [Display(Name="Last Year Depreciation As Reported on Tax Returns")]
        public virtual decimal? LastYearDepreciationAsReported { get; set; }

        public int? LeanPosition { get; set; }

        [Display(Name="Location"), MaxLength(50)]
        public virtual string LocationType { get; set; }

        [Display(Name="Acreage")]
        public virtual float? LotAcreage { get; set; }

        [Display(Name="120 Days Late")]
        public virtual int? M120DaysLate { get; set; }

        [Display(Name="30 Days Late")]
        public virtual int? M30DaysLate { get; set; }

        [Display(Name="60 Days Late")]
        public virtual int? M60DaysLate { get; set; }

        [Display(Name="90 Days Late")]
        public virtual int? M90DaysLate { get; set; }

        [Display(Name="Middle Name: ")]
        public virtual string MiddleName { get; set; }

        [Display(Name="Mobile Home Model"), MaxLength(50)]
        public virtual string MobileHomeModel { get; set; }

        [Display(Name="Mtg Insurance (Monthly):"), DefaultValue((double) 0.0)]
        public virtual decimal? MonthlyMortgageInsurance { get; set; }

        [Display(Name="Monthly Payment:")]
        public virtual decimal? MonthlyPayment { get; set; }

        [DefaultValue((double) 0.0), Display(Name="Mortage Type")]
        public virtual MortgageProgramOptionsEnum? MortageType { get; set; }

        [StringLength(50)]
        public virtual string Motive { get; set; }

        [Display(Name="Future Years to Recieve")]
        public virtual int NumberFutureYearsToReceive { get; set; }

        [Display(Name="Number Of Dependents: ")]
        public virtual int NumberOfDependents { get; set; }

        [Display(Name="Origination Date")]
        public DateTime? OriginationDate { get; set; }

        [Display(Name="How Offten do you get Payed")]
        public virtual PayScheduleEnum? PayPeriod { get; set; }

        [MaxLength(50)]
        public virtual string Position { get; set; }

        [Display(Name="Pre-Approved Payment:")]
        public virtual decimal? PreApprovedPayment { get; set; }

        [Display(Name="Pre-Approved Rate:")]
        public virtual decimal? PreApprovedRate { get; set; }

        [Display(Name="Pre-Payment Penalty:")]
        public PrepaymentEnum? PrePaymentPenalty { get; set; }

        [Display(Name="Years Recieved")]
        public virtual int PreviousYearsReceived { get; set; }

        [Display(Name="Street Address"), MaxLength(100)]
        public virtual string PropAddress { get; set; }

        [Display(Name="City"), MaxLength(50)]
        public virtual string PropCity { get; set; }

        [Display(Name="County"), MaxLength(50)]
        public virtual string propCounty { get; set; }

        [Display(Name="Improvements")]
        public virtual string PropertyImprovements { get; set; }

        public virtual string PropertyType { get; set; }

        [MaxLength(50), Display(Name="Sate")]
        public string PropState { get; set; }

        [MaxLength(20), Display(Name="Zipcode")]
        public virtual string PropZipCode { get; set; }

        [DataType(DataType.Date), Display(Name="Purshase Date")]
        public virtual DateTime? PurshaseDate { get; set; }

        [Display(Name="Purshase Price")]
        public virtual decimal? PurshasePrice { get; set; }

        [Display(Name="Pymt Includes Insurance?")]
        public virtual YesNoAns PymtIncludesInsurance { get; set; }

        [Display(Name="Pymt Includes Taxes")]
        public virtual YesNoAns PymtIncludesTaxes { get; set; }

        [StringLength(50), Display(Name="Rate Type")]
        public string RateType { get; set; }

        [Display(Name="How would you rate your credit")]
        public virtual CreditRatingEnum? Rating { get; set; }

        [StringLength(250)]
        public string Remarks { get; set; }

        [ForeignKey("Rep_ID")]
        public virtual CcsData.Models.Representative Rep { get; set; }

        public virtual int? Rep_ID { get; set; }

        [Display(Name="Representative")]
        public virtual CcsData.Models.Representative Representative { get; set; }

        [Display(Name="Full/PartTime"), MaxLength(50)]
        public virtual string ScheduleType { get; set; }

        [Display(Name="Credit Report Score")]
        public virtual int? Score { get; set; }

        [Display(Name="Credit Report Date")]
        public virtual DateTime? ScoreDate { get; set; }

        [MaxLength(11), Display(Name="SSN: ")]
        public virtual string SocialSecurityNumber { get; set; }

        [MaxLength(250)]
        public virtual string Source { get; set; }

        [Display(Name="Sq.Ft.")]
        public virtual double? SqFt { get; set; }

        [Required]
        public string State { get; set; }

        [Display(Name="Street Address")]
        public virtual string StreetAddress { get; set; }

        [Display(Name="Mortgage Term")]
        public int? Term { get; set; }

        [Display(Name="Total Balance Of Debt To Consolidate")]
        public virtual decimal? TotalBalanceOfDebtToConsolidate { get; set; }

        [Display(Name="Total Monthly Amount Of Debt Payments To Consolidate")]
        public virtual decimal? TotalMonthlyAmountOfDebtPaymentsToConsolidate { get; set; }

        [DataType(DataType.PhoneNumber), MaxLength(50), Display(Name="Work Phone: ")]
        public virtual string WorkPhone { get; set; }

        [Display(Name="Year before Last: Adusted Gross Income Declared On Tax Returns? ")]
        public virtual decimal? Year2_SE_EarningsReported_IRS { get; set; }

        [Display(Name="Year Before Last: Depreciation As Reported on Tax Returns")]
        public virtual decimal? Year2DepreciationAsReported { get; set; }

        [Display(Name="How long at this address")]
        public virtual int YearAtThisAddress { get; set; }

        public virtual int YearBuilt { get; set; }

        [Display(Name="Home Insurance Payment (Yearly):")]
        public virtual decimal? YearlyHomeInsurancePayment { get; set; }

        [Display(Name="Property Taxes (Yearly):")]
        public virtual decimal? YearlyPropertyTaxes { get; set; }

        [Display(Name="Years In School: ")]
        public virtual int YearsInSchool { get; set; }

        [Display(Name="Zip Code"), Required]
        public virtual string ZipCode { get; set; }
    }
}

