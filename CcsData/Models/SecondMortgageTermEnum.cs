namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum SecondMortgageTermEnum
    {
        [Display(Name="15 years")]
        fifteen = 15,
        [Display(Name="5 years")]
        five = 5,
        [Display(Name="Home Equity Line of credit")]
        HomeEquityLineOfcredit = 30,
        [Display(Name="10 years")]
        ten = 10,
        [Display(Name="20 years")]
        twenty = 20
    }
}

