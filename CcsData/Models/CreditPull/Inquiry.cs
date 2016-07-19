namespace CcsData.Models.CreditPull
{
    using CcsData.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class Inquiry
    {
        [ForeignKey("ApplicantID")]
        public virtual CcsData.Models.Applicant Applicant { get; set; }

        public virtual int? ApplicantID { get; set; }

        public virtual string Bureau { get; set; }

        public string BusninessType { get; set; }

        public virtual string Company { get; set; }

        [Key]
        public int Inquiry_Id { get; set; }

        public virtual DateTime? InquiryDate { get; set; }

        [ForeignKey("ResponseCredID")]
        public virtual CcsData.Models.CreditPull.ResponseCred ResponseCred { get; set; }

        public virtual int? ResponseCredID { get; set; }

        public virtual string Whose { get; set; }
    }
}

