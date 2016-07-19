namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum CreditMortgageTypeEnum
    {
        [Display(Name="For Sale")]
        ForSale = 3,
        [Display(Name="Primary Residence")]
        Primary = 4,
        Rental = 1,
        [Display(Name="second home")]
        SecondHome = 2
    }
}

