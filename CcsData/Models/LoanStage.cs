namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class LoanStage
    {
        public YesNoAns? AppraiserOrdred { get; set; }

        public DateTime? AppraiserOrdredDate { get; set; }

        public YesNoAns? AppraiserReceived { get; set; }

        public DateTime? AppraiserReceivedDate { get; set; }

        public YesNoAns? ClearToClose { get; set; }

        public DateTime? ClearToCloseDate { get; set; }

        public ClientDenialReasonEnum? ClientDenialReason { get; set; }

        public DateTime? ClosingDate { get; set; }

        public YesNoAns? ClosingPackageReceived { get; set; }

        public DateTime? ClosingPackageReceivedDate { get; set; }

        public YesNoAns? ClosingScheduked { get; set; }

        public DateTime? ClosingSchedukedDate { get; set; }

        public YesNoAns? ConditionsRequested { get; set; }

        public DateTime? ConditionsRequestedDate { get; set; }

        public YesNoAns? ConditionsSubmitted { get; set; }

        public DateTime? ConditionsSubmittedDate { get; set; }

        public YesNoAns? DocumentColectionStarted { get; set; }

        public DateTime? DocumentColectionStarteDate { get; set; }

        public YesNoAns DocumentReceived { get; set; }

        public DateTime? DocumentReceivedDate { get; set; }

        public YesNoAns HomeOwnerInsOrdred { get; set; }

        public DateTime? HomeOwnerInsOrdredDate { get; set; }

        public YesNoAns HomeOwnerInsReceived { get; set; }

        public DateTime? HomeOwnerInsReceivedDate { get; set; }

        public LenderDenialReasonEnum? LenderDenialReason { get; set; }

        public LetterTypeEnum? LetterType { get; set; }

        public YesNoAns? LoanClosed { get; set; }

        public DateTime? LoanClosedDate { get; set; }

        public YesNoAns? LoanConditionsReceived { get; set; }

        public DateTime? LoanConditionsReceivedDate { get; set; }

        public YesNoAns? LoanDenied { get; set; }

        public DateTime? LoanDeniedDate { get; set; }

        public YesNoAns? LoanFunded { get; set; }

        public DateTime? LoanFundedDate { get; set; }

        [Key]
        public int LoanStage_Id { get; set; }

        public YesNoAns? LoanSubmitted { get; set; }

        public DateTime? LoanSubmittedDate { get; set; }

        public YesNoAns? Mailed { get; set; }

        public DateTime? MailedDate { get; set; }

        public YesNoAns PayoffOrdred { get; set; }

        public DateTime? PayoffOrdredDate { get; set; }

        public YesNoAns PayoffReceived { get; set; }

        public DateTime? PayoffReceivedDate { get; set; }

        public ProspectEnum? Prospect { get; set; }

        public DateTime? ProspectDate { get; set; }

        public YesNoAns? QCAuditCompleted { get; set; }

        public DateTime? QCAuditCompletedDate { get; set; }

        public YesNoAns QCAuditStarted { get; set; }

        public DateTime? QCAuditStartedDate { get; set; }

        public YesNoAns? RowLead { get; set; }

        public YesNoAns? SurveyOrdered { get; set; }

        public DateTime? SurveyOrderedDate { get; set; }

        public YesNoAns? SurveyReceived { get; set; }

        public DateTime? SurveyReceivedDate { get; set; }

        public YesNoAns? TitleOrdred { get; set; }

        public DateTime? TitleOrdredDate { get; set; }

        public YesNoAns? TitleReceived { get; set; }

        public DateTime? TitleReceivedDate { get; set; }

        public YesNoAns? WDO_Ordered { get; set; }

        public DateTime? WDO_OrderedDate { get; set; }
    }
}

