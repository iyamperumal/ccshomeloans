namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum DaysLateEnum
    {
        [Display(Name="0 x 30 Days late")]
        L0x30 = 1,
        [Display(Name="1 x 30 Days late")]
        L1x30 = 2,
        [Display(Name="1 x 60 Days late")]
        L1x60 = 5,
        [Display(Name="1 x 90 Days late")]
        L1x90 = 8,
        [Display(Name="2 x 30 Days late")]
        L2x30 = 3,
        [Display(Name="2 x 60 Days late")]
        L2x60 = 6,
        [Display(Name="2 x 90 Days late")]
        L2x90 = 9,
        [Display(Name="3 x 30 Days late")]
        L3x30 = 4,
        [Display(Name="3 x 60 Days late")]
        L3x60 = 7,
        [Display(Name="3 x 90 Days late")]
        L3x90 = 10
    }
}

