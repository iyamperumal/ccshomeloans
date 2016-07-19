namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum ProspectEnum
    {
        ApplicationStarted = 0,
        [Display(Name="Applied Online")]
        Appliedonline = 1,
        [Display(Name="Client Pitched")]
        ClientPitched = 5,
        [Display(Name="ClientSold")]
        ClientSold = 6,
        [Display(Name="Inbound Call")]
        InboundCall = 3,
        [Display(Name="outbound Call")]
        OutboundCall = 2,
        [Display(Name="Workup Completed")]
        WorkupCompleted = 4
    }
}

