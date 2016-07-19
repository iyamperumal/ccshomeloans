namespace CcsWeb.Controllers
{
    using CcsData.Models;
    using CcsData.Models.FileUpload;
    using CcsWeb.DataContexts;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    public class DocFilesController : Controller
    {
        private CcsLocalDbContext db = new CcsLocalDbContext();

        public ActionResult Create()
        {
            ((dynamic) base.ViewBag).Applicant_ID = new SelectList(this.db.Applicants, "Applicant_Id", "FullName");
            return base.View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="DocFile_Id,DocFileType,FileName,FilePath,Note,Applicant_ID")] DocFile docFile)
        {
            if (base.ModelState.IsValid)
            {
                this.db.DocFiles.Add(docFile);
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            ((dynamic) base.ViewBag).Applicant_ID = new SelectList(this.db.Applicants, "Applicant_Id", "FullName", docFile.Applicant_ID);
            return base.View(docFile);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocFile model = this.db.DocFiles.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        [ActionName("Delete"), ValidateAntiForgeryToken, HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            DocFile entity = this.db.DocFiles.Find(new object[] { id });
            this.db.DocFiles.Remove(entity);
            this.db.SaveChanges();
            return base.RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocFile model = this.db.DocFiles.Find(new object[] { id });
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
        public ActionResult Edit([Bind(Include="DocFile_Id,DocFileType,FileName,FilePath,Note,Applicant_ID")] DocFile docFile)
        {
            if (base.ModelState.IsValid)
            {
                this.db.Entry<DocFile>(docFile).State = EntityState.Modified;
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            ((dynamic) base.ViewBag).Applicant_ID = new SelectList(this.db.Applicants, "Applicant_Id", "FullName", docFile.Applicant_ID);
            return base.View(docFile);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocFile model = this.db.DocFiles.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            ((dynamic) base.ViewBag).Applicant_ID = new SelectList(this.db.Applicants, "Applicant_Id", "FullName", model.Applicant_ID);
            return base.View(model);
        }

        public ActionResult GetDocFiles([DataSourceRequest] DataSourceRequest request) => 
            base.Json(this.db.DocFiles.ToList<DocFile>().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        public ActionResult Index()
        {
            IQueryable<DocFile> source = this.db.DocFiles.Include<DocFile, Applicant>(d => d.MortgageApplicant);
            return base.View(source.ToList<DocFile>());
        }

        public ActionResult Index2() => 
            base.View();

        public ActionResult SetDocFiles([DataSourceRequest] DataSourceRequest request, DocFile docFiles)
        {
            if ((docFiles != null) && base.ModelState.IsValid)
            {
                this.db.Entry<DocFile>(docFiles).State = EntityState.Modified;
                this.db.SaveChanges();
            }
            return base.Json(new DocFile[] { docFiles }.ToDataSourceResult(request, base.ModelState));
        }
    }
}

