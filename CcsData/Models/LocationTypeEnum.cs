namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum LocationTypeEnum
    {
        City = 1,
        [Display(Name="Country Rural")]
        Country_Rural = 2
    }
}

