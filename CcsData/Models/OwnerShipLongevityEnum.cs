namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum OwnerShipLongevityEnum
    {
        [Display(Name="8-10 years")]
        EightToTen = 6,
        [Display(Name="4-5 years")]
        FourToFive = 4,
        [Display(Name="6-7 years")]
        SixToSeven = 5,
        [Display(Name="10 + years")]
        TenPlusYears = 7,
        [Display(Name="3 years")]
        threeYears = 3,
        [Display(Name="2 years")]
        TwoYears = 2
    }
}

