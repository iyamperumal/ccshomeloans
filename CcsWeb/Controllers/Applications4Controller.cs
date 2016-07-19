namespace CcsWeb.Controllers
{
    using AutoMapper;
    using CcsData.Models;
    using CcsData.ViewModels;
    using CcsWeb;
    using CcsWeb.DataContexts;
    using CcsWeb.Mailers;
    using CcsWeb.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Mvc.Mailer;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    public class Applications4Controller : Controller
    {
        private IUserMailer _userMailer;
        private ApplicationUserManager _userManager;
        private CcsLocalDbContext db;

        public Applications4Controller()
        {
            this.db = new CcsLocalDbContext();
            this._userMailer = new CcsWeb.Mailers.UserMailer();
        }

        public Applications4Controller(ApplicationUserManager userManager)
        {
            this.db = new CcsLocalDbContext();
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

        public ActionResult App1()
        {
            CcsData.ViewModels.App1 model = this.GetApp1();
            if (model == null)
            {
                ((dynamic) base.ViewBag).RealtorID = new SelectList(this.db.Realtor, "Realtor_Id", "FirstName");
                return base.View();
            }
            ((dynamic) base.ViewBag).RealtorID = new SelectList(this.db.Realtor, "Realtor_Id", "FirstName", model.RealtorID);
            return base.View("App1", model);
        }

        [HttpPost]
        public ActionResult App1(CcsData.ViewModels.App1 App1Model, string BtnPrevious, string BtnNext)
        {
            CcsData.ViewModels.App2 app = this.GetApp2();
            if (base.ModelState.IsValid)
            {
                base.Session["App1"] = App1Model;
                if (!base.Request.IsAjaxRequest())
                {
                    return base.View("App2", this.GetApp2());
                }
                if (app == null)
                {
                    return base.PartialView("_App2");
                }
                return this.PartialView("_App2", this.GetApp2());
            }
            ((dynamic) base.ViewBag).RealtorID = new SelectList(this.db.Realtor, "Realtor_Id", "FirstName", App1Model.RealtorID);
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView("_App1", App1Model);
            }
            return base.View(App1Model);
        }

        public ActionResult App2()
        {
            CcsData.ViewModels.App2 model = this.GetApp2();
            if (model == null)
            {
                return base.View();
            }
            return base.View("_App2", model);
        }

        [HttpPost]
        public ActionResult App2(CcsData.ViewModels.App2 App2Model, string BtnPrevious, string BtnNext)
        {
            base.Session["App2"] = App2Model;
            CcsData.ViewModels.App1 model = this.GetApp1();
            CcsData.ViewModels.App3 app2 = this.GetApp3();
            CcsData.ViewModels.App5 app3 = this.GetApp5();
            if (BtnPrevious != null)
            {
                if (model != null)
                {
                    ((dynamic) base.ViewBag).RealtorID = new SelectList(this.db.Realtor, "Realtor_Id", "FirstName", model.RealtorID);
                }
                else
                {
                    ((dynamic) base.ViewBag).RealtorID = new SelectList(this.db.Realtor, "Realtor_Id", "FirstName");
                }
                if (base.Request.IsAjaxRequest())
                {
                    if (model == null)
                    {
                        return base.PartialView("_App1");
                    }
                    return this.PartialView("_App1", model);
                }
                if (model == null)
                {
                    return base.View("App1");
                }
                return base.View("App1", model);
            }
            if (base.ModelState.IsValid)
            {
                if (base.Request.IsAjaxRequest())
                {
                    if ((model.LoanTypeRequested == LoanTypeRequestedEnum.PurchaseLoan) || (model.LoanTypeRequested == LoanTypeRequestedEnum.RealtorReferredPurchaseLoan))
                    {
                        if (app3 == null)
                        {
                            return base.PartialView("_App5");
                        }
                        return this.PartialView("_App5", app3);
                    }
                    if (app2 == null)
                    {
                        return base.PartialView("_App3");
                    }
                    return this.PartialView("_App3", this.GetApp3());
                }
                if ((model.LoanTypeRequested == LoanTypeRequestedEnum.PurchaseLoan) || (model.LoanTypeRequested == LoanTypeRequestedEnum.RealtorReferredPurchaseLoan))
                {
                    if (app3 == null)
                    {
                        return base.View("APP5");
                    }
                    return base.View("_App5", app3);
                }
                if (app2 == null)
                {
                    return base.View("App3");
                }
                return base.View("App3", app2);
            }
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView("_App2", App2Model);
            }
            return base.View(App2Model);
        }

        public ActionResult App3()
        {
            CcsData.ViewModels.App3 model = this.GetApp3();
            if (model == null)
            {
                return base.View();
            }
            return base.View("_App3", model);
        }

        [HttpPost]
        public ActionResult App3(CcsData.ViewModels.App3 App3Model, string BtnPrevious, string BtnNext)
        {
            base.Session["App3"] = App3Model;
            CcsData.ViewModels.App2 model = this.GetApp2();
            CcsData.ViewModels.App4 app2 = this.GetApp4();
            if (BtnPrevious != null)
            {
                if (base.Request.IsAjaxRequest())
                {
                    if (model == null)
                    {
                        return base.PartialView("_App2");
                    }
                    return this.PartialView("_App2", model);
                }
                if (model == null)
                {
                    return base.View("App2");
                }
                return base.View("App2", model);
            }
            if (base.ModelState.IsValid)
            {
                if (base.Request.IsAjaxRequest())
                {
                    if (app2 == null)
                    {
                        return base.PartialView("_App4");
                    }
                    return this.PartialView("_App4", app2);
                }
                if (app2 == null)
                {
                    return base.View("App4");
                }
                return base.View("App4", app2);
            }
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView("_App3", App3Model);
            }
            return base.View(App3Model);
        }

        public ActionResult App4()
        {
            CcsData.ViewModels.App4 model = this.GetApp4();
            if (model == null)
            {
                return base.View();
            }
            return base.View("_App4", model);
        }

        [HttpPost]
        public ActionResult App4(CcsData.ViewModels.App4 App4Model, string BtnPrevious, string BtnNext)
        {
            base.Session["App4"] = App4Model;
            CcsData.ViewModels.App5 app = this.GetApp5();
            if (BtnPrevious != null)
            {
                CcsData.ViewModels.App3 model = this.GetApp3();
                if (base.Request.IsAjaxRequest())
                {
                    if (model == null)
                    {
                        return base.PartialView("_App3");
                    }
                    return this.PartialView("_App3", model);
                }
                if (model == null)
                {
                    return base.View("App3");
                }
                return base.View("App3", this.GetApp3());
            }
            if (base.ModelState.IsValid)
            {
                if (base.Request.IsAjaxRequest())
                {
                    if (app == null)
                    {
                        return base.PartialView("_App5");
                    }
                    return this.PartialView("_App5", this.GetApp5());
                }
                if (app == null)
                {
                    return base.View("App5");
                }
                return base.View("App5", this.GetApp5());
            }
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView("_App4", App4Model);
            }
            return base.View(App4Model);
        }

        public ActionResult App5()
        {
            CcsData.ViewModels.App5 model = this.GetApp5();
            if (model == null)
            {
                return base.View();
            }
            return base.View("_App5", model);
        }

        [HttpPost]
        public async Task<ActionResult> App5(CcsData.ViewModels.App5 App5Model, string BtnPrevious, string BtnNext)
        {
            ActionResult result2;
            this.Session["App5"] = App5Model;
            CcsData.ViewModels.App1 source = this.GetApp1();
            CcsData.ViewModels.App4 app4 = this.GetApp4();
            CcsData.ViewModels.App2 app2 = this.GetApp2();
            if (BtnPrevious != null)
            {
                if (this.Request.IsAjaxRequest())
                {
                    if ((source.LoanTypeRequested == LoanTypeRequestedEnum.PurchaseLoan) || (source.LoanTypeRequested == LoanTypeRequestedEnum.RealtorReferredPurchaseLoan))
                    {
                        if (app2 == null)
                        {
                            result2 = this.PartialView("_App2");
                        }
                        else
                        {
                            result2 = this.PartialView("_App2", this.GetApp2());
                        }
                    }
                    else if (app4 == null)
                    {
                        result2 = this.PartialView("_App4");
                    }
                    else
                    {
                        result2 = this.PartialView("_App4", app4);
                    }
                }
                else if ((source.LoanTypeRequested == LoanTypeRequestedEnum.PurchaseLoan) || (source.LoanTypeRequested == LoanTypeRequestedEnum.RealtorReferredPurchaseLoan))
                {
                    if (app2 == null)
                    {
                        result2 = this.View("_App2");
                    }
                    else
                    {
                        result2 = this.View("App2", this.GetApp2());
                    }
                }
                else if (app4 == null)
                {
                    result2 = this.View("App4");
                }
                else
                {
                    result2 = this.View("App4", this.GetApp4());
                }
                return result2;
            }
            ApplicationUser myUser = new ApplicationUser();
            if (this.Request.IsAuthenticated)
            {
                myUser = Util.GetUser(this.db, this.User.Identity.GetUserName());
                App5Model.FirstName = myUser.FirstName;
                App5Model.LastName = myUser.LastName;
                App5Model.EmailAddress = myUser.Email;
                this.ModelState.Remove("FirstName");
                this.ModelState.Remove("LastName");
                this.ModelState.Remove("EmailAddress");
                this.ModelState.Remove("Passwordr");
            }
            if (this.ModelState.IsValid)
            {
                app2 = this.GetApp2();
                CcsData.ViewModels.App3 app3 = this.GetApp3();
                CcsData.ViewModels.App4 app44 = this.GetApp4();
                CcsData.ViewModels.App5 app5 = App5Model;
                Application destination = Mapper.Map<Application>(source);
                Mapper.Map<CcsData.ViewModels.App2, Application>(app2, destination);
                Mapper.Map<CcsData.ViewModels.App3, Application>(app3, destination);
                Mapper.Map<CcsData.ViewModels.App4, Application>(app44, destination);
                Mapper.Map<CcsData.ViewModels.App5, Application>(app5, destination);
                destination.ApplicationDate = DateTime.Now;
                destination.setFips();
                if (!destination.EstimateTotalDebtToPayOff.HasValue)
                {
                    destination.EstimateTotalDebtToPayOff = 0;
                }
                if (!destination.TotalOfMonthlyPaymentsOnDebtToPayOff.HasValue)
                {
                    destination.TotalOfMonthlyPaymentsOnDebtToPayOff = 0;
                }
                this.db.Applications.Add(destination);
                this.Session["App"] = destination;
                RegisterViewModel registerModel = new RegisterViewModel();
                if (!this.Request.IsAuthenticated)
                {
                    registerModel.FirstNameReg = destination.FirstName;
                    registerModel.LastNameReg = destination.LastName;
                    registerModel.PasswordReg = app5.Passwordr;
                    registerModel.EmailReg = destination.EmailAddress;
                    registerModel.ConfirmPasswordReg = app5.ConfirmPasswordr;
                    bool RegisterSuccess = false;
                    try
                    {
                        RegisterSuccess = await this.RegisterUser(registerModel);
                    }
                    catch (Exception exception)
                    {
                        string message = exception.Message;
                    }
                    if (RegisterSuccess)
                    {
                        myUser = (from u in this.db.Users
                            where u.UserName == registerModel.EmailReg
                            select u).FirstOrDefault<ApplicationUser>();
                    }
                    else
                    {
                        this.ModelState.AddModelError("EmailAddress", "Email provided already in use, provide valid Email/password  ");
                        if (this.Request.IsAjaxRequest())
                        {
                            result2 = this.PartialView("_App5", App5Model);
                        }
                        else
                        {
                            result2 = this.View("App5", App5Model);
                        }
                        return result2;
                    }
                }
                myUser.Application = destination;
                Applicant applicant = (from a in this.db.Applicants
                    where a.Applicant_Id == myUser.Applicant_id
                    select a).Include<Applicant, List<Property>>(ap => ap.Properties).Include<Applicant, List<Mortgage>>(ap => ap.Mortgages).Include<Applicant, List<Address>>(ap => ap.Addresses).FirstOrDefault<Applicant>();
                bool isnewApplicant = applicant == null;
                applicant = this.MapAppToApplicant(applicant, destination);
                if (isnewApplicant)
                {
                    this.db.Applicants.Add(applicant);
                    myUser.Applicant = applicant;
                }
                bool isgood = false;
                try
                {
                    this.db.SaveChanges();
                    isgood = true;
                }
                catch (Exception exception2)
                {
                    string text2 = exception2.Message;
                }
                if (!isgood)
                {
                    try
                    {
                        this.db.SaveChanges();
                    }
                    catch (DbEntityValidationException exception3)
                    {
                        using (IEnumerator<DbEntityValidationResult> enumerator = exception3.EntityValidationErrors.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                DbEntityValidationResult current = enumerator.Current;
                                Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", current.Entry.Entity.GetType().Name, current.Entry.State);
                                using (IEnumerator<DbValidationError> enumerator2 = current.ValidationErrors.GetEnumerator())
                                {
                                    while (enumerator2.MoveNext())
                                    {
                                        DbValidationError error = enumerator2.Current;
                                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"", error.PropertyName, error.ErrorMessage);
                                        string propertyName = error.PropertyName;
                                        string errorMessage = error.ErrorMessage;
                                    }
                                    continue;
                                }
                            }
                        }
                        throw;
                    }
                }
                if (((destination.LoanTypeRequested == LoanTypeRequestedEnum.CashOutMortgage) || (destination.LoanTypeRequested == LoanTypeRequestedEnum.DebtConsolidationPayOffCreditors)) || ((destination.LoanTypeRequested == LoanTypeRequestedEnum.RateAndTermRefiLowerPayment) || (destination.LoanTypeRequested == LoanTypeRequestedEnum.RateAndTermRefiShorterTerm)))
                {
                    ThankyouRefi model = new ThankyouRefi {
                        LoanType = destination.LoanTypeRequested.ToString()
                    };
                    result2 = this.PartialView("_ThankyouRefi", model);
                }
                else if ((destination.LoanTypeRequested == LoanTypeRequestedEnum.PurchaseLoan) || (destination.LoanTypeRequested == LoanTypeRequestedEnum.RealtorReferredPurchaseLoan))
                {
                    ThankyouRefi refi2 = new ThankyouRefi {
                        LoanType = destination.LoanTypeRequested.ToString()
                    };
                    result2 = this.PartialView("_ThankyouPur", refi2);
                }
                else
                {
                    AppEmailModel appEmail = new AppEmailModel {
                        FirstName = destination.FirstName
                    };
                    appEmail.LastName = appEmail.LastName;
                    appEmail.Subject = "Mortgage Application recieved";
                    appEmail.ToEmailAddess = destination.EmailAddress;
                    appEmail.IsPurchase = false;
                    if ((destination.LoanTypeRequested == LoanTypeRequestedEnum.PurchaseLoan) || (destination.LoanTypeRequested == LoanTypeRequestedEnum.RealtorReferredPurchaseLoan))
                    {
                        appEmail.IsPurchase = true;
                    }
                    if (destination.RealtorID.HasValue)
                    {
                        Realtor realtor = this.db.Realtor.Find(new object[] { destination.RealtorID });
                        appEmail.RealtorEmail = realtor.Email;
                        appEmail.RealtorFName = realtor.FirstName;
                        appEmail.RealtorLName = realtor.LastName;
                    }
                    string loantype = destination.LoanTypeRequested.ToString();
                    ThankyouApply thApp = new ThankyouApply {
                        LoanType = loantype
                    };
                    result2 = this.PartialView("_Thankyou", thApp);
                }
                return result2;
            }
            if (this.Request.IsAjaxRequest())
            {
                result2 = this.PartialView("_App5", App5Model);
            }
            else
            {
                result2 = this.View(App5Model);
            }
            return result2;
        }

        public ActionResult Create()
        {
            ((dynamic) base.ViewBag).RealtorID = new SelectList(this.db.Realtor, "Realtor_Id", "FirstName");
            return base.View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Application_Id,LoanTypeRequested,CashOutRequested,RealtorID,OwnerShipType,PropertyType,EstimateTotalDebtToPayOff,TotalOfMonthlyPaymentsOnDebtToPayOff,AdditionalCashOutRequested,PurchasePrice,DownPaymentAmount,EstimatedHomeownersAssociationFeesAnnual,OwnerShipLongevity,CreditScoreEstimate,DaysLate,FiledBankruptcyType,BankruptcyDischargeDate,BankruptcyDischargeMonth,BankruptcyDischargeYear,Chapter13FilingDate,Chapter13FilingMonth,Chapter13FilingYear,ForeclosuresShortSaleDeedinLieu,ForeclosureShortSaleDeedinLieuDate,ForeclosureShortSaleDeedinLieuMonth,ForeclosureShortSaleDeedinLieuYear,GrossAnnualIncome,TotalMontlyPayments,RuralProperty,Veteran,EstimatedHomeValue,FirstMortgageBalance,CurrentInterestRate,InterestRateType,LoanType,MortgageTerm,Have2ndMortgage,PayOff2ndMortgage,SecondMortgageBalance,SecondMortgageInterestRate,SecondMortgageRateType,SecondMortgageTerm,SecondMortgagePayment,SecondMortgageOriginationDate,SecondMortgageOriginationMonth,SecondMortgageOriginationYear,FirstMortgagePayment,PymtIncludesMI,PymtIncludesPropTaxes,PymtIncludesMone,HomeownersInsurance,HoaDuesFees,AnnualPropertyTaxes,AnnualHomeownersInsur,AnnualHomeownersAssocDues,FirstName,LastName,EmailAddress,Phone,Address,City,State,ZipCode")] Application application)
        {
            if (base.ModelState.IsValid)
            {
                application.ApplicationDate = DateTime.Now;
                this.db.Applications.Add(application);
                this.db.SaveChanges();
                AppEmailModel model = new AppEmailModel {
                    FirstName = application.FirstName
                };
                model.LastName = model.LastName;
                model.Subject = "Mortgage Application recieved";
                model.ToEmailAddess = application.EmailAddress;
                model.IsPurchase = false;
                if ((application.LoanTypeRequested == LoanTypeRequestedEnum.PurchaseLoan) || (application.LoanTypeRequested == LoanTypeRequestedEnum.RealtorReferredPurchaseLoan))
                {
                    model.IsPurchase = true;
                }
                if (application.RealtorID.HasValue)
                {
                    Realtor realtor = this.db.Realtor.Find(new object[] { application.RealtorID });
                    model.RealtorEmail = realtor.Email;
                    model.RealtorFName = realtor.FirstName;
                    model.RealtorLName = realtor.LastName;
                }
                this.Respond(model);
                string str = application.LoanTypeRequested.ToString();
                ThankyouApply apply = new ThankyouApply {
                    LoanType = str
                };
                return base.View("Thankyou1", apply);
            }
            ((dynamic) base.ViewBag).RealtorID = new SelectList(this.db.Realtor, "Realtor_Id", "FirstName", application.RealtorID);
            return base.View(application);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application model = this.db.Applications.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        [ActionName("Delete"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application entity = this.db.Applications.Find(new object[] { id });
            this.db.Applications.Remove(entity);
            this.db.SaveChanges();
            return base.RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application model = this.db.Applications.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Application_Id,LoanTypeRequested,CashOutRequested,RealtorID,OwnerShipType,PropertyType,EstimateTotalDebtToPayOff,TotalOfMonthlyPaymentsOnDebtToPayOff,PurchasePrice,DownPaymentAmount,EstimatedHomeownersAssociationFeesAnnual,OwnerShipLongevity,CreditScoreEstimate,DaysLate,FiledBankruptcyType,BankruptcyDischargeDate,Chapter13FilingDate,ForeclosuresShortSaleDeedinLieu,ForeclosureShortSaleDeedinLieuDate,GrossAnnualIncome,TotalMontlyPayments,RuralProperty,Veteran,EstimatedHomeValue,FirstMortgageBalance,CurrentInterestRate,InterestRateType,LoanType,MortgageTerm,Have2ndMortgage,PayOff2ndMortgage,SecondMortgageBalance,SecondMortgageInterestRate,SecondMortgageRateType,SecondMortgageTerm,SecondMortgagePayment,SecondMortgageOriginationDate,FirstMortgagePayment,PymtIncludesMI,PymtIncludesPropTaxes,PymtIncludesMone,HomeownersInsurance,HoaDuesFees,AnnualPropertyTaxes,AnnualHomeownersInsur,AnnualHomeownersAssocDues,FirstName,LastName,EmailAddress,Phone,Address,City,State,ZipCode")] Application application)
        {
            if (base.ModelState.IsValid)
            {
                this.db.Entry<Application>(application).State = EntityState.Modified;
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            ((dynamic) base.ViewBag).RealtorID = new SelectList(this.db.Realtor, "Realtor_Id", "FirstName", application.RealtorID);
            return base.View(application);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application model = this.db.Applications.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            ((dynamic) base.ViewBag).RealtorID = new SelectList(this.db.Realtor, "Realtor_Id", "FirstName", model.RealtorID);
            return base.View(model);
        }

        public CcsData.ViewModels.App1 GetApp1()
        {
            if (base.Session["App1"] == null)
            {
                return null;
            }
            return (CcsData.ViewModels.App1) base.Session["App1"];
        }

        public CcsData.ViewModels.App2 GetApp2()
        {
            if (base.Session["App2"] == null)
            {
                return null;
            }
            return (CcsData.ViewModels.App2) base.Session["App2"];
        }

        public CcsData.ViewModels.App3 GetApp3()
        {
            if (base.Session["App3"] == null)
            {
                return null;
            }
            return (CcsData.ViewModels.App3) base.Session["App3"];
        }

        public CcsData.ViewModels.App4 GetApp4()
        {
            if (base.Session["App4"] == null)
            {
                return null;
            }
            return (CcsData.ViewModels.App4) base.Session["App4"];
        }

        public CcsData.ViewModels.App5 GetApp5()
        {
            if (base.Session["App5"] == null)
            {
                return null;
            }
            return (CcsData.ViewModels.App5) base.Session["App5"];
        }

        public ActionResult Index() => 
            base.View(this.db.Applications.ToList<Application>());

        private Applicant MapAppToApplicant(Applicant applicant, Application app)
        {
            if (applicant == null)
            {
                applicant = new Applicant();
            }
            Address item = new Address {
                StreetAddress = app.Address,
                City = app.City,
                State = app.State,
                ZipCode = app.ZipCode
            };
            if (applicant.Addresses == null)
            {
                applicant.Addresses = new List<Address>();
            }
            applicant.Addresses.Add(item);
            applicant.EmailAddress = app.EmailAddress;
            applicant.FullName = app.FirstName + " " + app.LastName;
            applicant.TotalBalanceOfDebtToConsolidate = app.EstimateTotalDebtToPayOff;
            applicant.TotalMonthlyAmountOfDebtPaymentsToConsolidate = app.TotalOfMonthlyPaymentsOnDebtToPayOff;
            applicant.FirstName = app.FirstName;
            applicant.LastName = app.LastName;
            applicant.HomePhone = app.Phone;
            applicant.CellPhone = app.Phone;
            applicant.WorkPhone = app.Phone;
            applicant.Veteran = app.Veteran;
            applicant.ClientApplicationDate = new DateTime?(DateTime.Now);
            applicant.LoanTypeRequested = app.LoanTypeRequested;
            applicant.RealtorID = app.RealtorID;
            Property property = new Property {
                EstimatedMarketValue = app.EstimatedHomeValue,
                PropertyTypeApp = new PropertyTypeEnum?(app.PropertyType),
                OwnerShipType = new OwnershipTypeEnum?(app.OwnerShipType),
                Rural = app.RuralProperty
            };
            if (applicant.Properties == null)
            {
                applicant.Properties = new List<Property>();
            }
            applicant.Properties.Add(property);
            if (!app.CashOutRequested.HasValue)
            {
                app.CashOutRequested = 0;
            }
            if (app.LoanTypeRequested == LoanTypeRequestedEnum.CashOutMortgage)
            {
                applicant.CashOutAmountRequested = app.CashOutRequested;
            }
            if (!app.AdditionalCashOutRequested.HasValue)
            {
                app.AdditionalCashOutRequested = 0;
            }
            if (app.LoanTypeRequested == LoanTypeRequestedEnum.DebtConsolidationPayOffCreditors)
            {
                applicant.CashOutAmountRequested = app.AdditionalCashOutRequested;
            }
            applicant.CreditScoreEstimate = new CreditScoreEstimateEnum?(app.CreditScoreEstimate);
            Mortgage mortgage = new Mortgage {
                MortgagedProperty = property,
                Balance = app.FirstMortgageBalance,
                Position = 1
            };
            if (app.CurrentInterestRate.HasValue)
            {
                mortgage.InterestRate = app.CurrentInterestRate.Value;
            }
            mortgage.InterestRateType = new InterestRateTypeEnum?(app.InterestRateType);
            if (!app.MonthlyMortgageInsur.HasValue)
            {
                app.MonthlyMortgageInsur = 0;
            }
            mortgage.MonthlyMortgageInsurance = app.MonthlyMortgageInsur;
            mortgage.MonthlyPayment = app.FirstMortgagePayment;
            mortgage.MonthlyPropertyTaxes = app.AnnualPropertyTaxes / 12M;
            mortgage.OriginationDate = app.FirstMortgageOriginationDate;
            mortgage.LoanType = new LoanTypeEnum?(app.LoanType);
            mortgage.PymtIncludesTaxes = EnumNorm.BoolToYesNo(app.PymtIncludesPropTaxes);
            mortgage.Term = new MortgageTermEnum?(app.MortgageTerm);
            mortgage.YearlyHomeInsurancePayment = app.AnnualHomeownersInsur;
            mortgage.YearlyMortgageInsurance = app.MonthlyMortgageInsur * 12M;
            mortgage.YearlyPropertyTaxes = app.AnnualPropertyTaxes;
            mortgage.DownPayment = app.DownPaymentAmount;
            mortgage.EstimatedHomeownersAssociationFeesAnnual = app.EstimatedHomeownersAssociationFeesAnnual;
            mortgage.SellerPaidCreditClosingCost = app.SellerPaidCreditClosingCost;
            mortgage.PurchasePrice = new decimal?(app.PurchasePrice);
            mortgage.PymtIncludesHomeownersInsurance = EnumNorm.BoolToYesNo(app.PymtIncludesHomeownersInsurance);
            if (applicant.Mortgages == null)
            {
                applicant.Mortgages = new List<Mortgage>();
            }
            applicant.Mortgages.Add(mortgage);
            if (app.Have2ndMortgage == YesNoAns.Yes)
            {
                applicant.Has2ndMortgage = app.Has2ndMortgage;
                applicant.PayOff2ndMortgage = app.PayOff2ndMortgage;
                SecondMortgage mortgage2 = new SecondMortgage {
                    Balance = app.SecondMortgageBalance
                };
                if (app.SecondMortgageInterestRate.HasValue)
                {
                    mortgage2.InterestRate = app.SecondMortgageInterestRate.Value;
                }
                mortgage2.Position = 2;
                mortgage2.SecondMortgageRateType = new SecondMortgageRateTypeEnum?(app.SecondMortgageRateType);
                mortgage2.SecondMortgageTerm = app.SecondMortgageTerm;
                mortgage2.MonthlyPayment = app.SecondMortgagePayment;
                mortgage2.OriginationDate = app.SecondMortgageOriginationDate;
                applicant.SecondMortgage = mortgage2;
            }
            if (applicant.Applications == null)
            {
                applicant.Applications = new List<Application>();
            }
            applicant.Applications.Add(app);
            return applicant;
        }

        public ActionResult purchaseNow() => 
            base.RedirectToAction("PurchaseOptionsNow", "Mortgages1");

        public ActionResult RefiNow() => 
            base.RedirectToAction("RefiOptionsNow", "Mortgages1");

        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser {
                    UserName = model.EmailReg,
                    Email = model.EmailReg
                };
                IdentityResult result = await this.UserManager.CreateAsync(user, model.PasswordReg);
                this.UserManager.AddToRole<ApplicationUser, string>(user.Id, "Org");
                if (result.Succeeded)
                {
                    await this.SignInAsync(user, true);
                    return this.RedirectToAction("Index", "Home");
                }
                this.AddErrors(result);
            }
            return this.View("_Register", model);
        }

        private IdentityResult RegisterInSync(RegisterViewModel model)
        {
            UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(this.db);
            UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(store);
            ApplicationUser user = new ApplicationUser {
                UserName = model.EmailReg,
                FirstName = model.FirstNameReg,
                LastName = model.LastNameReg,
                EmailAddress = model.EmailReg
            };
            IdentityResult result = manager.Create<ApplicationUser, string>(user, model.PasswordReg);
            manager.AddToRole<ApplicationUser, string>(user.Id, "Org");
            return result;
        }

        private bool RegisterOnly(RegisterViewModel model)
        {
            UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(this.db);
            UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(store);
            ApplicationUser user = new ApplicationUser {
                UserName = model.EmailReg,
                FirstName = model.FirstNameReg,
                LastName = model.LastNameReg,
                EmailAddress = model.EmailReg
            };
            if (!this.db.Users.Any<ApplicationUser>(u => (u.UserName == model.EmailReg)))
            {
                user.FirstName = model.FirstNameReg;
                user.LastName = model.LastNameReg;
                user.Email = model.EmailReg;
                manager.Create<ApplicationUser, string>(user, model.PasswordReg);
                manager.AddToRole<ApplicationUser, string>(user.Id, "user");
                return true;
            }
            return false;
        }

        public async Task<bool> RegisterUser(RegisterViewModel model)
        {
            ApplicationUser user = new ApplicationUser {
                UserName = model.EmailReg,
                Email = model.EmailReg,
                LastName = model.LastNameReg,
                FirstName = model.FirstNameReg
            };
            ApplicationUser userx = await this.UserManager.FindAsync(model.EmailReg, model.PasswordReg);
            if (userx != null)
            {
                await this.SignInAsync(userx, true);
                return true;
            }
            IdentityResult asyncVariable1 = await this.UserManager.CreateAsync(user, model.PasswordReg);
            ApplicationUser user2 = (from u in this.db.Users
                where u.UserName == model.EmailReg
                select u).FirstOrDefault<ApplicationUser>();
            if (asyncVariable1.Succeeded)
            {
                this.UserManager.AddToRole<ApplicationUser, string>(user.Id, "Org");
                await this.SignInAsync(user2, true);
                return true;
            }
            return false;
        }

        public void Respond(AppEmailModel model)
        {
            base.ViewData["viewdataMessage"] = "message from view data";
            ((dynamic) base.ViewBag).WelcomeMessage = "welcome Message";
            new CcsWeb.Mailers.UserMailer();
            if ((model.RealtorEmail != null) && (model.RealtorEmail.Trim() != ""))
            {
                MvcMailMessage message = this.UserMailer.Realtor(model);
                message.Subject = model.FirstName + " " + model.LastName + "Submited a mortgage application with CCS Home Loans";
                message.To.Add(model.RealtorEmail);
                message.Send(null);
            }
            if (model.IsPurchase)
            {
                MvcMailMessage message2 = this.UserMailer.PurchAppRecievedBor(model);
                message2.Subject = model.FirstName + " " + model.LastName + "Your Mortgage Application was received";
                message2.To.Add(model.ToEmailAddess);
                message2.Send(null);
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            this.AuthenticationManager.SignOut(new string[] { "ExternalCookie" });
            AuthenticationProperties asyncVariable0 = new AuthenticationProperties {
                IsPersistent = isPersistent
            };
            ClaimsIdentity[] identities = new ClaimsIdentity[1];
            ClaimsIdentity result = await user.GenerateUserIdentityAsync(this.UserManager);

            var tuple2 = new Tuple<IAuthenticationManager, AuthenticationProperties, ClaimsIdentity[], int, ClaimsIdentity[]>(
                this.AuthenticationManager, asyncVariable0, identities, 0, null);

            tuple2.Item3[tuple2.Item4] = result;
            tuple2.Item1.SignIn(tuple2.Item2, identities);
        }

        public ActionResult Thankyou1() => 
            base.View();

        private Microsoft.Owin.Security.IAuthenticationManager AuthenticationManager =>
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

    }
}

