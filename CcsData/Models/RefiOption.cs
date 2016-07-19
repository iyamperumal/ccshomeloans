namespace CcsData.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class RefiOption
    {
        [Display(Name="Accelerated Payoff: Total Amount of All Payments"), DataType(DataType.Currency)]
        public virtual decimal? AcceleratedPayoffTotalAmountOfAllPayments { get; set; }

        [Display(Name="Adjusted Loan Orgination Fee"), DataType(DataType.Currency)]
        public virtual decimal? AdjustedLoanOriginationFee { get; set; }

        [DataType(DataType.Currency), Display(Name="Amount of New Payment + Monthly Savings")]
        public virtual decimal? AmountOfNewPaymentPlusMonthlySavings { get; set; }

        [DataType(DataType.Currency), Display(Name="Appraisal Fee")]
        public virtual decimal? AppraisalFee { get; set; }

        public virtual double? APR { get; set; }

        [DefaultValue((double) 100.0)]
        public virtual double BackDTI { get; set; }

        public double? BlendedRateSavings { get; set; }

        public decimal Cashout { get; set; }

        [DataType(DataType.Currency), Display(Name="Closing/Escrow Fee")]
        public virtual decimal? ClosingEscrowFee { get; set; }

        [DefaultValue((double) 100.0)]
        public virtual double CLTV { get; set; }

        [Display(Name="Cost/Savings Break Even Analysis")]
        public virtual decimal? CostSavingsBreakEvenAnalysis { get; set; }

        [DataType(DataType.Currency), Display(Name="County Property Tax Reserves")]
        public virtual decimal? CountyPropertyTaxReserves { get; set; }

        [DataType(DataType.Currency), Display(Name="Credit: ")]
        public virtual decimal? Credit { get; set; }

        [Display(Name="Credit points: ")]
        public virtual double? CreditPoints { get; set; }

        [Display(Name="Credit Report Fee")]
        public virtual decimal? CreditReportFee { get; set; }

        [Display(Name="Monthly Amount"), DataType(DataType.Currency)]
        public virtual decimal? CT_MonthlyAmount { get; set; }

        [Display(Name="Months Reserves")]
        public virtual decimal? CT_MonthsReserves { get; set; }

        [Display(Name="Current Interest Rate: ")]
        public virtual double? CurrentInterestRate { get; set; }

        [DataType(DataType.Currency), Display(Name="Daily Interest Charges")]
        public virtual decimal? DailyInterestCharges { get; set; }

        [Display(Name="Date Of Orgination: "), DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true), DataType(DataType.Date)]
        public virtual DateTime? DateOfOrgination { get; set; }

        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true), DataType(DataType.Date), Display(Name="Date Prepared ")]
        public virtual DateTime? DatePrepared { get; set; }

        [DataType(DataType.Currency), Display(Name="Refi Debt to be Paid off")]
        public virtual decimal? DebtToBePaidOff { get; set; }

        [DataType(DataType.Currency), Display(Name="Discount: ")]
        public virtual decimal? Discount { get; set; }

        [Display(Name="Discount Points: ")]
        public virtual double? DiscountPoints { get; set; }

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

        [DataType(DataType.Currency), Display(Name="Estimated Prepaids/Reserves")]
        public virtual decimal? EstimatedPrepaidsReserves { get; set; }

        [DefaultValue((double) 0.0), Display(Name="Flood Certification Fee: ")]
        public virtual decimal FloodCertificationFee { get; set; }

        [DefaultValue((double) 100.0)]
        public virtual double FrontDTI { get; set; }

        [DataType(DataType.Currency), Display(Name="Hazard Insurance Reserves: ")]
        public virtual decimal? HazardInsuranceReserves { get; set; }

        [DataType(DataType.Currency), Display(Name="Monthly Amount")]
        public virtual decimal? HI_MonthlyAmount { get; set; }

        [Display(Name="Months Reserves")]
        public virtual decimal? HI_MonthsReserves { get; set; }

        [Display(Name="Insurance: "), DataType(DataType.Currency)]
        public virtual decimal? Insurance { get; set; }

        [Display(Name="Intangible Tax"), DataType(DataType.Currency)]
        public virtual decimal? IntangibleTax { get; set; }

        [Display(Name="Interest Rate")]
        public virtual double? InterestRate { get; set; }

        [Display(Name="Interest Rate Savings")]
        public virtual double? InterestRateSavings { get; set; }

        [DataType(DataType.Currency), Display(Name="Lender's Title Insurance")]
        public virtual decimal? LenderTitleInsurance { get; set; }

        [Display(Name="Loan Amount"), DataType(DataType.Currency)]
        public virtual decimal? LoanAmount { get; set; }

        [Display(Name="Loan Balance: "), DataType(DataType.Currency)]
        public virtual decimal? LoanBalance { get; set; }

        [DefaultValue((double) 100.0)]
        public virtual double LTV { get; set; }

        [DataType(DataType.Currency), Display(Name="Old Mortage Insurance Monthly Amount"), DefaultValue((double) 0.0)]
        public virtual decimal? MI_MonthlyAmount { get; set; }

        [Display(Name="Number Of Months Reserved")]
        public virtual decimal? MI_MonthsReserves { get; set; }

        [Display(Name="MI Premium/VA Funding Fee: "), DataType(DataType.Currency)]
        public virtual decimal? MI_Upfront_Fee { get; set; }

        [Display(Name="MI Amount: "), DataType(DataType.Currency)]
        public virtual decimal? MIAmount { get; set; }

        [DataType(DataType.Currency), Display(Name="Monthly Payment P&I: ")]
        public virtual decimal? MonthlyPayment { get; set; }

        [Display(Name="Monthly Payments Eliminated")]
        public virtual decimal? MonthlyPaymentsEliminated { get; set; }

        [DataType(DataType.Currency), Display(Name="Monthly Savings")]
        public virtual decimal? MonthlySavings { get; set; }

        [DataType(DataType.Currency), Display(Name="Taxes: ")]
        public virtual decimal? monthlyTaxes { get; set; }

        [Display(Name=" # of Months Remaining to be Paid: ")]
        public virtual int? MonthsPaidRemaining { get; set; }

        public int MonthsPaidRemaining2nd { get; set; }

        [Display(Name="Mortgage Recording Charges"), DataType(DataType.Currency)]
        public virtual decimal? MortgageRecordingCharges { get; set; }

        private Applicant MortgApplicant { get; set; }

        [DataType(DataType.Currency), Display(Name="Net Savings from Old Loan to New Loan")]
        public virtual decimal? NetSavingsFromOldLoanToNewLoan { get; set; }

        [DataType(DataType.Currency), DefaultValue((double) 0.0), Display(Name="New Mortage Insurance Monthly Amount")]
        public virtual decimal? NewMI_MonthlyAmount { get; set; }

        [DataType(DataType.Currency), Display(Name="New Monthly Payment Principal & Interest")]
        public virtual decimal? NewMonthlyPaymentPrincipalInterest { get; set; }

        public decimal? NewPaymentPlusMonthlySavings { get; set; }

        [DataType(DataType.Currency), Display(Name="New Loan: Total Amount of All Payments to be made")]
        public virtual decimal? NewTotalAmountOfAllPaymentsToBeMade { get; set; }

        [Display(Name="NumberOfDays"), DefaultValue(0x19)]
        public virtual int? NumberOfDays { get; set; }

        [Display(Name="Number of Payments to Maturity")]
        public virtual int? NumberofPaymentstoMaturity { get; set; }

        [DataType(DataType.Currency), Display(Name="Old Monthly Payment Principal & Interest")]
        public virtual decimal? OldMonthlyPaymentPrincipalInterest { get; set; }

        [Display(Name="Old Loan: Total Amount of All Payments to be made"), DataType(DataType.Currency)]
        public virtual decimal? OldTotalAmountOfAllPaymentsToBeMade { get; set; }

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
        public int RefiOption_Id { get; set; }

        public decimal? SecondMtgBalance { get; set; }

        [DefaultValue(false)]
        public virtual bool Selected { get; set; }

        [DefaultValue(false)]
        public bool ShowBlendedRate { get; set; }

        [DefaultValue(false)]
        public bool ShowCashout { get; set; }

        public bool ShowDebtConsal { get; set; }

        [DefaultValue(false)]
        public bool ShowInterestSaving { get; set; }

        public bool ShowMonthlySaving { get; set; }

        [DefaultValue(false)]
        public bool ShowNetSaving { get; set; }

        [DefaultValue(false)]
        public bool ShowSecondMtg { get; set; }

        [Display(Name="State Tax"), DataType(DataType.Currency)]
        public virtual decimal? StateTax { get; set; }

        [Display(Name="Tax Service Fee: "), DefaultValue((double) 0.0)]
        public virtual decimal TaxServiceFee { get; set; }

        [Display(Name="Term In Months: ")]
        public virtual int? TermInMonths { get; set; }

        [Display(Name="Term in Years")]
        public virtual int? TermInYears { get; set; }

        [Display(Name="Total Amount Needed"), DataType(DataType.Currency)]
        public virtual decimal? TotalAmountNeeded { get; set; }

        [DefaultValue((double) 0.0)]
        public decimal? TotalBalanceOfDebtToConsolidate { get; set; }

        [DataType(DataType.Currency), Display(Name="Total Fixed Fees:  ")]
        public virtual decimal? TotalFixedFees { get; set; }

        [DataType(DataType.Currency), DefaultValue((double) 0.0), Display(Name="Total New Principal Interest Payment With MI")]
        public virtual decimal? TotalNewPrincipalInterestPaymentWithMI { get; set; }

        [DefaultValue((double) 0.0), DataType(DataType.Currency), Display(Name="Total Old Principal Interest Payment With MI")]
        public virtual decimal? TotalOldPrincipalInterestPaymentWithMI { get; set; }

        [DataType(DataType.Currency), Display(Name="Total Savings from Old Mortgage to New Mortgage")]
        public virtual decimal? TotalSavingsFromOldMortgageToNewMortgage { get; set; }

        [Display(Name="Underwriting Fee"), DataType(DataType.Currency)]
        public virtual decimal? UnderwritingFee { get; set; }

        public int? VarNum { get; set; }

        [Display(Name="Years Required to Maturity")]
        public virtual double? YearsRequiredToMaturity { get; set; }
    }
}

