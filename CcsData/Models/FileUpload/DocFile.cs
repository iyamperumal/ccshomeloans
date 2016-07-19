namespace CcsData.Models.FileUpload
{
    using CcsData.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class DocFile
    {
        public int Applicant_ID { get; set; }

        [Key]
        public int DocFile_Id { get; set; }

        [UIHint("EnumCheck"), Display(Name="File Type")]
        public DocFileTypeEnum DocFileType { get; set; }

        [Display(Name="File Name"), StringLength(0xff)]
        public string FileName { get; set; }

        [Display(Name="File Name"), StringLength(0x200)]
        public string FilePath { get; set; }

        [ForeignKey("Applicant_ID")]
        public virtual Applicant MortgageApplicant { get; set; }

        [Display(Name="Add Note")]
        public string Note { get; set; }
    }
}

