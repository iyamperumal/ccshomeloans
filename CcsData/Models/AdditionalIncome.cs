namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class AdditionalIncome
    {
        [Key]
        public int AdditionalIncome_Id { get; set; }

        [DataType(DataType.Currency)]
        public virtual decimal? Amount { get; set; }

        [Display(Name="Type of Income")]
        public virtual IncomeTypeEnum IncomeType { get; set; }

        [Display(Name="Future Years to Recieve")]
        public virtual int NumberFutureYearsToReceive { get; set; }

        [Display(Name="Years Recieved")]
        public virtual int PreviousYearsReceived { get; set; }

        [StringLength(250)]
        public string Remarks { get; set; }

        [MaxLength(250)]
        public virtual string Source { get; set; }
    }
}

