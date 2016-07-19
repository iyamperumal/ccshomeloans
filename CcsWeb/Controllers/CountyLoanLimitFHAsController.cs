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
    public class CountyLoanLimitFHAsController : Controller
    {
        private CcsLocalDbContext db = new CcsLocalDbContext();

        public ActionResult Create() => 
            base.View();

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult Create([Bind(Include="CountyLoanLimitFHA_Id,State,County,Fips,LoanLimit1Unit,LoanLimit2Unit,LoanLimit3Unit,LoanLimit4Unit")] CountyLoanLimitFHA countyLoanLimitFHA)
        {
            if (base.ModelState.IsValid)
            {
                this.db.CountyLoanLimitFHAs.Add(countyLoanLimitFHA);
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            return base.View(countyLoanLimitFHA);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountyLoanLimitFHA model = this.db.CountyLoanLimitFHAs.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        [ActionName("Delete"), ValidateAntiForgeryToken, HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            CountyLoanLimitFHA entity = this.db.CountyLoanLimitFHAs.Find(new object[] { id });
            this.db.CountyLoanLimitFHAs.Remove(entity);
            this.db.SaveChanges();
            return base.RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountyLoanLimitFHA model = this.db.CountyLoanLimitFHAs.Find(new object[] { id });
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
        public ActionResult Edit([Bind(Include="CountyLoanLimitFHA_Id,State,County,LoanLimit1Unit,LoanLimit2Unit,LoanLimit3Unit,LoanLimit4Unit")] CountyLoanLimitFHA countyLoanLimitFHA)
        {
            if (base.ModelState.IsValid)
            {
                this.db.Entry<CountyLoanLimitFHA>(countyLoanLimitFHA).State = EntityState.Modified;
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            return base.View(countyLoanLimitFHA);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountyLoanLimitFHA model = this.db.CountyLoanLimitFHAs.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        public ActionResult Index() => 
            base.View((from s in this.db.CountyLoanLimitFHAs
                orderby s.State
                select s).Take<CountyLoanLimitFHA>(50).ToList<CountyLoanLimitFHA>());
    }
}

