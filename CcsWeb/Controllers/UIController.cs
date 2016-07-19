namespace CcsWeb.Controllers
{
    using System;
    using System.Web.Mvc;

    public class UIController : Controller
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

        public ActionResult RealtorEmail() => 
            base.View();

        public ActionResult Upload() => 
            base.View();
    }
}

