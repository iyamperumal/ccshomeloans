namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum DocFileTypeEnum
    {
        [Display(Name="ALL")]
        ALL = 13,
        [Display(Name="Current YTD Profit & Loss")]
        CurrentYTDProfitLoss = 10,
        [Display(Name="Divorce Decree ")]
        DivorceDecree = 12,
        [Display(Name="Homeowners Insurance Policy")]
        HomeownersInsurancePolicy = 5,
        [Display(Name="Inverstment Income Document")]
        InverstmentIncomeDocument = 11,
        [Display(Name="Last 2 Months Bank Statements")]
        Last2MonthsBankStatements = 4,
        [Display(Name="Last  2 Years Business Tax Return")]
        Last2YeaBusTaxReturn = 9,
        [Display(Name="Last Month Pay Stubs")]
        LastMonthPayStubs = 3,
        [Display(Name="Other")]
        Other = 15,
        [Display(Name="Partial")]
        Partial = 14,
        [Display(Name="Pension Award Letter")]
        PensionAwardLetter = 7,
        [Display(Name="Rental Income Signed Leases")]
        RentalIncomeSignedLeases = 8,
        [Display(Name="Social Security Award Letter")]
        SocialSecurityAwardLetter = 6,
        [Display(Name="Last  2 Years Personel Tax Return")]
        Taxreturnlast2Personel = 2,
        [Display(Name="W2 Last 2 Years")]
        W2Last2Years = 1
    }
}

