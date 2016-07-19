namespace CcsData.Models.CreditPull
{
    using CcsData.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class Lates
    {
        public string AccountIdentifier { get; set; }

        public DateTime? AccountOpenedDate { get; set; }

        public virtual string AccountType { get; set; }

        [ForeignKey("ApplicantID")]
        public virtual CcsData.Models.Applicant Applicant { get; set; }

        public virtual int? ApplicantID { get; set; }

        public double HighCreditAmount { get; set; }

        public virtual DateTime? Late120 { get; set; }

        public virtual DateTime? Late30 { get; set; }

        public virtual DateTime? Late60 { get; set; }

        public virtual DateTime? Late90 { get; set; }

        [Key]
        public int Lates_Id { get; set; }

        public virtual string Lender { get; set; }

        public double MonthlyPaymentAmount { get; set; }

        [ForeignKey("ResponseCredID")]
        public virtual CcsData.Models.CreditPull.ResponseCred ResponseCred { get; set; }

        public virtual int? ResponseCredID { get; set; }
    }
}

