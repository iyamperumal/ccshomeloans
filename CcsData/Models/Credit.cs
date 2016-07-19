namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class Credit
    {
        [Display(Name="Any foreclosures?")]
        public virtual YesNoAns? AnyForclosure { get; set; }

        [Display(Name="Any Non Medical judgments or collections")]
        public virtual YesNoAns? AnyJudgments { get; set; }

        public virtual BankruptcyChapterEnum? BankruptcyType { get; set; }

        [Display(Name="Date discharged"), DataType(DataType.Date), DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public virtual DateTime? BankruptyDischargeDate { get; set; }

        [Display(Name="120 days late")]
        public virtual int? C120DaysLate { get; set; }

        [Display(Name="30 days late")]
        public virtual int? C30DaysLate { get; set; }

        [Display(Name="60 days late")]
        public virtual int? C60DaysLate { get; set; }

        [Display(Name="90 days late")]
        public virtual int? C90DaysLate { get; set; }

        [Key]
        public virtual int Credit_Id { get; set; }

        [Display(Name="CurrentStanding:"), MaxLength(50)]
        public virtual string CurrentStanding { get; set; }

        [Display(Name="Have you ever filed bankruptcy? "), EnumDataType(typeof(YesNoAns))]
        public virtual YesNoAns? FiledBankruptcy { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true), Display(Name="If so, What is the forclusure date?")]
        public virtual DateTime? ForclosureDate { get; set; }

        [Display(Name="How Old Are Judment And Collections?")]
        public string HowOldAreJudmentAndCollections { get; set; }

        [DataType(DataType.Currency), Display(Name="If so, What is the total balance? ")]
        public virtual decimal? JudgmentsBalance { get; set; }

        public virtual int? LastKnownCreditScore { get; set; }

        public virtual DateTime? LastScoreDate { get; set; }

        [Display(Name="120 days late")]
        public virtual int? M120DaysLate { get; set; }

        [Display(Name="30 days late")]
        public virtual int? M30DaysLate { get; set; }

        [Display(Name="60 days late")]
        public virtual int? M60DaysLate { get; set; }

        [Display(Name="90 days late")]
        public virtual int? M90DaysLate { get; set; }

        [Display(Name="PreApproved Payment:")]
        public virtual decimal? PreApprovedPayment { get; set; }

        [Display(Name="PreApproved Rate:")]
        public virtual decimal? PreApprovedRate { get; set; }

        [Display(Name="How would you rate your credit")]
        public virtual CreditRatingEnum? Rating { get; set; }

        public virtual int? Score { get; set; }

        public virtual DateTime? ScoreDate { get; set; }
    }
}

