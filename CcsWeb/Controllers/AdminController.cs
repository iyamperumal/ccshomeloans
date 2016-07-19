namespace CcsWeb.Controllers
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;

    public class AdminController : Controller
    {
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

        public ActionResult RateData() => 
            base.View();

        public ActionResult Save(HttpPostedFileBase files)
        {
            if (files != null)
            {
                string fileName = Path.GetFileName(files.FileName);
                string filename = Path.Combine(base.Server.MapPath("~/Content/DataFiles"), fileName);
                files.SaveAs(filename);
            }
            return base.Content("");
        }
    }
}

