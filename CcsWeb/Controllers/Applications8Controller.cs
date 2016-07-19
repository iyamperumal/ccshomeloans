namespace CcsWeb.Controllers
{
    using CcsData.Models;
    using CcsWeb.DataContexts;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    public class Applications8Controller : Controller
    {
        private CcsLocalDbContext db = new CcsLocalDbContext();

        public ActionResult Create()
        {
            ((dynamic) base.ViewBag).RealtorID = new SelectList(this.db.Realtor, "Realtor_Id", "FirstName");
            return base.View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Application_Id,LoanTypeRequested,CashOutRequested,RealtorID,OwnerShipType,PropertyType,EstimateTotalDebtToPayOff,TotalOfMonthlyPaymentsOnDebtToPayOff,PurchasePrice,DownPaymentAmount,EstimatedHomeownersAssociationFeesAnnual,OwnerShipLongevity,CreditScoreEstimate,DaysLate,FiledBankruptcyType,BankruptcyDischargeDate,Chapter13FilingDate,ForeclosuresShortSaleDeedinLieu,ForeclosureShortSaleDeedinLieuDate,GrossAnnualIncome,TotalMontlyPayments,RuralProperty,Veteran,EstimatedHomeValue,FirstMortgageBalance,CurrentInterestRate,InterestRateType,LoanType,MortgageTerm,Have2ndMortgage,PayOff2ndMortgage,SecondMortgageBalance,SecondMortgageInterestRate,SecondMortgageRateType,SecondMortgageTerm,SecondMortgagePayment,SecondMortgageOriginationDate,FirstMortgagePayment,PymtIncludesMI,PymtIncludesPropTaxes,PymtIncludesMone,HomeownersInsurance,HoaDuesFees,AnnualPropertyTaxes,AnnualHomeownersInsur,AnnualHomeownersAssocDues,FirstName,LastName,EmailAddress,Phone,Address,City,State,ZipCode")] Application application)
        {
            if (base.ModelState.IsValid)
            {
                this.db.Applications.Add(application);
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
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

        [ValidateAntiForgeryToken, HttpPost, ActionName("Delete")]
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

        [ValidateAntiForgeryToken, HttpPost]
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

        public ActionResult Index()
        {
            IQueryable<Application> source = this.db.Applications.Include<Application, Realtor>(a => a.Realtor);
            return base.View(source.ToList<Application>());
        }
    }
}

