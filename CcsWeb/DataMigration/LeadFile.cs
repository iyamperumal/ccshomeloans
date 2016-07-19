namespace CcsWeb.DataMigration
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class LeadFile
    {
        [Display(Name="End Record")]
        public int EndRecord { get; set; }

        [Display(Name="Select File")]
        public string SelectedFile { get; set; }

        [Display(Name="Start Record")]
        public int StartRecord { get; set; }
    }
}

