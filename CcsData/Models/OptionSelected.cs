namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class OptionSelected
    {
        public decimal? AcceleratedPayoffTotalAmountOfAllPayments { get; set; }

        public virtual double? APR { get; set; }

        public double BackDTI { get; set; }

        public double? BlendedRateSavings { get; set; }

        public decimal Cashout { get; set; }

        public double CLTV { get; set; }

        public decimal? CostSavingsBreakEvenAnalysis { get; set; }

        public double? EffectiveInterestRate { get; set; }

        public double FrontDTI { get; set; }

        public string Hilited { get; set; }

        public decimal? Hl_MonthlyPaymentsEliminated { get; set; }

        public decimal? Hl_MonthlySavings { get; set; }

        public int? Hl_MonthsPaidRemaining { get; set; }

        public decimal? Hl_TotalSavingsFromOldMortgageToNewMortgage { get; set; }

        public double? InterestRate { get; set; }

        public double? InterestRateSavings { get; set; }

        public decimal? LoanAmount { get; set; }

        public decimal? LoanBalance { get; set; }

        public decimal? LowestPayment { get; set; }

        public double LTV { get; set; }

        public decimal? MonthlyPaymentsEliminated { get; set; }

        public decimal? MonthlySavings { get; set; }

        public int? MonthsPaidRemaining { get; set; }

        public decimal? NetSavingsFromOldLoanToNewLoan { get; set; }

        public decimal? NewMonthlyPaymentPrincipalInterest { get; set; }

        public decimal? NewPaymentPlusMonthlySavings { get; set; }

        public int? NumberofPaymentstoMaturity { get; set; }

        public decimal? OldMonthlyPaymentPrincipalInterest { get; set; }

        public int OptIntex { get; set; }

        public string OptionName { get; set; }

        [Key]
        public virtual int OptionSelected_Id { get; set; }

        [StringLength(80)]
        public string PreparedFor { get; set; }

        public decimal? SecondMtgBalance { get; set; }

        public bool ShowBlendedRate { get; set; }

        public bool ShowCashout { get; set; }

        public bool ShowDebtConsal { get; set; }

        public bool ShowInterestSaving { get; set; }

        public bool ShowMonthlySaving { get; set; }

        public bool ShowNetSaving { get; set; }

        public bool ShowSecondMtg { get; set; }

        public int? TermInYears { get; set; }

        public decimal? TotalBalanceOfDebtToConsolidate { get; set; }

        public decimal? TotalSavingsFromOldMortgageToNewMortgage { get; set; }

        public int? VarNum { get; set; }
    }
}

