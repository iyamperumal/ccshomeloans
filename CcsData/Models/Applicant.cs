namespace CcsData.Models
{
    using CreditPull;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class Applicant
    {
        public Applicant()
        {
            this.ClientApplicationDate = new DateTime?(DateTime.Today);
        }

        public virtual List<Address> Addresses { get; set; }

        [MaxLength(50)]
        public virtual string Ages { get; set; }

        [Key]
        public virtual int Applicant_Id { get; set; }

        public virtual List<Application> Applications { get; set; }

        [Display(Name="BackDTI Credit")]
        public virtual double? BackDTI_Credit { get; set; }

        public virtual List<BankAccount> BankAccounts { get; set; }

        public virtual List<Bank> Banks { get; set; }

        [MaxLength(50), Display(Name="Batch Number: ")]
        public virtual string BatchNumber { get; set; }

        [Display(Name="Call Back Date @ Time: "), DataType(DataType.Date), DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public virtual DateTime? CallBackDate { get; set; }

        [DataType(DataType.Time), Display(Name="Call Back Date & Time: ")]
        public virtual DateTime? CallBackTime { get; set; }

        public virtual List<CallLog> CallLogs { get; set; }

        [StringLength(50), Display(Name="Care Of: ")]
        public virtual string CareOfName { get; set; }

        [Display(Name="Cashout Requested ?"), DefaultValue((double) 0.0)]
        public virtual decimal? CashOutAmountRequested { get; set; }

        [MaxLength(50), Display(Name="Cashout Purpose")]
        public virtual string CashOutType { get; set; }

        [Display(Name="Cell Phone: "), DataType(DataType.PhoneNumber), StringLength(50)]
        public virtual string CellPhone { get; set; }

        [DataType(DataType.Date), Display(Name="Application Date: "), DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public virtual DateTime? ClientApplicationDate { get; set; }

        [UIHint("EnumCheck"), Display(Name="Client Refuse Reason: ")]
        public virtual ClientRefuseReasonEnum? ClientRefuseReason { get; set; }

        public virtual List<CoApplicant> CoApplicants { get; set; }

        [Display(Name="Computed Cashout Amount Available")]
        public virtual decimal? ComputedCashOutAmountAvailable { get; set; }

        [ForeignKey("CreditID")]
        public virtual CcsData.Models.Credit Credit { get; set; }

        public virtual int? CreditID { get; set; }

        public List<CreditLiability> CreditLiabilities { get; set; }

        public List<CreditMortgage> CreditMortgages { get; set; }

        [Display(Name="credit Pulled"), DefaultValue(false)]
        public virtual bool creditPulled { get; set; }

        [UIHint("EnumCheck"), Display(Name="Credit Rating Reason: ")]
        public virtual CreditRatingReasonEnum? CreditRatingReason { get; set; }

        public List<RequestCred> CreditRequests { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true), Display(Name="Credit Resolicit Date")]
        public virtual DateTime? CreditResolicitDate { get; set; }

        public List<ResponseData> CreditResponseDatas { get; set; }

        public List<ResponseCred> CreditResponses { get; set; }

        [DefaultValue(0), Display(Name="Credit Score")]
        public virtual int CreditScore { get; set; }

        [UIHint("EnumCheck"), Display(Name="Estimated Credit Score")]
        public CreditScoreEstimateEnum? CreditScoreEstimate { get; set; }

        [UIHint("EnumCheck"), Display(Name="Rate Cutomer Service: ")]
        public virtual CustomerServiceRatingEnum? CustomerServiceRating { get; set; }

        [Display(Name="Date Of Birth"), DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true), DataType(DataType.Date)]
        public virtual DateTime? DateOfBirth { get; set; }

        [Display(Name="Disposition: "), UIHint("EnumCheck")]
        public virtual DispositionEnum? Disposition { get; set; }

        public virtual List<CcsData.Models.FileUpload.DocFile> DocFile { get; set; }

        [UIHint("EnumCheck"), DefaultValue(0), Display(Name="Do Not Call")]
        public virtual YesNoAns? DoNotCall { get; set; }

        [Display(Name="DTI Resolicit Date "), DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true), DataType(DataType.Date)]
        public virtual DateTime? DTIResolicitDate { get; set; }

        [DataType(DataType.EmailAddress), MaxLength(50)]
        public virtual string EmailAddress { get; set; }

        public virtual List<Employment> Employments { get; set; }

        [DataType(DataType.PhoneNumber), StringLength(50), Display(Name="Fax #")]
        public virtual string Fax { get; set; }

        [StringLength(50), Display(Name="First Name: "), Required]
        public virtual string FirstName { get; set; }

        public virtual List<Foreclosure> Foreclosures { get; set; }

        [Display(Name="Full Name: "), StringLength(100)]
        public virtual string FullName { get; set; }

        [Display(Name="Do you have a 2nd Mortgage"), UIHint("EnumCheck")]
        public HaveSecondMortgageEnum Has2ndMortgage { get; set; }

        [DefaultValue(1), UIHint("EnumCheck")]
        public YesNoAns Have2ndMortgage { get; set; }

        [DataType(DataType.PhoneNumber), MaxLength(50), Display(Name="Home Phone: ")]
        public virtual string HomePhone { get; set; }

        public List<Inquiry> Inquiries { get; set; }

        [DefaultValue(true)]
        public bool IsQualifiedChapter13 { get; set; }

        [DefaultValue(true)]
        public bool IsQualifiedChapter7 { get; set; }

        [DefaultValue(true)]
        public bool IsQualifiedforeclosure { get; set; }

        [DefaultValue(true)]
        public bool IsQualifiedJudgment { get; set; }

        [StringLength(50), Display(Name="Last Name: "), Required]
        public virtual string LastName { get; set; }

        public List<CcsData.Models.CreditPull.Lates> Lates { get; set; }

        [DefaultValue(0), Display(Name="Lates12 Cred")]
        public virtual int lates12Credit { get; set; }

        [DefaultValue(0), Display(Name="Lates24 Cre")]
        public virtual int lates24Credit { get; set; }

        [ForeignKey("LeadsData_ID")]
        public virtual CcsData.Models.LeadData LeadData { get; set; }

        public virtual int? LeadsData_ID { get; set; }

        [Display(Name="Lender Refuse Reason: "), UIHint("EnumCheck")]
        public virtual LenderRefuseReasonEnum? LenderRefuseReason { get; set; }

        public virtual List<Liability> Liabilities { get; set; }

        public virtual List<Loan> loans { get; set; }

        [ForeignKey("LoanStageID")]
        public virtual CcsData.Models.LoanStage LoanStage { get; set; }

        public virtual int? LoanStageID { get; set; }

        [DefaultValue(0), Display(Name="Loan Type Requested"), Required, UIHint("Enum")]
        public LoanTypeRequestedEnum LoanTypeRequested { get; set; }

        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true), Display(Name="LTV Resolicit Date "), DataType(DataType.Date)]
        public virtual DateTime? LTVResolicitDate { get; set; }

        [UIHint("EnumCheck"), Display(Name="Marital Status: ")]
        public virtual Maritalstatus? MaritalStatus { get; set; }

        [Display(Name="Middle Name: "), StringLength(50)]
        public virtual string MiddleName { get; set; }

        public virtual List<Mortgage> Mortgages { get; set; }

        [UIHint("EnumCheck")]
        public virtual MotiveEnum? Motive { get; set; }

        public virtual List<NewMortgage> NewMortgages { get; set; }

        public bool NoMorgagesOnCredit { get; set; }

        [DefaultValue(0), Display(Name="Number Of Dependents: ")]
        public virtual int NumberOfDependents { get; set; }

        [Display(Name="Option Number: ")]
        public virtual int? OptionNumber { get; set; }

        [ForeignKey("OptionSelectedID")]
        public virtual CcsData.Models.OptionSelected OptionSelected { get; set; }

        public virtual int? OptionSelectedID { get; set; }

        public virtual List<AdditionalIncome> OtherIncomes { get; set; }

        [DefaultValue(1), UIHint("EnumCheck")]
        public YesNoAns? PayOff2ndMortgage { get; set; }

        public virtual List<Property> Properties { get; set; }

        public List<PublicRecord> publicRecords { get; set; }

        [ForeignKey("RealtorID")]
        public virtual CcsData.Models.Realtor Realtor { get; set; }

        public virtual int? RealtorID { get; set; }

        [ForeignKey("Rep_ID")]
        public virtual Representative Rep { get; set; }

        public virtual int? Rep_ID { get; set; }

        [ForeignKey("SecondMortgage_ID")]
        public virtual CcsData.Models.SecondMortgage SecondMortgage { get; set; }

        public virtual int? SecondMortgage_ID { get; set; }

        [ForeignKey("SellerID")]
        public virtual CcsData.Models.Seller Seller { get; set; }

        public virtual int? SellerID { get; set; }

        [UIHint("EnumCheck"), Display(Name="Rate this Site: ")]
        public virtual SiteRatingEnum? SiteRating { get; set; }

        [MaxLength(11), Display(Name="Last 4 SSN")]
        public virtual string SocialSecurity4 { get; set; }

        [Display(Name="SSN: "), MaxLength(11)]
        public virtual string SocialSecurityNumber { get; set; }

        [StringLength(50)]
        public virtual string Suffix { get; set; }

        [Display(Name="Times Mailed: ")]
        public virtual int TimesMailed { get; set; }

        [Display(Name="TOT Bal Cred")]
        public virtual decimal? TotalBalanceCredit { get; set; }

        [Display(Name="Total Balance Of Debt To Consolidate")]
        public virtual decimal? TotalBalanceOfDebtToConsolidate { get; set; }

        [Display(Name="Total Monthly Amount Of Debt Payments To Consolidate")]
        public virtual decimal? TotalMonthlyAmountOfDebtPaymentsToConsolidate { get; set; }

        [Display(Name="TOT Pmt Cred")]
        public virtual decimal? TotalPaymentCredit { get; set; }

        [ForeignKey("VariableCustID")]
        public virtual CcsData.Models.VariableCust VariableCust { get; set; }

        public virtual int? VariableCustID { get; set; }

        [UIHint("EnumCheck"), DefaultValue(1)]
        public YesNoAns Veteran { get; set; }

        [DataType(DataType.PhoneNumber), MaxLength(50), Display(Name="Work Phone: ")]
        public virtual string WorkPhone { get; set; }

        [DefaultValue(0), Display(Name="Years In School: ")]
        public virtual int YearsInSchool { get; set; }
    }
}

