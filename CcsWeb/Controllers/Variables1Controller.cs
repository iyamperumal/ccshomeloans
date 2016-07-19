namespace CcsWeb.Controllers
{
    using CcsData.Models;
    using CcsWeb.DataContexts;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class Variables1Controller : Controller
    {
        private CcsLocalDbContext db = new CcsLocalDbContext();

        public ActionResult Create() => 
            base.View();

        [ValidateAntiForgeryToken, HttpPost]
        public async Task<ActionResult> Create([Bind(Include="Variable_Id,Judgment,Active,LoanType,MortgageProgramOption,RateType,SFR,Condo,Manifactured,MobilHome,MultiUnits,TownHome,Lender,LenderLogo,ScheduleName,AdjustableTerms,MaxNumberOfUnits,newTermInYears,NewInterestRate,OriginationPercent,LenderCreditPercent,DiscountPercent,TitleInusrancePercent,IntangibleTaxPercent,StateTaxPercent,DeedStampPercent,LenderTitleInsuranceFee,PestInspectionFee,SurveyFee,TaxServiceFee,FloodCertificationFee,PropertyType,LTV_Range,MaxLTV,CLTV,MaxLoanAmount,MaxCashOut,OwnershipType,CreditScoreRange,NumOf30LateAllowedIn12Mo,NumOf30LateAllowedIn24Mo,MaxfrontDTI,MaxBacktDTI,Bankruptcy,Foreclosure,UpfrontMI,MiFactor,LenderPaidComp,MiDurationYears,FHA_Upfront_MIP_Refi_percent_beforeJune1_2009,FHA_Upfront_MIP_RefiOrPurchase_percent_AfterMay31_2009,FHA_Monthly_MIP_RefiOrPurchase_percent_AfterMay31_2009,FHA_Monthly_MIP_Refi_percent_BeforeJune1_2009,ConventionalPmiFactor,VaFundingFeeFactorZeroDown,VaFundingFeeFactor5to10Down,VaFundingFeeFactor10PlusDown,VaFundingFeeFactorRefiNoCashout,VaFundingFeeFactorWithCashout,VaFundingFeeFactorMobileHomeRefiNoCashout,HazardInsurancePercent,FloodInsurancePercent,PropertyTaxPercent,DailyInterestCalculation,NumofMonthstoEscrowTaxes,NumofMonthstoEscrowHazardInsurance,NumofMonthstoEscrowFloodInsurance,ProcessingFee,UnderwritingFee,AppraisalFee,CreditReportFee,ClosingEscrowFee,EndorsementsReconveyanceFee,MortgageRecordingfee,OptionNumber,State,County")] Variable variable)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Variables.Add(variable);
            }
            else
            {
                return this.View(variable);
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
            Variable model = await this.db.Variables.FindAsync(new object[] { id });
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
            Variable entity = await this.db.Variables.FindAsync(new object[] { id });
            this.db.Variables.Remove(entity);
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
            Variable model = await this.db.Variables.FindAsync(new object[] { id });
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
        public async Task<ActionResult> Edit([Bind(Include="Variable_Id,Judgment,Active,LoanType,MortgageProgramOption,RateType,SFR,Condo,Manifactured,MobilHome,MultiUnits,TownHome,Lender,LenderLogo,ScheduleName,AdjustableTerms,MaxNumberOfUnits,newTermInYears,NewInterestRate,OriginationPercent,LenderCreditPercent,DiscountPercent,TitleInusrancePercent,IntangibleTaxPercent,StateTaxPercent,DeedStampPercent,LenderTitleInsuranceFee,PestInspectionFee,SurveyFee,TaxServiceFee,FloodCertificationFee,PropertyType,LTV_Range,MaxLTV,CLTV,MaxLoanAmount,MaxCashOut,OwnershipType,CreditScoreRange,NumOf30LateAllowedIn12Mo,NumOf30LateAllowedIn24Mo,MaxfrontDTI,MaxBacktDTI,Bankruptcy,Foreclosure,UpfrontMI,MiFactor,LenderPaidComp,MiDurationYears,FHA_Upfront_MIP_Refi_percent_beforeJune1_2009,FHA_Upfront_MIP_RefiOrPurchase_percent_AfterMay31_2009,FHA_Monthly_MIP_RefiOrPurchase_percent_AfterMay31_2009,FHA_Monthly_MIP_Refi_percent_BeforeJune1_2009,ConventionalPmiFactor,VaFundingFeeFactorZeroDown,VaFundingFeeFactor5to10Down,VaFundingFeeFactor10PlusDown,VaFundingFeeFactorRefiNoCashout,VaFundingFeeFactorWithCashout,VaFundingFeeFactorMobileHomeRefiNoCashout,HazardInsurancePercent,FloodInsurancePercent,PropertyTaxPercent,DailyInterestCalculation,NumofMonthstoEscrowTaxes,NumofMonthstoEscrowHazardInsurance,NumofMonthstoEscrowFloodInsurance,ProcessingFee,UnderwritingFee,AppraisalFee,CreditReportFee,ClosingEscrowFee,EndorsementsReconveyanceFee,MortgageRecordingfee,OptionNumber,State,County")] Variable variable)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Entry<Variable>(variable).State = EntityState.Modified;
            }
            else
            {
                return this.View(variable);
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
            Variable model = await this.db.Variables.FindAsync(new object[] { id });
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

        public async Task<ActionResult> Index()
        {
            List<Variable> model = await this.db.Variables.ToListAsync<Variable>();
            return View(model);
        }







    }
}

