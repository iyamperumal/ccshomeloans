namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum LenderRefuseReasonEnum
    {
        [Display(Name="Credit Rating")]
        CreditRating = 3,
        DTI = 2,
        LTV = 1
    }
}

