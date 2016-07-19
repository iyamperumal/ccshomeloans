namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum InterestRateTypeEnum
    {
        [Display(Name="Adjustable 10yr Fixed 1 yr ARM")]
        Adjustable_10_1 = 5,
        [Display(Name="Adjustable 3yr Fixed 1 yr ARM")]
        Adjustable_3_1 = 2,
        [Display(Name="Adjustable 5yr Fixed 1 yr ARM")]
        Adjustable_5_1 = 3,
        [Display(Name="Adjustable 7yr Fixed 1 yr ARM")]
        Adjustable_7_1 = 4,
        Balloon = 8,
        Fixed = 1,
        [Display(Name="Interest Only Adjustable")]
        InterstOnlyAdjustable = 6,
        [Display(Name="Interest Only Fixed")]
        InterstOnlyFixed = 7
    }
}

