namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum FiledBankruptcyTypeEnum
    {
        [Display(Name="Chapter 13 Discharged")]
        Chapter13Discharged = 5,
        [Display(Name="Chapter 13 Repayment Still Open")]
        Chapter13RepaymentStillOpen = 4,
        [Display(Name="Chapter 7 Discharged")]
        Chapter7Discharged = 2,
        No = 1
    }
}

