namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class Foreclosure
    {
        public virtual int Applicant_ID { get; set; }

        [Display(Name="Attorney Name"), MaxLength(50)]
        public virtual string AttorneyName { get; set; }

        [Display(Name="Attorney Phone"), MaxLength(20)]
        public virtual string AttorneyPhone { get; set; }

        [Display(Name="Case Number"), MaxLength(100)]
        public virtual string CaseNumber { get; set; }

        [Display(Name="Default Amount? ")]
        public virtual decimal? DefaultAmount { get; set; }

        [Display(Name="Document Number"), MaxLength(100)]
        public virtual string DocumentNumber { get; set; }

        [MaxLength(50), Display(Name="Document Type")]
        public virtual string DocumentType { get; set; }

        [DataType(DataType.Date)]
        public virtual DateTime? EffectiveDate { get; set; }

        [Key]
        public virtual int Forclosure_Id { get; set; }

        public virtual Address LenderAddress { get; set; }

        [Display(Name="Lender Name:"), MaxLength(50)]
        public virtual string LenderName { get; set; }

        [MaxLength(20), Display(Name="Lender Phone")]
        public virtual string LenderPhone { get; set; }

        [Display(Name="Lien Position"), MaxLength(50)]
        public virtual string LienPosition { get; set; }

        [ForeignKey("Applicant_ID")]
        public virtual Applicant MortgageApplicant { get; set; }

        [MaxLength(50), Display(Name="Original Document Number")]
        public virtual string OriginalDocumentNumber { get; set; }

        [MaxLength(50)]
        public virtual string OriginalLender { get; set; }

        [Display(Name="Original Mortgage Amount? ")]
        public virtual decimal? OriginalMortgageAmount { get; set; }

        [Display(Name="Original Recording Date"), DataType(DataType.Date)]
        public virtual DateTime? OriginalRecordingDate { get; set; }

        [MaxLength(50)]
        public virtual string Plaintiff { get; set; }

        [DataType(DataType.Date)]
        public virtual DateTime? RecentAddedDate { get; set; }

        [DataType(DataType.Date), Display(Name="Recording date")]
        public virtual DateTime? RecordingDate { get; set; }

        public virtual Address TrusteeAddress { get; set; }

        [Display(Name="Trustee Name"), MaxLength(100)]
        public virtual string TrusteeName { get; set; }

        [MaxLength(20)]
        public virtual string TrusteePhone { get; set; }

        [Display(Name="Trustee Sale Number"), MaxLength(100)]
        public virtual string TrusteeSaleNumber { get; set; }

        [Display(Name="Unpaid Balance ")]
        public virtual decimal? UnpaidBalance { get; set; }
    }
}

