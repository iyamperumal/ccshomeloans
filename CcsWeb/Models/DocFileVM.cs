namespace CcsWeb.Models
{
    using CcsData.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;
    using System.Web;

    public class DocFileVM
    {
        public HttpPostedFileBase BaseFile { get; set; }

        [Key]
        public int DocFile_Id { get; set; }

        [Display(Name="File Type"), UIHint("EnumCheck")]
        public DocFileTypeEnum DocFileType { get; set; }

        [StringLength(0xff), Display(Name="File Name")]
        public string FileName { get; set; }

        [StringLength(0x200), Display(Name="File Name")]
        public string FilePath { get; set; }

        [Display(Name="Add Note")]
        public string Note { get; set; }
    }
}

