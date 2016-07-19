namespace CcsData.Models.CreditPull
{
    using CcsData.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ResponseCred
    {
        public int GetCreditScore()
        {
            int num = 0;
            int result = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            if (this.CreditScoreEF != null)
            {
                this.CreditScoreEF = this.CreditScoreEF.Trim();
                int.TryParse(this.CreditScoreEF, out result);
            }
            if (this.CreditScoreTU != null)
            {
                this.CreditScoreTU = this.CreditScoreTU.Trim();
                int.TryParse(this.CreditScoreTU, out num4);
            }
            if (this.CreditScoreEP != null)
            {
                this.CreditScoreEP = this.CreditScoreEP.Trim();
                int.TryParse(this.CreditScoreEP, out num3);
            }
            int[] source = new int[] { result, num3, num4 };
            num5 = source.Min();
            num6 = source.Max();
            for (int i = 0; i < source.Length; i++)
            {
                if (((source[i] != 0) && (source[i] > num5)) && (source[i] < num6))
                {
                    num = source[i];
                }
            }
            if (num == 0)
            {
                num = num6;
            }
            return num;
        }

        [ForeignKey("ApplicantID")]
        public virtual CcsData.Models.Applicant Applicant { get; set; }

        public virtual int? ApplicantID { get; set; }

        public virtual List<CreditMortgage> CreditMortgages { get; set; }

        public virtual string CreditScoreEF { get; set; }

        public virtual string CreditScoreEP { get; set; }

        public virtual string CreditScoreTU { get; set; }

        public virtual string FileNumber { get; set; }

        public string FullName { get; set; }

        public virtual string HTMLfile { get; set; }

        public virtual List<Inquiry> Inquiries { get; set; }

        public virtual List<Lates> lates { get; set; }

        public virtual List<CreditLiability> Liabilities { get; set; }

        public virtual List<PublicRecord> PublicRecords { get; set; }

        public virtual string REPOSITORIES { get; set; }

        [Key]
        public int ResponseCred_Id { get; set; }

        public virtual CcsData.Models.CreditPull.ResponseData ResponseData { get; set; }

        public virtual int TotalAccount { get; set; }

        public virtual double TotalBalance { get; set; }

        public virtual double TotalHiCredit { get; set; }

        public virtual double TotalPassdue { get; set; }

        public virtual double TotalPayments { get; set; }

        public virtual double TotalSecureDebt { get; set; }

        public virtual double TotalUnSecureDebt { get; set; }
    }
}

