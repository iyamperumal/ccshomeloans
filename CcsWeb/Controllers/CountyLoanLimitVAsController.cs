namespace CcsWeb.Controllers
{
    using CcsData.Models;
    using CcsWeb.DataContexts;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    [Authorize(Roles="Admin")]
    public class CountyLoanLimitVAsController : Controller
    {
        private CcsLocalDbContext db = new CcsLocalDbContext();

        public ActionResult Create() => 
            base.View();

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult Create([Bind(Include="CountyLoanLimitVA_Id,State,County,Fips,LoanLimit")] CountyLoanLimitVA countyLoanLimitVA)
        {
            if (base.ModelState.IsValid)
            {
                this.db.CountyLoanLimitVAs.Add(countyLoanLimitVA);
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            return base.View(countyLoanLimitVA);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountyLoanLimitVA model = this.db.CountyLoanLimitVAs.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        [ActionName("Delete"), ValidateAntiForgeryToken, HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            CountyLoanLimitVA entity = this.db.CountyLoanLimitVAs.Find(new object[] { id });
            this.db.CountyLoanLimitVAs.Remove(entity);
            this.db.SaveChanges();
            return base.RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountyLoanLimitVA model = this.db.CountyLoanLimitVAs.Find(new object[] { id });
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
        public ActionResult Edit([Bind(Include="CountyLoanLimitVA_Id,State,Fips,County,LoanLimit")] CountyLoanLimitVA countyLoanLimitVA)
        {
            if (base.ModelState.IsValid)
            {
                this.db.Entry<CountyLoanLimitVA>(countyLoanLimitVA).State = EntityState.Modified;
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            return base.View(countyLoanLimitVA);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountyLoanLimitVA model = this.db.CountyLoanLimitVAs.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        public ActionResult Index() => 
            base.View((from s in this.db.CountyLoanLimitVAs
                orderby s.State
                select s).Take<CountyLoanLimitVA>(50).ToList<CountyLoanLimitVA>());
    }
}

