namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum GarageSizeEnum
    {
        None = 0,
        [Display(Name="One Car")]
        One_Car = 1,
        [Display(Name="3 Cars")]
        three_Car = 3,
        [Display(Name="4 Cars +")]
        Three_Car_Plus = 4,
        [Display(Name="2 Cars")]
        Two_Car = 2
    }
}

