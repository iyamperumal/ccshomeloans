namespace CcsData.Models.CreditPull
{
    using CcsData.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class ResponseData
    {
        [ForeignKey("ApplicantID")]
        public virtual CcsData.Models.Applicant Applicant { get; set; }

        public virtual int? ApplicantID { get; set; }

        public virtual string HTMLResponse { get; set; }

        public int ResponseCredId { get; set; }

        [Key]
        public int ResponseData_Id { get; set; }

        public string XMLRequest { get; set; }

        public virtual string XMLResponse { get; set; }
    }
}

