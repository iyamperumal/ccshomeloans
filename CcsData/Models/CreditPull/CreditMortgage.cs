namespace CcsData.Models.CreditPull
{
    using CcsData.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class CreditMortgage
    {
        public string AccountIdentifier { get; set; }

        [DefaultValue("MTG")]
        public virtual string AccType { get; set; }

        [ForeignKey("ApplicantID")]
        public virtual CcsData.Models.Applicant Applicant { get; set; }

        public virtual int? ApplicantID { get; set; }

        public virtual double Balance { get; set; }

        [Key]
        public int CreditMortgage_Id { get; set; }

        public virtual DateTime? DateOpened { get; set; }

        public virtual double HiCredit { get; set; }

        [DefaultValue(false)]
        public bool IsDuplicate { get; set; }

        public DateTime? LastActivityDate { get; set; }

        [DefaultValue(0)]
        public virtual int Late30 { get; set; }

        public virtual string Late30Dates { get; set; }

        [DefaultValue(0)]
        public virtual int Late60 { get; set; }

        public virtual string Late60Dates { get; set; }

        [DefaultValue(0)]
        public virtual int Late90 { get; set; }

        public virtual string Late90Dates { get; set; }

        public virtual List<CcsData.Models.CreditPull.Lates> Lates { get; set; }

        public virtual string Lender { get; set; }

        public virtual double MonthlyPayment { get; set; }

        public virtual int MonthsRemaining { get; set; }

        [UIHint("EnumCheck")]
        public CreditMortgageTypeEnum PropertyType { get; set; }

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

