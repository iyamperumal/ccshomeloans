namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum ScheduleTypeEnum
    {
        [Display(Name="Full Time")]
        FullTime = 1,
        [Display(Name="Part Time")]
        PartTime = 2
    }
}

