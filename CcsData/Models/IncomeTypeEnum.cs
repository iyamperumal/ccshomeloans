namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum IncomeTypeEnum : byte
    {
        [Display(Name="Child Support")]
        Child_Support = 2,
        [Display(Name="IRA- 401K")]
        IRA_401K = 5,
        Other = 6,
        Pension = 4,
        Rental = 1,
        [Display(Name="Social Security")]
        Social_Security = 3
    }
}

