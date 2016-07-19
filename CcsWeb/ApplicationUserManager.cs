namespace CcsWeb
{
    using DataContexts;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.DataProtection;
    using Models;
    using System;

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<CcsLocalDbContext>()));
            UserValidator<ApplicationUser> validator = new UserValidator<ApplicationUser>(manager) {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            manager.UserValidator = validator;
            PasswordValidator validator2 = new PasswordValidator {
                RequiredLength = 6
            };
            manager.PasswordValidator = validator2;
            PhoneNumberTokenProvider<ApplicationUser> provider2 = new PhoneNumberTokenProvider<ApplicationUser> {
                MessageFormat = "Your security code is: {0}"
            };
            manager.RegisterTwoFactorProvider("PhoneCode", provider2);
            EmailTokenProvider<ApplicationUser> provider3 = new EmailTokenProvider<ApplicationUser> {
                Subject = "Security Code",
                BodyFormat = "Your security code is: {0}"
            };
            manager.RegisterTwoFactorProvider("EmailCode", provider3);
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            IDataProtectionProvider dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create(new string[] { "ASP.NET Identity" }));
            }
            return manager;
        }
    }
}

