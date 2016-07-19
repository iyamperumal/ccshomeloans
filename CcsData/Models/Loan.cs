namespace CcsData.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class Loan
    {
        [Display(Name="Account Number"), StringLength(100)]
        public string AccountNumber { get; set; }

        public virtual int Applicant_ID { get; set; }

        public decimal? Balance { get; set; }

        [Display(Name="Date Of Ballance"), DataType(DataType.Date)]
        public DateTime? DateOfBalance { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOpened { get; set; }

        [Display(Name="Interest Rate")]
        public float InterestRate { get; set; }

        [DefaultValue(false)]
        public bool Joint { get; set; }

        [StringLength(50)]
        public string Lender { get; set; }

        [Key]
        public int Loan_Id { get; set; }

        [ForeignKey("Applicant_ID")]
        public Applicant MortgageApplicant { get; set; }

        public decimal? OriginalLoanAmount { get; set; }

        [StringLength(50)]
        public string Term { get; set; }

        [Display(Name="Total Monthly Payment")]
        public decimal? TotalMonthlyPayment { get; set; }

        [StringLength(50)]
        public string Type { get; set; }
    }
}

