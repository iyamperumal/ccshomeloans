namespace CcsData.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    internal class OptionVM
    {
        public decimal? CostSavingsBreakEvenAnalysis { get; set; }

        public decimal? LoanAmount { get; set; }

        public decimal? MonthlySavings { get; set; }

        public decimal? NewMonthlyPaymentPrincipalInterest { get; set; }

        [Key]
        public virtual int Option_Id { get; set; }

        public string OptionName { get; set; }

        public int? TermInYears { get; set; }

        public decimal? TotalSavingsFromOldMortgageToNewMortgage { get; set; }

        public int? VarNum { get; set; }
    }
}

