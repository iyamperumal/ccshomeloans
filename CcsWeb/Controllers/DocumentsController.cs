namespace CcsWeb.Controllers
{
    using CcsData.Models.FileUpload;
    using CcsWeb.DataContexts;
    using CcsWeb.Models;
    using System;
    using System.IO;
    using System.Web.Mvc;

    public class DocumentsController : Controller
    {
        private CcsLocalDbContext db = new CcsLocalDbContext();

        public ActionResult Create() => 
            base.View();

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                return base.RedirectToAction("Index");
            }
            catch
            {
                return base.View();
            }
        }

        public ActionResult Delete(int id) => 
            base.View();

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return base.RedirectToAction("Index");
            }
            catch
            {
                return base.View();
            }
        }

        public ActionResult Details(int id) => 
            base.View();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Edit(int id) => 
            base.View();

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return base.RedirectToAction("Index");
            }
            catch
            {
                return base.View();
            }
        }

        public ActionResult Index() => 
            base.View();

        public ActionResult UploadDocs()
        {
            ((dynamic) base.ViewBag).HeaderText = "REQUIRED DOCUMENTS";
            return base.View();
        }

        [Authorize, HttpPost]
        public ActionResult UploadDocs(DocFileVM model)
        {
            if (model.BaseFile != null)
            {
                DocFile entity = new DocFile {
                    Applicant_ID = Util.GetApplicantId(this.db, base.User.Identity.Name),
                    DocFileType = model.DocFileType,
                    Note = model.Note
                };
                string fileName = Path.GetFileName(model.BaseFile.FileName);
                string str2 = Path.GetFileNameWithoutExtension(fileName) + entity.Applicant_ID.ToString() + Path.GetExtension(fileName);
                string filename = Path.Combine(base.Server.MapPath("~/Content/DataFiles"), str2);
                entity.FilePath = filename;
                entity.FileName = fileName;
                model.BaseFile.SaveAs(filename);
                this.db.DocFiles.Add(entity);
                this.db.SaveChanges();
                ((dynamic) base.ViewBag).uploadMesaage = "You Successfuly uploaded your file Would you like to upload anoother file";
            }
            return base.View();
        }
    }
}

