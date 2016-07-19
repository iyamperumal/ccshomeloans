namespace CcsWeb.Controllers
{
    using AutoMapper;
    using CcsData.Models;
    using CcsData.Models.CreditPull;
    using CcsData.ViewModels;
    using CcsWeb.DataContexts;
    using CcsWeb.Mailers;
    using CcsWeb.Models;
    using Mvc.Mailer;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    public class Mortgages1Controller : Controller
    {
        private IUserMailer _userMailer = new CcsWeb.Mailers.UserMailer();
        private CcsLocalDbContext db = new CcsLocalDbContext();

        public ActionResult Create()
        {
            ((dynamic) base.ViewBag).Applicant_ID = new SelectList(this.db.Applicants, "Applicant_Id", "FullName");
            return base.View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Mortgage_Id,MortgageName,MortageType,MonthlyPayment,PymtIncludesTaxes,PymtIncludesInsurance,YearlyPropertyTaxes,YearlyHomeInsurancePayment,MonthlyMortgageInsurance,InterestRate,RateType,Term,MaturityDate,PrimaryLoanType,LanderName,OriginationDate,OriginalAmount,PrePaymentPenalty,BalloonPayment,BalloonPaymentDueDate,YearlyMortgageInsurance,MonthlyPropertyTaxes,YearlyPropertyTacxes,Balance,Position,Applicant_ID")] Mortgage mortgage)
        {
            if (base.ModelState.IsValid)
            {
                this.db.Mortgages.Add(mortgage);
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            ((dynamic) base.ViewBag).Applicant_ID = new SelectList(this.db.Applicants, "Applicant_Id", "FullName", mortgage.Applicant_ID);
            return base.View(mortgage);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mortgage model = this.db.Mortgages.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mortgage entity = this.db.Mortgages.Find(new object[] { id });
            this.db.Mortgages.Remove(entity);
            this.db.SaveChanges();
            return base.RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mortgage model = this.db.Mortgages.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
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
        public ActionResult Edit([Bind(Include="Mortgage_Id,MortgageName,MortageType,MonthlyPayment,PymtIncludesTaxes,PymtIncludesInsurance,YearlyPropertyTaxes,YearlyHomeInsurancePayment,MonthlyMortgageInsurance,InterestRate,RateType,Term,MaturityDate,PrimaryLoanType,LanderName,OriginationDate,OriginalAmount,PrePaymentPenalty,BalloonPayment,BalloonPaymentDueDate,YearlyMortgageInsurance,MonthlyPropertyTaxes,YearlyPropertyTacxes,Balance,Position,Applicant_ID")] Mortgage mortgage)
        {
            if (base.ModelState.IsValid)
            {
                this.db.Entry<Mortgage>(mortgage).State = EntityState.Modified;
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            ((dynamic) base.ViewBag).Applicant_ID = new SelectList(this.db.Applicants, "Applicant_Id", "FullName", mortgage.Applicant_ID);
            return base.View(mortgage);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mortgage model = this.db.Mortgages.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            ((dynamic) base.ViewBag).Applicant_ID = new SelectList(this.db.Applicants, "Applicant_Id", "FullName", model.Applicant_ID);
            return base.View(model);
        }

        public JsonResult GetOptions()
        {
            int id = Util.GetApplicantId(this.db, base.User.Identity.Name);
            Applicant app = (from ap in this.db.Applicants
                where ap.Applicant_Id == id
                select ap).Include<Applicant, List<Application>>(ap => ap.Applications).Include<Applicant, List<PublicRecord>>(ap => ap.publicRecords).FirstOrDefault<Applicant>();
            Application lastApplication = (from d in app.Applications
                orderby d.ApplicationDate descending
                select d).FirstOrDefault<Application>();
            lastApplication.publicRecords = app.publicRecords;
            if ((lastApplication.LoanTypeRequested == LoanTypeRequestedEnum.PurchaseLoan) || (lastApplication.LoanTypeRequested == LoanTypeRequestedEnum.RealtorReferredPurchaseLoan))
            {
                return this.PurchaseOptionsJson(id, app, lastApplication);
            }
            return this.RefiOptionsJson(id, app, lastApplication);
        }

        public ActionResult Index()
        {
            IQueryable<Mortgage> source = this.db.Mortgages.Include<Mortgage, Applicant>(m => m.MortgageApplicant);
            return base.View(source.ToList<Mortgage>());
        }

        [Authorize]
        public ActionResult MortgageOptionsNow() => 
            base.View();

        public ActionResult MyOptions()
        {
            int id = Util.GetApplicantId(this.db, base.User.Identity.Name);
            Applicant applicant = (from ap in this.db.Applicants
                where ap.Applicant_Id == id
                select ap).Include<Applicant, List<Application>>(ap => ap.Applications).Include<Applicant, List<PublicRecord>>(ap => ap.publicRecords).FirstOrDefault<Applicant>();
            Application application = (from d in applicant.Applications
                orderby d.ApplicationDate descending
                select d).FirstOrDefault<Application>();
            application.publicRecords = applicant.publicRecords;
            if ((application.LoanTypeRequested != LoanTypeRequestedEnum.PurchaseLoan) && (application.LoanTypeRequested != LoanTypeRequestedEnum.RealtorReferredPurchaseLoan))
            {
                return base.View("RefiOptionsNow");
            }
            return base.View("PurchaseOptionsNow");
        }

        public JsonResult OptionsBack()
        {
            int id = Util.GetApplicantId(this.db, base.User.Identity.Name);
            Applicant applicant = (from ap in this.db.Applicants
                where ap.Applicant_Id == id
                select ap).Include<Applicant, SecondMortgage>(sm => sm.SecondMortgage).FirstOrDefault<Applicant>();
            Mortgage curMtg = (from m in this.db.Mortgages
                where (m.Applicant_ID == id) && (m.Position == 1)
                orderby m.EntryDate descending
                select m).Include<Mortgage, Property>(p => p.MortgagedProperty).FirstOrDefault<Mortgage>();
            curMtg.MortgageApplicant = applicant;
            List<OptionSelected> data = Util.getOptionsSelected(curMtg);
            decimal num = data[0].NewMonthlyPaymentPrincipalInterest.Value;
            int num2 = 0;
            for (int i = 1; i < data.Count; i++)
            {
                decimal num4 = data[i].NewMonthlyPaymentPrincipalInterest.Value;
                if (num4 < num)
                {
                    num = num4;
                    num2 = i;
                }
            }
            data[num2].Hilited = "popular";
            data[0].Hl_MonthlyPaymentsEliminated = data[num2].MonthlyPaymentsEliminated;
            data[0].Hl_MonthsPaidRemaining = data[num2].MonthsPaidRemaining;
            data[0].Hl_TotalSavingsFromOldMortgageToNewMortgage = data[num2].TotalSavingsFromOldMortgageToNewMortgage;
            data[0].Hl_MonthlySavings = data[num2].MonthlySavings;
            data[0].LowestPayment = new decimal?(num);
            return base.Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OrgOptions()
        {
            MortgageVM source = (MortgageVM) base.Session["mrtVm"];
            List<OptionSelected> data = Util.getOptionsSelected(Mapper.Map<MortgageVM, Mortgage>(source));
            new OptionSelected();
            decimal num = data[0].NewMonthlyPaymentPrincipalInterest.Value;
            int num2 = 0;
            for (int i = 1; i < data.Count; i++)
            {
                decimal num4 = data[i].NewMonthlyPaymentPrincipalInterest.Value;
                if (num4 < num)
                {
                    num = num4;
                    num2 = i;
                }
            }
            data[num2].Hilited = "popular";
            data[0].Hl_MonthlyPaymentsEliminated = data[num2].MonthlyPaymentsEliminated;
            data[0].Hl_MonthsPaidRemaining = data[num2].MonthsPaidRemaining;
            data[0].Hl_TotalSavingsFromOldMortgageToNewMortgage = data[num2].TotalSavingsFromOldMortgageToNewMortgage;
            data[0].Hl_MonthlySavings = data[num2].MonthlySavings;
            data[0].LowestPayment = new decimal?(num);
            return base.Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OrgOptionsNow() => 
            base.View();

        public JsonResult PurchaseOptionsJson(int id, Applicant app, Application LastApplication)
        {
            List<PurchaseOptionSelected> data = Util.getPurchaseOptionsSelected(this.db, LastApplication);
            new PurchaseOption();
            decimal num = data[0].MonthlyPaymentPrincipalInterest.Value;
            int num2 = 0;
            for (int i = 1; i < data.Count; i++)

            {
                decimal num4 = data[i].MonthlyPaymentPrincipalInterest.Value;
                if (num4 < num)
                {
                    num = num4;
                    num2 = i;
                }
            }
            data[num2].Hilited = "popular";
            data[0].LowestPayment = new decimal?(num);
            return base.Json(data, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult PurchaseOptionsNow() => 
            base.View();

        internal List<PurchaseOptionSelected> PurshaseOptionsResult()
        {
            int id = Util.GetApplicantId(this.db, base.User.Identity.Name);
            Application app = (from ap in (from ap in this.db.Applicants
                where ap.Applicant_Id == id
                select ap).Include<Applicant, List<Application>>(ap => ap.Applications).FirstOrDefault<Applicant>().Applications
                orderby ap.ApplicationDate descending
                select ap).FirstOrDefault<Application>();
            List<PurchaseOptionSelected> list = Util.getPurchaseOptionsSelected(this.db, app);
            new PurchaseOption();
            decimal num = list[0].MonthlyPaymentPrincipalInterest.Value;
            int num2 = 0;
            for (int i = 1; i < list.Count; i++)
            {
                decimal num4 = list[i].MonthlyPaymentPrincipalInterest.Value;
                if (num4 < num)
                {
                    num = num4;
                    num2 = i;
                }
            }
            list[num2].Hilited = "popular";
            list[0].LowestPayment = new decimal?(num);
            return list;
        }

        [Authorize]
        public ActionResult Refi1Now()
        {
            int id = Util.GetApplicantId(this.db, base.User.Identity.Name);
            Applicant applicant = this.db.Applicants.Find(new object[] { id });
            Mortgage source = (from m in this.db.Mortgages
                where m.Applicant_ID == id
                select m).FirstOrDefault<Mortgage>();
            MortgageVM model = Mapper.Map<Mortgage, MortgageVM>(source);
            model.CashOutAmountRequested = applicant.CashOutAmountRequested;
            Property property = (from p in this.db.Properties
                where p.Applicant_ID == id
                select p).FirstOrDefault<Property>();
            if (property != null)
            {
                model.EstimatedMarketValue = property.EstimatedMarketValue;
            }
            if (source == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult Refi1Now(MortgageVM mortVm)
        {
            if (!base.ModelState.IsValid)
            {
                return base.View("refi1Now", mortVm);
            }
            Mortgage destination = this.db.Mortgages.Find(new object[] { mortVm.Mortgage_Id });
            int cusId = destination.Applicant_ID.Value;
            Property entity = (from p in this.db.Properties
                where p.Applicant_ID == cusId
                select p).FirstOrDefault<Property>();
            this.db.Applicants.Find(new object[] { cusId }).CashOutAmountRequested = mortVm.CashOutAmountRequested;
            if (entity != null)
            {
                entity.EstimatedMarketValue = mortVm.EstimatedMarketValue;
                this.db.Entry<Property>(entity).State = EntityState.Modified;
            }
            Mortgage mortgage2 = Mapper.Map<MortgageVM, Mortgage>(mortVm, destination);
            this.db.Entry<Mortgage>(mortgage2).State = EntityState.Modified;
            this.db.SaveChanges();
            return base.RedirectToAction("RefiOptionsNow");
        }

        public ActionResult Refinow() => 
            base.View();

        public JsonResult RefiOptionsJson(int id, Applicant app, Application apl2)
        {
            List<OptionSelected> data = Util.getOptionsSelected(this.db, apl2);
            decimal num = data[0].NewMonthlyPaymentPrincipalInterest.Value;
            int num2 = 0;
            for (int i = 1; i < data.Count; i++)
            {
                decimal num4 = data[i].NewMonthlyPaymentPrincipalInterest.Value;
                if (num4 < num)
                {
                    num = num4;
                    num2 = i;
                }
            }
            data[num2].Hilited = "popular";
            data[0].Hl_MonthlyPaymentsEliminated = data[num2].MonthlyPaymentsEliminated;
            data[0].Hl_MonthsPaidRemaining = data[num2].MonthsPaidRemaining;
            data[0].Hl_TotalSavingsFromOldMortgageToNewMortgage = data[num2].TotalSavingsFromOldMortgageToNewMortgage;
            data[0].Hl_MonthlySavings = data[num2].MonthlySavings;
            data[0].LowestPayment = new decimal?(num);
            return base.Json(data, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult RefiOptionsNow() => 
            base.View();

        private void Respond(AppEmailModel model)
        {
            base.ViewData["viewdataMessage"] = "message from view data";
            ((dynamic) base.ViewBag).WelcomeMessage = "welcome Message";
            new CcsWeb.Mailers.UserMailer();
            MvcMailMessage message = this.UserMailer.Welcome(model);
            message.Subject = model.Subject;
            message.To.Add(model.ToEmailAddess);
            message.Send(null);
        }

        public void SaveSelectedOption(OptionSelected optionSelected)
        {
            int applicantId = Util.GetApplicantId(this.db, base.User.Identity.Name);
            Applicant applicant = this.db.Applicants.Find(new object[] { applicantId });
            if (applicant.OptionSelectedID.HasValue)
            {
                OptionSelected entity = this.db.OptionsSlected.Find(new object[] { applicant.OptionSelectedID });
                this.db.OptionsSlected.Remove(entity);
            }
            applicant.OptionSelected = optionSelected;
            this.db.SaveChanges();
        }

        public ActionResult SelectedOption()
        {
            int applicantId = Util.GetApplicantId(this.db, base.User.Identity.Name);
            Applicant source = this.db.Applicants.Find(new object[] { applicantId });
            ApplicantVm destination = new ApplicantVm();
            Mapper.Map<Applicant, ApplicantVm>(source, destination);
            return base.View("SelectedOption", destination);
        }

        [HttpPost]
        public ActionResult SelectedOption(ApplicantVm appVm)
        {
            if (base.ModelState.IsValid)
            {
                int applicantId = Util.GetApplicantId(this.db, base.User.Identity.Name);
                Applicant destination = this.db.Applicants.Find(new object[] { applicantId });
                Mapper.Map<ApplicantVm, Applicant>(appVm, destination);
                AppEmailModel model = new AppEmailModel {
                    FirstName = destination.FirstName,
                    LastName = destination.LastName,
                    Subject = "Mortgage Application recieved From a lead mailer",
                    ToEmailAddess = destination.EmailAddress
                };
                this.Respond(model);
                this.db.SaveChanges();
            }
            return base.RedirectToAction("Index", "home");
        }

        public ActionResult SelectedOption1()
        {
            int applicantId = Util.GetApplicantId(this.db, base.User.Identity.Name);
            Applicant source = this.db.Applicants.Find(new object[] { applicantId });
            ApplicantVm destination = new ApplicantVm();
            Mapper.Map<Applicant, ApplicantVm>(source, destination);
            return base.View("SelectedOption1", destination);
        }

        public JsonResult SelectedOptions()
        {
            int id = Util.GetApplicantId(this.db, base.User.Identity.Name);
            Applicant applicant = (from ap in this.db.Applicants
                where ap.Applicant_Id == id
                select ap).FirstOrDefault<Applicant>();
            OptionSelected data = this.db.OptionsSlected.Find(new object[] { applicant.OptionSelectedID });
            return base.Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SelectedOptions1()
        {
            int id = Util.GetApplicantId(this.db, base.User.Identity.Name);
            Applicant applicant = (from ap in this.db.Applicants
                where ap.Applicant_Id == id
                select ap).FirstOrDefault<Applicant>();
            OptionSelected data = this.db.OptionsSlected.Find(new object[] { applicant.OptionSelectedID });
            return base.Json(data, JsonRequestBehavior.AllowGet);
        }

        public IUserMailer UserMailer
        {
            get
            {
                return this._userMailer;
            }
            set
            {
                this._userMailer = value;
            }
        }
    }
}

