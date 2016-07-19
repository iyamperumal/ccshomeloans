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

    [Authorize(Roles="Admin")]
    public class CountyLoanLimitConvsController : Controller
    {
        private CcsLocalDbContext db = new CcsLocalDbContext();

        public ActionResult Create() => 
            base.View();

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult Create([Bind(Include="CountyLoanLimitConv_Id,State,County,Fips,LoanLimit1Unit,LoanLimit2Unit,LoanLimit3Unit,LoanLimit4Unit")] CountyLoanLimitConv countyLoanLimitConv)
        {
            if (base.ModelState.IsValid)
            {
                this.db.CountyLoanLimitConvs.Add(countyLoanLimitConv);
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            return base.View(countyLoanLimitConv);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountyLoanLimitConv model = this.db.CountyLoanLimitConvs.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CountyLoanLimitConv entity = this.db.CountyLoanLimitConvs.Find(new object[] { id });
            this.db.CountyLoanLimitConvs.Remove(entity);
            this.db.SaveChanges();
            return base.RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountyLoanLimitConv model = this.db.CountyLoanLimitConvs.Find(new object[] { id });
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
        public ActionResult Edit([Bind(Include="CountyLoanLimitConv_Id,State,County,LoanLimit1Unit,LoanLimit2Unit,LoanLimit3Unit,LoanLimit4Unit")] CountyLoanLimitConv countyLoanLimitConv)
        {
            if (base.ModelState.IsValid)
            {
                this.db.Entry<CountyLoanLimitConv>(countyLoanLimitConv).State = EntityState.Modified;
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            return base.View(countyLoanLimitConv);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountyLoanLimitConv model = this.db.CountyLoanLimitConvs.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        public ActionResult Index() => 
            base.View((from s in this.db.CountyLoanLimitConvs
                orderby s.State
                select s).Take<CountyLoanLimitConv>(50).ToList<CountyLoanLimitConv>());

        public ActionResult GetCountyLoanLimitConvs([DataSourceRequest] DataSourceRequest request)
        {
            List<CountyLoanLimitConv> enumerable = this.db.CountyLoanLimitConvs.ToList<CountyLoanLimitConv>();
            return base.Json(enumerable.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, CountyLoanLimitConv countyLoanLimitConv)
        {
            if ((countyLoanLimitConv != null) && base.ModelState.IsValid)
            {
                this.db.Entry<CountyLoanLimitConv>(countyLoanLimitConv).State = EntityState.Modified;
                this.db.SaveChanges();
            }
            return base.Json(new CountyLoanLimitConv[] { countyLoanLimitConv }.ToDataSourceResult(request, base.ModelState));
        }
    }
}

