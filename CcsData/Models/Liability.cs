namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class Liability
    {
        [MaxLength(50)]
        public virtual string AccountType { get; set; }

        public virtual decimal? Balance { get; set; }

        [MaxLength(50)]
        public virtual string Creditor { get; set; }

        [Key]
        public virtual int Liability_Id { get; set; }

        public Loan LoanInfo { get; set; }

        [Display(Name="Monthly Payment")]
        public virtual decimal? MonthlyPmt { get; set; }

        [Display(Name="To Payed Off")]
        public virtual bool? ToBePaidOff { get; set; }
    }
}

