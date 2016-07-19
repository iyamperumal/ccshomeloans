namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum ForeclosuresShortSaleDeedinLieuEnum
    {
        No = 1,
        [Display(Name="Yes Deed and Lieu")]
        YesDeedAndLieu = 4,
        [Display(Name="Yes Forclosure")]
        YesForclosure = 2,
        [Display(Name="Yes Short Sale")]
        YesShortSale = 3
    }
}

