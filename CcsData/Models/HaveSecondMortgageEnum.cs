namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum HaveSecondMortgageEnum
    {
        No = 1,
        [Display(Name="Yes: Aquired After Purchase")]
        YesForCash = 3,
        [Display(Name="Yes: Used to Purchase Home")]
        YesForPurchase = 2
    }
}

