namespace CcsWeb.Controllers
{
    using CcsWeb;
    using CcsWeb.Mailers;
    using CcsWeb.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.CSharp.RuntimeBinder;
    using Microsoft.Owin.Security;
    using Mvc.Mailer;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    [Authorize]
    public class AccountController : Controller
    {
        private IUserMailer _userMailer;
        private ApplicationUserManager _userManager;
        private const string XsrfKey = "XsrfId";

        public AccountController()
        {
            this._userMailer = new CcsWeb.Mailers.UserMailer();
        }

        public AccountController(ApplicationUserManager userManager)
        {
            this._userMailer = new CcsWeb.Mailers.UserMailer();
            this.UserManager = userManager;
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (string str in result.Errors)
            {
                base.ModelState.AddModelError("", str);
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            ActionResult result;
            if ((userId == null) || (code == null))
            {
                return this.View("Error");
            }
            IdentityResult asyncVariable0 = await this.UserManager.ConfirmEmailAsync(userId, code);
            if (asyncVariable0.Succeeded)
            {
                result = this.View("ConfirmEmail");
            }
            else
            {
                this.AddErrors(asyncVariable0);
                result = this.View();
            }
            return result;
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult asyncVariable0 = await this.UserManager.RemoveLoginAsync(this.User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (asyncVariable0.Succeeded)
            {
                ApplicationUser user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());
                await this.SignInAsync(user, false);
                message = (ManageMessageId)2;
            }
            else
            {
                message = (ManageMessageId)3;
            }
            return this.RedirectToAction("Manage", new { Message = message });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.UserManager != null))
            {
                this.UserManager.Dispose();
                this.UserManager = null;
            }
            base.Dispose(disposing);
        }

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl) =>
            new ChallengeResult(provider, base.Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            ExternalLoginInfo loginInfo = await this.AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return this.RedirectToAction("Login");
            }
            ApplicationUser user = await this.UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await this.SignInAsync(user, false);
                return this.RedirectToLocal(returnUrl);
            }
            ((dynamic)this.ViewBag).ReturnUrl = returnUrl;
            ((dynamic)this.ViewBag).LoginProvider = loginInfo.Login.LoginProvider;
            ExternalLoginConfirmationViewModel model = new ExternalLoginConfirmationViewModel
            {
                Email = loginInfo.Email
            };
            return this.View("ExternalLoginConfirmation", model);
        }

        [ValidateAntiForgeryToken, HttpPost, AllowAnonymous]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Manage");
            }
            if (this.ModelState.IsValid)
            {
                ExternalLoginInfo asyncVariable0 = await this.AuthenticationManager.GetExternalLoginInfoAsync();
                if (asyncVariable0 == null)
                {
                    return this.View("ExternalLoginFailure");
                }
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                IdentityResult result = await this.UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await this.UserManager.AddLoginAsync(user.Id, asyncVariable0.Login);
                    if (result.Succeeded)
                    {
                        await this.SignInAsync(user, false);
                        return this.RedirectToLocal(returnUrl);
                    }
                }
                this.AddErrors(result);
            }
            ((dynamic)this.ViewBag).ReturnUrl = returnUrl;
            return this.View(model);
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure() =>
            base.View();

        [AllowAnonymous]
        public ActionResult ForgotPassword() =>
            base.View();

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                ApplicationUser asyncVariable0 = await this.UserManager.FindByNameAsync(model.Email);
                if (asyncVariable0 != null)
                {
                    bool introduced10 = await this.UserManager.IsEmailConfirmedAsync(asyncVariable0.Id);
                    if (introduced10)
                    {
                        goto Label_0162;
                    }
                }
                this.ModelState.AddModelError("", "The user either does not exist or is not confirmed.");
                return this.View();
            }
        Label_0162:
            return this.View(model);
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation() =>
            base.View();

        private bool HasPassword()
        {
            ApplicationUser user = this.UserManager.FindById<ApplicationUser, string>(base.User.Identity.GetUserId());
            return ((user != null) && (user.PasswordHash != null));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider) =>
            new ChallengeResult(provider, base.Url.Action("LinkLoginCallback", "Account"), base.User.Identity.GetUserId());

        public async Task<ActionResult> LinkLoginCallback()
        {
            ActionResult result;
            ExternalLoginInfo loginInfo = await this.AuthenticationManager.GetExternalLoginInfoAsync("XsrfId", this.User.Identity.GetUserId());
            if (loginInfo == null)
            {
                result = this.RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            else
            {
                IdentityResult asyncVariable0 = await this.UserManager.AddLoginAsync(this.User.Identity.GetUserId(), loginInfo.Login);
                if (asyncVariable0.Succeeded)
                {
                    result = this.RedirectToAction("Manage");
                }
                else
                {
                    result = this.RedirectToAction("Manage", new { Message = ManageMessageId.Error });
                }
            }
            return result;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ((dynamic)base.ViewBag).ReturnUrl = returnUrl;
            return base.View();
        }

        [ValidateAntiForgeryToken, HttpPost, AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (this.ModelState.IsValid)
            {
                ApplicationUser user = await this.UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    ActionResult result;
                    await this.SignInAsync(user, model.RememberMe);
                    if (this.UserManager.IsInRole<ApplicationUser, string>(user.Id, "Lead"))
                    {
                        result = this.RedirectToAction("refi1now", "Mortgages1");
                    }
                    else
                    {
                        result = this.RedirectToLocal(returnUrl);
                    }
                    return result;
                }
                this.ModelState.AddModelError("", "Invalid username or password.");
            }
            return this.View("Login", model);
        }

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult LogOff()
        {
            this.AuthenticationManager.SignOut(new string[0]);
            return base.RedirectToAction("Index", "Home");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = this.HasPassword();
            ((dynamic)this.ViewBag).HasLocalPassword = hasPassword;
            ((dynamic)this.ViewBag).ReturnUrl = this.Url.Action("Manage");
            if (hasPassword)
            {
                if (this.ModelState.IsValid)
                {
                    IdentityResult result = await this.UserManager.ChangePasswordAsync(this.User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        ApplicationUser user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());
                        await this.SignInAsync(user, false);
                        return this.RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    this.AddErrors(result);
                }
            }
            else
            {
                ModelState state = this.ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }
                if (this.ModelState.IsValid)
                {
                    IdentityResult asyncVariable2 = await this.UserManager.AddPasswordAsync(this.User.Identity.GetUserId(), model.NewPassword);
                    if (asyncVariable2.Succeeded)
                    {
                        return this.RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    this.AddErrors(asyncVariable2);
                }
            }
            return this.View(model);
        }

        public ActionResult Manage(ManageMessageId? message)
        {
            ManageMessageId? nullable4 = null;
            if (message == ManageMessageId.ChangePasswordSuccess || 
                message == ManageMessageId.SetPasswordSuccess || 
                message == ManageMessageId.RemoveLoginSuccess)
            {
                nullable4 = message;
            }
            ((dynamic)base.ViewBag).StatusMessage = ((((ManageMessageId)nullable4.GetValueOrDefault()) != ManageMessageId.Error) && nullable4.HasValue) ? "Your password has been changed." : "";
            ((dynamic)base.ViewBag).HasLocalPassword = this.HasPassword();
            ((dynamic)base.ViewBag).ReturnUrl = base.Url.Action("Manage");
            return base.View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (base.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            return base.RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register() =>
            base.View();

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.EmailReg,
                    Email = model.EmailReg,
                    FirstName = model.FirstNameReg,
                    LastName = model.LastNameReg
                };
                IdentityResult result = await this.UserManager.CreateAsync(user, model.PasswordReg);
                if (result.Succeeded)
                {
                    await this.SignInAsync(user, false);
                    AppEmailModel Emailmodel = new AppEmailModel
                    {
                        ToEmailAddess = model.EmailReg,
                        FirstName = model.FirstNameReg,
                        LastName = model.LastNameReg
                    };
                    MvcMailMessage welcomeMessage = this.UserMailer.Welcome(Emailmodel);
                    welcomeMessage.Subject = Emailmodel.FirstName + " " + Emailmodel.LastName + " Welcome to CCSHomeloans.com";
                    welcomeMessage.To.Add(Emailmodel.ToEmailAddess);
                    return this.RedirectToAction("Index", "Home");
                }
                this.AddErrors(result);
            }
            return this.View(model);
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            IList<UserLoginInfo> logins = this.UserManager.GetLogins<ApplicationUser, string>(base.User.Identity.GetUserId());
            ((dynamic)base.ViewBag).ShowRemoveButton = this.HasPassword() || (logins.Count > 1);
            return this.PartialView("_RemoveAccountPartial", logins);
        }

        [ValidateAntiForgeryToken, HttpPost, AllowAnonymous]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                ActionResult result;
                ApplicationUser asyncVariable0 = await this.UserManager.FindByNameAsync(model.Email);
                if (asyncVariable0 == null)
                {
                    this.ModelState.AddModelError("", "No user found.");
                    result = this.View();
                }
                else
                {
                    IdentityResult asyncVariable1 = await this.UserManager.ResetPasswordAsync(asyncVariable0.Id, model.Code, model.Password);
                    if (asyncVariable1.Succeeded)
                    {
                        result = this.RedirectToAction("ResetPasswordConfirmation", "Account");
                    }
                    else
                    {
                        this.AddErrors(asyncVariable1);
                        result = this.View();
                    }
                }
                return result;
            }
            return this.View(model);
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            if (code == null)
            {
                return base.View("Error");
            }
            return base.View();
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation() =>
            base.View();

        private void SendEmail(string email, string callbackUrl, string subject, string message)
        {
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            this.AuthenticationManager.SignOut(new string[] { "ExternalCookie" });
            AuthenticationProperties asyncVariable0 = new AuthenticationProperties
            {
                IsPersistent = isPersistent
            };
            ClaimsIdentity[] identities = new ClaimsIdentity[1];
            ClaimsIdentity result = await user.GenerateUserIdentityAsync(this.UserManager);

            var tuple2 = new Tuple<IAuthenticationManager, AuthenticationProperties, ClaimsIdentity[], int, ClaimsIdentity[]>(
                this.AuthenticationManager, asyncVariable0, identities, 0, null);
            tuple2.Item3[tuple2.Item4] = result;
            tuple2.Item1.SignIn(tuple2.Item2, identities);
        }

        private IAuthenticationManager AuthenticationManager =>
            base.HttpContext.GetOwinContext().Authentication;

        public IUserMailer UserMailer
        {
            get
            {
                return this._userMailer;
            }
            set
            {
                this._userMailer = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return
                 (this._userManager ?? base.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>());
            }
            private set
            {
                this._userManager = value;
            }
        }












        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                this.LoginProvider = provider;
                this.RedirectUri = redirectUri;
                this.UserId = userId;
            }

            public override void ExecuteResult(ControllerContext context)
            {
                AuthenticationProperties properties = new AuthenticationProperties
                {
                    RedirectUri = this.RedirectUri
                };
                if (this.UserId != null)
                {
                    properties.Dictionary["XsrfId"] = this.UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, new string[] { this.LoginProvider });
            }

            public string LoginProvider { get; set; }

            public string RedirectUri { get; set; }

            public string UserId { get; set; }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }
    }
}

