namespace CcsData.Models.CreditPull
{
    using CcsData.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class CreditLiability
    {
        public string AccountIdentifier { get; set; }

        public virtual string AccType { get; set; }

        [ForeignKey("ApplicantID")]
        public virtual CcsData.Models.Applicant Applicant { get; set; }

        public virtual int? ApplicantID { get; set; }

        public virtual double Balance { get; set; }

        public virtual string creditor { get; set; }

        public virtual DateTime? DateOpened { get; set; }

        [DefaultValue(false)]
        public bool EditedByApplicant { get; set; }

        public virtual double HiCredit { get; set; }

        [DefaultValue(false)]
        public bool IsDuplicate { get; set; }

        public DateTime? LastActivityDate { get; set; }

        public virtual List<CcsData.Models.CreditPull.Lates> Lates { get; set; }

        [Key]
        public int Liability_Id { get; set; }

        public virtual double MonthlyPayment { get; set; }

        public virtual int MonthsRemaining { get; set; }

        [ForeignKey("ResponseCredID")]
        public virtual CcsData.Models.CreditPull.ResponseCred ResponseCred { get; set; }

        public virtual int? ResponseCredID { get; set; }

        public string Sourse { get; set; }

        public virtual int Term { get; set; }

        [DefaultValue(false)]
        public bool ToBePaidOff { get; set; }

        public string Whose { get; set; }
    }
}

