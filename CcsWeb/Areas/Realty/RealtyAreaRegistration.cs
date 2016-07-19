namespace CcsWeb.Areas.Realty
{
    using System;
    using System.Web.Mvc;

    public class RealtyAreaRegistration : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Realty_default", "Realty/{controller}/{action}/{id}", new { 
                action = "Index",
                id = UrlParameter.Optional
            });
        }

        public override string AreaName =>
            "Realty";
    }
}

