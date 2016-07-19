namespace CcsWeb.Controllers
{
    using CcsData.Models;
    using CcsWeb.DataContexts;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    public class LoanProgramsController : Controller
    {
        private CcsLocalDbContext db = new CcsLocalDbContext();

        public ActionResult Create() => 
            base.View();

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Address_Id,Programname,LoanName,Rate,Rate15,Rate30,Rate45,Rate60")] LoanProgram loanProgram)
        {
            if (base.ModelState.IsValid)
            {
                this.db.LoanPrograms.Add(loanProgram);
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            return base.View(loanProgram);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanProgram model = this.db.LoanPrograms.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        [ActionName("Delete"), ValidateAntiForgeryToken, HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            LoanProgram entity = this.db.LoanPrograms.Find(new object[] { id });
            this.db.LoanPrograms.Remove(entity);
            this.db.SaveChanges();
            return base.RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanProgram model = this.db.LoanPrograms.Find(new object[] { id });
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
        public ActionResult Edit([Bind(Include="Address_Id,Programname,LoanName,Rate,Rate15,Rate30,Rate45,Rate60")] LoanProgram loanProgram)
        {
            if (base.ModelState.IsValid)
            {
                this.db.Entry<LoanProgram>(loanProgram).State = EntityState.Modified;
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            return base.View(loanProgram);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanProgram model = this.db.LoanPrograms.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        public ActionResult Index()
        {
            List<LoanProgram> model = (from lp in this.db.LoanPrograms
                where (lp.ProgramName == "Government Rates and Adjustments") && (lp.LoanName == "FHA300")
                select lp).ToList<LoanProgram>();
            return base.View(model);
        }
    }
}

