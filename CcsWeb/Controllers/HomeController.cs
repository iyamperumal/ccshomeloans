namespace CcsWeb.Controllers
{
    using System;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult About()
        {
            ((dynamic) base.ViewBag).Message = "Your application description page.";
            return base.View();
        }

        public ActionResult About1()
        {
            ((dynamic) base.ViewBag).Message = "Your application description page.";
            return base.View();
        }

        public ActionResult About2()
        {
            ((dynamic) base.ViewBag).Message = "Your application description page.";
            return base.View();
        }

        public ActionResult About3()
        {
            ((dynamic) base.ViewBag).Message = "Your application description page.";
            return base.View();
        }

        public ActionResult About4()
        {
            ((dynamic) base.ViewBag).Message = "Your application description page.";
            return base.View();
        }

        public ActionResult About5()
        {
            ((dynamic) base.ViewBag).Message = "Your application description page.";
            return base.View();
        }

        public ActionResult About6()
        {
            ((dynamic) base.ViewBag).Message = "Your application description page.";
            return base.View();
        }

        public ActionResult About7()
        {
            ((dynamic) base.ViewBag).Message = "Your application description page.";
            return base.View();
        }

        public ActionResult AdjustableMortgage()
        {
            ((dynamic) base.ViewBag).loanProgramsClass = "active";
            return base.View();
        }

        public ActionResult Cashout()
        {
            ((dynamic) base.ViewBag).LoanTypesClass = "active";
            return base.View();
        }

        public ActionResult CheatSheet()
        {
            ((dynamic) base.ViewBag).LoanTypesClass = "active";
            return base.View();
        }

        public ActionResult Contact()
        {
            ((dynamic) base.ViewBag).Message = "Your contact page.";
            return base.View();
        }

        public ActionResult Conventional()
        {
            ((dynamic) base.ViewBag).loanProgramsClass = "active";
            return base.View();
        }

        public ActionResult DebtConsolidation()
        {
            ((dynamic) base.ViewBag).LoanTypesClass = "active";
            return base.View();
        }

        public ActionResult FHA()
        {
            ((dynamic) base.ViewBag).loanProgramsClass = "active";
            return base.View();
        }

        public ActionResult FixedMortgage()
        {
            ((dynamic) base.ViewBag).loanProgramsClass = "active";
            return base.View();
        }

        public ActionResult Index() => 
            base.View();

        public ActionResult JumboMortgage()
        {
            ((dynamic) base.ViewBag).loanProgramsClass = "active";
            return base.View();
        }

        public ActionResult LearnMore()
        {
            ((dynamic) base.ViewBag).Message = "Your application description page.";
            return base.View();
        }

        public ActionResult LoanPrograms()
        {
            ((dynamic) base.ViewBag).loanProgramsClass = "active";
            return base.View();
        }

        public ActionResult LoanTypes()
        {
            ((dynamic) base.ViewBag).LoanTypesClass = "active";
            return base.View();
        }

        public ActionResult NonQualifiedMortgage()
        {
            ((dynamic) base.ViewBag).loanProgramsClass = "active";
            return base.View();
        }

        public ActionResult Purchase()
        {
            ((dynamic) base.ViewBag).LoanTypesClass = "active";
            return base.View();
        }

        public ActionResult RateAndTerm()
        {
            ((dynamic) base.ViewBag).LoanTypesClass = "active";
            return base.View();
        }

        public void SetViewBag()
        {
            ((dynamic) base.ViewBag).AboutClass = "";
        }

        public ActionResult Testimonials()
        {
            ((dynamic) base.ViewBag).Message = "Your application description page.";
            return base.View();
        }

        public ActionResult USRDA()
        {
            ((dynamic) base.ViewBag).loanProgramsClass = "active";
            return base.View();
        }

        public ActionResult VA()
        {
            ((dynamic) base.ViewBag).loanProgramsClass = "active";
            return base.View();
        }
    }
}

