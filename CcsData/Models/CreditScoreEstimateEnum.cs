namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum CreditScoreEstimateEnum
    {
        [Display(Name="500-539")]
        S_500_539 = 0x21b,
        [Display(Name="540-579")]
        S_540_579 = 0x243,
        [Display(Name="580-599")]
        S_580_599 = 0x257,
        [Display(Name="600-619")]
        S_600_619 = 0x26b,
        [Display(Name="620-639")]
        S_620_639 = 0x27f,
        [Display(Name="640-659")]
        S_640_659 = 0x293,
        [Display(Name="660-679")]
        S_660_679 = 0x2a7,
        [Display(Name="680-699")]
        S_680_699 = 0x2bb,
        [Display(Name="700-719")]
        S_700_719 = 0x2cf,
        [Display(Name="720-739")]
        S_720_739 = 0x2e3,
        [Display(Name="740-779")]
        S_740_779 = 0x30b,
        [Display(Name="780 +")]
        S_780 = 780
    }
}

