namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum MortgageTermEnum
    {
        [Display(Name="15 years")]
        fifteen = 15,
        [Display(Name="5 years")]
        five = 5,
        [Display(Name="10 years")]
        ten = 10,
        [Display(Name="30 years")]
        thirty = 30,
        [Display(Name="20 years")]
        twenty = 20,
        [Display(Name="25 years")]
        twentyFive = 0x19
    }
}

