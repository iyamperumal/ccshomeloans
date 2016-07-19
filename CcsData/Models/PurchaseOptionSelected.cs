namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class PurchaseOptionSelected
    {
        public virtual double? APR { get; set; }

        public double BackDTI { get; set; }

        public decimal? CashRequiredForClosing { get; set; }

        public double CLTV { get; set; }

        public decimal? DiscoutRateBuyDown { get; set; }

        public decimal? DownPayment { get; set; }

        public decimal? EstimatedClosingCost { get; set; }

        public decimal? EstimatePrepaidItems { get; set; }

        public double FrontDTI { get; set; }

        public string Hilited { get; set; }

        public decimal? Hl_MonthlySavings { get; set; }

        public double? InterestRate { get; set; }

        public decimal? LenderCredit { get; set; }

        public decimal? LoanAmount { get; set; }

        public decimal? LowestPayment { get; set; }

        public double LTV { get; set; }

        public decimal? MonthlyHomeownerAssociationFees { get; set; }

        public decimal? MonthlyHomeownersInsurance { get; set; }

        public decimal? MonthlyMortgageInsurance { get; set; }

        public decimal? MonthlyPaymentPrincipalInterest { get; set; }

        public decimal? MonthlyPropertyTaxes { get; set; }

        public int OptIndex { get; set; }

        public int OptIntex { get; set; }

        public string OptionName { get; set; }

        public decimal? PMI_MIP_FundingFee { get; set; }

        [StringLength(80)]
        public string PreparedFor { get; set; }

        [Key]
        public virtual int purchaseOptionSelected_Id { get; set; }

        public decimal? PurchasePrice { get; set; }

        public decimal? SellerPaidClosingCost { get; set; }

        public int? TermInYears { get; set; }

        public decimal? TotalCostToPuchase { get; set; }

        public decimal? TotalMortgagePayment { get; set; }

        public int? VarNum { get; set; }
    }
}

