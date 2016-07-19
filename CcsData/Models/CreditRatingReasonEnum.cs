namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum CreditRatingReasonEnum
    {
        bankruptcy = 1,
        [Display(Name="Late Payment")]
        LatePayment = 3,
        [Display(Name="Low Score")]
        LowScore = 2
    }
}

