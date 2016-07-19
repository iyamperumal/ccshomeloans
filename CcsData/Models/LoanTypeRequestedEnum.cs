namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum LoanTypeRequestedEnum : byte
    {
        [Display(Name="Cash Out Mortgage")]
        CashOutMortgage = 6,
        [Display(Name="Debt Consolidation: Pay Off Creditors")]
        DebtConsolidationPayOffCreditors = 5,
        [Display(Name="Purchase Loan")]
        PurchaseLoan = 1,
        [Display(Name="Rate & Term Refi: Lower Payment")]
        RateAndTermRefiLowerPayment = 4,
        [Display(Name="Rate & Term Refi: Shorter Term")]
        RateAndTermRefiShorterTerm = 3,
        [Display(Name="Realtor Referred Purchase Loan")]
        RealtorReferredPurchaseLoan = 2,
        [Display(Name="Select")]
        Select = 0
    }
}

