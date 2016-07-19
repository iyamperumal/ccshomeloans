namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum PropertyTypeEnum
    {
        Commercial = 0x63,
        Condo = 2,
        [Display(Name="Mobile Home W/Land")]
        MobileHomeWithLand = 4,
        [Display(Name="Modular/Manufactured")]
        Modular_Manufactured = 3,
        [Display(Name="Multi Familly Residence 2 units")]
        Multi_Familly_Residence_2_units = 5,
        [Display(Name="Multi Familly Residence 3 units")]
        Multi_Familly_Residence_3_units = 6,
        [Display(Name="Multi Familly Residence 4 units")]
        Multi_Familly_Residence_4_units = 7,
        [Display(Name="Multi Familly Residence 5+")]
        Multi_Familly_Residence_5_Plus = 8,
        [Display(Name="Single Familly Residence")]
        Single_Familly_Residence_1_unit = 1,
        Townhome = 9
    }
}

