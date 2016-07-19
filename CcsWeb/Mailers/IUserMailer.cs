namespace CcsWeb.Mailers
{
    using CcsWeb.Models;
    using Mvc.Mailer;

    public interface IUserMailer
    {
        MvcMailMessage Contact(AppEmailModel model);
        MvcMailMessage PurchAppRecievedBor(AppEmailModel model);
        MvcMailMessage Realtor(AppEmailModel model);
        MvcMailMessage ThankYou();
        MvcMailMessage Welcome(AppEmailModel model);
    }
}

