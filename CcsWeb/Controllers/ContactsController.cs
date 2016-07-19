namespace CcsWeb.Controllers
{
    using CcsData.Models;
    using CcsWeb.DataContexts;
    using CcsWeb.Mailers;
    using CcsWeb.Models;
    using Mvc.Mailer;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    public class ContactsController : Controller
    {
        private IUserMailer _userMailer = new CcsWeb.Mailers.UserMailer();
        private CcsLocalDbContext db = new CcsLocalDbContext();

        public ActionResult Create() => 
            base.View();

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult Create([Bind(Include="Contact_Id,FullName,EmailAddress,Subject,Message")] Contact contact)
        {
            if (base.ModelState.IsValid)
            {
                this.db.Contacts.Add(contact);
                this.db.SaveChanges();
                AppEmailModel contactEmail = new AppEmailModel {
                    FirstName = contact.FullName,
                    ToEmailAddess = contact.EmailAddress,
                    Message = contact.Message,
                    Subject = contact.Subject
                };
                this.SendContactEmail(contactEmail);
                return base.RedirectToAction("Index");
            }
            return base.View(contact);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact model = this.db.Contacts.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact entity = this.db.Contacts.Find(new object[] { id });
            this.db.Contacts.Remove(entity);
            this.db.SaveChanges();
            return base.RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact model = this.db.Contacts.Find(new object[] { id });
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
        public ActionResult Edit([Bind(Include="Contact_Id,FullName,EmailAddress,Subject,Message")] Contact contact)
        {
            if (base.ModelState.IsValid)
            {
                this.db.Entry<Contact>(contact).State = EntityState.Modified;
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            return base.View(contact);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact model = this.db.Contacts.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        public ActionResult Index() => 
            base.View(this.db.Contacts.ToList<Contact>());

        private void SendContactEmail(AppEmailModel contactEmail)
        {
            MvcMailMessage message = this.UserMailer.Contact(contactEmail);
            message.Subject = contactEmail.Subject;
            message.To.Add(contactEmail.ToEmailAddess);
            message.Send(null);
        }

        public IUserMailer UserMailer
        {
            get
            {
                return this._userMailer;
            }
            set
            {
                this._userMailer = value;
            }
        }
    }
}

