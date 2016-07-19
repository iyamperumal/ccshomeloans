namespace CcsWeb.MailersBak
{
    using CcsWeb.Models;
    using Mvc.Mailer;

    public interface IUserMailer
    {
        MvcMailMessage Realtor(AppEmailModel model);
        MvcMailMessage ThankYou();
        MvcMailMessage Welcome(AppEmailModel model);
    }
}

