namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum ClientDenialReasonEnum
    {
        [Display(Name="Lower Closing Cost")]
        LowerClosingCost = 1,
        [Display(Name="Lower Rate")]
        LowerRate = 0
    }
}

