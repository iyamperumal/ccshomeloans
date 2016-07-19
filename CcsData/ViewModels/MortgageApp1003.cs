namespace CcsData.ViewModels
{
    using Models;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    internal class MortgageApp1003
    {
        [Display(Name="Case Number"), DefaultValue((double) 0.0)]
        public virtual string CaseNumber { get; set; }

        public int ID { get; set; }

        [Display(Name="Mortgage Applied For"), DefaultValue((double) 0.0)]
        public virtual MortgageProgramOptionsEnum? MortageType { get; set; }
    }
}

