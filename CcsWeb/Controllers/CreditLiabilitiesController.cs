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

    public class CreditLiabilitiesController : Controller
    {
        private CcsLocalDbContext db = new CcsLocalDbContext();

        public ActionResult Create()
        {
            ((dynamic) base.ViewBag).ApplicantID = new SelectList(this.db.Applicants, "Applicant_Id", "FullName");
            ((dynamic) base.ViewBag).ResponseCredID = new SelectList(this.db.ResponseCreds, "ResponseCred_Id", "FullName");
            return base.View();
        }

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult Create([Bind(Include="Liability_Id,Whose,Sourse,creditor,Balance,MonthlyPayment,Late30,Late30Dates,Late60,Late60Dates,Late90,Late90Dates,DateOpened,HiCredit,Term,MonthsRemaining,AccType,ResponseCredID,ApplicantID")] CreditLiability creditLiability)
        {
            if (base.ModelState.IsValid)
            {
                this.db.CreditLiabilities.Add(creditLiability);
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            ((dynamic) base.ViewBag).ApplicantID = new SelectList(this.db.Applicants, "Applicant_Id", "FullName", creditLiability.ApplicantID);
            ((dynamic) base.ViewBag).ResponseCredID = new SelectList(this.db.ResponseCreds, "ResponseCred_Id", "FullName", creditLiability.ResponseCredID);
            return base.View(creditLiability);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditLiability model = this.db.CreditLiabilities.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        [ValidateAntiForgeryToken, ActionName("Delete"), HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            CreditLiability entity = this.db.CreditLiabilities.Find(new object[] { id });
            this.db.CreditLiabilities.Remove(entity);
            this.db.SaveChanges();
            return base.RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditLiability model = this.db.CreditLiabilities.Find(new object[] { id });
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
        public ActionResult Edit([Bind(Include="Liability_Id,Whose,Sourse,creditor,Balance,MonthlyPayment,Late30,Late30Dates,Late60,Late60Dates,Late90,Late90Dates,DateOpened,HiCredit,Term,MonthsRemaining,AccType,ResponseCredID,ApplicantID")] CreditLiability creditLiability)
        {
            if (base.ModelState.IsValid)
            {
                this.db.Entry<CreditLiability>(creditLiability).State = EntityState.Modified;
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            ((dynamic) base.ViewBag).ApplicantID = new SelectList(this.db.Applicants, "Applicant_Id", "FullName", creditLiability.ApplicantID);
            ((dynamic) base.ViewBag).ResponseCredID = new SelectList(this.db.ResponseCreds, "ResponseCred_Id", "FullName", creditLiability.ResponseCredID);
            return base.View(creditLiability);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditLiability model = this.db.CreditLiabilities.Find(new object[] { id });
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
            IQueryable<CreditLiability> source = this.db.CreditLiabilities.Include<CreditLiability, Applicant>(c => c.Applicant).Include<CreditLiability, ResponseCred>(c => c.ResponseCred);
            return base.View(source.ToList<CreditLiability>());
        }
    }
}

