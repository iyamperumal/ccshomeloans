namespace CcsData.Models.CreditPull
{
    using CcsData.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class RequestCred
    {
        public string AgeAtApplicationYears { get; set; }

        [ForeignKey("ApplicantID")]
        public virtual CcsData.Models.Applicant Applicant { get; set; }

        public virtual int? ApplicantID { get; set; }

        public string BorrowerID { get; set; }

        public string BorrowerResidencyType { get; set; }

        public string CITY { get; set; }

        public string CreditReportIdentifier { get; set; }

        public string CreditReportRequestActionType { get; set; }

        public string CreditReportType { get; set; }

        public string CreditRequestDateTime { get; set; }

        public string CreditRequestID { get; set; }

        public string CreditRequestType { get; set; }

        public string EquifaxIndicator { get; set; }

        public virtual string Exeption { get; set; }

        public string ExperianIndicator { get; set; }

        public string FirstName { get; set; }

        public string InternalAccountIdentifier { get; set; }

        public string LastName { get; set; }

        public string LenderCaseIdentifier { get; set; }

        public string LoginAccountIdentifier { get; set; }

        public string LoginAccountPassword { get; set; }

        public string MaritalStatusType { get; set; }

        public string MiddleName { get; set; }

        public string MISMOVersionID { get; set; }

        public string NameSuffix { get; set; }

        public string PostalCode { get; set; }

        public string PREFERRED_RESPONSE_Format { get; set; }

        public string PREFERRED_RESPONSE_FormatOtherDescription { get; set; }

        public string PREFERRED_RESPONSE_UseEmbeddedFileIndicator { get; set; }

        public string PrintPositionType { get; set; }

        public string RECEIVING_PARTY_Identifier { get; set; }

        public string REQUEST_DATA_BorrowerID { get; set; }

        [Key]
        public int RequestCred_Id { get; set; }

        public virtual DateTime RequestDate { get; set; }

        public string RequestDatetime { get; set; }

        public string RequestingPartyRequestedByName { get; set; }

        [ForeignKey("ResponseCredID")]
        public virtual CcsData.Models.CreditPull.ResponseCred ResponseCred { get; set; }

        public virtual int? ResponseCredID { get; set; }

        [ForeignKey("ResponseDataID")]
        public virtual CcsData.Models.CreditPull.ResponseData ResponseData { get; set; }

        public virtual int? ResponseDataID { get; set; }

        public string SSN { get; set; }

        public string STATE { get; set; }

        public string StreetAddress { get; set; }

        public string SUBMITTING_PARTY_Identifier { get; set; }

        public string SUBMITTING_PARTY_Name { get; set; }

        public string TransUnionIndicator { get; set; }
    }
}

