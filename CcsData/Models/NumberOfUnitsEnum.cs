namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum NumberOfUnitsEnum
    {
        [Display(Name="Five +")]
        Five_Plus = 5,
        Four = 4,
        One = 1,
        three = 3,
        Tow = 2
    }
}

