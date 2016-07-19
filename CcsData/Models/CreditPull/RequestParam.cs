namespace CcsData.Models.CreditPull
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    internal class RequestParam
    {
        public bool Active { get; set; }

        public CreditAccountTypeEnum CreditAccountType { get; set; }

        public string CreditCompanyName { get; set; }

        public string CreditReportIdentifier { get; set; }

        public string CreditReportRequestActionType { get; set; }

        public string CreditReportType { get; set; }

        public string CreditRequestDateTime { get; set; }

        public string CreditRequestID { get; set; }

        public string CreditRequestType { get; set; }

        public string InternalAccountIdentifier { get; set; }

        public string LenderCaseIdentifier { get; set; }

        public string LoginAccountIdentifier { get; set; }

        public string LoginAccountPassword { get; set; }

        public string MISMOVersionID { get; set; }

        public virtual string Note { get; set; }

        public string PREFERRED_RESPONSE_Format { get; set; }

        public string PREFERRED_RESPONSE_FormatOtherDescription { get; set; }

        public string PREFERRED_RESPONSE_UseEmbeddedFileIndicator { get; set; }

        public string RECEIVING_PARTY_Identifier { get; set; }

        public string REQUEST_DATA_BorrowerID { get; set; }

        public string RequestingPartyRequestedByName { get; set; }

        [Key]
        public int RequestParam_Id { get; set; }

        public string SUBMITTING_PARTY_Identifier { get; set; }

        public string SUBMITTING_PARTY_Name { get; set; }
    }
}

