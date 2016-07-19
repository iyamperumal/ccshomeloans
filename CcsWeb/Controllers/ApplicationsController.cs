namespace CcsWeb.Controllers
{
    using CcsData.Models;
    using CcsWeb.DataContexts;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.CSharp.RuntimeBinder;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    [Authorize(Roles="Admin")]
    public class ApplicationsController : Controller
    {
        private CcsLocalDbContext db = new CcsLocalDbContext();

        public ActionResult Create()
        {
            ((dynamic) base.ViewBag).LoanOfficerID = new SelectList(this.db.LoanOfficers, "LoanOfficer_Id", "FirstName");
            ((dynamic) base.ViewBag).LoanProcessorID = new SelectList(this.db.LoanProcessors, "LoanProcessor_Id", "FirstName");
            ((dynamic) base.ViewBag).OptionSelectedID = new SelectList(this.db.OptionsSlected, "OptionSelected_Id", "PreparedFor");
            ((dynamic) base.ViewBag).PropertyID = new SelectList(this.db.Properties, "Property_Id", "Address");
            ((dynamic) base.ViewBag).RealtorID = new SelectList(this.db.Realtor, "Realtor_Id", "FirstName");
            return base.View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Application_Id,LoanTypeRequested,CashOutRequested,RealtorID,LoanOfficerID,LoanProcessorID,SocialSecurityNumber,DateOfBirth,OwnerShipType,PropertyType,EstimateTotalDebtToPayOff,TotalOfMonthlyPaymentsOnDebtToPayOff,AdditionalCashOutRequested,PurchasePrice,DownPaymentAmount,EstimatedHomeownersAssociationFeesAnnual,OwnerShipLongevity,CreditScoreEstimate,DaysLate,FiledBankruptcyType,BankruptcyDischargeDate,BankruptcyDischargeMonth,BankruptcyDischargeYear,Chapter13FilingDate,Chapter13FilingMonth,Chapter13FilingYear,ForeclosuresShortSaleDeedinLieu,ForeclosureShortSaleDeedinLieuDate,ForeclosureShortSaleDeedinLieuMonth,ForeclosureShortSaleDeedinLieuYear,GrossAnnualIncome,TotalMontlyPayments,RuralProperty,Veteran,EstimatedHomeValue,FirstMortgageBalance,CurrentInterestRate,InterestRateType,LoanType,MortgageTerm,Have2ndMortgage,PayOff2ndMortgage,SecondMortgageBalance,SecondMortgageInterestRate,SecondMortgageRateType,SecondMortgageTerm,SecondMortgagePayment,SecondMortgageOriginationDate,FirstMortgageOriginationDate,SecondMortgageOriginationMonth,SecondMortgageOriginationYear,FirstMortgagePayment,PymtIncludesMI,PymtIncludesPropTaxes,PymtIncludesMone,PymtIncludesHomeownersInsurance,HoaDuesFees,AnnualPropertyTaxes,AnnualHomeownersInsur,SellerPaidCreditClosingCost,MonthlyMortgageInsur,AnnualHomeownersAssocDues,FirstName,LastName,EmailAddress,Phone,Address,City,State,ZipCode,ApplicationDate,PropertyID,OptionSelectedID")] Application application)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Applications.Add(application);
            }
            else
            {
                ((dynamic) this.ViewBag).LoanOfficerID = new SelectList(this.db.LoanOfficers, "LoanOfficer_Id", "FirstName", application.LoanOfficerID);
                ((dynamic) this.ViewBag).LoanProcessorID = new SelectList(this.db.LoanProcessors, "LoanProcessor_Id", "FirstName", application.LoanProcessorID);
                ((dynamic) this.ViewBag).OptionSelectedID = new SelectList(this.db.OptionsSlected, "OptionSelected_Id", "PreparedFor", application.OptionSelectedID);
                ((dynamic) this.ViewBag).PropertyID = new SelectList(this.db.Properties, "Property_Id", "Address", application.PropertyID);
                ((dynamic) this.ViewBag).RealtorID = new SelectList(this.db.Realtor, "Realtor_Id", "FirstName", application.RealtorID);
                return this.View(application);
            }
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int? id)
        {
            ActionResult result;
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application model = await this.db.Applications.FindAsync(new object[] { id });
            if (model == null)
            {
                result = this.HttpNotFound();
            }
            else
            {
                result = this.View(model);
            }
            return result;
        }

        [ActionName("Delete"), HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Application entity = await this.db.Applications.FindAsync(new object[] { id });
            this.db.Applications.Remove(entity);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int? id)
        {
            ActionResult result;
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application model = await this.db.Applications.FindAsync(new object[] { id });
            if (model == null)
            {
                result = this.HttpNotFound();
            }
            else
            {
                result = this.View(model);
            }
            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }

        [ValidateAntiForgeryToken, HttpPost]
        public async Task<ActionResult> Edit([Bind(Include="Application_Id,LoanTypeRequested,CashOutRequested,RealtorID,LoanOfficerID,LoanProcessorID,SocialSecurityNumber,DateOfBirth,OwnerShipType,PropertyType,EstimateTotalDebtToPayOff,TotalOfMonthlyPaymentsOnDebtToPayOff,AdditionalCashOutRequested,PurchasePrice,DownPaymentAmount,EstimatedHomeownersAssociationFeesAnnual,OwnerShipLongevity,CreditScoreEstimate,DaysLate,FiledBankruptcyType,BankruptcyDischargeDate,BankruptcyDischargeMonth,BankruptcyDischargeYear,Chapter13FilingDate,Chapter13FilingMonth,Chapter13FilingYear,ForeclosuresShortSaleDeedinLieu,ForeclosureShortSaleDeedinLieuDate,ForeclosureShortSaleDeedinLieuMonth,ForeclosureShortSaleDeedinLieuYear,GrossAnnualIncome,TotalMontlyPayments,RuralProperty,Veteran,EstimatedHomeValue,FirstMortgageBalance,CurrentInterestRate,InterestRateType,LoanType,MortgageTerm,Have2ndMortgage,PayOff2ndMortgage,SecondMortgageBalance,SecondMortgageInterestRate,SecondMortgageRateType,SecondMortgageTerm,SecondMortgagePayment,SecondMortgageOriginationDate,FirstMortgageOriginationDate,SecondMortgageOriginationMonth,SecondMortgageOriginationYear,FirstMortgagePayment,PymtIncludesMI,PymtIncludesPropTaxes,PymtIncludesMone,PymtIncludesHomeownersInsurance,HoaDuesFees,AnnualPropertyTaxes,AnnualHomeownersInsur,SellerPaidCreditClosingCost,MonthlyMortgageInsur,AnnualHomeownersAssocDues,FirstName,LastName,EmailAddress,Phone,Address,City,State,ZipCode,ApplicationDate,PropertyID,OptionSelectedID")] Application application)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Entry<Application>(application).State = EntityState.Modified;
            }
            else
            {
                ((dynamic) this.ViewBag).LoanOfficerID = new SelectList(this.db.LoanOfficers, "LoanOfficer_Id", "FirstName", application.LoanOfficerID);
                ((dynamic) this.ViewBag).LoanProcessorID = new SelectList(this.db.LoanProcessors, "LoanProcessor_Id", "FirstName", application.LoanProcessorID);
                ((dynamic) this.ViewBag).OptionSelectedID = new SelectList(this.db.OptionsSlected, "OptionSelected_Id", "PreparedFor", application.OptionSelectedID);
                ((dynamic) this.ViewBag).PropertyID = new SelectList(this.db.Properties, "Property_Id", "Address", application.PropertyID);
                ((dynamic) this.ViewBag).RealtorID = new SelectList(this.db.Realtor, "Realtor_Id", "FirstName", application.RealtorID);
                return this.View(application);
            }
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int? id)
        {
            ActionResult result;
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application model = await this.db.Applications.FindAsync(new object[] { id });
            if (model == null)
            {
                result = this.HttpNotFound();
            }
            else
            {
                ((dynamic) this.ViewBag).LoanOfficerID = new SelectList(this.db.LoanOfficers, "LoanOfficer_Id", "FirstName", model.LoanOfficerID);
                ((dynamic) this.ViewBag).LoanProcessorID = new SelectList(this.db.LoanProcessors, "LoanProcessor_Id", "FirstName", model.LoanProcessorID);
                ((dynamic) this.ViewBag).OptionSelectedID = new SelectList(this.db.OptionsSlected, "OptionSelected_Id", "PreparedFor", model.OptionSelectedID);
                ((dynamic) this.ViewBag).PropertyID = new SelectList(this.db.Properties, "Property_Id", "Address", model.PropertyID);
                ((dynamic) this.ViewBag).RealtorID = new SelectList(this.db.Realtor, "Realtor_Id", "FirstName", model.RealtorID);
                result = this.View(model);
            }
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, Application application)
        {
            if ((application != null) && base.ModelState.IsValid)
            {
                this.db.Entry<Application>(application).State = EntityState.Modified;
                this.db.SaveChanges();
            }
            return base.Json(new Application[] { application }.ToDataSourceResult(request, base.ModelState));
        }

        public ActionResult GetApplications([DataSourceRequest] DataSourceRequest request)
        {
            List<Application> enumerable = this.db.Applications.ToList<Application>();
            //from c in enumerable
            //             select new { 
            //    FirstName = c.FirstName,
            //    LastName = c.LastName
            //};
            return base.Json(enumerable.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index() => 
            base.View();

        public async Task<ActionResult> Index2()
        {
            ApplicationsController controller2 = new ApplicationsController();
            IQueryable<Application> source = this.db.Applications.Include<Application, LoanOfficer>(a => a.LoanOfficer).Include<Application, LoanProcessor>(a => a.LoanProcessor).Include<Application, OptionSelected>(a => a.OptionSelected).Include<Application, Property>(a => a.Property).Include<Application, Realtor>(a => a.Realtor);
            List<Application> model = await source.ToListAsync<Application>();
            return controller2.View(model);
        }







    }
}

