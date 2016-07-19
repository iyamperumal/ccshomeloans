namespace CcsWeb.DataContexts
{
    using CcsData.Models;
    using CcsData.Models.CreditPull;
    using CcsData.Models.FileUpload;
    using CcsData.ViewModels;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Runtime.CompilerServices;

    public class VpsContext : IdentityDbContext<ApplicationUser>
    {
        public VpsContext() : base("ccshl1Arvixevps", false)
        {
        }

        public static CcsRemoteDbContext Create() => 
            new CcsRemoteDbContext();

        public DbSet<AdditionalIncome> AdditionalIncomes { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Applicant> Applicants { get; set; }

        public DbSet<Application> Applications { get; set; }

        public DbSet<Asset> Assets { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<Bank> Banks { get; set; }

        public DbSet<CallLog> CallLogs { get; set; }

        public DbSet<CoApplicant> CoApplicants { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<CountyLoanLimitConv> CountyLoanLimitConvs { get; set; }

        public DbSet<CountyLoanLimitFHA> CountyLoanLimitFHAs { get; set; }

        public DbSet<CountyLoanLimit> CountyLoanLimits { get; set; }

        public DbSet<CountyLoanLimitVA> CountyLoanLimitVAs { get; set; }

        public DbSet<CreditLiability> CreditLiabilities { get; set; }

        public DbSet<CreditMortgage> CreditMortgages { get; set; }

        public DbSet<Credit> Credits { get; set; }

        public DbSet<DocFile> DocFiles { get; set; }

        public DbSet<Employer> Employers { get; set; }

        public DbSet<Employment> Employments { get; set; }

        public DbSet<Foreclosure> Forclosures { get; set; }

        public DbSet<Inquiry> Inquiries { get; set; }

        public DbSet<CcsData.Models.CreditPull.Lates> Lates { get; set; }

        public DbSet<LeadData> LeadDataFiles { get; set; }

        public DbSet<Liability> Liabilities { get; set; }

        public DbSet<LoanOfficer> LoanOfficers { get; set; }

        public DbSet<LoanProcessor> LoanProcessors { get; set; }

        public DbSet<LoanProgram> LoanPrograms { get; set; }

        public DbSet<Loan> Loans { get; set; }

        public DbSet<LoanStage> LoanStages { get; set; }

        public DbSet<Mortgage> Mortgages { get; set; }

        public DbSet<MortgageVM> MortgageVMs { get; set; }

        public DbSet<NewMortgage> NewMortgages { get; set; }

        public DbSet<OptionSelected> OptionsSlected { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<PublicRecord> PublicRecords { get; set; }

        public DbSet<CcsData.Models.Realtor> Realtor { get; set; }

        public DbSet<RefiOption> RefiOptions { get; set; }

        public DbSet<Representative> Representatives { get; set; }

        public DbSet<RequestCred> RequestCreds { get; set; }

        public DbSet<ResponseCred> ResponseCreds { get; set; }

        public DbSet<ResponseData> ResponseDatas { get; set; }

        public DbSet<SecondMortgage> SecondMortgages { get; set; }

        public DbSet<CcsData.Models.Seller> Seller { get; set; }

        public DbSet<SiteStat> SiteStats { get; set; }

        public DbSet<Testimonial> Testimonials { get; set; }

        public DbSet<UsdaCountiesAndIncome> UsdaCountiesAndIncomes { get; set; }

        public DbSet<VariableBackup> VariableBackups { get; set; }

        public DbSet<CcsData.Models.VariableCust> VariableCust { get; set; }

        public DbSet<Variable> Variables { get; set; }
    }
}

