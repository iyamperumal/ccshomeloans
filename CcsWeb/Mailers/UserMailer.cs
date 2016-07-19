namespace CcsWeb.Mailers
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

        public virtual MvcMailMessage Contact(AppEmailModel model)
        {
            base.ViewData.Model = model;
            return this.Populate(delegate (MvcMailMessage x) {
                x.Subject = "Contact";
                x.ViewName = "Contact";
                x.Bcc.Add("jjhammond2001@yahoo.com");
                x.Bcc.Add("hamid.ouakil@live.com");
            });
        }

        public virtual MvcMailMessage PurchAppRecievedBor(AppEmailModel model)
        {
            base.ViewData.Model = model;
            return this.Populate(delegate (MvcMailMessage x) {
                x.ViewName = "PurchAppRecievedBor";
                x.Bcc.Add("jjhammond2001@yahoo.com");
                x.Bcc.Add("hamid.ouakil@live.com");
            });
        }

        public virtual MvcMailMessage Realtor(AppEmailModel model)
        {
            base.ViewData.Model = model;
            return this.Populate(delegate (MvcMailMessage x) {
                x.ViewName = "Realtor";
                x.Bcc.Add("jjhammond2001@yahoo.com");
                x.Bcc.Add("hamid.ouakil@live.com");
            });
        }

        public virtual MvcMailMessage ThankYou() => 
            this.Populate(delegate (MvcMailMessage x) {
                x.Subject = "ThankYou";
                x.ViewName = "ThankYou";
                x.Bcc.Add("some-email@example.com");
            });

        public virtual MvcMailMessage Welcome(AppEmailModel model)
        {
            base.ViewData.Model = model;
            return this.Populate(delegate (MvcMailMessage x) {
                x.ViewName = "Welcome";
                x.Bcc.Add("jjhammond2001@yahoo.com");
                x.Bcc.Add("hamid.ouakil@live.com");
            });
        }
    }
}

