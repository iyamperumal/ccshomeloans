namespace CcsWeb.Controllers
{
    using CcsData.Models;
    using CcsWeb.DataContexts;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    public class TestimonialsController : Controller
    {
        private CcsLocalDbContext db = new CcsLocalDbContext();

        public ActionResult Create()
        {
            ((dynamic) base.ViewBag).ApplicantID = new SelectList(this.db.Applicants, "Applicant_Id", "FullName");
            return base.View();
        }

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult Create([Bind(Include="Testimonial_Id,FirstName,LastName,MiddleName,ApplicationNumber,City,State,Comment,ApplicantID")] Testimonial testimonial)
        {
            if (base.ModelState.IsValid)
            {
                this.db.Testimonials.Add(testimonial);
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            ((dynamic) base.ViewBag).ApplicantID = new SelectList(this.db.Applicants, "Applicant_Id", "FullName", testimonial.ApplicantID);
            return base.View(testimonial);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonial model = this.db.Testimonials.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        [ValidateAntiForgeryToken, HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Testimonial entity = this.db.Testimonials.Find(new object[] { id });
            this.db.Testimonials.Remove(entity);
            this.db.SaveChanges();
            return base.RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonial model = this.db.Testimonials.Find(new object[] { id });
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
        public ActionResult Edit([Bind(Include="Testimonial_Id,FirstName,LastName,MiddleName,ApplicationNumber,City,State,Comment,ApplicantID")] Testimonial testimonial)
        {
            if (base.ModelState.IsValid)
            {
                this.db.Entry<Testimonial>(testimonial).State = EntityState.Modified;
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            ((dynamic) base.ViewBag).ApplicantID = new SelectList(this.db.Applicants, "Applicant_Id", "FullName", testimonial.ApplicantID);
            return base.View(testimonial);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonial model = this.db.Testimonials.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            ((dynamic) base.ViewBag).ApplicantID = new SelectList(this.db.Applicants, "Applicant_Id", "FullName", model.ApplicantID);
            return base.View(model);
        }

        public ActionResult Index()
        {
            IQueryable<Testimonial> source = this.db.Testimonials.Include<Testimonial, Applicant>(t => t.Applicant);
            return base.View(source.ToList<Testimonial>());
        }

        public ActionResult Index2()
        {
            DbSet<Testimonial> testimonials = this.db.Testimonials;
            return base.View(testimonials.ToList<Testimonial>());
        }
    }
}

