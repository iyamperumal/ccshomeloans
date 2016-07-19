namespace CcsData.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class CountyLoanLimit
    {
        public string County { get; set; }

        [Key]
        public virtual int CountyLoanLimit_Id { get; set; }

        [DefaultValue(0)]
        public decimal LoanLimit1Unit { get; set; }

        [DefaultValue(0)]
        public decimal LoanLimit2Unit { get; set; }

        [DefaultValue(0)]
        public decimal LoanLimit3Unit { get; set; }

        [DefaultValue(0)]
        public decimal LoanLimit4Unit { get; set; }

        public LoanTypeEnum Loantype { get; set; }

        [Display(Name="Propety Type")]
        public PropertyTypeEnum propType { get; set; }

        public string State { get; set; }

        public int Zipcode { get; set; }
    }
}

