namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum DispositionEnum
    {
        Called = 3,
        [Display(Name="Client Refused")]
        ClientRefused = 4,
        [Display(Name="Hit Site")]
        HitSite = 4,
        [Display(Name="Lender Refused")]
        LenderRefused = 4,
        [Display(Name="Mailer Sent")]
        MailerSent = 2,
        [Display(Name="No Contact")]
        NoContact = 1
    }
}

