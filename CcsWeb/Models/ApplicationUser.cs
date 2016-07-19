namespace CcsWeb.Models
{
    using CcsData.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager) => 
            await manager.CreateIdentityAsync(this, "ApplicationCookie");

        [ForeignKey("Applicant_id")]
        public virtual CcsData.Models.Applicant Applicant { get; set; }

        public virtual int? Applicant_id { get; set; }

        [ForeignKey("Application_id")]
        public virtual CcsData.Models.Application Application { get; set; }

        public virtual int? Application_id { get; set; }

        [DefaultValue(false)]
        public bool AppVerified { get; set; }

        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [StringLength(50)]
        public string VerificationCode { get; set; }

    }
}

