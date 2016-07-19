namespace CcsWeb.MailersBak
{
    using CcsWeb.Models;
    using Mvc.Mailer;
    using System;

    public class UserMailer : MailerBase, IUserMailer
    {
        public UserMailer()
        {
            this.MasterName = "_Layout";
        }

        public virtual MvcMailMessage Realtor(AppEmailModel model) => 
            this.Populate(delegate (MvcMailMessage x) {
                x.Subject = "ThankYou";
                x.ViewName = "ThankYou";
                x.To.Add("some-email@example.com");
            });

        public virtual MvcMailMessage ThankYou() => 
            this.Populate(delegate (MvcMailMessage x) {
                x.Subject = "ThankYou";
                x.ViewName = "ThankYou";
                x.To.Add("some-email@example.com");
            });

        public virtual MvcMailMessage Welcome(AppEmailModel model)
        {
            base.ViewData.Model = model;
            return this.Populate(delegate (MvcMailMessage x) {
                x.ViewName = "Welcome";
                x.To.Add("jjhammond2001@yahoo.com");
                x.To.Add("hamid.ouakil@live.com");
            });
        }
    }
}

