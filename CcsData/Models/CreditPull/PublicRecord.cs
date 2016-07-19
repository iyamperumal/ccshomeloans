namespace CcsData.Models.CreditPull
{
    using CcsData.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class PublicRecord
    {
        public bool IsQualifiedChapter13(int months)
        {
            bool flag = true;
            if ((this.ActionType == null) || (this.ActionType.ToLower().Trim() != "bankruptcychapter13"))
            {
                return flag;
            }
            if ((this.Status.ToLower().Trim() != "filed") && this.StatusDate.HasValue)
            {
                DateTime time = this.StatusDate.Value;
                return this.qualify(time, months);
            }
            return false;
        }

        public bool IsQualifiedChapter7(int months)
        {
            bool flag = true;
            if ((this.ActionType == null) || (this.ActionType.ToLower().Trim() != "bankruptcychapter7"))
            {
                return flag;
            }
            if ((this.Status.ToLower().Trim() != "filed") && this.StatusDate.HasValue)
            {
                DateTime time = this.StatusDate.Value;
                return this.qualify(time, months);
            }
            return false;
        }

        public bool IsQualifiedforeclosure(int months)
        {
            bool flag = true;
            if ((this.ActionType == null) || (this.ActionType.ToLower().Trim() != "foreclosure"))
            {
                return flag;
            }
            if (this.StatusDate.HasValue)
            {
                DateTime time = this.StatusDate.Value;
                return this.qualify(time, months);
            }
            return false;
        }

        public bool IsQualifiedJudgment(double amount)
        {
            bool flag = true;
            if ((((this.ActionType != null) && (this.ActionType.ToLower().Trim() == "judgment")) && ((this.Status != null) && (this.Status.ToLower().Trim() == "unsatisfied"))) && (this.Amount > amount))
            {
                flag = false;
            }
            return flag;
        }

        private bool qualify(DateTime dt1, int months)
        {
            bool flag = false;
            DateTime time = dt1;
            DateTime now = DateTime.Now;
            int year = months / 12;
            int month = months - (year * 12);
            if (now.Month <= month)
            {
                year++;
                month = (now.Month + 12) - month;
            }
            else
            {
                month = now.Month - month;
            }
            year = now.Year - year;
            DateTime time3 = new DateTime(year, month, now.Day);
            if (DateTime.Compare(time3, time) >= 0)
            {
                flag = true;
            }
            return flag;
        }

        public virtual string ActionType { get; set; }

        public virtual double Amount { get; set; }

        [ForeignKey("ApplicantID")]
        public virtual CcsData.Models.Applicant Applicant { get; set; }

        public virtual int? ApplicantID { get; set; }

        public virtual DateTime? FileDate { get; set; }

        public virtual string Plaintiff { get; set; }

        [Key]
        public int PublicRecord_Id { get; set; }

        [ForeignKey("ResponseCredID")]
        public virtual CcsData.Models.CreditPull.ResponseCred ResponseCred { get; set; }

        public virtual int? ResponseCredID { get; set; }

        public virtual string Sourse { get; set; }

        public virtual string Status { get; set; }

        public virtual DateTime? StatusDate { get; set; }

        public virtual string Whose { get; set; }
    }
}

