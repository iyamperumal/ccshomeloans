namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum EmploymentTypeEnum
    {
        [Display(Name="Form 1099")]
        F_1099 = 2,
        [Display(Name="Form W2")]
        F_W2 = 1
    }
}

