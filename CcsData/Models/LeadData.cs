namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class LeadData
    {
        [Display(Name="Column Mapping"), MaxLength(250)]
        public virtual string ColumnMapping { get; set; }

        public virtual string Criteria { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public virtual DateTime? DateMailed { get; set; }

        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true), DataType(DataType.Date)]
        public virtual DateTime? DatePuchased { get; set; }

        [Display(Name="File Name"), MaxLength(250)]
        public virtual string FileName { get; set; }

        [MaxLength(50), Display(Name="Lead Company Name")]
        public virtual string LeadCompanyName { get; set; }

        [Key]
        public virtual int LeadData_Id { get; set; }

        public virtual decimal? Price { get; set; }

        public virtual int Quantity { get; set; }

        public virtual int Responce { get; set; }

        [Display(Name="Responce Rate")]
        public virtual float ResponceRate { get; set; }

        [Display(Name="Table Mapping"), MaxLength(250)]
        public virtual string TableMapping { get; set; }

        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true), DataType(DataType.Date)]
        public virtual DateTime? UploadDate { get; set; }
    }
}

