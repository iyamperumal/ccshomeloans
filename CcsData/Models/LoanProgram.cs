namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class LoanProgram
    {
        [Key]
        public virtual int Address_Id { get; set; }

        [Display(Name="Loan Name")]
        public string LoanName { get; set; }

        [Display(Name="Program Name")]
        public string ProgramName { get; set; }

        public double Rate { get; set; }

        public double Rate15 { get; set; }

        public double Rate30 { get; set; }

        public double Rate45 { get; set; }

        public double Rate60 { get; set; }
    }
}

