namespace CcsWeb.Controllers
{
    using CcsData.Models;
    using CcsData.Models.CreditPull;
    using CcsWeb.DataContexts;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    public class CreditMortgagesController : Controller
    {
        private CcsLocalDbContext db = new CcsLocalDbContext();

        public ActionResult Create()
        {
            ((dynamic) base.ViewBag).ApplicantID = new SelectList(this.db.Applicants, "Applicant_Id", "FullName");
            ((dynamic) base.ViewBag).ResponseCredID = new SelectList(this.db.ResponseCreds, "ResponseCred_Id", "FullName");
            return base.View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CreditMortgage_Id,Whose,Sourse,Lender,Balance,MonthlyPayment,Late30,Late30Dates,Late60,Late60Dates,Late90,Late90Dates,DateOpened,HiCredit,Term,MonthsRemaining,AccType,ResponseCredID,ApplicantID")] CreditMortgage creditMortgage)
        {
            if (base.ModelState.IsValid)
            {
                this.db.CreditMortgages.Add(creditMortgage);
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            ((dynamic) base.ViewBag).ApplicantID = new SelectList(this.db.Applicants, "Applicant_Id", "FullName", creditMortgage.ApplicantID);
            ((dynamic) base.ViewBag).ResponseCredID = new SelectList(this.db.ResponseCreds, "ResponseCred_Id", "FullName", creditMortgage.ResponseCredID);
            return base.View(creditMortgage);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditMortgage model = this.db.CreditMortgages.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        [ValidateAntiForgeryToken, ActionName("Delete"), HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            CreditMortgage entity = this.db.CreditMortgages.Find(new object[] { id });
            this.db.CreditMortgages.Remove(entity);
            this.db.SaveChanges();
            return base.RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditMortgage model = this.db.CreditMortgages.Find(new object[] { id });
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

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult Edit([Bind(Include="CreditMortgage_Id,Whose,Sourse,Lender,Balance,MonthlyPayment,Late30,Late30Dates,Late60,Late60Dates,Late90,Late90Dates,DateOpened,HiCredit,Term,MonthsRemaining,AccType,ResponseCredID,ApplicantID")] CreditMortgage creditMortgage)
        {
            if (base.ModelState.IsValid)
            {
                this.db.Entry<CreditMortgage>(creditMortgage).State = EntityState.Modified;
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            ((dynamic) base.ViewBag).ApplicantID = new SelectList(this.db.Applicants, "Applicant_Id", "FullName", creditMortgage.ApplicantID);
            ((dynamic) base.ViewBag).ResponseCredID = new SelectList(this.db.ResponseCreds, "ResponseCred_Id", "FullName", creditMortgage.ResponseCredID);
            return base.View(creditMortgage);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditMortgage model = this.db.CreditMortgages.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            ((dynamic) base.ViewBag).ApplicantID = new SelectList(this.db.Applicants, "Applicant_Id", "FullName", model.ApplicantID);
            ((dynamic) base.ViewBag).ResponseCredID = new SelectList(this.db.ResponseCreds, "ResponseCred_Id", "FullName", model.ResponseCredID);
            return base.View(model);
        }

        public ActionResult Index()
        {
            IQueryable<CreditMortgage> source = this.db.CreditMortgages.Include<CreditMortgage, Applicant>(c => c.Applicant).Include<CreditMortgage, ResponseCred>(c => c.ResponseCred);
            return base.View(source.ToList<CreditMortgage>());
        }
    }
}

