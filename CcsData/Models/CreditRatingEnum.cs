namespace CcsData.Models
{
    using System;
    using System.ComponentModel;

    public enum CreditRatingEnum
    {
        [Description("721 +")]
        Excellent = 3,
        [Description("639 to 680")]
        Fair = 1,
        [Description("681 to 720")]
        Good = 2,
        [Description("under 639")]
        Poor = 0
    }
}

