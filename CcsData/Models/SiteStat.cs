namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class SiteStat
    {
        [Display(Name="Avg Saving/mo")]
        public double AvgSavingsPerMnt { get; set; }

        [Display(Name="Avg Saving/year")]
        public double AvgSavingsPerYear { get; set; }

        [Key]
        public int SietStat_Id { get; set; }

        public int TotalFinancedMembers { get; set; }

        [Display(Name="Members")]
        public int TotalMembers { get; set; }

        public int TotalSignInMembers { get; set; }
    }
}

