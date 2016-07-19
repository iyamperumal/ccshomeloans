namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum LoanTypeEnum
    {
        [Display(Name="Conventonal Conforming")]
        ConventonalConforming = 4,
        FHA = 1,
        [Display(Name="FHA Streamline")]
        FHA_Streamline = 8,
        [Display(Name="HARP")]
        HARP = 10,
        Jumbo = 5,
        [Display(Name="SubprimeNon/Qualified")]
        SubprimeNonQualified = 6,
        [Display(Name="USRDA (Rural)")]
        USRDA = 3,
        VA = 2,
        [Display(Name="VA IRRL")]
        VA_IRRL = 9
    }
}

