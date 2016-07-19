namespace CcsData.ViewModels
{
    using CcsData.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class EmploymentVM
    {
        [Display(Name="Can You Provide Bank Statements? ")]
        public virtual YesNoAns? CanProvideBankStatements { get; set; }

        [ForeignKey("Employer_ID")]
        public virtual Employer ClientEmployer { get; set; }

        [Display(Name="Earning per pay period")]
        public virtual decimal? EarningsBeforeTax { get; set; }

        public virtual int Employer_ID { get; set; }

        [MaxLength(50)]
        public virtual string EmployerName { get; set; }

        public virtual string EmploymentType { get; set; }

        [DataType(DataType.Date)]
        public virtual DateTime? EndDate { get; set; }

        [DataType(DataType.Date)]
        public virtual DateTime? HireDate { get; set; }

        public virtual int Id { get; set; }

        [Display(Name="Last Year's Adusted Gross Income Declared On Tax Returns? ")]
        public virtual decimal? LastYear_SE_EarningsReported_IRS { get; set; }

        [Display(Name="Last Year Depreciation As Reported on Tax Returns")]
        public virtual decimal? LastYearDepreciationAsReported { get; set; }

        [MaxLength(50)]
        public virtual string LengthOfEmployment { get; set; }

        public virtual decimal? MonthlyGrossIncome { get; set; }

        [Display(Name="How Offten do you get Payed")]
        public virtual PayScheduleEnum? PayPeriod { get; set; }

        [MaxLength(50)]
        public virtual string Position { get; set; }

        [MaxLength(50)]
        public virtual string ScheduleType { get; set; }

        [Display(Name="Year before Last: Adusted Gross Income Declared On Tax Returns? ")]
        public virtual decimal? Year2_SE_EarningsReported_IRS { get; set; }

        [Display(Name="Year Before Last: Depreciation As Reported on Tax Returns")]
        public virtual decimal? Year2DepreciationAsReported { get; set; }
    }
}

