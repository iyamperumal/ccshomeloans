namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum MortgageProgramOptionsEnum
    {
        Conventional = 5,
        FHA = 2,
        [Display(Name="FHA Steamline After 2009")]
        FHA_SteamlineAfter2009 = 4,
        [Display(Name="FHA Steamline Before 2009")]
        FHA_SteamlineBefore2009 = 3,
        [Display(Name="HARP Fannie Before 2009")]
        HARP_FannieBefore2009 = 9,
        [Display(Name="HARP Fredie Before 2009")]
        HARP_FredieBefore2009 = 10,
        HELOC = 11,
        Jumbo = 7,
        Other = 12,
        Subprime = 8,
        USRDA = 6,
        VA = 0,
        VA_IRRL = 1
    }
}

