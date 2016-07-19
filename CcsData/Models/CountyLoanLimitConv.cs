namespace CcsData.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class CountyLoanLimitConv
    {
        [MaxLength(50)]
        public string County { get; set; }

        [Key]
        public virtual int CountyLoanLimitConv_Id { get; set; }

        public int Fips { get; set; }

        [DefaultValue(0)]
        public decimal LoanLimit1Unit { get; set; }

        [DefaultValue(0)]
        public decimal LoanLimit2Unit { get; set; }

        [DefaultValue(0)]
        public decimal LoanLimit3Unit { get; set; }

        [DefaultValue(0)]
        public decimal LoanLimit4Unit { get; set; }

        [MaxLength(50)]
        public string State { get; set; }
    }
}

