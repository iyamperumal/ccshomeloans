namespace CcsWeb.Controllers
{
    using CcsData.Models;
    using CcsWeb.DataContexts;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    public class RealtorsController : Controller
    {
        private CcsLocalDbContext db = new CcsLocalDbContext();

        public ActionResult Create()
        {
            ((dynamic) base.ViewBag).AddressID = new SelectList(this.db.Addresses, "Address_Id", "StreetAddress");
            return base.View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Realtor_Id,FirstName,LastName,Email,PhoneNumber,WorkNumber,CellNumber,FaxNumber,CompanyName,ImageFile,AddressID")] Realtor realtor)
        {
            if (base.ModelState.IsValid)
            {
                this.db.Realtor.Add(realtor);
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            ((dynamic) base.ViewBag).AddressID = new SelectList(this.db.Addresses, "Address_Id", "StreetAddress", realtor.AddressID);
            return base.View(realtor);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Realtor model = this.db.Realtor.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        [ActionName("Delete"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Realtor entity = this.db.Realtor.Find(new object[] { id });
            this.db.Realtor.Remove(entity);
            this.db.SaveChanges();
            return base.RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Realtor model = this.db.Realtor.Find(new object[] { id });
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
        public ActionResult Edit([Bind(Include="Realtor_Id,FirstName,LastName,Email,PhoneNumber,WorkNumber,CellNumber,FaxNumber,CompanyName,ImageFile,AddressID")] Realtor realtor)
        {
            if (base.ModelState.IsValid)
            {
                this.db.Entry<Realtor>(realtor).State = EntityState.Modified;
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            ((dynamic) base.ViewBag).AddressID = new SelectList(this.db.Addresses, "Address_Id", "StreetAddress", realtor.AddressID);
            return base.View(realtor);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Realtor model = this.db.Realtor.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            ((dynamic) base.ViewBag).AddressID = new SelectList(this.db.Addresses, "Address_Id", "StreetAddress", model.AddressID);
            return base.View(model);
        }

        public ActionResult Index()
        {
            IQueryable<Realtor> source = this.db.Realtor.Include<Realtor, Address>(r => r.Address);
            return base.View(source.ToList<Realtor>());
        }
    }
}

