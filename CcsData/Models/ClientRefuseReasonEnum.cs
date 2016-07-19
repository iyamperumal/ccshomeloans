namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum ClientRefuseReasonEnum
    {
        Fees = 3,
        [Display(Name="Interest Rate")]
        InterestRate = 1,
        [Display(Name="Other Lender")]
        OtherLender = 2,
        [Display(Name="Upset With CCS")]
        UpsetWithCCS = 4
    }
}

