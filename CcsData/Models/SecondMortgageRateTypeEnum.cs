namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum SecondMortgageRateTypeEnum
    {
        Adjustable = 2,
        [Display(Name="Adjustable Interest Only")]
        AdjustableInterestOnly = 10,
        Fixed = 1,
        [Display(Name="Fixed Interest Only")]
        FixedInterestOnly = 3
    }
}

