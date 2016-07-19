namespace CcsData.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class CountyLoanLimitVA
    {
        [MaxLength(50)]
        public string County { get; set; }

        [Key]
        public virtual int CountyLoanLimitVA_Id { get; set; }

        public int Fips { get; set; }

        [DefaultValue(0)]
        public decimal LoanLimit { get; set; }

        [MaxLength(50)]
        public string State { get; set; }
    }
}

