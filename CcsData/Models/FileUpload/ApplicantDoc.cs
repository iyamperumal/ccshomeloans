namespace CcsData.Models.FileUpload
{
    using CcsData.Models;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class ApplicantDoc
    {
        public int Applicant_ID { get; set; }

        [Key]
        public int DocFile_Id { get; set; }

        [Display(Name="File Type"), DefaultValue(14)]
        public DocFileTypeEnum DocFileType { get; set; }

        [StringLength(0xff), Display(Name="File Name")]
        public string FileName { get; set; }

        [Display(Name="File Name"), StringLength(0x200)]
        public string FilePath { get; set; }

        [Display(Name="Self Employed 1099's")]
        public bool HasEmployment1099 { get; set; }

        [Display(Name="Employed W2")]
        public bool HasEmploymentW2 { get; set; }

        [Display(Name="Investment Income")]
        public bool HasInvestmentIncome { get; set; }

        [Display(Name="Pensions/401k")]
        public bool HasPensions401k { get; set; }

        [Display(Name="Rental Income (signed leases)")]
        public bool HasRentalIncome { get; set; }

        [Display(Name="Social Security")]
        public bool HasSocialSecurity { get; set; }

        [ForeignKey("Applicant_ID")]
        public virtual Applicant MortgageApplicant { get; set; }

        [Display(Name="Add Note")]
        public string Note { get; set; }
    }
}

