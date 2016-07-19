namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum CashOutTypeEnum
    {
        [Display(Name="Cash in Hand")]
        Cash_in_Hand = 1,
        [Display(Name="Debt Consolidation")]
        Debt_Consolidation = 0
    }
}

