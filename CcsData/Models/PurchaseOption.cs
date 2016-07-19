namespace CcsData.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class PurchaseOption
    {
        [DataType(DataType.Currency), Display(Name="Accelerated Payoff: Total Amount of All Payments")]
        public virtual decimal? AcceleratedPayoffTotalAmountOfAllPayments { get; set; }

        [Display(Name="Adjusted Loan Orgination Fee"), DataType(DataType.Currency)]
        public virtual decimal? AdjustedLoanOriginationFee { get; set; }

        [DataType(DataType.Currency), Display(Name="Amount Requested for Renovations ")]
        public virtual decimal? AmountRequestedForRenovations { get; set; }

        [DataType(DataType.Currency), Display(Name="Appraisal Fee")]
        public virtual decimal? AppraisalFee { get; set; }

        public virtual double? APR { get; set; }

        [DefaultValue((double) 100.0)]
        public virtual double BackDTI { get; set; }

        [Display(Name="Closing/Escrow Fee"), DataType(DataType.Currency)]
        public virtual decimal? ClosingEscrowFee { get; set; }

        [DefaultValue((double) 100.0)]
        public virtual double CLTV { get; set; }

        [DataType(DataType.Currency), Display(Name="County Property Tax Reserves")]
        public virtual decimal? CountyPropertyTaxReserves { get; set; }

        [Display(Name="Credit: "), DataType(DataType.Currency)]
        public virtual decimal? Credit { get; set; }

        [Display(Name="Credit points: ")]
        public virtual double? CreditPoints { get; set; }

        [Display(Name="Credit Report Fee")]
        public virtual decimal? CreditReportFee { get; set; }

        [Display(Name="Monthly Amount"), DataType(DataType.Currency)]
        public virtual decimal? CT_MonthlyAmount { get; set; }

        [Display(Name="Months Reserves")]
        public virtual decimal? CT_MonthsReserves { get; set; }

        [DataType(DataType.Currency), Display(Name="Daily Interest Charges")]
        public virtual decimal? DailyInterestCharges { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true), Display(Name="Date Prepared ")]
        public virtual DateTime? DatePrepared { get; set; }

        [DataType(DataType.Currency), Display(Name="Discount: ")]
        public virtual decimal? Discount { get; set; }

        [Display(Name="Discount Points: ")]
        public virtual double? DiscountPoints { get; set; }

        public decimal? DownPayment { get; set; }

        [Display(Name="Effective Interest Rate")]
        public virtual double? EffectiveInterestRate { get; set; }

        [Display(Name="Endorsements Reconveyance Fee"), DataType(DataType.Currency)]
        public virtual decimal? EndorsementsReconveyanceFee { get; set; }

        [DataType(DataType.Currency), Display(Name="Escrows: ")]
        public virtual decimal? Escrows { get; set; }

        [Display(Name="Est. Closing Costs")]
        public virtual decimal? EstClosingCosts { get; set; }

        [Display(Name="Estimated Funds Needed"), DataType(DataType.Currency)]
        public virtual decimal? EstimatedFundsNeeded { get; set; }

        [Display(Name="Estimated Homeowners Association Fees (Annual)"), DefaultValue((double) 0.0), DataType(DataType.Currency)]
        public virtual decimal? EstimatedHomeownersAssociationFeesAnnual { get; set; }

        [DataType(DataType.Currency), Display(Name="Estimated Prepaids/Reserves")]
        public virtual decimal? EstimatedPrepaidsReserves { get; set; }

        [DefaultValue((double) 0.0), Display(Name="Flood Certification Fee: ")]
        public virtual decimal FloodCertificationFee { get; set; }

        [DefaultValue((double) 100.0)]
        public virtual double FrontDTI { get; set; }

        [Display(Name="Hazard Insurance Reserves: "), DataType(DataType.Currency)]
        public virtual decimal? HazardInsuranceReserves { get; set; }

        [DataType(DataType.Currency), Display(Name="Monthly Amount")]
        public virtual decimal? HI_MonthlyAmount { get; set; }

        [Display(Name="Months Reserves")]
        public virtual decimal? HI_MonthsReserves { get; set; }

        [Display(Name="Insurance: "), DataType(DataType.Currency)]
        public virtual decimal? Insurance { get; set; }

        [Display(Name="Intangible Tax"), DataType(DataType.Currency)]
        public virtual decimal? IntangibleTax { get; set; }

        [Display(Name="Interest Rate"), DefaultValue(0)]
        public virtual double InterestRate { get; set; }

        [Display(Name="Lender's Title Insurance"), DataType(DataType.Currency)]
        public virtual decimal? LenderTitleInsurance { get; set; }

        [DataType(DataType.Currency), Display(Name="Loan Amount")]
        public virtual decimal? LoanAmount { get; set; }

        [DefaultValue((double) 100.0)]
        public virtual double LTV { get; set; }

        [Display(Name="Number Of Months Reserved")]
        public virtual decimal? MI_MonthsReserves { get; set; }

        [DataType(DataType.Currency), Display(Name="MI Premium/VA Funding Fee: ")]
        public virtual decimal? MI_Upfront_Fee { get; set; }

        [DataType(DataType.Currency), Display(Name="MI Amount: ")]
        public virtual decimal? MIAmount { get; set; }

        [DataType(DataType.Currency), Display(Name="Monthly Payment P&I: ")]
        public virtual decimal? MonthlyPayment { get; set; }

        [Display(Name="Taxes: "), DataType(DataType.Currency)]
        public virtual decimal? monthlyTaxes { get; set; }

        [Display(Name="Mortgage Recording Charges"), DataType(DataType.Currency)]
        public virtual decimal? MortgageRecordingCharges { get; set; }

        private Applicant MortgApplicant { get; set; }

        [DefaultValue((double) 0.0), DataType(DataType.Currency), Display(Name="New Mortage Insurance Monthly Amount")]
        public virtual decimal? NewMI_MonthlyAmount { get; set; }

        [Display(Name="New Monthly Payment Principal & Interest"), DataType(DataType.Currency)]
        public virtual decimal? NewMonthlyPaymentPrincipalInterest { get; set; }

        [Display(Name="New Loan: Total Amount of All Payments to be made"), DataType(DataType.Currency)]
        public virtual decimal? NewTotalAmountOfAllPaymentsToBeMade { get; set; }

        [Display(Name="NumberOfDays"), DefaultValue(0x19)]
        public virtual int? NumberOfDays { get; set; }

        [Display(Name="Number of Payments to Maturity")]
        public virtual int? NumberofPaymentstoMaturity { get; set; }

        [Display(Name="Option: ")]
        public virtual string OptionName { get; set; }

        [Display(Name="Percent: ")]
        public virtual double? OriginationChargesPercent { get; set; }

        [Display(Name="% Charge: ")]
        public virtual double? PercentCharge { get; set; }

        [Display(Name="Pest Inspection Fee: "), DefaultValue((double) 0.0)]
        public virtual decimal PestInspectionFee { get; set; }

        [DataType(DataType.Currency), Display(Name="PMI/MIP/VA FF Reserves")]
        public virtual decimal? PMI_MIP_VA_FFReserves { get; set; }

        [Display(Name="Prepared For")]
        public virtual string PreparedFor { get; set; }

        [Display(Name="Processing Fee"), DataType(DataType.Currency)]
        public virtual decimal? ProcessingFee { get; set; }

        [Key]
        public int PurchaseOption_Id { get; set; }

        public decimal? PurchasePrice { get; set; }

        [DataType(DataType.Currency), Display(Name="Sale Price: ")]
        public virtual decimal? SalePrice { get; set; }

        [DefaultValue(false)]
        public virtual bool Selected { get; set; }

        [DataType(DataType.Currency), Display(Name="Seller Paid Credit for Closing Cost"), DefaultValue((double) 0.0)]
        public virtual decimal? SellerPaidCreditClosingCost { get; set; }

        [Display(Name="State Tax"), DataType(DataType.Currency)]
        public virtual decimal? StateTax { get; set; }

        [DefaultValue((double) 0.0), Display(Name="Survey Fee: ")]
        public virtual decimal SurveyFee { get; set; }

        [DefaultValue((double) 0.0), Display(Name="Tax Service Fee: ")]
        public virtual decimal TaxServiceFee { get; set; }

        [Display(Name="Term in Months")]
        public virtual int? TermInMonths { get; set; }

        [Display(Name="Term In Years: ")]
        public virtual int? TermInYears { get; set; }

        [Display(Name="Total Amount Needed"), DataType(DataType.Currency)]
        public virtual decimal? TotalAmountNeeded { get; set; }

        [Display(Name="Total Fixed Fees:  "), DataType(DataType.Currency)]
        public virtual decimal? TotalFixedFees { get; set; }

        [DataType(DataType.Currency), Display(Name="Total New Principal Interest Payment With MI"), DefaultValue((double) 0.0)]
        public virtual decimal? TotalNewPrincipalInterestPaymentWithMI { get; set; }

        [DataType(DataType.Currency), Display(Name="Underwriting Fee")]
        public virtual decimal? UnderwritingFee { get; set; }

        public int? VarNum { get; set; }

        [Display(Name="Years Required to Maturity")]
        public virtual double? YearsRequiredToMaturity { get; set; }
    }
}

