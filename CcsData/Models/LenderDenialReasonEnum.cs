namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum LenderDenialReasonEnum
    {
        [Display(Name="Credit Score")]
        CreditScore = 0,
        Income = 2,
        LTV = 1,
        TDI = 3
    }
}

