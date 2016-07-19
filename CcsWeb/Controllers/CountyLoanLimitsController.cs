namespace CcsWeb.Controllers
{
    using CcsData.Models;
    using CcsWeb.DataContexts;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    public class CountyLoanLimitsController : Controller
    {
        private CcsLocalDbContext db = new CcsLocalDbContext();

        public ActionResult Create() => 
            base.View();

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CountyLoanLimit_Id,Zipcode,State,County,LoanLimit")] CountyLoanLimit countyLoanLimit)
        {
            if (base.ModelState.IsValid)
            {
                this.db.CountyLoanLimits.Add(countyLoanLimit);
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            return base.View(countyLoanLimit);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountyLoanLimit model = this.db.CountyLoanLimits.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        [ValidateAntiForgeryToken, HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CountyLoanLimit entity = this.db.CountyLoanLimits.Find(new object[] { id });
            this.db.CountyLoanLimits.Remove(entity);
            this.db.SaveChanges();
            return base.RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountyLoanLimit model = this.db.CountyLoanLimits.Find(new object[] { id });
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

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountyLoanLimit model = this.db.CountyLoanLimits.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CountyLoanLimit_Id,Zipcode,State,County,LoanLimit,Loantype,propType")] CountyLoanLimit countyLoanLimit, string Btnasnew)
        {
            if (!base.ModelState.IsValid)
            {
                return base.View(countyLoanLimit);
            }
            if (Btnasnew != null)
            {
                this.db.CountyLoanLimits.Add(countyLoanLimit);
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            this.db.Entry<CountyLoanLimit>(countyLoanLimit).State = EntityState.Modified;
            this.db.SaveChanges();
            return base.RedirectToAction("Index");
        }

        public ActionResult Index() => 
            base.View(this.db.CountyLoanLimits.ToList<CountyLoanLimit>());
    }
}

