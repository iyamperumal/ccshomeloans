namespace CcsWeb.Controllers
{
    using CcsData.Models;
    using CcsWeb.DataContexts;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.CSharp.RuntimeBinder;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class ApplicantsController : Controller
    {
        private CcsLocalDbContext db = new CcsLocalDbContext();

        public ActionResult Create()
        {
            ((dynamic) base.ViewBag).CreditID = new SelectList(this.db.Credits, "Credit_Id", "HowOldAreJudmentAndCollections");
            ((dynamic) base.ViewBag).LeadsData_ID = new SelectList(this.db.LeadDataFiles, "LeadData_Id", "Criteria");
            ((dynamic) base.ViewBag).LoanStageID = new SelectList(this.db.LoanStages, "LoanStage_Id", "LoanStage_Id");
            ((dynamic) base.ViewBag).OptionSelectedID = new SelectList(this.db.OptionsSlected, "OptionSelected_Id", "PreparedFor");
            ((dynamic) base.ViewBag).RealtorID = new SelectList(this.db.Realtor, "Realtor_Id", "FirstName");
            ((dynamic) base.ViewBag).Rep_ID = new SelectList(this.db.Representatives, "Representative_Id", "FullName");
            ((dynamic) base.ViewBag).SecondMortgage_ID = new SelectList(this.db.SecondMortgages, "SecondMortgage_Id", "MortgageName");
            ((dynamic) base.ViewBag).SellerID = new SelectList(this.db.Seller, "Seller_Id", "FirstName");
            ((dynamic) base.ViewBag).VariableCustID = new SelectList(this.db.VariableCust, "VariableCust_Id", "MortgageProgramOption");
            return base.View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Applicant_Id,LoanTypeRequested,FullName,LastName,FirstName,MiddleName,Suffix,CreditScoreEstimate,Motive,CashOutAmountRequested,CashOutType,ComputedCashOutAmountAvailable,TotalBalanceOfDebtToConsolidate,TotalMonthlyAmountOfDebtPaymentsToConsolidate,EmailAddress,CareOfName,HomePhone,WorkPhone,CellPhone,Fax,DoNotCall,MaritalStatus,DateOfBirth,SocialSecurityNumber,SocialSecurity4,YearsInSchool,NumberOfDependents,Ages,ClientApplicationDate,CallBackDate,CallBackTime,TimesMailed,BatchNumber,CreditResolicitDate,LTVResolicitDate,DTIResolicitDate,OptionNumber,Disposition,LenderRefuseReason,ClientRefuseReason,CreditRatingReason,SiteRating,CustomerServiceRating,SecondMortgage_ID,LeadsData_ID,Rep_ID,CreditID,VariableCustID,LoanStageID,RealtorID,SellerID,OptionSelectedID,Veteran,Have2ndMortgage,PayOff2ndMortgage")] Applicant applicant)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Applicants.Add(applicant);
            }
            else
            {
                ((dynamic) this.ViewBag).CreditID = new SelectList(this.db.Credits, "Credit_Id", "HowOldAreJudmentAndCollections", applicant.CreditID);
                ((dynamic) this.ViewBag).LeadsData_ID = new SelectList(this.db.LeadDataFiles, "LeadData_Id", "Criteria", applicant.LeadsData_ID);
                ((dynamic) this.ViewBag).LoanStageID = new SelectList(this.db.LoanStages, "LoanStage_Id", "LoanStage_Id", applicant.LoanStageID);
                ((dynamic) this.ViewBag).OptionSelectedID = new SelectList(this.db.OptionsSlected, "OptionSelected_Id", "PreparedFor", applicant.OptionSelectedID);
                ((dynamic) this.ViewBag).RealtorID = new SelectList(this.db.Realtor, "Realtor_Id", "FirstName", applicant.RealtorID);
                ((dynamic) this.ViewBag).Rep_ID = new SelectList(this.db.Representatives, "Representative_Id", "FullName", applicant.Rep_ID);
                ((dynamic) this.ViewBag).SecondMortgage_ID = new SelectList(this.db.SecondMortgages, "SecondMortgage_Id", "MortgageName", applicant.SecondMortgage_ID);
                ((dynamic) this.ViewBag).SellerID = new SelectList(this.db.Seller, "Seller_Id", "FirstName", applicant.SellerID);
                ((dynamic) this.ViewBag).VariableCustID = new SelectList(this.db.VariableCust, "VariableCust_Id", "MortgageProgramOption", applicant.VariableCustID);
                return this.View(applicant);
            }
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int? id)
        {
            ActionResult result;
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant model = await this.db.Applicants.FindAsync(new object[] { id });
            if (model == null)
            {
                result = this.HttpNotFound();
            }
            else
            {
                result = this.View(model);
            }
            return result;
        }

        [ActionName("Delete"), HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Applicant entity = await this.db.Applicants.FindAsync(new object[] { id });
            this.db.Applicants.Remove(entity);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int? id)
        {
            ActionResult result;
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant model = await this.db.Applicants.FindAsync(new object[] { id });
            if (model == null)
            {
                result = this.HttpNotFound();
            }
            else
            {
                result = this.View(model);
            }
            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Applicant_Id,LoanTypeRequested,FullName,LastName,FirstName,MiddleName,Suffix,CreditScoreEstimate,Motive,CashOutAmountRequested,CashOutType,ComputedCashOutAmountAvailable,TotalBalanceOfDebtToConsolidate,TotalMonthlyAmountOfDebtPaymentsToConsolidate,EmailAddress,CareOfName,HomePhone,WorkPhone,CellPhone,Fax,DoNotCall,MaritalStatus,DateOfBirth,SocialSecurityNumber,SocialSecurity4,YearsInSchool,NumberOfDependents,Ages,ClientApplicationDate,CallBackDate,CallBackTime,TimesMailed,BatchNumber,CreditResolicitDate,LTVResolicitDate,DTIResolicitDate,OptionNumber,Disposition,LenderRefuseReason,ClientRefuseReason,CreditRatingReason,SiteRating,CustomerServiceRating,SecondMortgage_ID,LeadsData_ID,Rep_ID,CreditID,VariableCustID,LoanStageID,RealtorID,SellerID,OptionSelectedID,Veteran,Have2ndMortgage,PayOff2ndMortgage")] Applicant applicant)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Entry<Applicant>(applicant).State = EntityState.Modified;
            }
            else
            {
                ((dynamic) this.ViewBag).CreditID = new SelectList(this.db.Credits, "Credit_Id", "HowOldAreJudmentAndCollections", applicant.CreditID);
                ((dynamic) this.ViewBag).LeadsData_ID = new SelectList(this.db.LeadDataFiles, "LeadData_Id", "Criteria", applicant.LeadsData_ID);
                ((dynamic) this.ViewBag).LoanStageID = new SelectList(this.db.LoanStages, "LoanStage_Id", "LoanStage_Id", applicant.LoanStageID);
                ((dynamic) this.ViewBag).OptionSelectedID = new SelectList(this.db.OptionsSlected, "OptionSelected_Id", "PreparedFor", applicant.OptionSelectedID);
                ((dynamic) this.ViewBag).RealtorID = new SelectList(this.db.Realtor, "Realtor_Id", "FirstName", applicant.RealtorID);
                ((dynamic) this.ViewBag).Rep_ID = new SelectList(this.db.Representatives, "Representative_Id", "FullName", applicant.Rep_ID);
                ((dynamic) this.ViewBag).SecondMortgage_ID = new SelectList(this.db.SecondMortgages, "SecondMortgage_Id", "MortgageName", applicant.SecondMortgage_ID);
                ((dynamic) this.ViewBag).SellerID = new SelectList(this.db.Seller, "Seller_Id", "FirstName", applicant.SellerID);
                ((dynamic) this.ViewBag).VariableCustID = new SelectList(this.db.VariableCust, "VariableCust_Id", "MortgageProgramOption", applicant.VariableCustID);
                return this.View(applicant);
            }
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int? id)
        {
            ActionResult result;
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant model = await this.db.Applicants.FindAsync(new object[] { id });
            if (model == null)
            {
                result = this.HttpNotFound();
            }
            else
            {
                ((dynamic) this.ViewBag).CreditID = new SelectList(this.db.Credits, "Credit_Id", "HowOldAreJudmentAndCollections", model.CreditID);
                ((dynamic) this.ViewBag).LeadsData_ID = new SelectList(this.db.LeadDataFiles, "LeadData_Id", "Criteria", model.LeadsData_ID);
                ((dynamic) this.ViewBag).LoanStageID = new SelectList(this.db.LoanStages, "LoanStage_Id", "LoanStage_Id", model.LoanStageID);
                ((dynamic) this.ViewBag).OptionSelectedID = new SelectList(this.db.OptionsSlected, "OptionSelected_Id", "PreparedFor", model.OptionSelectedID);
                ((dynamic) this.ViewBag).RealtorID = new SelectList(this.db.Realtor, "Realtor_Id", "FirstName", model.RealtorID);
                ((dynamic) this.ViewBag).Rep_ID = new SelectList(this.db.Representatives, "Representative_Id", "FullName", model.Rep_ID);
                ((dynamic) this.ViewBag).SecondMortgage_ID = new SelectList(this.db.SecondMortgages, "SecondMortgage_Id", "MortgageName", model.SecondMortgage_ID);
                ((dynamic) this.ViewBag).SellerID = new SelectList(this.db.Seller, "Seller_Id", "FirstName", model.SellerID);
                ((dynamic) this.ViewBag).VariableCustID = new SelectList(this.db.VariableCust, "VariableCust_Id", "MortgageProgramOption", model.VariableCustID);
                result = this.View(model);
            }
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, Applicant applicant)
        {
            if ((applicant != null) && base.ModelState.IsValid)
            {
                this.db.Entry<Applicant>(applicant).State = EntityState.Modified;
                this.db.SaveChanges();
            }
            return base.Json(new Applicant[] { applicant }.ToDataSourceResult(request, base.ModelState));
        }

        public ActionResult GetApplicants([DataSourceRequest] DataSourceRequest request)
        {
            //from c in this.db.Applicants.ToList<Applicant>() select new { 
            //    LoanTypeRequested = c.LoanTypeRequested,
            //    CashOutAmountRequested = c.CashOutAmountRequested,
            //    TotalBalanceOfDebtToConsolidate = c.TotalBalanceOfDebtToConsolidate,
            //    TotalMonthlyAmountOfDebtPaymentsToConsolidate = c.TotalMonthlyAmountOfDebtPaymentsToConsolidate,
            //    Has2ndMortgage = c.Has2ndMortgage,
            //    PayOff2ndMortgage = c.PayOff2ndMortgage,
            //    Veteran = c.Veteran,
            //    CreditScoreEstimate = c.CreditScoreEstimate,
            //    FullName = c.FullName,
            //    Applicant_Id = c.Applicant_Id
            //};
            List<Applicant> enumerable = this.db.Applicants.ToList<Applicant>();
            return base.Json(enumerable.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProperty(int applicantID, [DataSourceRequest] DataSourceRequest request)
        {
            List<Property> enumerable = (from p in this.db.Properties
                where p.Applicant_ID == applicantID
                select p).ToList<Property>();
            return base.Json(enumerable.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index() => 
            base.View();

        public async Task<ActionResult> Index2()
        {
            IQueryable<Applicant> source = this.db.Applicants.Include<Applicant, Credit>(a => a.Credit).Include<Applicant, LeadData>(a => a.LeadData).Include<Applicant, LoanStage>(a => a.LoanStage).Include<Applicant, OptionSelected>(a => a.OptionSelected).Include<Applicant, Realtor>(a => a.Realtor).Include<Applicant, Representative>(a => a.Rep).Include<Applicant, SecondMortgage>(a => a.SecondMortgage).Include<Applicant, Seller>(a => a.Seller).Include<Applicant, VariableCust>(a => a.VariableCust);
            List<Applicant> model = await source.ToListAsync<Applicant>();
            return View(model);
        }







    }
}

