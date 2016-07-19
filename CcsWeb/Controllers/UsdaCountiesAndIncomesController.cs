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
    public class UsdaCountiesAndIncomesController : Controller
    {
        private CcsLocalDbContext db = new CcsLocalDbContext();

        public ActionResult Create() => 
            base.View();

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult Create([Bind(Include="UsdaCountiesAndIncome_Id,State,County,Fips,IncomeLimit1")] UsdaCountiesAndIncome usdaCountiesAndIncome)
        {
            if (base.ModelState.IsValid)
            {
                this.db.UsdaCountiesAndIncomes.Add(usdaCountiesAndIncome);
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            return base.View(usdaCountiesAndIncome);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsdaCountiesAndIncome model = this.db.UsdaCountiesAndIncomes.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        [ActionName("Delete"), ValidateAntiForgeryToken, HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            UsdaCountiesAndIncome entity = this.db.UsdaCountiesAndIncomes.Find(new object[] { id });
            this.db.UsdaCountiesAndIncomes.Remove(entity);
            this.db.SaveChanges();
            return base.RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsdaCountiesAndIncome model = this.db.UsdaCountiesAndIncomes.Find(new object[] { id });
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
        public ActionResult Edit([Bind(Include="UsdaCountiesAndIncome_Id,State,County,Fips,IncomeLimit1")] UsdaCountiesAndIncome usdaCountiesAndIncome)
        {
            if (base.ModelState.IsValid)
            {
                this.db.Entry<UsdaCountiesAndIncome>(usdaCountiesAndIncome).State = EntityState.Modified;
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            return base.View(usdaCountiesAndIncome);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsdaCountiesAndIncome model = this.db.UsdaCountiesAndIncomes.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        public ActionResult Index() => 
            base.View((from s in this.db.UsdaCountiesAndIncomes
                orderby s.State
                select s).Take<UsdaCountiesAndIncome>(50).ToList<UsdaCountiesAndIncome>());
    }
}

