namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum OwnershipTypeEnum
    {
        Investment = 3,
        [Display(Name="Primary Residence")]
        Primary_Residence = 1,
        [Display(Name="Second Home")]
        Second_Home = 2
    }
}

