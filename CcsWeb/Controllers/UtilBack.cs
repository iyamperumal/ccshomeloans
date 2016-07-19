namespace CcsWeb.Controllers
{
    using CcsData.Models;
    using CcsData.Models.CreditPull;
    using CcsData.Models.RunTimeModels;
    using CcsWeb.DataContexts;
    using CcsWeb.Models;
    using CreditRequestLib;
    using LiquidTechnologies.Runtime.Net45;
    using RateSheetRebateProcessor;
    using RateTableVariables;
    using ResponseLib;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web;
    using System.Xml;

    internal static class UtilBack
    {
        public static bool RateSheetLoaded;

        internal static double CalcNewMortgage(double bal) => 
            0.0;

        internal static DateTime DatefromEnums(MonthsEnum mo, YearsEnum year)
        {
            DateTime now = DateTime.Now;
            try
            {
                int month = (int) mo;
                int num2 = (int) year;
                now = new DateTime(num2, month, 1);
            }
            catch
            {
                return now;
            }
            return now;
        }

        internal static int GetApplicantId(CcsLocalDbContext db, string userName) => 
            (from r in db.Users
                where r.UserName == userName
                select r).FirstOrDefault<ApplicationUser>().Applicant_id.Value;

        private static double GetAPR(double loanAmount, double rate, double termInMonths, double payment)
        {
            double num = rate + 0.0025;
            double num2 = rate - 0.00083333333333333339;
            double num3 = rate;
            int num4 = 1;
            double num5 = 0.0;
            while (num4 < 0x2710)
            {
                num5 = loanAmount * ((num3 * Math.Pow(1.0 + num3, termInMonths)) / (Math.Pow(1.0 + num3, termInMonths) - 1.0));
                if (Math.Abs((double) (num5 - payment)) <= 0.001)
                {
                    return (num3 * 1200.0);
                }
                if (num5 < payment)
                {
                    num2 = num3;
                    num3 += (num - num3) / 2.0;
                }
                else if (num5 > payment)
                {
                    num = num3;
                    num3 -= (num3 - num2) / 2.0;
                }
                num4++;
            }
            return (num3 * 1200.0);
        }

        internal static decimal GetBPMI(decimal loanAmount, decimal propValue, int termInYears)
        {
            double num = (double) loanAmount;
            double num2 = 0.0059000002220273018;
            double num3 = (num * num2) / 12.0;
            return (decimal) num3;
        }

        internal static List<RefiOption> GetCashoutOptions(Application app)
        {
            List<RefiOption> list = new List<RefiOption>();
            List<Variable> cashoutVariables = GetCashoutVariables();
            for (int i = 0; i < cashoutVariables.Count; i++)
            {
                LoanTypeRequestedEnum loanTypeRequested = app.LoanTypeRequested;
                if (((((((((app.LoanTypeRequested != LoanTypeRequestedEnum.DebtConsolidationPayOffCreditors) || cashoutVariables[i].RefiCashout) && ((app.LoanTypeRequested != LoanTypeRequestedEnum.RateAndTermRefiLowerPayment) || cashoutVariables[i].RateTerm)) && (((app.LoanTypeRequested != LoanTypeRequestedEnum.RateAndTermRefiShorterTerm) || cashoutVariables[i].RateTerm) && ((app.OwnerShipType != OwnershipTypeEnum.Investment) || cashoutVariables[i].Investment))) && ((((app.OwnerShipType != OwnershipTypeEnum.Primary_Residence) || cashoutVariables[i].PrimaryResidence) && ((app.OwnerShipType != OwnershipTypeEnum.Second_Home) || cashoutVariables[i].SecondHome)) && (((app.PropertyType != PropertyTypeEnum.Modular_Manufactured) || cashoutVariables[i].Manufactured) && ((app.PropertyType != PropertyTypeEnum.Single_Familly_Residence_1_unit) || cashoutVariables[i].SFR)))) && (((((app.PropertyType != PropertyTypeEnum.Multi_Familly_Residence_2_units) || cashoutVariables[i].MultiUnits) && ((app.PropertyType != PropertyTypeEnum.Multi_Familly_Residence_3_units) || cashoutVariables[i].MultiUnits)) && (((app.PropertyType != PropertyTypeEnum.Multi_Familly_Residence_4_units) || cashoutVariables[i].MultiUnits) && ((app.PropertyType != PropertyTypeEnum.Multi_Familly_Residence_5_Plus) || cashoutVariables[i].MultiUnits))) && ((((app.PropertyType != PropertyTypeEnum.Condo) || cashoutVariables[i].Condo) && ((app.PropertyType != PropertyTypeEnum.MobileHomeWithLand) || cashoutVariables[i].MobileHome)) && (((app.PropertyType != PropertyTypeEnum.Townhome) || cashoutVariables[i].TownHome) && ((app.Veteran != YesNoAns.No) || (cashoutVariables[i].LoanType != LoanTypeEnum.VA)))))) && ((((app.RuralProperty != YesNoAns.No) || (cashoutVariables[i].LoanType != LoanTypeEnum.USRDA)) && ((app.LoanType == LoanTypeEnum.FHA) || (cashoutVariables[i].LoanType != LoanTypeEnum.FHA_Streamline))) && ((app.LoanType == LoanTypeEnum.VA) || (cashoutVariables[i].LoanType != LoanTypeEnum.VA_IRRL)))) && ((app.PropertyType != PropertyTypeEnum.Multi_Familly_Residence_2_units) || (cashoutVariables[i].MaxNumberOfUnits == 2))) && (((app.PropertyType != PropertyTypeEnum.Multi_Familly_Residence_3_units) || (cashoutVariables[i].MaxNumberOfUnits == 3)) && ((app.PropertyType != PropertyTypeEnum.Multi_Familly_Residence_4_units) || (cashoutVariables[i].MaxNumberOfUnits == 4))))
                {
                    decimal? nullable6;
                    double maxLoanAmount = cashoutVariables[i].MaxLoanAmount;
                    RefiOption item = new RefiOption {
                        ShowCashout = true,
                        VarNum = cashoutVariables[i].OptionNumber,
                        NewMI_MonthlyAmount = 0.00M,
                        OptionName = cashoutVariables[i].ScheduleName,
                        DatePrepared = new DateTime?(DateTime.Today),
                        LoanBalance = app.FirstMortgageBalance,
                        CurrentInterestRate = app.CurrentInterestRate,
                        PreparedFor = app.FirstName + " " + app.LastName,
                        MonthlyPayment = app.FirstMortgagePayment
                    };
                    decimal? monthlyPayment = item.MonthlyPayment;
                    decimal? monthlyMortgageInsur = app.MonthlyMortgageInsur;
                    item.OldMonthlyPaymentPrincipalInterest = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    if (app.PymtIncludesPropTaxes)
                    {
                        item.OldMonthlyPaymentPrincipalInterest -= app.AnnualPropertyTaxes / 12M;
                    }
                    if (app.PymtIncludesHomeownersInsurance)
                    {
                        item.OldMonthlyPaymentPrincipalInterest -= app.AnnualHomeownersInsur / 12M;
                    }
                    item.TermInMonths = new int?(EnumNorm.TermToInt(new MortgageTermEnum?(app.MortgageTerm)) * 12);
                    item.PercentCharge = new double?(cashoutVariables[i].OriginationPercent * 100.0);
                    item.ProcessingFee = new decimal?(cashoutVariables[i].ProcessingFee);
                    item.UnderwritingFee = new decimal?(cashoutVariables[i].UnderwritingFee);
                    item.CreditReportFee = new decimal?(cashoutVariables[i].CreditReportFee);
                    item.AppraisalFee = new decimal?(cashoutVariables[i].AppraisalFee);
                    item.ClosingEscrowFee = new decimal?(cashoutVariables[i].ClosingEscrowFee);
                    item.EndorsementsReconveyanceFee = new decimal?(cashoutVariables[i].EndorsementsReconveyanceFee);
                    item.MortgageRecordingCharges = new decimal?(cashoutVariables[i].MortgageRecordingfee);
                    item.NumberOfDays = 5;
                    item.DebtToBePaidOff = app.FirstMortgageBalance;
                    item.AmountOfNewPaymentPlusMonthlySavings = item.OldMonthlyPaymentPrincipalInterest;
                    item.InterestRate = new double?(cashoutVariables[i].NewInterestRate);
                    item.InterestRateSavings = item.CurrentInterestRate - item.InterestRate;
                    item.TaxServiceFee = cashoutVariables[i].TaxServiceFee;
                    item.FloodCertificationFee = cashoutVariables[i].FloodCertificationFee;
                    item.DateOfOrgination = app.FirstMortgageOriginationDate;
                    if (item.DateOfOrgination.HasValue)
                    {
                        DateTime time = item.DateOfOrgination.Value;
                        int num3 = DateTime.Now.Year - time.Year;
                        int month = time.Month;
                        int num5 = DateTime.Now.Month;
                        int num6 = Math.Abs((int) (((12 * num3) + month) - num5));
                        item.MonthsPaidRemaining = new int?(360 - num6);
                    }
                    item.MI_MonthlyAmount = app.MonthlyMortgageInsur;
                    item.monthlyTaxes = app.AnnualPropertyTaxes / 12.00M;
                    item.CT_MonthlyAmount = item.monthlyTaxes;
                    decimal? monthlyTaxes = item.monthlyTaxes;
                    decimal numofMonthstoEscrowTaxes = cashoutVariables[i].NumofMonthstoEscrowTaxes;
                    item.CountyPropertyTaxReserves = monthlyTaxes.HasValue ? new decimal?(monthlyTaxes.GetValueOrDefault() * numofMonthstoEscrowTaxes) : null;
                    item.CT_MonthsReserves = new decimal?(cashoutVariables[i].NumofMonthstoEscrowTaxes);
                    item.HI_MonthlyAmount = app.AnnualHomeownersInsur / 12.00M;
                    item.Insurance = item.HI_MonthlyAmount;
                    item.HI_MonthsReserves = new decimal?(cashoutVariables[i].NumofMonthstoEscrowHazardInsurance);
                    item.HazardInsuranceReserves = item.HI_MonthlyAmount * item.HI_MonthsReserves;
                    item.TermInYears = new int?(cashoutVariables[i].newTermInYears);
                    item.OriginationChargesPercent = new double?(cashoutVariables[i].OriginationPercent);
                    item.TotalFixedFees = new decimal?((((((((cashoutVariables[i].ProcessingFee + cashoutVariables[i].UnderwritingFee) + cashoutVariables[i].AppraisalFee) + cashoutVariables[i].CreditReportFee) + cashoutVariables[i].ClosingEscrowFee) + cashoutVariables[i].EndorsementsReconveyanceFee) + cashoutVariables[i].MortgageRecordingfee) + cashoutVariables[i].TaxServiceFee) + cashoutVariables[i].FloodCertificationFee);
                    decimal num7 = 0.00M;
                    if (app.LoanTypeRequested == LoanTypeRequestedEnum.CashOutMortgage)
                    {
                        num7 = app.AdditionalCashOutRequested.Value;
                    }
                    else if (app.LoanTypeRequested == LoanTypeRequestedEnum.DebtConsolidationPayOffCreditors)
                    {
                        num7 = app.AdditionalCashOutRequested.Value;
                    }
                    item.Cashout = num7;
                    double num8 = (0.05 * cashoutVariables[i].NewInterestRate) / 360.0;
                    double num9 = cashoutVariables[i].MiFactor / 6.0;
                    double upfrontMI = cashoutVariables[i].UpfrontMI;
                    double num11 = (((((((cashoutVariables[i].OriginationPercent - cashoutVariables[i].LenderCreditPercent) + cashoutVariables[i].TitleInusrancePercent) + cashoutVariables[i].IntangibleTaxPercent) + cashoutVariables[i].StateTaxPercent) + cashoutVariables[i].DiscountPercent) + num8) + num9) + upfrontMI;
                    item.MI_MonthsReserves = 2;
                    monthlyPayment = app.FirstMortgageBalance;
                    monthlyMortgageInsur = item.TotalFixedFees;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyMortgageInsur = item.HazardInsuranceReserves;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyMortgageInsur = item.CountyPropertyTaxReserves;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    numofMonthstoEscrowTaxes = num7;
                    monthlyMortgageInsur = monthlyPayment.HasValue ? new decimal?(monthlyPayment.GetValueOrDefault() + numofMonthstoEscrowTaxes) : null;
                    double num12 = (double) monthlyMortgageInsur.Value;
                    if ((cashoutVariables[i].MortgageProgramOption.Trim() == "HARP_FannieBefore2009") || (cashoutVariables[i].MortgageProgramOption.Trim() == "HARP_FredieBefore2009"))
                    {
                        num12 += ((double) app.MonthlyMortgageInsur.Value) * 2.0;
                    }
                    double num13 = num12 / (1.0 - num11);
                    item.LoanAmount = new decimal?((decimal) num13);
                    double num14 = cashoutVariables[i].MaxLTV * ((double) app.EstimatedHomeValue.Value);
                    if (((double) item.LoanAmount.Value) > num14)
                    {
                        item.LoanAmount = new decimal?((decimal) num14);
                    }
                    if (((double) item.LoanAmount.Value) > maxLoanAmount)
                    {
                        item.LoanAmount = new decimal?((decimal) maxLoanAmount);
                    }
                    if ((cashoutVariables[i].MortgageProgramOption.Trim() == "HARP_FannieBefore2009") || (cashoutVariables[i].MortgageProgramOption.Trim() == "HARP_FredieBefore2009"))
                    {
                        item.NewMI_MonthlyAmount = app.MonthlyMortgageInsur;
                        item.PMI_MIP_VA_FFReserves = item.NewMI_MonthlyAmount * 2.00M;
                        item.MI_Upfront_Fee = 0.00M;
                    }
                    else
                    {
                        item.MI_Upfront_Fee = new decimal?((decimal) (num13 * upfrontMI));
                        item.PMI_MIP_VA_FFReserves = new decimal?((decimal) (num13 * num9));
                        item.NewMI_MonthlyAmount = item.PMI_MIP_VA_FFReserves / 2.00M;
                    }
                    double mortgageAmount = (double) item.LoanAmount.Value;
                    item.NewMonthlyPaymentPrincipalInterest = 0.00M;
                    item.NewMonthlyPaymentPrincipalInterest = new decimal?((decimal) GetPayment(mortgageAmount, item.InterestRate.Value, (double) item.TermInYears.Value));
                    monthlyPayment = item.OldMonthlyPaymentPrincipalInterest;
                    monthlyMortgageInsur = item.MI_MonthlyAmount;
                    item.TotalOldPrincipalInterestPaymentWithMI = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyPayment = item.NewMonthlyPaymentPrincipalInterest;
                    monthlyMortgageInsur = item.NewMI_MonthlyAmount;
                    item.TotalNewPrincipalInterestPaymentWithMI = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyPayment = item.TotalOldPrincipalInterestPaymentWithMI;
                    monthlyMortgageInsur = item.TotalNewPrincipalInterestPaymentWithMI;
                    item.MonthlySavings = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyPayment = item.OldMonthlyPaymentPrincipalInterest;
                    int? monthsPaidRemaining = item.MonthsPaidRemaining;
                    item.OldTotalAmountOfAllPaymentsToBeMade = (monthlyPayment.HasValue & monthsPaidRemaining.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() * monthsPaidRemaining.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsur = null));
                    item.NewTotalAmountOfAllPaymentsToBeMade = item.NewMonthlyPaymentPrincipalInterest * item.TermInMonths;
                    monthlyPayment = item.OldTotalAmountOfAllPaymentsToBeMade;
                    monthlyMortgageInsur = item.NewTotalAmountOfAllPaymentsToBeMade;
                    item.NetSavingsFromOldLoanToNewLoan = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    item.CreditPoints = new double?(cashoutVariables[i].LenderCreditPercent);
                    monthlyPayment = item.LoanAmount;
                    numofMonthstoEscrowTaxes = (decimal) item.CreditPoints.Value;
                    item.Credit = monthlyPayment.HasValue ? new decimal?(monthlyPayment.GetValueOrDefault() * numofMonthstoEscrowTaxes) : ((decimal?) (monthlyMortgageInsur = null));
                    monthlyPayment = item.LoanAmount;
                    numofMonthstoEscrowTaxes = (decimal) cashoutVariables[i].OriginationPercent;
                    monthlyPayment = monthlyPayment.HasValue ? new decimal?(monthlyPayment.GetValueOrDefault() * numofMonthstoEscrowTaxes) : null;
                    monthlyMortgageInsur = item.Credit;
                    item.AdjustedLoanOriginationFee = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    numofMonthstoEscrowTaxes = (decimal) cashoutVariables[i].TitleInusrancePercent;
                    monthlyPayment = item.LoanAmount;
                    item.LenderTitleInsurance = monthlyPayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * monthlyPayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsur = null));
                    item.DiscountPoints = new double?(cashoutVariables[i].DiscountPercent * 100.0);
                    numofMonthstoEscrowTaxes = (decimal) cashoutVariables[i].DiscountPercent;
                    monthlyPayment = item.LoanAmount;
                    item.Discount = monthlyPayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * monthlyPayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsur = null));
                    numofMonthstoEscrowTaxes = (decimal) cashoutVariables[i].IntangibleTaxPercent;
                    monthlyPayment = item.LoanAmount;
                    item.IntangibleTax = monthlyPayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * monthlyPayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsur = null));
                    numofMonthstoEscrowTaxes = (decimal) cashoutVariables[i].StateTaxPercent;
                    monthlyPayment = item.LoanAmount;
                    item.StateTax = monthlyPayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * monthlyPayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsur = null));
                    numofMonthstoEscrowTaxes = (decimal) num8;
                    item.DailyInterestCharges = numofMonthstoEscrowTaxes * item.LoanAmount;
                    monthlyPayment = item.HazardInsuranceReserves;
                    monthlyMortgageInsur = item.CountyPropertyTaxReserves;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyMortgageInsur = item.PMI_MIP_VA_FFReserves;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyMortgageInsur = item.DailyInterestCharges;
                    item.EstimatedPrepaidsReserves = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyPayment = item.ProcessingFee;
                    monthlyMortgageInsur = item.UnderwritingFee;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyMortgageInsur = item.AppraisalFee;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyMortgageInsur = item.CreditReportFee;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyMortgageInsur = item.ClosingEscrowFee;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyMortgageInsur = item.EndorsementsReconveyanceFee;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyMortgageInsur = item.MortgageRecordingCharges;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyMortgageInsur = item.AdjustedLoanOriginationFee;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyMortgageInsur = item.LenderTitleInsurance;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyMortgageInsur = item.Discount;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyMortgageInsur = item.IntangibleTax;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyMortgageInsur = item.StateTax;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyMortgageInsur = item.MI_Upfront_Fee;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    numofMonthstoEscrowTaxes = item.TaxServiceFee;
                    monthlyPayment = monthlyPayment.HasValue ? new decimal?(monthlyPayment.GetValueOrDefault() + numofMonthstoEscrowTaxes) : ((decimal?) (monthlyMortgageInsur = null));
                    numofMonthstoEscrowTaxes = item.FloodCertificationFee;
                    item.EstClosingCosts = monthlyPayment.HasValue ? new decimal?(monthlyPayment.GetValueOrDefault() + numofMonthstoEscrowTaxes) : null;
                    monthlyPayment = item.LoanBalance;
                    monthlyMortgageInsur = item.EstClosingCosts;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyMortgageInsur = item.EstimatedPrepaidsReserves;
                    item.TotalAmountNeeded = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyPayment = item.LoanAmount;
                    monthlyMortgageInsur = item.TotalAmountNeeded;
                    item.EstimatedFundsNeeded = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyPayment = item.EstClosingCosts;
                    monthlyMortgageInsur = item.MonthlySavings;
                    item.CostSavingsBreakEvenAnalysis = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() / monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    double num17 = (double) item.LoanAmount.Value;
                    double? interestRate = item.InterestRate;
                    double num18 = (interestRate.Value / 100.0) / 12.0;
                    double num19 = (double) app.FirstMortgagePayment.Value;
                    double a = -Math.Log(1.0 - ((num17 / num19) * num18)) / Math.Log(1.0 + num18);
                    item.NumberofPaymentstoMaturity = new int?((int) Math.Ceiling(a));
                    monthsPaidRemaining = item.NumberofPaymentstoMaturity;
                    float? nullable32 = monthsPaidRemaining.HasValue ? new float?(((float) monthsPaidRemaining.GetValueOrDefault()) / 12f) : null;
                    item.YearsRequiredToMaturity = nullable32.HasValue ? new double?((double) nullable32.GetValueOrDefault()) : null;
                    item.AcceleratedPayoffTotalAmountOfAllPayments = app.FirstMortgagePayment * item.NumberofPaymentstoMaturity;
                    monthlyPayment = item.OldTotalAmountOfAllPaymentsToBeMade;
                    monthlyMortgageInsur = item.AcceleratedPayoffTotalAmountOfAllPayments;
                    item.TotalSavingsFromOldMortgageToNewMortgage = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthsPaidRemaining = item.MonthsPaidRemaining;
                    int? numberofPaymentstoMaturity = item.NumberofPaymentstoMaturity;
                    monthsPaidRemaining = (monthsPaidRemaining.HasValue & numberofPaymentstoMaturity.HasValue) ? new int?(monthsPaidRemaining.GetValueOrDefault() - numberofPaymentstoMaturity.GetValueOrDefault()) : ((int?) null);
                    item.MonthlyPaymentsEliminated = monthsPaidRemaining.HasValue ? new decimal?(monthsPaidRemaining.GetValueOrDefault()) : null;
                    monthlyPayment = item.OldMonthlyPaymentPrincipalInterest;
                    monthlyMortgageInsur = item.MI_MonthlyAmount;
                    item.TotalOldPrincipalInterestPaymentWithMI = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    monthlyPayment = item.NewMonthlyPaymentPrincipalInterest;
                    monthlyMortgageInsur = item.NewMI_MonthlyAmount;
                    item.TotalNewPrincipalInterestPaymentWithMI = (monthlyPayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable6 = null));
                    double loanAmount = ((((((((double) item.LoanAmount.Value) - ((double) item.AdjustedLoanOriginationFee.Value)) - ((double) item.Discount.Value)) - ((double) item.ProcessingFee.Value)) - ((double) item.UnderwritingFee.Value)) - ((double) item.ClosingEscrowFee.Value)) - ((double) item.MI_Upfront_Fee.Value)) - ((double) item.DailyInterestCharges.Value);
                    double payment = 0.0;
                    if (cashoutVariables[i].LoanType == LoanTypeEnum.FHA)
                    {
                        payment = ((((((double) item.NewMonthlyPaymentPrincipalInterest.Value) + ((double) item.NewMI_MonthlyAmount.Value)) * cashoutVariables[i].MiDurationYears) * 12.0) + ((((double) item.NewMonthlyPaymentPrincipalInterest.Value) * (30.0 - cashoutVariables[i].MiDurationYears)) * 12.0)) / 360.0;
                    }
                    else
                    {
                        int num23 = getMILTV(loanAmount, (double) item.TermInMonths.Value, item.InterestRate.Value, (double) item.NewMonthlyPaymentPrincipalInterest.Value, 78.0, (double) app.EstimatedHomeValue.Value);
                        payment = (((((double) item.NewMonthlyPaymentPrincipalInterest.Value) + ((double) item.NewMI_MonthlyAmount.Value)) * num23) + (((double) item.NewMonthlyPaymentPrincipalInterest.Value) * (((double) item.TermInMonths.Value) - num23))) / 360.0;
                    }
                    item.APR = new double?(GetAPR(loanAmount, item.InterestRate.Value / 1200.0, (double) item.TermInMonths.Value, payment));
                    interestRate = item.DiscountPoints;
                    item.DiscountPoints = interestRate.HasValue ? new double?(interestRate.GetValueOrDefault() / 100.0) : ((double?) null);
                    item.PercentCharge /= 100.0;
                    list.Add(item);
                }
            }
            return list;
        }

        internal static List<RefiOption> GetCashoutOptions(Mortgage curMtg)
        {
            List<RefiOption> list = new List<RefiOption>();
            List<Variable> cashoutVariables = GetCashoutVariables();
            for (int i = 0; i < cashoutVariables.Count; i++)
            {
                LoanTypeRequestedEnum loanTypeRequested = curMtg.MortgageApplicant.LoanTypeRequested;
                if ((((((curMtg.MortgageApplicant.LoanTypeRequested != LoanTypeRequestedEnum.DebtConsolidationPayOffCreditors) || cashoutVariables[i].RefiCashout) && ((curMtg.MortgageApplicant.LoanTypeRequested != LoanTypeRequestedEnum.RateAndTermRefiLowerPayment) || cashoutVariables[i].RateTerm)) && ((curMtg.MortgageApplicant.LoanTypeRequested != LoanTypeRequestedEnum.RateAndTermRefiShorterTerm) || cashoutVariables[i].RateTerm)) && (!curMtg.MortgagedProperty.OwnerShipType.HasValue || ((((((OwnershipTypeEnum) curMtg.MortgagedProperty.OwnerShipType) != OwnershipTypeEnum.Investment) || cashoutVariables[i].Investment) && ((((OwnershipTypeEnum) curMtg.MortgagedProperty.OwnerShipType) != OwnershipTypeEnum.Primary_Residence) || cashoutVariables[i].PrimaryResidence)) && ((((OwnershipTypeEnum) curMtg.MortgagedProperty.OwnerShipType) != OwnershipTypeEnum.Second_Home) || cashoutVariables[i].SecondHome)))) && ((!curMtg.MortgagedProperty.PropertyTypeApp.HasValue || ((((((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Modular_Manufactured) || cashoutVariables[i].Manufactured) && ((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Single_Familly_Residence_1_unit) || cashoutVariables[i].SFR)) && (((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Multi_Familly_Residence_2_units) || cashoutVariables[i].MultiUnits) && ((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Multi_Familly_Residence_3_units) || cashoutVariables[i].MultiUnits))) && ((((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Multi_Familly_Residence_4_units) || cashoutVariables[i].MultiUnits) && ((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Multi_Familly_Residence_5_Plus) || cashoutVariables[i].MultiUnits)) && (((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Condo) || cashoutVariables[i].Condo) && ((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.MobileHomeWithLand) || cashoutVariables[i].MobileHome)))) && ((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Townhome) || cashoutVariables[i].TownHome))) && (((((((curMtg.MortgageApplicant.Veteran != YesNoAns.No) || (cashoutVariables[i].LoanType != LoanTypeEnum.VA)) && ((curMtg.MortgagedProperty.Rural != YesNoAns.No) || (cashoutVariables[i].LoanType != LoanTypeEnum.USRDA))) && ((((LoanTypeEnum) curMtg.LoanType) == LoanTypeEnum.FHA) || (cashoutVariables[i].LoanType != LoanTypeEnum.FHA_Streamline))) && ((((LoanTypeEnum) curMtg.LoanType) == LoanTypeEnum.VA) || (cashoutVariables[i].LoanType != LoanTypeEnum.VA_IRRL))) && (((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Multi_Familly_Residence_2_units) || (cashoutVariables[i].MaxNumberOfUnits == 2)) && ((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Multi_Familly_Residence_3_units) || (cashoutVariables[i].MaxNumberOfUnits == 3)))) && ((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Multi_Familly_Residence_4_units) || (cashoutVariables[i].MaxNumberOfUnits == 4)))))
                {
                    decimal? nullable25;
                    double maxLoanAmount = cashoutVariables[i].MaxLoanAmount;
                    RefiOption item = new RefiOption {
                        ShowCashout = true,
                        VarNum = cashoutVariables[i].OptionNumber,
                        NewMI_MonthlyAmount = 0.00M,
                        OptionName = cashoutVariables[i].ScheduleName,
                        DatePrepared = new DateTime?(DateTime.Today),
                        LoanBalance = curMtg.Balance,
                        CurrentInterestRate = new double?(curMtg.InterestRate),
                        PreparedFor = curMtg.MortgageApplicant.FirstName + " " + curMtg.MortgageApplicant.LastName,
                        MonthlyPayment = curMtg.MonthlyPayment
                    };
                    decimal? monthlyPayment = curMtg.MonthlyPayment;
                    decimal? monthlyMortgageInsurance = curMtg.MonthlyMortgageInsurance;
                    item.OldMonthlyPaymentPrincipalInterest = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    if (curMtg.PymtIncludesTaxes == YesNoAns.Yes)
                    {
                        item.OldMonthlyPaymentPrincipalInterest -= curMtg.YearlyPropertyTaxes / 12M;
                    }
                    if (curMtg.PymtIncludesHomeownersInsurance == YesNoAns.Yes)
                    {
                        item.OldMonthlyPaymentPrincipalInterest -= curMtg.YearlyHomeInsurancePayment / 12M;
                    }
                    item.TermInMonths = new int?(EnumNorm.TermToInt(curMtg.Term) * 12);
                    item.PercentCharge = new double?(cashoutVariables[i].OriginationPercent * 100.0);
                    item.ProcessingFee = new decimal?(cashoutVariables[i].ProcessingFee);
                    item.UnderwritingFee = new decimal?(cashoutVariables[i].UnderwritingFee);
                    item.CreditReportFee = new decimal?(cashoutVariables[i].CreditReportFee);
                    item.AppraisalFee = new decimal?(cashoutVariables[i].AppraisalFee);
                    item.ClosingEscrowFee = new decimal?(cashoutVariables[i].ClosingEscrowFee);
                    item.EndorsementsReconveyanceFee = new decimal?(cashoutVariables[i].EndorsementsReconveyanceFee);
                    item.MortgageRecordingCharges = new decimal?(cashoutVariables[i].MortgageRecordingfee);
                    item.NumberOfDays = 5;
                    item.DebtToBePaidOff = curMtg.Balance;
                    item.AmountOfNewPaymentPlusMonthlySavings = item.OldMonthlyPaymentPrincipalInterest;
                    item.InterestRate = new double?(cashoutVariables[i].NewInterestRate);
                    item.InterestRateSavings = item.CurrentInterestRate - item.InterestRate;
                    item.TaxServiceFee = cashoutVariables[i].TaxServiceFee;
                    item.FloodCertificationFee = cashoutVariables[i].FloodCertificationFee;
                    item.DateOfOrgination = curMtg.OriginationDate;
                    if (item.DateOfOrgination.HasValue)
                    {
                        DateTime time = item.DateOfOrgination.Value;
                        int num3 = DateTime.Now.Year - time.Year;
                        int month = time.Month;
                        int num5 = DateTime.Now.Month;
                        int num6 = Math.Abs((int) (((12 * num3) + month) - num5));
                        item.MonthsPaidRemaining = new int?(360 - num6);
                    }
                    item.MI_MonthlyAmount = curMtg.MonthlyMortgageInsurance;
                    monthlyPayment = curMtg.YearlyPropertyTaxes;
                    item.monthlyTaxes = monthlyPayment.HasValue ? new decimal?(monthlyPayment.GetValueOrDefault() / 12.00M) : ((decimal?) (monthlyMortgageInsurance = null));
                    item.CT_MonthlyAmount = item.monthlyTaxes;
                    monthlyPayment = item.monthlyTaxes;
                    decimal numofMonthstoEscrowTaxes = cashoutVariables[i].NumofMonthstoEscrowTaxes;
                    item.CountyPropertyTaxReserves = monthlyPayment.HasValue ? new decimal?(monthlyPayment.GetValueOrDefault() * numofMonthstoEscrowTaxes) : ((decimal?) (monthlyMortgageInsurance = null));
                    item.CT_MonthsReserves = new decimal?(cashoutVariables[i].NumofMonthstoEscrowTaxes);
                    item.HI_MonthlyAmount = curMtg.YearlyHomeInsurancePayment / 12.00M;
                    item.Insurance = item.HI_MonthlyAmount;
                    item.HI_MonthsReserves = new decimal?(cashoutVariables[i].NumofMonthstoEscrowHazardInsurance);
                    monthlyPayment = item.HI_MonthlyAmount;
                    monthlyMortgageInsurance = item.HI_MonthsReserves;
                    item.HazardInsuranceReserves = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() * monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    item.TermInYears = new int?(cashoutVariables[i].newTermInYears);
                    item.OriginationChargesPercent = new double?(cashoutVariables[i].OriginationPercent);
                    item.TotalFixedFees = new decimal?((((((((cashoutVariables[i].ProcessingFee + cashoutVariables[i].UnderwritingFee) + cashoutVariables[i].AppraisalFee) + cashoutVariables[i].CreditReportFee) + cashoutVariables[i].ClosingEscrowFee) + cashoutVariables[i].EndorsementsReconveyanceFee) + cashoutVariables[i].MortgageRecordingfee) + cashoutVariables[i].TaxServiceFee) + cashoutVariables[i].FloodCertificationFee);
                    decimal num7 = curMtg.MortgageApplicant.CashOutAmountRequested.Value;
                    item.Cashout = num7;
                    double num8 = (0.05 * cashoutVariables[i].NewInterestRate) / 360.0;
                    double num9 = cashoutVariables[i].MiFactor / 6.0;
                    double upfrontMI = cashoutVariables[i].UpfrontMI;
                    double num11 = (((((((cashoutVariables[i].OriginationPercent - cashoutVariables[i].LenderCreditPercent) + cashoutVariables[i].TitleInusrancePercent) + cashoutVariables[i].IntangibleTaxPercent) + cashoutVariables[i].StateTaxPercent) + cashoutVariables[i].DiscountPercent) + num8) + num9) + upfrontMI;
                    item.MI_MonthsReserves = 2;
                    monthlyPayment = curMtg.Balance;
                    monthlyMortgageInsurance = item.TotalFixedFees;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyMortgageInsurance = item.HazardInsuranceReserves;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyMortgageInsurance = item.CountyPropertyTaxReserves;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    numofMonthstoEscrowTaxes = num7;
                    monthlyMortgageInsurance = monthlyPayment.HasValue ? new decimal?(monthlyPayment.GetValueOrDefault() + numofMonthstoEscrowTaxes) : null;
                    double num12 = (double) monthlyMortgageInsurance.Value;
                    if ((cashoutVariables[i].MortgageProgramOption.Trim() == "HARP_FannieBefore2009") || (cashoutVariables[i].MortgageProgramOption.Trim() == "HARP_FredieBefore2009"))
                    {
                        num12 += ((double) curMtg.MonthlyMortgageInsurance.Value) * 2.0;
                    }
                    double num13 = num12 / (1.0 - num11);
                    item.LoanAmount = new decimal?((decimal) num13);
                    double num14 = cashoutVariables[i].MaxLTV * ((double) curMtg.MortgagedProperty.EstimatedMarketValue.Value);
                    if (((double) item.LoanAmount.Value) > num14)
                    {
                        item.LoanAmount = new decimal?((decimal) num14);
                    }
                    if (((double) item.LoanAmount.Value) > maxLoanAmount)
                    {
                        item.LoanAmount = new decimal?((decimal) maxLoanAmount);
                    }
                    if ((cashoutVariables[i].MortgageProgramOption.Trim() == "HARP_FannieBefore2009") || (cashoutVariables[i].MortgageProgramOption.Trim() == "HARP_FredieBefore2009"))
                    {
                        item.NewMI_MonthlyAmount = curMtg.MonthlyMortgageInsurance;
                        item.PMI_MIP_VA_FFReserves = item.NewMI_MonthlyAmount * 2.00M;
                        item.MI_Upfront_Fee = 0.00M;
                    }
                    else
                    {
                        item.MI_Upfront_Fee = new decimal?((decimal) (num13 * upfrontMI));
                        item.PMI_MIP_VA_FFReserves = new decimal?((decimal) (num13 * num9));
                        item.NewMI_MonthlyAmount = item.PMI_MIP_VA_FFReserves / 2.00M;
                    }
                    double mortgageAmount = (double) item.LoanAmount.Value;
                    item.NewMonthlyPaymentPrincipalInterest = 0.00M;
                    item.NewMonthlyPaymentPrincipalInterest = new decimal?((decimal) GetPayment(mortgageAmount, item.InterestRate.Value, (double) item.TermInYears.Value));
                    monthlyPayment = item.OldMonthlyPaymentPrincipalInterest;
                    monthlyMortgageInsurance = item.MI_MonthlyAmount;
                    item.TotalOldPrincipalInterestPaymentWithMI = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyPayment = item.NewMonthlyPaymentPrincipalInterest;
                    monthlyMortgageInsurance = item.NewMI_MonthlyAmount;
                    item.TotalNewPrincipalInterestPaymentWithMI = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyPayment = item.TotalOldPrincipalInterestPaymentWithMI;
                    monthlyMortgageInsurance = item.TotalNewPrincipalInterestPaymentWithMI;
                    item.MonthlySavings = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyPayment = item.OldMonthlyPaymentPrincipalInterest;
                    int? monthsPaidRemaining = item.MonthsPaidRemaining;
                    item.OldTotalAmountOfAllPaymentsToBeMade = (monthlyPayment.HasValue & monthsPaidRemaining.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() * monthsPaidRemaining.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsurance = null));
                    item.NewTotalAmountOfAllPaymentsToBeMade = item.NewMonthlyPaymentPrincipalInterest * item.TermInMonths;
                    monthlyPayment = item.OldTotalAmountOfAllPaymentsToBeMade;
                    monthlyMortgageInsurance = item.NewTotalAmountOfAllPaymentsToBeMade;
                    item.NetSavingsFromOldLoanToNewLoan = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    item.CreditPoints = new double?(cashoutVariables[i].LenderCreditPercent);
                    monthlyPayment = item.LoanAmount;
                    numofMonthstoEscrowTaxes = (decimal) item.CreditPoints.Value;
                    item.Credit = monthlyPayment.HasValue ? new decimal?(monthlyPayment.GetValueOrDefault() * numofMonthstoEscrowTaxes) : ((decimal?) (monthlyMortgageInsurance = null));
                    monthlyPayment = item.LoanAmount;
                    numofMonthstoEscrowTaxes = (decimal) cashoutVariables[i].OriginationPercent;
                    monthlyPayment = monthlyPayment.HasValue ? new decimal?(monthlyPayment.GetValueOrDefault() * numofMonthstoEscrowTaxes) : null;
                    monthlyMortgageInsurance = item.Credit;
                    item.AdjustedLoanOriginationFee = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    numofMonthstoEscrowTaxes = (decimal) cashoutVariables[i].TitleInusrancePercent;
                    monthlyPayment = item.LoanAmount;
                    item.LenderTitleInsurance = monthlyPayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * monthlyPayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsurance = null));
                    item.DiscountPoints = new double?(cashoutVariables[i].DiscountPercent * 100.0);
                    numofMonthstoEscrowTaxes = (decimal) cashoutVariables[i].DiscountPercent;
                    monthlyPayment = item.LoanAmount;
                    item.Discount = monthlyPayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * monthlyPayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsurance = null));
                    numofMonthstoEscrowTaxes = (decimal) cashoutVariables[i].IntangibleTaxPercent;
                    monthlyPayment = item.LoanAmount;
                    item.IntangibleTax = monthlyPayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * monthlyPayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsurance = null));
                    numofMonthstoEscrowTaxes = (decimal) cashoutVariables[i].StateTaxPercent;
                    monthlyPayment = item.LoanAmount;
                    item.StateTax = monthlyPayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * monthlyPayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsurance = null));
                    numofMonthstoEscrowTaxes = (decimal) num8;
                    item.DailyInterestCharges = numofMonthstoEscrowTaxes * item.LoanAmount;
                    monthlyPayment = item.HazardInsuranceReserves;
                    monthlyMortgageInsurance = item.CountyPropertyTaxReserves;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyMortgageInsurance = item.PMI_MIP_VA_FFReserves;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyMortgageInsurance = item.DailyInterestCharges;
                    item.EstimatedPrepaidsReserves = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyPayment = item.ProcessingFee;
                    monthlyMortgageInsurance = item.UnderwritingFee;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyMortgageInsurance = item.AppraisalFee;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyMortgageInsurance = item.CreditReportFee;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyMortgageInsurance = item.ClosingEscrowFee;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyMortgageInsurance = item.EndorsementsReconveyanceFee;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyMortgageInsurance = item.MortgageRecordingCharges;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyMortgageInsurance = item.AdjustedLoanOriginationFee;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyMortgageInsurance = item.LenderTitleInsurance;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyMortgageInsurance = item.Discount;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyMortgageInsurance = item.IntangibleTax;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyMortgageInsurance = item.StateTax;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyMortgageInsurance = item.MI_Upfront_Fee;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    numofMonthstoEscrowTaxes = item.TaxServiceFee;
                    monthlyPayment = monthlyPayment.HasValue ? new decimal?(monthlyPayment.GetValueOrDefault() + numofMonthstoEscrowTaxes) : ((decimal?) (monthlyMortgageInsurance = null));
                    numofMonthstoEscrowTaxes = item.FloodCertificationFee;
                    item.EstClosingCosts = monthlyPayment.HasValue ? new decimal?(monthlyPayment.GetValueOrDefault() + numofMonthstoEscrowTaxes) : null;
                    monthlyPayment = item.LoanBalance;
                    monthlyMortgageInsurance = item.EstClosingCosts;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyMortgageInsurance = item.EstimatedPrepaidsReserves;
                    item.TotalAmountNeeded = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyPayment = item.LoanAmount;
                    monthlyMortgageInsurance = item.TotalAmountNeeded;
                    item.EstimatedFundsNeeded = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyPayment = item.EstClosingCosts;
                    monthlyMortgageInsurance = item.MonthlySavings;
                    item.CostSavingsBreakEvenAnalysis = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() / monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    double num17 = (double) item.LoanAmount.Value;
                    double? interestRate = item.InterestRate;
                    double num18 = (interestRate.Value / 100.0) / 12.0;
                    double num19 = (double) curMtg.MonthlyPayment.Value;
                    double a = -Math.Log(1.0 - ((num17 / num19) * num18)) / Math.Log(1.0 + num18);
                    item.NumberofPaymentstoMaturity = new int?((int) Math.Ceiling(a));
                    monthsPaidRemaining = item.NumberofPaymentstoMaturity;
                    float? nullable39 = monthsPaidRemaining.HasValue ? new float?(((float) monthsPaidRemaining.GetValueOrDefault()) / 12f) : null;
                    item.YearsRequiredToMaturity = nullable39.HasValue ? new double?((double) nullable39.GetValueOrDefault()) : null;
                    item.AcceleratedPayoffTotalAmountOfAllPayments = curMtg.MonthlyPayment * item.NumberofPaymentstoMaturity;
                    monthlyPayment = item.OldTotalAmountOfAllPaymentsToBeMade;
                    monthlyMortgageInsurance = item.AcceleratedPayoffTotalAmountOfAllPayments;
                    item.TotalSavingsFromOldMortgageToNewMortgage = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthsPaidRemaining = item.MonthsPaidRemaining;
                    int? numberofPaymentstoMaturity = item.NumberofPaymentstoMaturity;
                    monthsPaidRemaining = (monthsPaidRemaining.HasValue & numberofPaymentstoMaturity.HasValue) ? new int?(monthsPaidRemaining.GetValueOrDefault() - numberofPaymentstoMaturity.GetValueOrDefault()) : ((int?) null);
                    item.MonthlyPaymentsEliminated = monthsPaidRemaining.HasValue ? new decimal?(monthsPaidRemaining.GetValueOrDefault()) : null;
                    monthlyPayment = item.OldMonthlyPaymentPrincipalInterest;
                    monthlyMortgageInsurance = item.MI_MonthlyAmount;
                    item.TotalOldPrincipalInterestPaymentWithMI = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    monthlyPayment = item.NewMonthlyPaymentPrincipalInterest;
                    monthlyMortgageInsurance = item.NewMI_MonthlyAmount;
                    item.TotalNewPrincipalInterestPaymentWithMI = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable25 = null));
                    double loanAmount = ((((((((double) item.LoanAmount.Value) - ((double) item.AdjustedLoanOriginationFee.Value)) - ((double) item.Discount.Value)) - ((double) item.ProcessingFee.Value)) - ((double) item.UnderwritingFee.Value)) - ((double) item.ClosingEscrowFee.Value)) - ((double) item.MI_Upfront_Fee.Value)) - ((double) item.DailyInterestCharges.Value);
                    double payment = 0.0;
                    if (cashoutVariables[i].LoanType == LoanTypeEnum.FHA)
                    {
                        payment = ((((((double) item.NewMonthlyPaymentPrincipalInterest.Value) + ((double) item.NewMI_MonthlyAmount.Value)) * cashoutVariables[i].MiDurationYears) * 12.0) + ((((double) item.NewMonthlyPaymentPrincipalInterest.Value) * (30.0 - cashoutVariables[i].MiDurationYears)) * 12.0)) / 360.0;
                    }
                    else
                    {
                        int num23 = getMILTV(loanAmount, (double) item.TermInMonths.Value, item.InterestRate.Value, (double) item.NewMonthlyPaymentPrincipalInterest.Value, 78.0, (double) curMtg.MortgagedProperty.EstimatedMarketValue.Value);
                        payment = (((((double) item.NewMonthlyPaymentPrincipalInterest.Value) + ((double) item.NewMI_MonthlyAmount.Value)) * num23) + (((double) item.NewMonthlyPaymentPrincipalInterest.Value) * (((double) item.TermInMonths.Value) - num23))) / 360.0;
                    }
                    item.APR = new double?(GetAPR(loanAmount, item.InterestRate.Value / 1200.0, (double) item.TermInMonths.Value, payment));
                    interestRate = item.DiscountPoints;
                    item.DiscountPoints = interestRate.HasValue ? new double?(interestRate.GetValueOrDefault() / 100.0) : ((double?) null);
                    item.PercentCharge /= 100.0;
                    list.Add(item);
                }
            }
            return list;
        }

        internal static List<Variable> GetCashoutVariables()
        {
            CcsLocalDbContext context = new CcsLocalDbContext();
            List<Variable> list = (from v in context.Variables
                where v.RefiCashout && (((int) v.Active) == 2)
                select v).ToList<Variable>();
            context.Dispose();
            return list;
        }

        internal static List<RefiOption> GetConvHarpOptions(Mortgage curMtg)
        {
            List<RefiOption> list = new List<RefiOption>();
            List<Variable> refiVariables = GetRefiVariables();
            for (int i = 0; i < refiVariables.Count; i++)
            {
                LoanTypeRequestedEnum loanTypeRequested = curMtg.MortgageApplicant.LoanTypeRequested;
                if ((((((curMtg.MortgageApplicant.LoanTypeRequested != LoanTypeRequestedEnum.CashOutMortgage) || refiVariables[i].RefiCashout) && ((curMtg.MortgageApplicant.LoanTypeRequested != LoanTypeRequestedEnum.DebtConsolidationPayOffCreditors) || refiVariables[i].RefiCashout)) && ((((curMtg.MortgageApplicant.LoanTypeRequested != LoanTypeRequestedEnum.RateAndTermRefiLowerPayment) || refiVariables[i].RateTerm) || refiVariables[i].RefiCashout) && (((curMtg.MortgageApplicant.LoanTypeRequested != LoanTypeRequestedEnum.RateAndTermRefiShorterTerm) || refiVariables[i].RateTerm) || refiVariables[i].RefiCashout))) && (!curMtg.MortgagedProperty.OwnerShipType.HasValue || ((((((OwnershipTypeEnum) curMtg.MortgagedProperty.OwnerShipType) != OwnershipTypeEnum.Investment) || refiVariables[i].Investment) && ((((OwnershipTypeEnum) curMtg.MortgagedProperty.OwnerShipType) != OwnershipTypeEnum.Primary_Residence) || refiVariables[i].PrimaryResidence)) && ((((OwnershipTypeEnum) curMtg.MortgagedProperty.OwnerShipType) != OwnershipTypeEnum.Second_Home) || refiVariables[i].SecondHome)))) && ((!curMtg.MortgagedProperty.PropertyTypeApp.HasValue || ((((((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Modular_Manufactured) || refiVariables[i].Manufactured) && ((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Single_Familly_Residence_1_unit) || refiVariables[i].SFR)) && (((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Multi_Familly_Residence_2_units) || refiVariables[i].MultiUnits) && ((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Multi_Familly_Residence_3_units) || refiVariables[i].MultiUnits))) && ((((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Multi_Familly_Residence_4_units) || refiVariables[i].MultiUnits) && ((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Multi_Familly_Residence_5_Plus) || refiVariables[i].MultiUnits)) && (((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Condo) || refiVariables[i].Condo) && ((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.MobileHomeWithLand) || refiVariables[i].MobileHome)))) && ((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Townhome) || refiVariables[i].TownHome))) && (((((((curMtg.MortgageApplicant.Veteran != YesNoAns.No) || (refiVariables[i].LoanType != LoanTypeEnum.VA)) && ((curMtg.MortgagedProperty.Rural != YesNoAns.No) || (refiVariables[i].LoanType != LoanTypeEnum.USRDA))) && ((((LoanTypeEnum) curMtg.LoanType) == LoanTypeEnum.FHA) || (refiVariables[i].LoanType != LoanTypeEnum.FHA_Streamline))) && ((((LoanTypeEnum) curMtg.LoanType) == LoanTypeEnum.VA) || (refiVariables[i].LoanType != LoanTypeEnum.VA_IRRL))) && (((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Multi_Familly_Residence_2_units) || (refiVariables[i].MaxNumberOfUnits == 2)) && ((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Multi_Familly_Residence_3_units) || (refiVariables[i].MaxNumberOfUnits == 3)))) && (((((PropertyTypeEnum) curMtg.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Multi_Familly_Residence_4_units) || (refiVariables[i].MaxNumberOfUnits == 4)) && ((int)curMtg.MortgageApplicant.CreditScoreEstimate.Value >= int.Parse(refiVariables[i].CreditScoreRange))))))
                {
                    decimal? nullable28;
                    double? nullable30;
                    bool flag = curMtg.MortgageApplicant.Has2ndMortgage > HaveSecondMortgageEnum.No;
                    bool flag2 = ((YesNoAns) curMtg.MortgageApplicant.PayOff2ndMortgage) == YesNoAns.Yes;
                    SecondMortgage secondMortgage = new SecondMortgage();
                    if (flag)
                    {
                        secondMortgage = curMtg.MortgageApplicant.SecondMortgage;
                    }
                    double maxLoanAmount = refiVariables[i].MaxLoanAmount;
                    RefiOption item = new RefiOption {
                        VarNum = refiVariables[i].OptionNumber,
                        NewMI_MonthlyAmount = 0.00M,
                        InterestRate = new double?(refiVariables[i].NewInterestRate)
                    };
                    double num2 = refiVariables[i].MiFactor / 6.0;
                    double upfrontMI = refiVariables[i].UpfrontMI;
                    double num4 = (0.05 * refiVariables[i].NewInterestRate) / 360.0;
                    item.OptionName = refiVariables[i].ScheduleName;
                    item.DatePrepared = new DateTime?(DateTime.Today);
                    item.LoanBalance = curMtg.Balance;
                    item.CurrentInterestRate = new double?(curMtg.InterestRate);
                    item.PreparedFor = curMtg.MortgageApplicant.FirstName + " " + curMtg.MortgageApplicant.LastName;
                    item.MonthlyPayment = curMtg.MonthlyPayment;
                    decimal? monthlyPayment = curMtg.MonthlyPayment;
                    decimal? monthlyMortgageInsurance = curMtg.MonthlyMortgageInsurance;
                    item.OldMonthlyPaymentPrincipalInterest = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                    if (curMtg.PymtIncludesTaxes == YesNoAns.Yes)
                    {
                        monthlyPayment = item.OldMonthlyPaymentPrincipalInterest;
                        monthlyMortgageInsurance = curMtg.YearlyPropertyTaxes;
                        monthlyMortgageInsurance = monthlyMortgageInsurance.HasValue ? new decimal?(monthlyMortgageInsurance.GetValueOrDefault() / 12M) : ((decimal?) (nullable28 = null));
                        item.OldMonthlyPaymentPrincipalInterest = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                    }
                    if (curMtg.PymtIncludesHomeownersInsurance == YesNoAns.Yes)
                    {
                        monthlyPayment = item.OldMonthlyPaymentPrincipalInterest;
                        monthlyMortgageInsurance = curMtg.YearlyHomeInsurancePayment;
                        monthlyMortgageInsurance = monthlyMortgageInsurance.HasValue ? new decimal?(monthlyMortgageInsurance.GetValueOrDefault() / 12M) : ((decimal?) (nullable28 = null));
                        item.OldMonthlyPaymentPrincipalInterest = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                    }
                    item.TermInMonths = new int?(EnumNorm.TermToInt(curMtg.Term) * 12);
                    item.PercentCharge = new double?(refiVariables[i].OriginationPercent * 100.0);
                    item.ProcessingFee = new decimal?(refiVariables[i].ProcessingFee);
                    item.UnderwritingFee = new decimal?(refiVariables[i].UnderwritingFee);
                    item.CreditReportFee = new decimal?(refiVariables[i].CreditReportFee);
                    item.TaxServiceFee = refiVariables[i].TaxServiceFee;
                    item.FloodCertificationFee = refiVariables[i].FloodCertificationFee;
                    item.AppraisalFee = new decimal?(refiVariables[i].AppraisalFee);
                    item.ClosingEscrowFee = new decimal?(refiVariables[i].ClosingEscrowFee);
                    item.EndorsementsReconveyanceFee = new decimal?(refiVariables[i].EndorsementsReconveyanceFee);
                    item.MortgageRecordingCharges = new decimal?(refiVariables[i].MortgageRecordingfee);
                    item.NumberOfDays = 5;
                    item.DebtToBePaidOff = curMtg.Balance;
                    item.DateOfOrgination = curMtg.OriginationDate;
                    item.AmountOfNewPaymentPlusMonthlySavings = item.OldMonthlyPaymentPrincipalInterest;
                    item.TotalBalanceOfDebtToConsolidate = curMtg.MortgageApplicant.TotalBalanceOfDebtToConsolidate;
                    item.InterestRateSavings = item.CurrentInterestRate - item.InterestRate;
                    double? interestRateSavings = item.InterestRateSavings;
                    if ((interestRateSavings.GetValueOrDefault() > 0.0) && interestRateSavings.HasValue)
                    {
                        item.ShowInterestSaving = true;
                    }
                    if (flag && flag2)
                    {
                        double num30 = ((((double) curMtg.Balance.Value) * curMtg.InterestRate) + (((double) secondMortgage.Balance.Value) * secondMortgage.InterestRate)) / (((double) curMtg.Balance.Value) + ((double) secondMortgage.Balance.Value));
                        interestRateSavings = item.InterestRate;
                        item.BlendedRateSavings = interestRateSavings.HasValue ? new double?(num30 - interestRateSavings.GetValueOrDefault()) : ((double?) (nullable30 = null));
                        interestRateSavings = item.BlendedRateSavings;
                        if ((interestRateSavings.GetValueOrDefault() > 0.0) && interestRateSavings.HasValue)
                        {
                            item.ShowBlendedRate = true;
                            item.ShowInterestSaving = false;
                        }
                    }
                    item.MonthsPaidRemaining = new int?(GetRemainingMonthsTobePaid(item.DateOfOrgination.Value, (int)curMtg.Term.Value));
                    if (flag && flag2)
                    {
                        item.MonthsPaidRemaining2nd = GetRemainingMonthsTobePaid(secondMortgage.OriginationDate.Value, (int)secondMortgage.SecondMortgageTerm.Value);
                    }
                    item.MI_MonthlyAmount = curMtg.MonthlyMortgageInsurance;
                    monthlyPayment = curMtg.YearlyPropertyTaxes;
                    item.monthlyTaxes = monthlyPayment.HasValue ? new decimal?(monthlyPayment.GetValueOrDefault() / 12.00M) : ((decimal?) (monthlyMortgageInsurance = null));
                    item.CT_MonthlyAmount = item.monthlyTaxes;
                    monthlyPayment = item.monthlyTaxes;
                    decimal numofMonthstoEscrowTaxes = refiVariables[i].NumofMonthstoEscrowTaxes;
                    item.CountyPropertyTaxReserves = monthlyPayment.HasValue ? new decimal?(monthlyPayment.GetValueOrDefault() * numofMonthstoEscrowTaxes) : ((decimal?) (monthlyMortgageInsurance = null));
                    item.CT_MonthsReserves = new decimal?(refiVariables[i].NumofMonthstoEscrowTaxes);
                    item.HI_MonthlyAmount = curMtg.YearlyHomeInsurancePayment / 12.00M;
                    item.Insurance = item.HI_MonthlyAmount;
                    item.HI_MonthsReserves = new decimal?(refiVariables[i].NumofMonthstoEscrowHazardInsurance);
                    monthlyPayment = item.HI_MonthlyAmount;
                    monthlyMortgageInsurance = item.HI_MonthsReserves;
                    item.HazardInsuranceReserves = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() * monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                    item.TermInYears = new int?(refiVariables[i].newTermInYears);
                    item.OriginationChargesPercent = new double?(refiVariables[i].OriginationPercent);
                    item.TotalFixedFees = new decimal?((((((((refiVariables[i].ProcessingFee + refiVariables[i].UnderwritingFee) + refiVariables[i].AppraisalFee) + refiVariables[i].CreditReportFee) + refiVariables[i].ClosingEscrowFee) + refiVariables[i].EndorsementsReconveyanceFee) + refiVariables[i].MortgageRecordingfee) + refiVariables[i].TaxServiceFee) + refiVariables[i].FloodCertificationFee);
                    decimal num6 = curMtg.MortgageApplicant.CashOutAmountRequested.Value;
                    if (num6 > 0M)
                    {
                        item.ShowCashout = true;
                    }
                    if (curMtg.MortgageApplicant.LoanTypeRequested == LoanTypeRequestedEnum.CashOutMortgage)
                    {
                        if (((double) item.TotalBalanceOfDebtToConsolidate.Value) > refiVariables[i].MaxCashOut)
                        {
                            continue;
                        }
                        if ((((double) num6) + ((double) item.TotalBalanceOfDebtToConsolidate.Value)) > refiVariables[i].MaxCashOut)
                        {
                            num6 = ((decimal) refiVariables[i].MaxCashOut) - item.TotalBalanceOfDebtToConsolidate.Value;
                        }
                        if (flag && flag2)
                        {
                            if (refiVariables[i].LoanType == LoanTypeEnum.VA)
                            {
                                TimeSpan span = (TimeSpan) (DateTime.Now - secondMortgage.OriginationDate.Value);
                                if (span.Days < 330)
                                {
                                    monthlyPayment = secondMortgage.Balance;
                                    numofMonthstoEscrowTaxes = (decimal) refiVariables[i].MaxCashOut;
                                    if ((monthlyPayment.GetValueOrDefault() > numofMonthstoEscrowTaxes) && monthlyPayment.HasValue)
                                    {
                                        continue;
                                    }
                                    if (num6 > (((decimal) refiVariables[i].MaxCashOut) - secondMortgage.Balance.Value))
                                    {
                                        num6 = ((decimal) refiVariables[i].MaxCashOut) - secondMortgage.Balance.Value;
                                    }
                                }
                            }
                            else if (curMtg.MortgageApplicant.Has2ndMortgage == HaveSecondMortgageEnum.YesForCash)
                            {
                                monthlyPayment = secondMortgage.Balance;
                                numofMonthstoEscrowTaxes = (decimal) refiVariables[i].MaxCashOut;
                                if ((monthlyPayment.GetValueOrDefault() > numofMonthstoEscrowTaxes) && monthlyPayment.HasValue)
                                {
                                    continue;
                                }
                                if (num6 > (((decimal) refiVariables[i].MaxCashOut) - secondMortgage.Balance.Value))
                                {
                                    num6 = ((decimal) refiVariables[i].MaxCashOut) - secondMortgage.Balance.Value;
                                }
                            }
                        }
                    }
                    else if (curMtg.MortgageApplicant.LoanTypeRequested == LoanTypeRequestedEnum.DebtConsolidationPayOffCreditors)
                    {
                        item.TotalBalanceOfDebtToConsolidate = curMtg.MortgageApplicant.TotalBalanceOfDebtToConsolidate;
                        item.ShowDebtConsal = true;
                        if (num6 > (((decimal) refiVariables[i].MaxCashOut) - item.TotalBalanceOfDebtToConsolidate.Value))
                        {
                            num6 = ((decimal) refiVariables[i].MaxCashOut) - item.TotalBalanceOfDebtToConsolidate.Value;
                            if (num6 < 0M)
                            {
                                num6 = 0M;
                            }
                        }
                        if (flag && flag2)
                        {
                            if (refiVariables[i].LoanType == LoanTypeEnum.VA)
                            {
                                TimeSpan span2 = (TimeSpan) (DateTime.Now - secondMortgage.OriginationDate.Value);
                                if (span2.Days < 330)
                                {
                                    monthlyPayment = secondMortgage.Balance;
                                    numofMonthstoEscrowTaxes = (decimal) refiVariables[i].MaxCashOut;
                                    if ((monthlyPayment.GetValueOrDefault() > numofMonthstoEscrowTaxes) && monthlyPayment.HasValue)
                                    {
                                        continue;
                                    }
                                    if (num6 > ((((decimal) refiVariables[i].MaxCashOut) - secondMortgage.Balance.Value) - item.TotalBalanceOfDebtToConsolidate.Value))
                                    {
                                        num6 = (((decimal) refiVariables[i].MaxCashOut) - secondMortgage.Balance.Value) - item.TotalBalanceOfDebtToConsolidate.Value;
                                    }
                                    monthlyPayment = item.TotalBalanceOfDebtToConsolidate;
                                    monthlyMortgageInsurance = secondMortgage.Balance;
                                    numofMonthstoEscrowTaxes = ((decimal) refiVariables[i].MaxCashOut) - monthlyMortgageInsurance.Value;
                                    if ((monthlyPayment.GetValueOrDefault() > numofMonthstoEscrowTaxes) && monthlyPayment.HasValue)
                                    {
                                        item.TotalBalanceOfDebtToConsolidate = new decimal?(((decimal) refiVariables[i].MaxCashOut) - secondMortgage.Balance.Value);
                                    }
                                }
                            }
                            else if (curMtg.MortgageApplicant.Has2ndMortgage == HaveSecondMortgageEnum.YesForCash)
                            {
                                monthlyPayment = secondMortgage.Balance;
                                numofMonthstoEscrowTaxes = (decimal) refiVariables[i].MaxCashOut;
                                if ((monthlyPayment.GetValueOrDefault() > numofMonthstoEscrowTaxes) && monthlyPayment.HasValue)
                                {
                                    continue;
                                }
                                if (num6 > ((((decimal) refiVariables[i].MaxCashOut) - secondMortgage.Balance.Value) - item.TotalBalanceOfDebtToConsolidate.Value))
                                {
                                    num6 = (((decimal) refiVariables[i].MaxCashOut) - secondMortgage.Balance.Value) - item.TotalBalanceOfDebtToConsolidate.Value;
                                }
                                monthlyPayment = item.TotalBalanceOfDebtToConsolidate;
                                monthlyMortgageInsurance = secondMortgage.Balance;
                                numofMonthstoEscrowTaxes = ((decimal) refiVariables[i].MaxCashOut) - monthlyMortgageInsurance.Value;
                                if ((monthlyPayment.GetValueOrDefault() > numofMonthstoEscrowTaxes) && monthlyPayment.HasValue)
                                {
                                    item.TotalBalanceOfDebtToConsolidate = new decimal?(((decimal) refiVariables[i].MaxCashOut) - secondMortgage.Balance.Value);
                                }
                            }
                        }
                        if (num6 <= 0M)
                        {
                            monthlyPayment = item.TotalBalanceOfDebtToConsolidate;
                            numofMonthstoEscrowTaxes = (decimal) refiVariables[i].MaxCashOut;
                            if ((monthlyPayment.GetValueOrDefault() > numofMonthstoEscrowTaxes) && monthlyPayment.HasValue)
                            {
                                item.TotalBalanceOfDebtToConsolidate = new decimal?((decimal) refiVariables[i].MaxCashOut);
                            }
                        }
                    }
                    double num11 = (((((((refiVariables[i].OriginationPercent - refiVariables[i].LenderCreditPercent) + refiVariables[i].TitleInusrancePercent) + refiVariables[i].IntangibleTaxPercent) + refiVariables[i].StateTaxPercent) + refiVariables[i].DiscountPercent) + num4) + num2) + upfrontMI;
                    item.MI_MonthsReserves = 2;
                    decimal num12 = curMtg.Balance.Value;
                    if (flag && flag2)
                    {
                        if (refiVariables[i].LoanType == LoanTypeEnum.VA)
                        {
                            TimeSpan span3 = (TimeSpan) (DateTime.Now - secondMortgage.OriginationDate.Value);
                            if (span3.Days < 330)
                            {
                                monthlyPayment = secondMortgage.Balance;
                                numofMonthstoEscrowTaxes = (decimal) refiVariables[i].MaxCashOut;
                                if ((monthlyPayment.GetValueOrDefault() > numofMonthstoEscrowTaxes) && monthlyPayment.HasValue)
                                {
                                    continue;
                                }
                            }
                        }
                        if (curMtg.MortgageApplicant.Has2ndMortgage == HaveSecondMortgageEnum.YesForCash)
                        {
                            monthlyPayment = secondMortgage.Balance;
                            numofMonthstoEscrowTaxes = (decimal) refiVariables[i].MaxCashOut;
                            if ((monthlyPayment.GetValueOrDefault() > numofMonthstoEscrowTaxes) && monthlyPayment.HasValue)
                            {
                                continue;
                            }
                        }
                        item.ShowSecondMtg = true;
                        item.SecondMtgBalance = secondMortgage.Balance;
                        num12 += secondMortgage.Balance.Value;
                    }
                    numofMonthstoEscrowTaxes = num12;
                    monthlyPayment = numofMonthstoEscrowTaxes + item.TotalFixedFees;
                    monthlyMortgageInsurance = item.HazardInsuranceReserves;
                    nullable28 = ((monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null))) + item.CountyPropertyTaxReserves;
                    double num15 = (double) nullable28.Value;
                    double num16 = (refiVariables[i].MaxLTV / 100.0) * ((double) curMtg.MortgagedProperty.EstimatedMarketValue.Value);
                    double num17 = ((1.0 - num11) * num16) - num15;
                    numofMonthstoEscrowTaxes = (decimal) num17;
                    decimal num32 = num6;
                    monthlyPayment = item.TotalBalanceOfDebtToConsolidate;
                    monthlyPayment = monthlyPayment.HasValue ? new decimal?(num32 + monthlyPayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsurance = null));
                    if ((numofMonthstoEscrowTaxes < monthlyPayment.GetValueOrDefault()) && monthlyPayment.HasValue)
                    {
                        num6 = ((decimal) num17) - item.TotalBalanceOfDebtToConsolidate.Value;
                    }
                    monthlyPayment = item.TotalBalanceOfDebtToConsolidate;
                    numofMonthstoEscrowTaxes = (decimal) num17;
                    if ((monthlyPayment.GetValueOrDefault() > numofMonthstoEscrowTaxes) && monthlyPayment.HasValue)
                    {
                        item.TotalBalanceOfDebtToConsolidate = new decimal?((decimal) num17);
                    }
                    if (num17 < 0.0)
                    {
                        num6 = 0M;
                    }
                    if (num6 < 0M)
                    {
                        num6 = 0M;
                    }
                    monthlyPayment = item.TotalBalanceOfDebtToConsolidate;
                    if ((monthlyPayment.GetValueOrDefault() < 0M) && monthlyPayment.HasValue)
                    {
                        item.TotalBalanceOfDebtToConsolidate = 0;
                    }
                    item.Cashout = num6;
                    numofMonthstoEscrowTaxes = num12;
                    monthlyPayment = numofMonthstoEscrowTaxes + item.TotalFixedFees;
                    monthlyMortgageInsurance = item.HazardInsuranceReserves;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                    monthlyMortgageInsurance = item.CountyPropertyTaxReserves;
                    monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                    numofMonthstoEscrowTaxes = num6;
                    nullable28 = (monthlyPayment.HasValue ? new decimal?(monthlyPayment.GetValueOrDefault() + numofMonthstoEscrowTaxes) : null) + item.TotalBalanceOfDebtToConsolidate;
                    double num18 = (double) nullable28.Value;
                    if ((refiVariables[i].MortgageProgramOption.Trim() == "HARP_FannieBefore2009") || (refiVariables[i].MortgageProgramOption.Trim() == "HARP_FredieBefore2009"))
                    {
                        num18 += ((double) curMtg.MonthlyMortgageInsurance.Value) * 2.0;
                    }
                    double num19 = num18 / (1.0 - num11);
                    item.LoanAmount = new decimal?((decimal) num19);
                    numofMonthstoEscrowTaxes = (int) num16;
                    if (numofMonthstoEscrowTaxes >= item.LoanAmount)
                    {
                        double num20 = (num19 / ((double) curMtg.MortgagedProperty.EstimatedMarketValue.Value)) * 100.0;
                        if (num20 >= 80.0)
                        {
                            if ((refiVariables[i].MortgageProgramOption.Trim() == "HARP_FannieBefore2009") || (refiVariables[i].MortgageProgramOption.Trim() == "HARP_FredieBefore2009"))
                            {
                                item.NewMI_MonthlyAmount = curMtg.MonthlyMortgageInsurance;
                                item.PMI_MIP_VA_FFReserves = item.NewMI_MonthlyAmount * 2.00M;
                                item.MI_Upfront_Fee = 0.00M;
                            }
                            else
                            {
                                item.MI_Upfront_Fee = new decimal?((decimal) (num19 * upfrontMI));
                                item.PMI_MIP_VA_FFReserves = new decimal?((decimal) (num19 * num2));
                                item.NewMI_MonthlyAmount = item.PMI_MIP_VA_FFReserves / 2.00M;
                            }
                        }
                        else
                        {
                            item.NewMI_MonthlyAmount = 0.00M;
                            item.MI_Upfront_Fee = new decimal?((decimal) (num19 * upfrontMI));
                            item.PMI_MIP_VA_FFReserves = 0;
                        }
                        double mortgageAmount = (double) item.LoanAmount.Value;
                        item.NewMonthlyPaymentPrincipalInterest = 0.00M;
                        item.NewMonthlyPaymentPrincipalInterest = new decimal?((decimal) GetPayment(mortgageAmount, item.InterestRate.Value, (double) item.TermInYears.Value));
                        monthlyPayment = item.OldMonthlyPaymentPrincipalInterest;
                        monthlyMortgageInsurance = item.MI_MonthlyAmount;
                        item.TotalOldPrincipalInterestPaymentWithMI = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyPayment = item.NewMonthlyPaymentPrincipalInterest;
                        monthlyMortgageInsurance = item.NewMI_MonthlyAmount;
                        item.TotalNewPrincipalInterestPaymentWithMI = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyPayment = curMtg.MortgageApplicant.TotalMonthlyAmountOfDebtPaymentsToConsolidate;
                        monthlyMortgageInsurance = item.TotalBalanceOfDebtToConsolidate;
                        monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() * monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyMortgageInsurance = curMtg.MortgageApplicant.TotalBalanceOfDebtToConsolidate;
                        decimal? nullable = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() / monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyPayment = item.TotalOldPrincipalInterestPaymentWithMI;
                        monthlyMortgageInsurance = item.TotalNewPrincipalInterestPaymentWithMI;
                        monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyMortgageInsurance = nullable;
                        item.MonthlySavings = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyPayment = item.TotalNewPrincipalInterestPaymentWithMI;
                        monthlyMortgageInsurance = item.MonthlySavings;
                        item.NewPaymentPlusMonthlySavings = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        if (((YesNoAns) curMtg.MortgageApplicant.PayOff2ndMortgage) == YesNoAns.Yes)
                        {
                            monthlyPayment = item.TotalOldPrincipalInterestPaymentWithMI;
                            monthlyMortgageInsurance = curMtg.MortgageApplicant.SecondMortgage.MonthlyPayment;
                            monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                            monthlyMortgageInsurance = item.TotalNewPrincipalInterestPaymentWithMI;
                            monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                            monthlyMortgageInsurance = nullable;
                            item.MonthlySavings = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                            monthlyPayment = item.TotalNewPrincipalInterestPaymentWithMI;
                            monthlyMortgageInsurance = item.MonthlySavings;
                            item.NewPaymentPlusMonthlySavings = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        }
                        monthlyPayment = item.MonthlySavings;
                        if ((monthlyPayment.GetValueOrDefault() > 0M) && monthlyPayment.HasValue)
                        {
                            item.ShowMonthlySaving = true;
                        }
                        monthlyPayment = item.OldMonthlyPaymentPrincipalInterest;
                        int? monthsPaidRemaining = item.MonthsPaidRemaining;
                        item.OldTotalAmountOfAllPaymentsToBeMade = (monthlyPayment.HasValue & monthsPaidRemaining.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() * monthsPaidRemaining.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsurance = null));
                        if (flag && flag2)
                        {
                            monthlyPayment = item.OldTotalAmountOfAllPaymentsToBeMade;
                            monthlyMortgageInsurance = secondMortgage.MonthlyPayment;
                            numofMonthstoEscrowTaxes = item.MonthsPaidRemaining2nd;
                            monthlyMortgageInsurance = monthlyMortgageInsurance.HasValue ? new decimal?(monthlyMortgageInsurance.GetValueOrDefault() * numofMonthstoEscrowTaxes) : ((decimal?) (nullable28 = null));
                            item.OldTotalAmountOfAllPaymentsToBeMade = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        }
                        item.NewTotalAmountOfAllPaymentsToBeMade = item.NewMonthlyPaymentPrincipalInterest * item.TermInMonths;
                        monthlyPayment = item.OldTotalAmountOfAllPaymentsToBeMade;
                        monthlyMortgageInsurance = item.NewTotalAmountOfAllPaymentsToBeMade;
                        item.NetSavingsFromOldLoanToNewLoan = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyPayment = item.NetSavingsFromOldLoanToNewLoan;
                        if ((monthlyPayment.GetValueOrDefault() > 0M) && monthlyPayment.HasValue)
                        {
                            item.ShowNetSaving = true;
                        }
                        item.CreditPoints = new double?(refiVariables[i].LenderCreditPercent);
                        monthlyPayment = item.LoanAmount;
                        numofMonthstoEscrowTaxes = (decimal) item.CreditPoints.Value;
                        item.Credit = monthlyPayment.HasValue ? new decimal?(monthlyPayment.GetValueOrDefault() * numofMonthstoEscrowTaxes) : ((decimal?) (monthlyMortgageInsurance = null));
                        monthlyPayment = item.LoanAmount;
                        numofMonthstoEscrowTaxes = (decimal) refiVariables[i].OriginationPercent;
                        monthlyPayment = monthlyPayment.HasValue ? new decimal?(monthlyPayment.GetValueOrDefault() * numofMonthstoEscrowTaxes) : null;
                        monthlyMortgageInsurance = item.Credit;
                        item.AdjustedLoanOriginationFee = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        numofMonthstoEscrowTaxes = (decimal) refiVariables[i].TitleInusrancePercent;
                        monthlyPayment = item.LoanAmount;
                        item.LenderTitleInsurance = monthlyPayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * monthlyPayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsurance = null));
                        item.DiscountPoints = new double?(refiVariables[i].DiscountPercent * 100.0);
                        numofMonthstoEscrowTaxes = (decimal) refiVariables[i].DiscountPercent;
                        monthlyPayment = item.LoanAmount;
                        item.Discount = monthlyPayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * monthlyPayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsurance = null));
                        numofMonthstoEscrowTaxes = (decimal) refiVariables[i].IntangibleTaxPercent;
                        monthlyPayment = item.LoanAmount;
                        item.IntangibleTax = monthlyPayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * monthlyPayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsurance = null));
                        numofMonthstoEscrowTaxes = (decimal) refiVariables[i].StateTaxPercent;
                        monthlyPayment = item.LoanAmount;
                        item.StateTax = monthlyPayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * monthlyPayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsurance = null));
                        numofMonthstoEscrowTaxes = (decimal) num4;
                        item.DailyInterestCharges = numofMonthstoEscrowTaxes * item.LoanAmount;
                        monthlyPayment = item.HazardInsuranceReserves;
                        monthlyMortgageInsurance = item.CountyPropertyTaxReserves;
                        monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyMortgageInsurance = item.PMI_MIP_VA_FFReserves;
                        monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyMortgageInsurance = item.DailyInterestCharges;
                        item.EstimatedPrepaidsReserves = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyPayment = item.ProcessingFee;
                        monthlyMortgageInsurance = item.UnderwritingFee;
                        monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyMortgageInsurance = item.AppraisalFee;
                        monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyMortgageInsurance = item.CreditReportFee;
                        monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyMortgageInsurance = item.ClosingEscrowFee;
                        monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyMortgageInsurance = item.EndorsementsReconveyanceFee;
                        monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyMortgageInsurance = item.MortgageRecordingCharges;
                        monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyMortgageInsurance = item.AdjustedLoanOriginationFee;
                        monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyMortgageInsurance = item.LenderTitleInsurance;
                        monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyMortgageInsurance = item.Discount;
                        monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyMortgageInsurance = item.IntangibleTax;
                        monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyMortgageInsurance = item.StateTax;
                        monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyMortgageInsurance = item.MI_Upfront_Fee;
                        monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        numofMonthstoEscrowTaxes = item.TaxServiceFee;
                        monthlyPayment = monthlyPayment.HasValue ? new decimal?(monthlyPayment.GetValueOrDefault() + numofMonthstoEscrowTaxes) : ((decimal?) (monthlyMortgageInsurance = null));
                        numofMonthstoEscrowTaxes = item.FloodCertificationFee;
                        item.EstClosingCosts = monthlyPayment.HasValue ? new decimal?(monthlyPayment.GetValueOrDefault() + numofMonthstoEscrowTaxes) : null;
                        monthlyPayment = item.LoanBalance;
                        monthlyMortgageInsurance = item.EstClosingCosts;
                        monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyMortgageInsurance = item.EstimatedPrepaidsReserves;
                        item.TotalAmountNeeded = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyPayment = item.LoanAmount;
                        monthlyMortgageInsurance = item.TotalAmountNeeded;
                        item.EstimatedFundsNeeded = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyPayment = item.EstClosingCosts;
                        monthlyMortgageInsurance = item.MonthlySavings;
                        item.CostSavingsBreakEvenAnalysis = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() / monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        double num23 = (double) item.LoanAmount.Value;
                        interestRateSavings = item.InterestRate;
                        double num24 = (interestRateSavings.Value / 100.0) / 12.0;
                        double num25 = (double) curMtg.MonthlyPayment.Value;
                        if (((YesNoAns) curMtg.MortgageApplicant.PayOff2ndMortgage) == YesNoAns.Yes)
                        {
                            num25 += (double) curMtg.MortgageApplicant.SecondMortgage.MonthlyPayment.Value;
                        }
                        double a = -Math.Log(1.0 - ((num23 / num25) * num24)) / Math.Log(1.0 + num24);
                        item.NumberofPaymentstoMaturity = new int?((int) Math.Ceiling(a));
                        monthsPaidRemaining = item.NumberofPaymentstoMaturity;
                        float? nullable35 = monthsPaidRemaining.HasValue ? new float?(((float) monthsPaidRemaining.GetValueOrDefault()) / 12f) : null;
                        item.YearsRequiredToMaturity = nullable35.HasValue ? new double?((double) nullable35.GetValueOrDefault()) : null;
                        item.AcceleratedPayoffTotalAmountOfAllPayments = item.NewPaymentPlusMonthlySavings * item.NumberofPaymentstoMaturity;
                        monthlyPayment = item.OldTotalAmountOfAllPaymentsToBeMade;
                        monthlyMortgageInsurance = item.AcceleratedPayoffTotalAmountOfAllPayments;
                        item.TotalSavingsFromOldMortgageToNewMortgage = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthsPaidRemaining = item.MonthsPaidRemaining;
                        int? numberofPaymentstoMaturity = item.NumberofPaymentstoMaturity;
                        monthsPaidRemaining = (monthsPaidRemaining.HasValue & numberofPaymentstoMaturity.HasValue) ? new int?(monthsPaidRemaining.GetValueOrDefault() - numberofPaymentstoMaturity.GetValueOrDefault()) : ((int?) null);
                        item.MonthlyPaymentsEliminated = monthsPaidRemaining.HasValue ? new decimal?(monthsPaidRemaining.GetValueOrDefault()) : null;
                        monthlyPayment = item.OldMonthlyPaymentPrincipalInterest;
                        monthlyMortgageInsurance = item.MI_MonthlyAmount;
                        item.TotalOldPrincipalInterestPaymentWithMI = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        monthlyPayment = item.NewMonthlyPaymentPrincipalInterest;
                        monthlyMortgageInsurance = item.NewMI_MonthlyAmount;
                        item.TotalNewPrincipalInterestPaymentWithMI = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable28 = null));
                        double loanAmount = ((((((((double) item.LoanAmount.Value) - ((double) item.AdjustedLoanOriginationFee.Value)) - ((double) item.Discount.Value)) - ((double) item.ProcessingFee.Value)) - ((double) item.UnderwritingFee.Value)) - ((double) item.ClosingEscrowFee.Value)) - ((double) item.MI_Upfront_Fee.Value)) - ((double) item.DailyInterestCharges.Value);
                        double payment = 0.0;
                        if (refiVariables[i].LoanType == LoanTypeEnum.FHA)
                        {
                            payment = ((((((double) item.NewMonthlyPaymentPrincipalInterest.Value) + ((double) item.NewMI_MonthlyAmount.Value)) * refiVariables[i].MiDurationYears) * 12.0) + ((((double) item.NewMonthlyPaymentPrincipalInterest.Value) * (30.0 - refiVariables[i].MiDurationYears)) * 12.0)) / 360.0;
                        }
                        else
                        {
                            int num29 = getMILTV(loanAmount, (double) item.TermInMonths.Value, item.InterestRate.Value, (double) item.NewMonthlyPaymentPrincipalInterest.Value, 78.0, (double) curMtg.MortgagedProperty.EstimatedMarketValue.Value);
                            payment = (((((double) item.NewMonthlyPaymentPrincipalInterest.Value) + ((double) item.NewMI_MonthlyAmount.Value)) * num29) + (((double) item.NewMonthlyPaymentPrincipalInterest.Value) * (((double) item.TermInMonths.Value) - num29))) / 360.0;
                        }
                        item.APR = new double?(GetAPR(loanAmount, item.InterestRate.Value / 1200.0, (double) item.TermInMonths.Value, payment));
                        interestRateSavings = item.DiscountPoints;
                        item.DiscountPoints = interestRateSavings.HasValue ? new double?(interestRateSavings.GetValueOrDefault() / 100.0) : ((double?) (nullable30 = null));
                        item.PercentCharge /= 100.0;
                        item.ShowBlendedRate = true;
                        item.ShowCashout = true;
                        item.ShowDebtConsal = true;
                        item.ShowInterestSaving = true;
                        item.ShowMonthlySaving = true;
                        item.ShowNetSaving = true;
                        item.ShowSecondMtg = true;
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        internal static List<RefiOption> GetConvHarpOptions(CcsLocalDbContext db, Application app)
        {
            if (!RateSheetLoaded)
            {
                RateSheetProcessor.LoadFromSerializedFile(Path.Combine(HttpContext.Current.Server.MapPath("~/Content/DataFiles"), "Plaza_JAX_Rates.phz"));
            }
            List<RefiOption> list = new List<RefiOption>();
            List<Variable> refiVariables = GetRefiVariables();
            Variables v = new Variables();
            for (int i = 0; i < refiVariables.Count; i++)
            {
                int? maxNumberOfUnits;
                int? numberofPaymentstoMaturity;
                int? nullable4;
                decimal? nullable12;
                LoanTypeRequestedEnum loanTypeRequested = app.LoanTypeRequested;
                if (((((((app.LoanTypeRequested == LoanTypeRequestedEnum.CashOutMortgage) && !refiVariables[i].RefiCashout) || ((app.LoanTypeRequested == LoanTypeRequestedEnum.DebtConsolidationPayOffCreditors) && !refiVariables[i].RefiCashout)) || ((((app.LoanTypeRequested == LoanTypeRequestedEnum.RateAndTermRefiLowerPayment) && !refiVariables[i].RateTerm) && !refiVariables[i].RefiCashout) || (((app.LoanTypeRequested == LoanTypeRequestedEnum.RateAndTermRefiShorterTerm) && !refiVariables[i].RateTerm) && !refiVariables[i].RefiCashout))) || ((((app.OwnerShipType == OwnershipTypeEnum.Investment) && !refiVariables[i].Investment) || ((app.OwnerShipType == OwnershipTypeEnum.Primary_Residence) && !refiVariables[i].PrimaryResidence)) || (((app.OwnerShipType == OwnershipTypeEnum.Second_Home) && !refiVariables[i].SecondHome) || ((app.PropertyType == PropertyTypeEnum.Modular_Manufactured) && !refiVariables[i].Manufactured)))) || (((((app.PropertyType == PropertyTypeEnum.Single_Familly_Residence_1_unit) && !refiVariables[i].SFR) || ((app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_2_units) && !refiVariables[i].MultiUnits)) || (((app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_3_units) && !refiVariables[i].MultiUnits) || ((app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_4_units) && !refiVariables[i].MultiUnits))) || ((((app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_5_Plus) && !refiVariables[i].MultiUnits) || ((app.PropertyType == PropertyTypeEnum.Condo) && !refiVariables[i].Condo)) || (((app.PropertyType == PropertyTypeEnum.MobileHomeWithLand) && !refiVariables[i].MobileHome) || ((app.PropertyType == PropertyTypeEnum.Townhome) && !refiVariables[i].TownHome))))) || ((((app.Veteran == YesNoAns.No) && (refiVariables[i].LoanType == LoanTypeEnum.VA)) || ((app.RuralProperty == YesNoAns.No) && (refiVariables[i].LoanType == LoanTypeEnum.USRDA))) || (((app.LoanType != LoanTypeEnum.FHA) && (refiVariables[i].LoanType == LoanTypeEnum.FHA_Streamline)) || ((app.LoanType != LoanTypeEnum.VA) && (refiVariables[i].LoanType == LoanTypeEnum.VA_IRRL)))))
                {
                    continue;
                }
                if (app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_2_units)
                {
                    maxNumberOfUnits = refiVariables[i].MaxNumberOfUnits;
                    if ((maxNumberOfUnits.GetValueOrDefault() < 2) && maxNumberOfUnits.HasValue)
                    {
                        continue;
                    }
                }
                if (app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_3_units)
                {
                    numberofPaymentstoMaturity = refiVariables[i].MaxNumberOfUnits;
                    if ((numberofPaymentstoMaturity.GetValueOrDefault() < 3) && numberofPaymentstoMaturity.HasValue)
                    {
                        continue;
                    }
                }
                if (app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_4_units)
                {
                    nullable4 = refiVariables[i].MaxNumberOfUnits;
                    if ((nullable4.GetValueOrDefault() < 4) && nullable4.HasValue)
                    {
                        continue;
                    }
                }
                if (app.creditPulled && !app.NoMorgagesOnCredit)
                {
                    if (app.lates12Credit <= refiVariables[i].NumOf30LateAllowedIn12Mo)
                    {
                        goto Label_036A;
                    }
                    continue;
                }
                int num2 = ((int) app.DaysLate) - 1;
                if (num2 > refiVariables[i].NumOf30LateAllowedIn12Mo)
                {
                    continue;
                }
            Label_036A:
                if (app.creditPulled)
                {
                    if (app.CreditScore >= int.Parse(refiVariables[i].CreditScoreRange))
                    {
                        goto Label_03AE;
                    }
                    continue;
                }
                if ((int)app.CreditScoreEstimate < int.Parse(refiVariables[i].CreditScoreRange))
                {
                    continue;
                }
            Label_03AE:
                if (app.creditPulled)
                {
                    foreach (PublicRecord record in app.publicRecords)
                    {
                        if ((record.IsQualifiedChapter13(refiVariables[i].Bankruptcy.Value) && record.IsQualifiedChapter7(refiVariables[i].Bankruptcy.Value)) && record.IsQualifiedChapter13(refiVariables[i].Bankruptcy.Value))
                        {
                            record.IsQualifiedforeclosure(refiVariables[i].Foreclosure.Value);
                        }
                    }
                }
                else if ((((app.FiledBankruptcyType == FiledBankruptcyTypeEnum.Chapter13Discharged) && !qualify(app.BankruptcyDischargeDate.Value, refiVariables[i].Bankruptcy.Value)) || (app.FiledBankruptcyType == FiledBankruptcyTypeEnum.Chapter13RepaymentStillOpen)) || (((app.FiledBankruptcyType == FiledBankruptcyTypeEnum.Chapter7Discharged) && !qualify(app.BankruptcyDischargeDate.Value, refiVariables[i].Bankruptcy.Value)) || ((app.ForeclosuresShortSaleDeedinLieu > ForeclosuresShortSaleDeedinLieuEnum.No) && !qualify(app.ForeclosureShortSaleDeedinLieuDate.Value, refiVariables[i].Foreclosure.Value))))
                {
                    continue;
                }
                bool flag = app.Has2ndMortgage > HaveSecondMortgageEnum.No;
                bool flag2 = ((YesNoAns) app.PayOff2ndMortgage) == YesNoAns.Yes;
                HiBalance hibalance = GetMaxLoanLimit(db, app.Fips, refiVariables[i].LoanType, app.PropertyType, refiVariables[i].MaxLoanAmount);
                double maxLoanLimit = (double) hibalance.maxLoanLimit;
                if (maxLoanLimit == 0.0)
                {
                    maxLoanLimit = refiVariables[i].MaxLoanAmount;
                }
                RefiOption item = new RefiOption {
                    VarNum = refiVariables[i].OptionNumber,
                    NewMI_MonthlyAmount = 0.00M
                };
                double num4 = refiVariables[i].MiFactor / 6.0;
                double upfrontMI = refiVariables[i].UpfrontMI;
                double num6 = (0.05 * refiVariables[i].NewInterestRate) / 360.0;
                item.OptionName = refiVariables[i].ScheduleName;
                item.DatePrepared = new DateTime?(DateTime.Today);
                item.LoanBalance = app.FirstMortgageBalance;
                item.CurrentInterestRate = app.CurrentInterestRate;
                item.PreparedFor = app.FirstName + " " + app.LastName;
                item.MonthlyPayment = app.FirstMortgagePayment;
                decimal? firstMortgagePayment = app.FirstMortgagePayment;
                decimal? monthlyMortgageInsur = app.MonthlyMortgageInsur;
                item.OldMonthlyPaymentPrincipalInterest = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                if (app.PymtIncludesPropTaxes)
                {
                    firstMortgagePayment = item.OldMonthlyPaymentPrincipalInterest;
                    monthlyMortgageInsur = app.AnnualPropertyTaxes;
                    monthlyMortgageInsur = monthlyMortgageInsur.HasValue ? new decimal?(monthlyMortgageInsur.GetValueOrDefault() / 12.00M) : ((decimal?) (nullable12 = null));
                    item.OldMonthlyPaymentPrincipalInterest = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                }
                if (app.PymtIncludesHomeownersInsurance)
                {
                    firstMortgagePayment = item.OldMonthlyPaymentPrincipalInterest;
                    monthlyMortgageInsur = app.AnnualHomeownersInsur;
                    monthlyMortgageInsur = monthlyMortgageInsur.HasValue ? new decimal?(monthlyMortgageInsur.GetValueOrDefault() / 12.00M) : ((decimal?) (nullable12 = null));
                    item.OldMonthlyPaymentPrincipalInterest = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                }
                item.TermInMonths = new int?(EnumNorm.TermToInt(new MortgageTermEnum?(app.MortgageTerm)) * 12);
                item.PercentCharge = new double?(refiVariables[i].OriginationPercent * 100.0);
                item.ProcessingFee = new decimal?(refiVariables[i].ProcessingFee);
                item.UnderwritingFee = new decimal?(refiVariables[i].UnderwritingFee);
                item.CreditReportFee = new decimal?(refiVariables[i].CreditReportFee);
                item.TaxServiceFee = refiVariables[i].TaxServiceFee;
                item.FloodCertificationFee = refiVariables[i].FloodCertificationFee;
                item.AppraisalFee = new decimal?(refiVariables[i].AppraisalFee);
                item.ClosingEscrowFee = new decimal?(refiVariables[i].ClosingEscrowFee);
                item.EndorsementsReconveyanceFee = new decimal?(refiVariables[i].EndorsementsReconveyanceFee);
                item.MortgageRecordingCharges = new decimal?(refiVariables[i].MortgageRecordingfee);
                item.NumberOfDays = 5;
                item.DebtToBePaidOff = app.FirstMortgageBalance;
                item.DateOfOrgination = app.FirstMortgageOriginationDate;
                item.AmountOfNewPaymentPlusMonthlySavings = item.OldMonthlyPaymentPrincipalInterest;
                if (!app.EstimateTotalDebtToPayOff.HasValue)
                {
                    app.EstimateTotalDebtToPayOff = 0;
                }
                item.TotalBalanceOfDebtToConsolidate = app.EstimateTotalDebtToPayOff;
                if (!item.TotalBalanceOfDebtToConsolidate.HasValue)
                {
                    item.TotalBalanceOfDebtToConsolidate = 0;
                }
                if (!app.SecondMortgageBalance.HasValue)
                {
                    app.SecondMortgageBalance = 0;
                }
                item.SecondMtgBalance = app.SecondMortgageBalance;
                item.MonthsPaidRemaining = new int?(GetRemainingMonthsTobePaid(item.DateOfOrgination.Value, (int) app.MortgageTerm));
                if (flag && flag2)
                {
                    item.MonthsPaidRemaining2nd = GetRemainingMonthsTobePaid(app.SecondMortgageOriginationDate.Value, (int)app.SecondMortgageTerm.Value);
                }
                item.MI_MonthlyAmount = app.MonthlyMortgageInsur;
                firstMortgagePayment = app.AnnualPropertyTaxes;
                item.monthlyTaxes = firstMortgagePayment.HasValue ? new decimal?(firstMortgagePayment.GetValueOrDefault() / 12.00M) : ((decimal?) (monthlyMortgageInsur = null));
                item.CT_MonthlyAmount = item.monthlyTaxes;
                firstMortgagePayment = item.monthlyTaxes;
                decimal numofMonthstoEscrowTaxes = refiVariables[i].NumofMonthstoEscrowTaxes;
                item.CountyPropertyTaxReserves = firstMortgagePayment.HasValue ? new decimal?(firstMortgagePayment.GetValueOrDefault() * numofMonthstoEscrowTaxes) : ((decimal?) (monthlyMortgageInsur = null));
                item.CT_MonthsReserves = new decimal?(refiVariables[i].NumofMonthstoEscrowTaxes);
                item.HI_MonthlyAmount = app.AnnualHomeownersInsur / 12.00M;
                item.Insurance = item.HI_MonthlyAmount;
                item.HI_MonthsReserves = new decimal?(refiVariables[i].NumofMonthstoEscrowHazardInsurance);
                firstMortgagePayment = item.HI_MonthlyAmount;
                monthlyMortgageInsur = item.HI_MonthsReserves;
                item.HazardInsuranceReserves = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() * monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                item.TermInYears = new int?(refiVariables[i].newTermInYears);
                item.OriginationChargesPercent = new double?(refiVariables[i].OriginationPercent);
                item.TotalFixedFees = new decimal?((((((((refiVariables[i].ProcessingFee + refiVariables[i].UnderwritingFee) + refiVariables[i].AppraisalFee) + refiVariables[i].CreditReportFee) + refiVariables[i].ClosingEscrowFee) + refiVariables[i].EndorsementsReconveyanceFee) + refiVariables[i].MortgageRecordingfee) + refiVariables[i].TaxServiceFee) + refiVariables[i].FloodCertificationFee);
                decimal num7 = 0.00M;
                if (app.LoanTypeRequested == LoanTypeRequestedEnum.CashOutMortgage)
                {
                    num7 = app.CashOutRequested.Value;
                }
                else if (app.LoanTypeRequested == LoanTypeRequestedEnum.DebtConsolidationPayOffCreditors)
                {
                    num7 = app.AdditionalCashOutRequested.Value;
                }
                if (num7 > 0M)
                {
                    item.ShowCashout = true;
                }
                if (app.LoanTypeRequested == LoanTypeRequestedEnum.CashOutMortgage)
                {
                    if (((double) item.TotalBalanceOfDebtToConsolidate.Value) > refiVariables[i].MaxCashOut)
                    {
                        continue;
                    }
                    if ((((double) num7) + ((double) item.TotalBalanceOfDebtToConsolidate.Value)) > refiVariables[i].MaxCashOut)
                    {
                        num7 = ((decimal) refiVariables[i].MaxCashOut) - item.TotalBalanceOfDebtToConsolidate.Value;
                    }
                    if (flag && flag2)
                    {
                        if (refiVariables[i].LoanType == LoanTypeEnum.VA)
                        {
                            TimeSpan span = (TimeSpan) (DateTime.Now - app.SecondMortgageOriginationDate.Value);
                            if (span.Days < 330)
                            {
                                if (!refiVariables[i].RefiCashout)
                                {
                                    continue;
                                }
                                firstMortgagePayment = app.SecondMortgageBalance;
                                numofMonthstoEscrowTaxes = (decimal) refiVariables[i].MaxCashOut;
                                if ((firstMortgagePayment.GetValueOrDefault() > numofMonthstoEscrowTaxes) && firstMortgagePayment.HasValue)
                                {
                                    continue;
                                }
                                if (num7 > (((decimal) refiVariables[i].MaxCashOut) - app.SecondMortgageBalance.Value))
                                {
                                    num7 = ((decimal) refiVariables[i].MaxCashOut) - app.SecondMortgageBalance.Value;
                                }
                            }
                        }
                        else if (app.Has2ndMortgage == HaveSecondMortgageEnum.YesForCash)
                        {
                            firstMortgagePayment = app.SecondMortgageBalance;
                            numofMonthstoEscrowTaxes = (decimal) refiVariables[i].MaxCashOut;
                            if ((firstMortgagePayment.GetValueOrDefault() > numofMonthstoEscrowTaxes) && firstMortgagePayment.HasValue)
                            {
                                continue;
                            }
                            if (num7 > (((decimal) refiVariables[i].MaxCashOut) - app.SecondMortgageBalance.Value))
                            {
                                num7 = ((decimal) refiVariables[i].MaxCashOut) - app.SecondMortgageBalance.Value;
                            }
                        }
                    }
                }
                else if (app.LoanTypeRequested == LoanTypeRequestedEnum.DebtConsolidationPayOffCreditors)
                {
                    item.TotalBalanceOfDebtToConsolidate = app.EstimateTotalDebtToPayOff;
                    item.ShowDebtConsal = true;
                    if (num7 > (((decimal) refiVariables[i].MaxCashOut) - item.TotalBalanceOfDebtToConsolidate.Value))
                    {
                        num7 = ((decimal) refiVariables[i].MaxCashOut) - item.TotalBalanceOfDebtToConsolidate.Value;
                        if (num7 < 0M)
                        {
                            num7 = 0M;
                        }
                    }
                    if (flag && flag2)
                    {
                        if (refiVariables[i].LoanType == LoanTypeEnum.VA)
                        {
                            TimeSpan span2 = (TimeSpan) (DateTime.Now - app.SecondMortgageOriginationDate.Value);
                            if (span2.Days < 330)
                            {
                                firstMortgagePayment = app.SecondMortgageBalance;
                                numofMonthstoEscrowTaxes = (decimal) refiVariables[i].MaxCashOut;
                                if ((firstMortgagePayment.GetValueOrDefault() > numofMonthstoEscrowTaxes) && firstMortgagePayment.HasValue)
                                {
                                    continue;
                                }
                                if (num7 > ((((decimal) refiVariables[i].MaxCashOut) - app.SecondMortgageBalance.Value) - item.TotalBalanceOfDebtToConsolidate.Value))
                                {
                                    num7 = (((decimal) refiVariables[i].MaxCashOut) - app.SecondMortgageBalance.Value) - item.TotalBalanceOfDebtToConsolidate.Value;
                                }
                                firstMortgagePayment = item.TotalBalanceOfDebtToConsolidate;
                                monthlyMortgageInsur = app.SecondMortgageBalance;
                                numofMonthstoEscrowTaxes = ((decimal) refiVariables[i].MaxCashOut) - monthlyMortgageInsur.Value;
                                if ((firstMortgagePayment.GetValueOrDefault() > numofMonthstoEscrowTaxes) && firstMortgagePayment.HasValue)
                                {
                                    item.TotalBalanceOfDebtToConsolidate = new decimal?(((decimal) refiVariables[i].MaxCashOut) - app.SecondMortgageBalance.Value);
                                }
                            }
                        }
                        else if (app.Has2ndMortgage == HaveSecondMortgageEnum.YesForCash)
                        {
                            firstMortgagePayment = app.SecondMortgageBalance;
                            numofMonthstoEscrowTaxes = (decimal) refiVariables[i].MaxCashOut;
                            if ((firstMortgagePayment.GetValueOrDefault() > numofMonthstoEscrowTaxes) && firstMortgagePayment.HasValue)
                            {
                                continue;
                            }
                            if (num7 > ((((decimal) refiVariables[i].MaxCashOut) - app.SecondMortgageBalance.Value) - item.TotalBalanceOfDebtToConsolidate.Value))
                            {
                                num7 = (((decimal) refiVariables[i].MaxCashOut) - app.SecondMortgageBalance.Value) - item.TotalBalanceOfDebtToConsolidate.Value;
                            }
                            firstMortgagePayment = item.TotalBalanceOfDebtToConsolidate;
                            monthlyMortgageInsur = app.SecondMortgageBalance;
                            numofMonthstoEscrowTaxes = ((decimal) refiVariables[i].MaxCashOut) - monthlyMortgageInsur.Value;
                            if ((firstMortgagePayment.GetValueOrDefault() > numofMonthstoEscrowTaxes) && firstMortgagePayment.HasValue)
                            {
                                item.TotalBalanceOfDebtToConsolidate = new decimal?(((decimal) refiVariables[i].MaxCashOut) - app.SecondMortgageBalance.Value);
                            }
                        }
                    }
                    if (num7 <= 0M)
                    {
                        firstMortgagePayment = item.TotalBalanceOfDebtToConsolidate;
                        numofMonthstoEscrowTaxes = (decimal) refiVariables[i].MaxCashOut;
                        if ((firstMortgagePayment.GetValueOrDefault() > numofMonthstoEscrowTaxes) && firstMortgagePayment.HasValue)
                        {
                            item.TotalBalanceOfDebtToConsolidate = new decimal?((decimal) refiVariables[i].MaxCashOut);
                        }
                    }
                }
                item.MI_MonthsReserves = 2;
                decimal num12 = item.LoanBalance.Value;
                if (flag && flag2)
                {
                    if (refiVariables[i].LoanType == LoanTypeEnum.VA)
                    {
                        TimeSpan span3 = (TimeSpan) (DateTime.Now - app.SecondMortgageOriginationDate.Value);
                        if (span3.Days < 330)
                        {
                            firstMortgagePayment = app.SecondMortgageBalance;
                            numofMonthstoEscrowTaxes = (decimal) refiVariables[i].MaxCashOut;
                            if ((firstMortgagePayment.GetValueOrDefault() > numofMonthstoEscrowTaxes) && firstMortgagePayment.HasValue)
                            {
                                continue;
                            }
                        }
                    }
                    if (app.Has2ndMortgage == HaveSecondMortgageEnum.YesForCash)
                    {
                        firstMortgagePayment = app.SecondMortgageBalance;
                        numofMonthstoEscrowTaxes = (decimal) refiVariables[i].MaxCashOut;
                        if ((firstMortgagePayment.GetValueOrDefault() > numofMonthstoEscrowTaxes) && firstMortgagePayment.HasValue)
                        {
                            continue;
                        }
                    }
                    item.ShowSecondMtg = true;
                    item.SecondMtgBalance = app.SecondMortgageBalance;
                    num12 += app.SecondMortgageBalance.Value;
                }
                double num15 = (((((((refiVariables[i].OriginationPercent - refiVariables[i].LenderCreditPercent) + refiVariables[i].TitleInusrancePercent) + refiVariables[i].IntangibleTaxPercent) + refiVariables[i].StateTaxPercent) + refiVariables[i].DiscountPercent) + num6) + num4) + upfrontMI;
                numofMonthstoEscrowTaxes = num12;
                firstMortgagePayment = numofMonthstoEscrowTaxes + item.TotalFixedFees;
                monthlyMortgageInsur = item.HazardInsuranceReserves;
                nullable12 = ((firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null))) + item.CountyPropertyTaxReserves;
                double num16 = (double) nullable12.Value;
                double num17 = (refiVariables[i].MaxLTV / 100.0) * ((double) app.EstimatedHomeValue.Value);
                double num18 = ((1.0 - num15) * num17) - num16;
                numofMonthstoEscrowTaxes = (decimal) num18;
                decimal num38 = num7;
                firstMortgagePayment = item.TotalBalanceOfDebtToConsolidate;
                firstMortgagePayment = firstMortgagePayment.HasValue ? new decimal?(num38 + firstMortgagePayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsur = null));
                if ((numofMonthstoEscrowTaxes < firstMortgagePayment.GetValueOrDefault()) && firstMortgagePayment.HasValue)
                {
                    num7 = ((decimal) num18) - item.TotalBalanceOfDebtToConsolidate.Value;
                }
                firstMortgagePayment = item.TotalBalanceOfDebtToConsolidate;
                numofMonthstoEscrowTaxes = (decimal) num18;
                if ((firstMortgagePayment.GetValueOrDefault() > numofMonthstoEscrowTaxes) && firstMortgagePayment.HasValue)
                {
                    item.TotalBalanceOfDebtToConsolidate = new decimal?((decimal) num18);
                }
                if (num18 < 0.0)
                {
                    num7 = 0M;
                }
                if (num7 < 0M)
                {
                    num7 = 0M;
                }
                firstMortgagePayment = item.TotalBalanceOfDebtToConsolidate;
                if ((firstMortgagePayment.GetValueOrDefault() < 0M) && firstMortgagePayment.HasValue)
                {
                    item.TotalBalanceOfDebtToConsolidate = 0;
                }
                item.Cashout = num7;
                numofMonthstoEscrowTaxes = num12;
                firstMortgagePayment = numofMonthstoEscrowTaxes + item.TotalFixedFees;
                monthlyMortgageInsur = item.HazardInsuranceReserves;
                firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                monthlyMortgageInsur = item.CountyPropertyTaxReserves;
                firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                numofMonthstoEscrowTaxes = num7;
                nullable12 = (firstMortgagePayment.HasValue ? new decimal?(firstMortgagePayment.GetValueOrDefault() + numofMonthstoEscrowTaxes) : null) + item.TotalBalanceOfDebtToConsolidate;
                double num19 = (double) nullable12.Value;
                if ((refiVariables[i].MortgageProgramOption.Trim() == "HARP_FannieBefore2009") || (refiVariables[i].MortgageProgramOption.Trim() == "HARP_FredieBefore2009"))
                {
                    num19 += ((double) app.MonthlyMortgageInsur.Value) * 2.0;
                }
                double num20 = num19 / (1.0 - num15);
                app.ProposedLoanAmount = (decimal) num20;
                numofMonthstoEscrowTaxes = app.ProposedLoanAmount;
                firstMortgagePayment = app.EstimatedHomeValue;
                monthlyMortgageInsur = (firstMortgagePayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes / firstMortgagePayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsur = null))) * 100M;
                app.CurrentLTV = (double) monthlyMortgageInsur.Value;
                v = GetLoanVariablesForRateSheet(app, refiVariables[i], hibalance);
                RateSheetProcessor.RetrieveRebate(refiVariables[i].ScheduleName, v);
                if (v.FinalAdjustment >= -1000f)
                {
                    double? nullable15;
                    double num39;
                    RatePricing pricing = RatePricing.GetPar30(PopPricingList(v, refiVariables[i].ScheduleName, refiVariables[i].LenderPaidComp));
                    item.LoanAmount = new decimal?(app.ProposedLoanAmount);
                    item.InterestRate = new double?(pricing.interest);
                    num6 = (0.05 * refiVariables[i].NewInterestRate) / 360.0;
                    item.InterestRateSavings = item.CurrentInterestRate - item.InterestRate;
                    double? interestRateSavings = item.InterestRateSavings;
                    if ((interestRateSavings.GetValueOrDefault() > 0.0) && interestRateSavings.HasValue)
                    {
                        item.ShowInterestSaving = true;
                    }
                    if (flag && flag2)
                    {
                        num39 = ((((double) item.LoanBalance.Value) * item.CurrentInterestRate.Value) + (((double) item.SecondMtgBalance.Value) * app.SecondMortgageInterestRate.Value)) / (((double) item.LoanBalance.Value) + ((double) item.SecondMtgBalance.Value));
                        interestRateSavings = item.InterestRate;
                        item.BlendedRateSavings = interestRateSavings.HasValue ? new double?(num39 - interestRateSavings.GetValueOrDefault()) : ((double?) (nullable15 = null));
                        interestRateSavings = item.BlendedRateSavings;
                        if ((interestRateSavings.GetValueOrDefault() > 0.0) && interestRateSavings.HasValue)
                        {
                            item.ShowBlendedRate = true;
                            item.ShowInterestSaving = false;
                        }
                    }
                    if (pricing.Cost30Days > 0.0)
                    {
                        item.DiscountPoints = new double?(pricing.Cost30Days);
                        item.Discount = new decimal?((decimal) (pricing.Cost30Days * ((double) item.LoanAmount.Value)));
                        item.CreditPoints = 0.0;
                        item.Credit = 0;
                    }
                    else
                    {
                        item.DiscountPoints = 0.0;
                        item.Discount = 0;
                        item.CreditPoints = new double?(pricing.Cost30Days * -1.0);
                        interestRateSavings = item.CreditPoints;
                        num39 = (double) item.LoanAmount.Value;
                        nullable15 = interestRateSavings.HasValue ? new double?(interestRateSavings.GetValueOrDefault() * num39) : null;
                        item.Credit = new decimal?((decimal) nullable15.Value);
                    }
                    num15 = (((((((refiVariables[i].OriginationPercent - item.CreditPoints.Value) + refiVariables[i].TitleInusrancePercent) + refiVariables[i].IntangibleTaxPercent) + refiVariables[i].StateTaxPercent) + item.DiscountPoints.Value) + num6) + num4) + upfrontMI;
                    numofMonthstoEscrowTaxes = num12;
                    firstMortgagePayment = numofMonthstoEscrowTaxes + item.TotalFixedFees;
                    monthlyMortgageInsur = item.HazardInsuranceReserves;
                    nullable12 = ((firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null))) + item.CountyPropertyTaxReserves;
                    num16 = (double) nullable12.Value;
                    num17 = (refiVariables[i].MaxLTV / 100.0) * ((double) app.EstimatedHomeValue.Value);
                    num18 = ((1.0 - num15) * num17) - num16;
                    numofMonthstoEscrowTaxes = (decimal) num18;
                    num38 = num7;
                    firstMortgagePayment = item.TotalBalanceOfDebtToConsolidate;
                    firstMortgagePayment = firstMortgagePayment.HasValue ? new decimal?(num38 + firstMortgagePayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsur = null));
                    if ((numofMonthstoEscrowTaxes < firstMortgagePayment.GetValueOrDefault()) && firstMortgagePayment.HasValue)
                    {
                        num7 = ((decimal) num18) - item.TotalBalanceOfDebtToConsolidate.Value;
                    }
                    firstMortgagePayment = item.TotalBalanceOfDebtToConsolidate;
                    numofMonthstoEscrowTaxes = (decimal) num18;
                    if ((firstMortgagePayment.GetValueOrDefault() > numofMonthstoEscrowTaxes) && firstMortgagePayment.HasValue)
                    {
                        item.TotalBalanceOfDebtToConsolidate = new decimal?((decimal) num18);
                    }
                    if (num18 < 0.0)
                    {
                        num7 = 0M;
                    }
                    if (num7 < 0M)
                    {
                        num7 = 0M;
                    }
                    firstMortgagePayment = item.TotalBalanceOfDebtToConsolidate;
                    if ((firstMortgagePayment.GetValueOrDefault() < 0M) && firstMortgagePayment.HasValue)
                    {
                        item.TotalBalanceOfDebtToConsolidate = 0;
                    }
                    item.Cashout = num7;
                    numofMonthstoEscrowTaxes = num12;
                    firstMortgagePayment = numofMonthstoEscrowTaxes + item.TotalFixedFees;
                    monthlyMortgageInsur = item.HazardInsuranceReserves;
                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                    monthlyMortgageInsur = item.CountyPropertyTaxReserves;
                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                    numofMonthstoEscrowTaxes = num7;
                    nullable12 = (firstMortgagePayment.HasValue ? new decimal?(firstMortgagePayment.GetValueOrDefault() + numofMonthstoEscrowTaxes) : null) + item.TotalBalanceOfDebtToConsolidate;
                    num19 = (double) nullable12.Value;
                    if ((refiVariables[i].MortgageProgramOption.Trim() == "HARP_FannieBefore2009") || (refiVariables[i].MortgageProgramOption.Trim() == "HARP_FredieBefore2009"))
                    {
                        num19 += ((double) app.MonthlyMortgageInsur.Value) * 2.0;
                    }
                    num20 = num19 / (1.0 - num15);
                    item.LoanAmount = new decimal?((decimal) num20);
                    numofMonthstoEscrowTaxes = (int) num17;
                    if (numofMonthstoEscrowTaxes >= item.LoanAmount)
                    {
                        firstMortgagePayment = item.LoanAmount;
                        numofMonthstoEscrowTaxes = (decimal) maxLoanLimit;
                        if ((firstMortgagePayment.GetValueOrDefault() <= numofMonthstoEscrowTaxes) || !firstMortgagePayment.HasValue)
                        {
                            item.NewMI_MonthlyAmount = 0;
                            item.PMI_MIP_VA_FFReserves = 0;
                            item.MI_Upfront_Fee = 0.00M;
                            double num22 = (num20 / ((double) app.EstimatedHomeValue.Value)) * 100.0;
                            item.LTV = num22;
                            item.CLTV = num22;
                            if ((app.Has2ndMortgage > HaveSecondMortgageEnum.No) && (((YesNoAns) app.PayOff2ndMortgage) == YesNoAns.No))
                            {
                                item.CLTV = ((num20 + ((double) app.SecondMortgageBalance.Value)) / ((double) app.EstimatedHomeValue.Value)) * 100.0;
                            }
                            int lTV = (int) item.LTV;
                            item.LTV = lTV;
                            lTV = (int) item.CLTV;
                            item.CLTV = lTV;
                            if (item.CLTV <= refiVariables[i].CLTV)
                            {
                                if (refiVariables[i].LoanType == LoanTypeEnum.HARP)
                                {
                                    item.NewMI_MonthlyAmount = app.MonthlyMortgageInsur;
                                    item.PMI_MIP_VA_FFReserves = item.NewMI_MonthlyAmount * 2.00M;
                                    item.MI_Upfront_Fee = 0.00M;
                                }
                                else if (refiVariables[i].LoanType == LoanTypeEnum.ConventonalConforming)
                                {
                                    if (num22 >= 80.0)
                                    {
                                        item.MI_Upfront_Fee = new decimal?((decimal) (num20 * upfrontMI));
                                        item.PMI_MIP_VA_FFReserves = new decimal?((decimal) (num20 * num4));
                                        item.NewMI_MonthlyAmount = item.PMI_MIP_VA_FFReserves / 2.00M;
                                    }
                                }
                                else if (((refiVariables[i].LoanType == LoanTypeEnum.USRDA) || (refiVariables[i].LoanType == LoanTypeEnum.VA)) || (refiVariables[i].LoanType == LoanTypeEnum.VA_IRRL))
                                {
                                    item.MI_Upfront_Fee = new decimal?((decimal) (num20 * upfrontMI));
                                    item.PMI_MIP_VA_FFReserves = new decimal?((decimal) (num20 * num4));
                                    item.NewMI_MonthlyAmount = item.PMI_MIP_VA_FFReserves / 2.00M;
                                }
                                double mortgageAmount = (double) item.LoanAmount.Value;
                                item.NewMonthlyPaymentPrincipalInterest = 0.00M;
                                item.NewMonthlyPaymentPrincipalInterest = new decimal?((decimal) GetPayment(mortgageAmount, item.InterestRate.Value, (double) item.TermInYears.Value));
                                firstMortgagePayment = item.OldMonthlyPaymentPrincipalInterest;
                                monthlyMortgageInsur = item.MI_MonthlyAmount;
                                item.TotalOldPrincipalInterestPaymentWithMI = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                firstMortgagePayment = item.NewMonthlyPaymentPrincipalInterest;
                                monthlyMortgageInsur = item.NewMI_MonthlyAmount;
                                item.TotalNewPrincipalInterestPaymentWithMI = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                double num25 = ((((double) item.TotalNewPrincipalInterestPaymentWithMI.Value) + (((double) app.AnnualPropertyTaxes.Value) / 12.0)) + (((double) app.AnnualHomeownersInsur.Value) / 12.0)) + (((double) app.AnnualHomeownersAssocDues.Value) / 12.0);
                                double monthlyIncome = ((double) app.GrossAnnualIncome) / 12.0;
                                double monthlyDebt = 0.0;
                                if (app.creditPulled)
                                {
                                    monthlyDebt = (double) app.TotalPaymentCredit.Value;
                                }
                                else
                                {
                                    monthlyDebt = ((double) app.TotalMontlyPayments) - ((double) app.TotalOfMonthlyPaymentsOnDebtToPayOff.Value);
                                    if ((app.Has2ndMortgage > HaveSecondMortgageEnum.No) && (((YesNoAns) app.PayOff2ndMortgage) == YesNoAns.No))
                                    {
                                        monthlyDebt += (double) app.SecondMortgagePayment.Value;
                                    }
                                }
                                if (GetMaxPayment(refiVariables[i].MaxfrontDTI, refiVariables[i].MaxBacktDTI, monthlyIncome, monthlyDebt) >= num25)
                                {
                                    item.FrontDTI = (num25 / monthlyIncome) * 100.0;
                                    item.BackDTI = ((num25 + monthlyDebt) / monthlyIncome) * 100.0;
                                    decimal? nullable = 0M;
                                    if (app.EstimateTotalDebtToPayOff != 0M)
                                    {
                                        firstMortgagePayment = app.TotalOfMonthlyPaymentsOnDebtToPayOff;
                                        monthlyMortgageInsur = item.TotalBalanceOfDebtToConsolidate;
                                        firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() * monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                        monthlyMortgageInsur = app.EstimateTotalDebtToPayOff;
                                        nullable = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() / monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    }
                                    firstMortgagePayment = item.TotalOldPrincipalInterestPaymentWithMI;
                                    monthlyMortgageInsur = item.TotalNewPrincipalInterestPaymentWithMI;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = nullable;
                                    item.MonthlySavings = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    firstMortgagePayment = item.TotalNewPrincipalInterestPaymentWithMI;
                                    monthlyMortgageInsur = item.MonthlySavings;
                                    item.NewPaymentPlusMonthlySavings = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    if (((YesNoAns) app.PayOff2ndMortgage) == YesNoAns.Yes)
                                    {
                                        firstMortgagePayment = item.TotalOldPrincipalInterestPaymentWithMI;
                                        monthlyMortgageInsur = app.SecondMortgagePayment;
                                        firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                        monthlyMortgageInsur = item.TotalNewPrincipalInterestPaymentWithMI;
                                        firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                        monthlyMortgageInsur = nullable;
                                        item.MonthlySavings = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                        firstMortgagePayment = item.TotalNewPrincipalInterestPaymentWithMI;
                                        monthlyMortgageInsur = item.MonthlySavings;
                                        item.NewPaymentPlusMonthlySavings = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    }
                                    firstMortgagePayment = item.MonthlySavings;
                                    if ((firstMortgagePayment.GetValueOrDefault() > 0M) && firstMortgagePayment.HasValue)
                                    {
                                        item.ShowMonthlySaving = true;
                                    }
                                    firstMortgagePayment = item.OldMonthlyPaymentPrincipalInterest;
                                    maxNumberOfUnits = item.MonthsPaidRemaining;
                                    item.OldTotalAmountOfAllPaymentsToBeMade = (firstMortgagePayment.HasValue & maxNumberOfUnits.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() * maxNumberOfUnits.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsur = null));
                                    if (flag && flag2)
                                    {
                                        firstMortgagePayment = item.OldTotalAmountOfAllPaymentsToBeMade;
                                        monthlyMortgageInsur = app.SecondMortgagePayment;
                                        numofMonthstoEscrowTaxes = item.MonthsPaidRemaining2nd;
                                        monthlyMortgageInsur = monthlyMortgageInsur.HasValue ? new decimal?(monthlyMortgageInsur.GetValueOrDefault() * numofMonthstoEscrowTaxes) : ((decimal?) (nullable12 = null));
                                        item.OldTotalAmountOfAllPaymentsToBeMade = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    }
                                    item.NewTotalAmountOfAllPaymentsToBeMade = item.NewMonthlyPaymentPrincipalInterest * item.TermInMonths;
                                    firstMortgagePayment = item.OldTotalAmountOfAllPaymentsToBeMade;
                                    monthlyMortgageInsur = item.NewTotalAmountOfAllPaymentsToBeMade;
                                    item.NetSavingsFromOldLoanToNewLoan = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    firstMortgagePayment = item.NetSavingsFromOldLoanToNewLoan;
                                    if ((firstMortgagePayment.GetValueOrDefault() > 0M) && firstMortgagePayment.HasValue)
                                    {
                                        item.ShowNetSaving = true;
                                    }
                                    firstMortgagePayment = item.LoanAmount;
                                    numofMonthstoEscrowTaxes = (decimal) refiVariables[i].OriginationPercent;
                                    firstMortgagePayment = firstMortgagePayment.HasValue ? new decimal?(firstMortgagePayment.GetValueOrDefault() * numofMonthstoEscrowTaxes) : null;
                                    monthlyMortgageInsur = item.Credit;
                                    item.AdjustedLoanOriginationFee = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    numofMonthstoEscrowTaxes = (decimal) refiVariables[i].TitleInusrancePercent;
                                    firstMortgagePayment = item.LoanAmount;
                                    item.LenderTitleInsurance = firstMortgagePayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * firstMortgagePayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsur = null));
                                    numofMonthstoEscrowTaxes = (decimal) refiVariables[i].IntangibleTaxPercent;
                                    firstMortgagePayment = item.LoanAmount;
                                    item.IntangibleTax = firstMortgagePayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * firstMortgagePayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsur = null));
                                    numofMonthstoEscrowTaxes = (decimal) refiVariables[i].StateTaxPercent;
                                    firstMortgagePayment = item.LoanAmount;
                                    item.StateTax = firstMortgagePayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * firstMortgagePayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsur = null));
                                    numofMonthstoEscrowTaxes = (decimal) num6;
                                    item.DailyInterestCharges = numofMonthstoEscrowTaxes * item.LoanAmount;
                                    firstMortgagePayment = item.HazardInsuranceReserves;
                                    monthlyMortgageInsur = item.CountyPropertyTaxReserves;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.PMI_MIP_VA_FFReserves;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.DailyInterestCharges;
                                    item.EstimatedPrepaidsReserves = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    firstMortgagePayment = item.ProcessingFee;
                                    monthlyMortgageInsur = item.UnderwritingFee;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.AppraisalFee;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.CreditReportFee;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.ClosingEscrowFee;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.EndorsementsReconveyanceFee;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.MortgageRecordingCharges;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    numofMonthstoEscrowTaxes = item.TaxServiceFee;
                                    firstMortgagePayment = firstMortgagePayment.HasValue ? new decimal?(firstMortgagePayment.GetValueOrDefault() + numofMonthstoEscrowTaxes) : null;
                                    numofMonthstoEscrowTaxes = item.FloodCertificationFee;
                                    if (firstMortgagePayment.HasValue)
                                    {
                                        new decimal?(firstMortgagePayment.GetValueOrDefault() + numofMonthstoEscrowTaxes);
                                    }
                                    firstMortgagePayment = item.AdjustedLoanOriginationFee;
                                    monthlyMortgageInsur = item.LenderTitleInsurance;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.Discount;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.IntangibleTax;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.StateTax;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.MI_Upfront_Fee;
                                    if (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue)
                                    {
                                        new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault());
                                    }
                                    firstMortgagePayment = item.ProcessingFee;
                                    monthlyMortgageInsur = item.UnderwritingFee;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.AppraisalFee;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.CreditReportFee;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.ClosingEscrowFee;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.EndorsementsReconveyanceFee;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.MortgageRecordingCharges;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    numofMonthstoEscrowTaxes = item.TaxServiceFee;
                                    firstMortgagePayment = firstMortgagePayment.HasValue ? new decimal?(firstMortgagePayment.GetValueOrDefault() + numofMonthstoEscrowTaxes) : ((decimal?) (monthlyMortgageInsur = null));
                                    numofMonthstoEscrowTaxes = item.FloodCertificationFee;
                                    firstMortgagePayment = firstMortgagePayment.HasValue ? new decimal?(firstMortgagePayment.GetValueOrDefault() + numofMonthstoEscrowTaxes) : null;
                                    monthlyMortgageInsur = item.AdjustedLoanOriginationFee;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.LenderTitleInsurance;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.Discount;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.IntangibleTax;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.StateTax;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.MI_Upfront_Fee;
                                    item.EstClosingCosts = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    firstMortgagePayment = item.LoanBalance;
                                    monthlyMortgageInsur = item.EstClosingCosts;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    monthlyMortgageInsur = item.EstimatedPrepaidsReserves;
                                    item.TotalAmountNeeded = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    firstMortgagePayment = item.LoanAmount;
                                    monthlyMortgageInsur = item.TotalAmountNeeded;
                                    item.EstimatedFundsNeeded = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    firstMortgagePayment = item.EstClosingCosts;
                                    monthlyMortgageInsur = item.MonthlySavings;
                                    item.CostSavingsBreakEvenAnalysis = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() / monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    double num30 = (double) item.LoanAmount.Value;
                                    interestRateSavings = item.InterestRate;
                                    double num31 = (interestRateSavings.Value / 100.0) / 12.0;
                                    double num32 = (double) app.FirstMortgagePayment.Value;
                                    if (((YesNoAns) app.PayOff2ndMortgage) == YesNoAns.Yes)
                                    {
                                        num32 += (double) app.SecondMortgagePayment.Value;
                                    }
                                    double a = -Math.Log(1.0 - ((num30 / num32) * num31)) / Math.Log(1.0 + num31);
                                    item.NumberofPaymentstoMaturity = new int?((int) Math.Ceiling(a));
                                    maxNumberOfUnits = item.NumberofPaymentstoMaturity;
                                    float? nullable17 = maxNumberOfUnits.HasValue ? new float?(((float) maxNumberOfUnits.GetValueOrDefault()) / 12f) : null;
                                    item.YearsRequiredToMaturity = nullable17.HasValue ? new double?((double) nullable17.GetValueOrDefault()) : null;
                                    item.AcceleratedPayoffTotalAmountOfAllPayments = item.NewPaymentPlusMonthlySavings * item.NumberofPaymentstoMaturity;
                                    firstMortgagePayment = item.OldTotalAmountOfAllPaymentsToBeMade;
                                    monthlyMortgageInsur = item.AcceleratedPayoffTotalAmountOfAllPayments;
                                    item.TotalSavingsFromOldMortgageToNewMortgage = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    maxNumberOfUnits = item.MonthsPaidRemaining;
                                    numberofPaymentstoMaturity = item.NumberofPaymentstoMaturity;
                                    maxNumberOfUnits = (maxNumberOfUnits.HasValue & numberofPaymentstoMaturity.HasValue) ? new int?(maxNumberOfUnits.GetValueOrDefault() - numberofPaymentstoMaturity.GetValueOrDefault()) : ((int?) (nullable4 = null));
                                    item.MonthlyPaymentsEliminated = maxNumberOfUnits.HasValue ? new decimal?(maxNumberOfUnits.GetValueOrDefault()) : null;
                                    firstMortgagePayment = item.OldMonthlyPaymentPrincipalInterest;
                                    monthlyMortgageInsur = item.MI_MonthlyAmount;
                                    item.TotalOldPrincipalInterestPaymentWithMI = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    firstMortgagePayment = item.NewMonthlyPaymentPrincipalInterest;
                                    monthlyMortgageInsur = item.NewMI_MonthlyAmount;
                                    item.TotalNewPrincipalInterestPaymentWithMI = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable12 = null));
                                    double loanAmount = ((((((((double) item.LoanAmount.Value) - ((double) item.AdjustedLoanOriginationFee.Value)) - ((double) item.Discount.Value)) - ((double) item.ProcessingFee.Value)) - ((double) item.UnderwritingFee.Value)) - ((double) item.ClosingEscrowFee.Value)) - ((double) item.MI_Upfront_Fee.Value)) - ((double) item.DailyInterestCharges.Value);
                                    double payment = 0.0;
                                    if (refiVariables[i].LoanType == LoanTypeEnum.FHA)
                                    {
                                        payment = ((((((double) item.NewMonthlyPaymentPrincipalInterest.Value) + ((double) item.NewMI_MonthlyAmount.Value)) * refiVariables[i].MiDurationYears) * 12.0) + ((((double) item.NewMonthlyPaymentPrincipalInterest.Value) * (30.0 - refiVariables[i].MiDurationYears)) * 12.0)) / 360.0;
                                    }
                                    else
                                    {
                                        int num36 = getMILTV(loanAmount, (double) item.TermInMonths.Value, item.InterestRate.Value, (double) item.NewMonthlyPaymentPrincipalInterest.Value, 78.0, (double) app.EstimatedHomeValue.Value);
                                        payment = (((((double) item.NewMonthlyPaymentPrincipalInterest.Value) + ((double) item.NewMI_MonthlyAmount.Value)) * num36) + (((double) item.NewMonthlyPaymentPrincipalInterest.Value) * (((double) item.TermInMonths.Value) - num36))) / 360.0;
                                    }
                                    item.APR = new double?(GetAPR(loanAmount, item.InterestRate.Value / 1200.0, (double) item.TermInMonths.Value, payment));
                                    item.PercentCharge /= 100.0;
                                    item.ShowBlendedRate = true;
                                    item.ShowCashout = true;
                                    item.ShowDebtConsal = true;
                                    item.ShowInterestSaving = true;
                                    item.ShowMonthlySaving = true;
                                    item.ShowNetSaving = true;
                                    item.ShowSecondMtg = true;
                                    list.Add(item);
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }

        internal static Mortgage GetCurMortgage(Applicant app)
        {
            CcsLocalDbContext context = new CcsLocalDbContext();
            Mortgage mortgage = context.Mortgages.FirstOrDefault<Mortgage>(x => x.Applicant_ID == app.Applicant_Id);
            context.Dispose();
            return mortgage;
        }

        internal static List<RefiOption> GetFHAOptions(Mortgage CurMtg)
        {
            List<RefiOption> list = new List<RefiOption>();
            List<Variable> variables = GetVariables();
            for (int i = 0; i < variables.Count; i++)
            {
                decimal? nullable3;
                RefiOption item = new RefiOption {
                    OptionName = variables[i].ScheduleName,
                    DatePrepared = new DateTime?(DateTime.Today),
                    LoanBalance = CurMtg.Balance,
                    CurrentInterestRate = new double?(CurMtg.InterestRate),
                    PreparedFor = CurMtg.MortgageApplicant.FirstName + " " + CurMtg.MortgageApplicant.LastName,
                    MonthlyPayment = CurMtg.MonthlyPayment
                };
                decimal? monthlyPayment = CurMtg.MonthlyPayment;
                decimal? monthlyMortgageInsurance = CurMtg.MonthlyMortgageInsurance;
                item.OldMonthlyPaymentPrincipalInterest = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                if (CurMtg.PymtIncludesTaxes == YesNoAns.Yes)
                {
                    item.OldMonthlyPaymentPrincipalInterest -= CurMtg.YearlyPropertyTaxes / 12M;
                }
                if (CurMtg.PymtIncludesHomeownersInsurance == YesNoAns.Yes)
                {
                    item.OldMonthlyPaymentPrincipalInterest -= CurMtg.YearlyHomeInsurancePayment / 12M;
                }
                item.TermInMonths = new int?(EnumNorm.TermToInt(CurMtg.Term) * 12);
                item.PercentCharge = new double?(variables[i].OriginationPercent * 100.0);
                item.ProcessingFee = new decimal?(variables[i].ProcessingFee);
                item.UnderwritingFee = new decimal?(variables[i].UnderwritingFee);
                item.CreditReportFee = new decimal?(variables[i].CreditReportFee);
                item.AppraisalFee = new decimal?(variables[i].AppraisalFee);
                item.ClosingEscrowFee = new decimal?(variables[i].ClosingEscrowFee);
                item.EndorsementsReconveyanceFee = new decimal?(variables[i].EndorsementsReconveyanceFee);
                item.MortgageRecordingCharges = new decimal?(variables[i].MortgageRecordingfee);
                item.NumberOfDays = 5;
                item.DebtToBePaidOff = CurMtg.Balance;
                item.AmountOfNewPaymentPlusMonthlySavings = item.OldMonthlyPaymentPrincipalInterest;
                item.InterestRate = new double?(variables[i].NewInterestRate);
                item.InterestRateSavings = item.CurrentInterestRate - item.InterestRate;
                item.DateOfOrgination = CurMtg.OriginationDate;
                if (item.DateOfOrgination.HasValue)
                {
                    DateTime time = item.DateOfOrgination.Value;
                    int num2 = DateTime.Now.Year - time.Year;
                    int month = time.Month;
                    int num4 = DateTime.Now.Month;
                    int num5 = Math.Abs((int) (((12 * num2) + month) - num4));
                    item.MonthsPaidRemaining = new int?((EnumNorm.TermToInt(CurMtg.Term) * 12) - num5);
                }
                item.MI_MonthlyAmount = CurMtg.MonthlyMortgageInsurance;
                item.PMI_MIP_VA_FFReserves = item.MI_MonthlyAmount * 2.00M;
                item.MI_MonthsReserves = 2;
                item.monthlyTaxes = CurMtg.YearlyPropertyTaxes / 12.00M;
                item.CT_MonthlyAmount = item.monthlyTaxes;
                decimal? monthlyTaxes = item.monthlyTaxes;
                decimal numofMonthstoEscrowTaxes = variables[i].NumofMonthstoEscrowTaxes;
                item.CountyPropertyTaxReserves = monthlyTaxes.HasValue ? new decimal?(monthlyTaxes.GetValueOrDefault() * numofMonthstoEscrowTaxes) : null;
                item.CT_MonthsReserves = new decimal?(variables[i].NumofMonthstoEscrowTaxes);
                item.HI_MonthlyAmount = CurMtg.YearlyHomeInsurancePayment / 12.00M;
                item.Insurance = item.HI_MonthlyAmount;
                item.HI_MonthsReserves = new decimal?(variables[i].NumofMonthstoEscrowHazardInsurance);
                item.HazardInsuranceReserves = item.HI_MonthlyAmount * item.HI_MonthsReserves;
                item.TermInYears = new int?(variables[i].newTermInYears);
                item.OriginationChargesPercent = new double?(variables[i].OriginationPercent);
                item.TotalFixedFees = new decimal?((((((variables[i].ProcessingFee + variables[i].UnderwritingFee) + variables[i].AppraisalFee) + variables[i].CreditReportFee) + variables[i].ClosingEscrowFee) + variables[i].EndorsementsReconveyanceFee) + variables[i].MortgageRecordingfee);
                double num6 = (0.05 * variables[i].NewInterestRate) / 360.0;
                double num7 = (((((variables[i].OriginationPercent - variables[i].LenderCreditPercent) + variables[i].TitleInusrancePercent) + variables[i].IntangibleTaxPercent) + variables[i].StateTaxPercent) + variables[i].DiscountPercent) + num6;
                nullable3 = (((CurMtg.Balance + item.TotalFixedFees) + item.HazardInsuranceReserves) + item.CountyPropertyTaxReserves) + item.PMI_MIP_VA_FFReserves;
                double num8 = (double) nullable3.Value;
                double num9 = num8 / (1.0 - num7);
                item.LoanAmount = new decimal?((decimal) num9);
                DateTime time2 = new DateTime(0x7d9, 5, 0x1f);
                if (DateTime.Compare(CurMtg.OriginationDate.Value, time2) > 0)
                {
                    item.MI_MonthlyAmount = new decimal?((decimal) (((variables[i].FHA_Monthly_MIP_RefiOrPurchase_percent_AfterMay31_2009 / 100.0) * ((double) item.LoanAmount.Value)) / 12.0));
                    item.MI_Upfront_Fee = new decimal?((decimal) ((variables[i].FHA_Upfront_MIP_RefiOrPurchase_percent_AfterMay31_2009 / 100.0) * ((double) item.LoanAmount.Value)));
                    monthlyPayment = item.LoanAmount;
                    monthlyMortgageInsurance = item.MI_Upfront_Fee;
                    item.LoanAmount = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                }
                else
                {
                    item.MI_MonthlyAmount = new decimal?((decimal) (((variables[i].FHA_Monthly_MIP_Refi_percent_BeforeJune1_2009 / 100.0) * ((double) item.LoanAmount.Value)) / 12.0));
                    item.MI_Upfront_Fee = new decimal?((decimal) ((variables[i].FHA_Upfront_MIP_Refi_percent_beforeJune1_2009 / 100.0) * ((double) item.LoanAmount.Value)));
                    monthlyPayment = item.LoanAmount;
                    monthlyMortgageInsurance = item.MI_Upfront_Fee;
                    item.LoanAmount = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                }
                double mortgageAmount = (double) item.LoanAmount.Value;
                item.NewMonthlyPaymentPrincipalInterest = 0.00M;
                item.NewMonthlyPaymentPrincipalInterest = new decimal?((decimal) GetPayment(mortgageAmount, item.InterestRate.Value, (double) item.TermInYears.Value));
                monthlyPayment = item.OldMonthlyPaymentPrincipalInterest;
                monthlyMortgageInsurance = item.NewMonthlyPaymentPrincipalInterest;
                item.MonthlySavings = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                monthlyPayment = item.OldMonthlyPaymentPrincipalInterest;
                int? monthsPaidRemaining = item.MonthsPaidRemaining;
                item.OldTotalAmountOfAllPaymentsToBeMade = (monthlyPayment.HasValue & monthsPaidRemaining.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() * monthsPaidRemaining.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsurance = null));
                item.NewTotalAmountOfAllPaymentsToBeMade = item.NewMonthlyPaymentPrincipalInterest * item.TermInMonths;
                monthlyPayment = item.OldTotalAmountOfAllPaymentsToBeMade;
                monthlyMortgageInsurance = item.NewTotalAmountOfAllPaymentsToBeMade;
                item.NetSavingsFromOldLoanToNewLoan = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                item.CreditPoints = new double?(variables[i].LenderCreditPercent);
                monthlyPayment = item.LoanAmount;
                numofMonthstoEscrowTaxes = (decimal) item.CreditPoints.Value;
                item.Credit = monthlyPayment.HasValue ? new decimal?(monthlyPayment.GetValueOrDefault() * numofMonthstoEscrowTaxes) : ((decimal?) (monthlyMortgageInsurance = null));
                monthlyPayment = item.LoanAmount;
                numofMonthstoEscrowTaxes = (decimal) variables[i].OriginationPercent;
                monthlyPayment = monthlyPayment.HasValue ? new decimal?(monthlyPayment.GetValueOrDefault() * numofMonthstoEscrowTaxes) : null;
                monthlyMortgageInsurance = item.Credit;
                item.AdjustedLoanOriginationFee = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                numofMonthstoEscrowTaxes = (decimal) variables[i].TitleInusrancePercent;
                monthlyPayment = item.LoanAmount;
                item.LenderTitleInsurance = monthlyPayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * monthlyPayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsurance = null));
                item.DiscountPoints = new double?(variables[i].DiscountPercent * 100.0);
                numofMonthstoEscrowTaxes = (decimal) variables[i].DiscountPercent;
                monthlyPayment = item.LoanAmount;
                item.Discount = monthlyPayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * monthlyPayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsurance = null));
                numofMonthstoEscrowTaxes = (decimal) variables[i].IntangibleTaxPercent;
                monthlyPayment = item.LoanAmount;
                item.IntangibleTax = monthlyPayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * monthlyPayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsurance = null));
                numofMonthstoEscrowTaxes = (decimal) variables[i].StateTaxPercent;
                monthlyPayment = item.LoanAmount;
                item.StateTax = monthlyPayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * monthlyPayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsurance = null));
                numofMonthstoEscrowTaxes = (decimal) num6;
                item.DailyInterestCharges = numofMonthstoEscrowTaxes * item.LoanAmount;
                monthlyPayment = item.HazardInsuranceReserves;
                monthlyMortgageInsurance = item.CountyPropertyTaxReserves;
                monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                monthlyMortgageInsurance = item.PMI_MIP_VA_FFReserves;
                monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                monthlyMortgageInsurance = item.DailyInterestCharges;
                item.EstimatedPrepaidsReserves = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                monthlyPayment = item.ProcessingFee;
                monthlyMortgageInsurance = item.UnderwritingFee;
                monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                monthlyMortgageInsurance = item.AppraisalFee;
                monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                monthlyMortgageInsurance = item.CreditReportFee;
                monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                monthlyMortgageInsurance = item.ClosingEscrowFee;
                monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                monthlyMortgageInsurance = item.EndorsementsReconveyanceFee;
                monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                monthlyMortgageInsurance = item.MortgageRecordingCharges;
                monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                monthlyMortgageInsurance = item.AdjustedLoanOriginationFee;
                monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                monthlyMortgageInsurance = item.LenderTitleInsurance;
                monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                monthlyMortgageInsurance = item.Discount;
                monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                monthlyMortgageInsurance = item.IntangibleTax;
                monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                monthlyMortgageInsurance = item.StateTax;
                item.EstClosingCosts = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                monthlyPayment = item.LoanBalance;
                monthlyMortgageInsurance = item.EstClosingCosts;
                monthlyPayment = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                monthlyMortgageInsurance = item.EstimatedPrepaidsReserves;
                item.TotalAmountNeeded = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                monthlyPayment = item.LoanAmount;
                monthlyMortgageInsurance = item.TotalAmountNeeded;
                item.EstimatedFundsNeeded = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                monthlyPayment = item.EstClosingCosts;
                monthlyMortgageInsurance = item.MonthlySavings;
                item.CostSavingsBreakEvenAnalysis = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() / monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                double num12 = (double) item.LoanAmount.Value;
                double? interestRate = item.InterestRate;
                double num13 = (interestRate.Value / 100.0) / 12.0;
                double num14 = (double) CurMtg.MonthlyPayment.Value;
                double a = -Math.Log(1.0 - ((num12 / num14) * num13)) / Math.Log(1.0 + num13);
                item.NumberofPaymentstoMaturity = new int?((int) Math.Ceiling(a));
                monthsPaidRemaining = item.NumberofPaymentstoMaturity;
                float? nullable40 = monthsPaidRemaining.HasValue ? new float?(((float) monthsPaidRemaining.GetValueOrDefault()) / 12f) : null;
                item.YearsRequiredToMaturity = nullable40.HasValue ? new double?((double) nullable40.GetValueOrDefault()) : null;
                item.AcceleratedPayoffTotalAmountOfAllPayments = CurMtg.MonthlyPayment * item.NumberofPaymentstoMaturity;
                monthlyPayment = item.OldTotalAmountOfAllPaymentsToBeMade;
                monthlyMortgageInsurance = item.AcceleratedPayoffTotalAmountOfAllPayments;
                item.TotalSavingsFromOldMortgageToNewMortgage = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() - monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                monthsPaidRemaining = item.MonthsPaidRemaining - item.NumberofPaymentstoMaturity;
                item.MonthlyPaymentsEliminated = monthsPaidRemaining.HasValue ? new decimal?(monthsPaidRemaining.GetValueOrDefault()) : null;
                monthlyPayment = item.OldMonthlyPaymentPrincipalInterest;
                monthlyMortgageInsurance = item.MI_MonthlyAmount;
                item.TotalOldPrincipalInterestPaymentWithMI = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                monthlyPayment = item.NewMonthlyPaymentPrincipalInterest;
                monthlyMortgageInsurance = item.NewMI_MonthlyAmount;
                item.TotalNewPrincipalInterestPaymentWithMI = (monthlyPayment.HasValue & monthlyMortgageInsurance.HasValue) ? new decimal?(monthlyPayment.GetValueOrDefault() + monthlyMortgageInsurance.GetValueOrDefault()) : ((decimal?) (nullable3 = null));
                list.Add(item);
            }
            return list;
        }

        internal static int getLates30_12mo(List<Lates> ls)
        {
            DateTime now = DateTime.Now;
            int year = now.Year - 1;
            new DateTime(year, now.Month, now.Day);
            return (from l in ls
                group l by new { 
                    AccountOpenedDate = l.AccountOpenedDate,
                    HighCreditAmount = l.HighCreditAmount,
                    MonthlyPaymentAmount = l.MonthlyPaymentAmount,
                    Late30 = l.Late30
                } into gr
                select gr.First<Lates>()).Count<Lates>();
        }

        internal static int getLates30_12mo(CcsLocalDbContext db, int applicantId)
        {
            DateTime now = DateTime.Now;
            int year = now.Year - 1;
            DateTime dt12 = new DateTime(year, now.Month, now.Day);
            return (from l in (from l in db.Lates
                where ((l.ApplicantID == applicantId) && (l.AccountType == "Mortgage")) && (l.Late30 != null)
                where DateTime.Compare((DateTime) l.Late30, dt12) >= 0
                select l).ToList<Lates>()
                group l by new { 
                    AccountOpenedDate = l.AccountOpenedDate,
                    HighCreditAmount = l.HighCreditAmount,
                    MonthlyPaymentAmount = l.MonthlyPaymentAmount,
                    Late30 = l.Late30
                } into gr
                select gr.First<Lates>()).Count<Lates>();
        }

        internal static int getLates30_24mo(List<Lates> ls)
        {
            DateTime now = DateTime.Now;
            int year = now.Year - 2;
            new DateTime(year, now.Month, now.Day);
            return (from l in ls
                group l by new { 
                    AccountOpenedDate = l.AccountOpenedDate,
                    HighCreditAmount = l.HighCreditAmount,
                    MonthlyPaymentAmount = l.MonthlyPaymentAmount,
                    Late30 = l.Late30
                } into gr
                select gr.First<Lates>()).Count<Lates>();
        }

        internal static int getLates30_24mo(CcsLocalDbContext db, int applicantId)
        {
            DateTime now = DateTime.Now;
            int year = now.Year - 2;
            DateTime dt24 = new DateTime(year, now.Month, now.Day);
            return (from l in (from l in db.Lates
                where ((l.ApplicantID == applicantId) && (l.AccountType == "Mortgage")) && (l.Late30 != null)
                where DateTime.Compare((DateTime) l.Late30, dt24) >= 0
                select l).ToList<Lates>()
                group l by new { 
                    AccountOpenedDate = l.AccountOpenedDate,
                    HighCreditAmount = l.HighCreditAmount,
                    MonthlyPaymentAmount = l.MonthlyPaymentAmount,
                    Late30 = l.Late30
                } into gr
                select gr.First<Lates>()).Count<Lates>();
        }

        internal static int getLates60_24mo(CcsLocalDbContext db, int applicantId) => 
            0;

        internal static Variables GetLoanVariablesForRateSheet(Application app, Variable v, HiBalance hibalance)
        {
            Variables variables = new Variables();
            if (app.OwnerShipType == OwnershipTypeEnum.Investment)
            {
                variables.isInvestmentProperty = true;
            }
            if (app.OwnerShipType == OwnershipTypeEnum.Primary_Residence)
            {
                variables.isOwnerOccupied = true;
            }
            if (app.OwnerShipType == OwnershipTypeEnum.Second_Home)
            {
                variables.ishome2nd = true;
            }
            if (app.PropertyType == PropertyTypeEnum.Condo)
            {
                variables.isCondo = true;
            }
            if (app.PropertyType == PropertyTypeEnum.MobileHomeWithLand)
            {
                variables.isManufacturedHome = true;
            }
            if (app.PropertyType == PropertyTypeEnum.Modular_Manufactured)
            {
                variables.isManufacturedHome = true;
            }
            if (app.PropertyType == PropertyTypeEnum.Single_Familly_Residence_1_unit)
            {
                variables.units = 1;
            }
            if (app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_2_units)
            {
                variables.units = 2;
            }
            if (app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_3_units)
            {
                variables.units = 3;
            }
            if (app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_4_units)
            {
                variables.units = 4;
            }
            if (app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_5_Plus)
            {
                variables.units = 5;
            }
            bool flag = app.Has2ndMortgage > HaveSecondMortgageEnum.No;
            bool flag2 = ((YesNoAns) app.PayOff2ndMortgage) == YesNoAns.Yes;
            if (app.LoanTypeRequested == LoanTypeRequestedEnum.CashOutMortgage)
            {
                variables.isCashOut = true;
            }
            if (flag && flag2)
            {
                if (v.LoanType == LoanTypeEnum.VA)
                {
                    TimeSpan span = (TimeSpan) (DateTime.Now - app.SecondMortgageOriginationDate.Value);
                    if (span.Days < 330)
                    {
                        variables.isCashOut = true;
                    }
                }
                else if (app.Has2ndMortgage == HaveSecondMortgageEnum.YesForCash)
                {
                    variables.isCashOut = true;
                }
            }
            if (app.LoanTypeRequested == LoanTypeRequestedEnum.PurchaseLoan)
            {
                variables.isPurchase = true;
            }
            if (app.LoanTypeRequested == LoanTypeRequestedEnum.RealtorReferredPurchaseLoan)
            {
                variables.isPurchase = true;
            }
            if (app.LoanTypeRequested == LoanTypeRequestedEnum.RealtorReferredPurchaseLoan)
            {
                variables.isPurchase = true;
            }
            if ((app.LoanTypeRequested == LoanTypeRequestedEnum.RateAndTermRefiLowerPayment) || (app.LoanTypeRequested == LoanTypeRequestedEnum.RateAndTermRefiShorterTerm))
            {
                variables.isRateTerm = true;
            }
            int creditScoreEstimate = (int) app.CreditScoreEstimate;
            variables.fico = creditScoreEstimate;
            if (v.LoanType == LoanTypeEnum.VA)
            {
                variables.isVA = true;
            }
            if (v.LoanType == LoanTypeEnum.USRDA)
            {
                variables.isUSDA = true;
            }
            if (v.LoanType == LoanTypeEnum.FHA)
            {
                variables.isFHA = true;
            }
            if ((v.LoanType == LoanTypeEnum.VA_IRRL) && ((app.LoanTypeRequested == LoanTypeRequestedEnum.RateAndTermRefiLowerPayment) || (app.LoanTypeRequested == LoanTypeRequestedEnum.RateAndTermRefiShorterTerm)))
            {
                variables.isVA = true;
            }
            if ((v.LoanType == LoanTypeEnum.FHA_Streamline) && ((app.LoanTypeRequested == LoanTypeRequestedEnum.RateAndTermRefiLowerPayment) || (app.LoanTypeRequested == LoanTypeRequestedEnum.RateAndTermRefiShorterTerm)))
            {
                variables.isFHA = true;
                variables.isStreamline = true;
            }
            if (v.LoanType == LoanTypeEnum.Jumbo)
            {
                variables.isJumbo = true;
            }
            variables.isHighBalance = hibalance.IsHibalance;
            if (((v.RateType == InterestRateTypeEnum.Adjustable_10_1) || (v.RateType == InterestRateTypeEnum.Adjustable_3_1)) || (((v.RateType == InterestRateTypeEnum.Adjustable_5_1) || (v.RateType == InterestRateTypeEnum.Adjustable_7_1)) || (v.RateType == InterestRateTypeEnum.InterstOnlyAdjustable)))
            {
                variables.isARM = true;
            }
            if (app.PymtIncludesMI)
            {
                variables.isEscrow = true;
            }
            if (((v.RateType == InterestRateTypeEnum.Fixed) || (v.RateType == InterestRateTypeEnum.InterstOnlyFixed)) || (v.RateType == InterestRateTypeEnum.Balloon))
            {
                variables.isFixed = true;
            }
            if ((v.LoanType == LoanTypeEnum.FHA_Streamline) || (v.LoanType == LoanTypeEnum.VA_IRRL))
            {
                variables.isFullappraisal = false;
                variables.isFulldoc = false;
                variables.isStreamline = true;
            }
            else
            {
                variables.isFullappraisal = true;
                variables.isFulldoc = true;
                variables.isStreamline = false;
            }
            if (v.LoanType == LoanTypeEnum.HARP)
            {
                variables.isMaxCumulativeUsed = true;
            }
            if ((app.Has2ndMortgage > HaveSecondMortgageEnum.No) && (((YesNoAns) app.PayOff2ndMortgage) == YesNoAns.No))
            {
                variables.isSubordinateFinancing = true;
            }
            if (v.RateType == InterestRateTypeEnum.Adjustable_10_1)
            {
                variables.isYear10 = true;
            }
            else if (v.RateType == InterestRateTypeEnum.Adjustable_5_1)
            {
                variables.isYear5 = true;
            }
            else if (v.RateType == InterestRateTypeEnum.Adjustable_7_1)
            {
                variables.isYear7 = true;
            }
            else if (v.RateType == InterestRateTypeEnum.Fixed)
            {
                if (v.newTermInYears == 30)
                {
                    variables.isYear30 = true;
                }
                else if (v.newTermInYears == 0x19)
                {
                    variables.isYear25 = true;
                }
                else if (v.newTermInYears == 20)
                {
                    variables.isYear20 = true;
                }
                else if (v.newTermInYears == 15)
                {
                    variables.isYear15 = true;
                }
                else if (v.newTermInYears == 10)
                {
                    variables.isYear10 = true;
                }
            }
            if (v.LenderPaidComp > 0.0)
            {
                variables.lenderPaidMorIns = true;
            }
            variables.loanamount = (float) app.ProposedLoanAmount;
            variables.loanTerm = v.newTermInYears;
            variables.lockTerm = 30f;
            variables.ltv = (float) app.CurrentLTV;
            variables.RateLockDays = 30f;
            variables.stateAbbreviation = app.UsState.ToString();
            return variables;
        }

        private static double GetMaxAffordableLoanAmount(double hoiYearly, double InterestRateYearly, double termInMonths, double downPayment, double maxMonthlyPayment, double propertyTaxesYearly, double MiRatio, double upfrontFundingFee)
        {
            double num = hoiYearly / 12.0;
            double num2 = InterestRateYearly / 12.0;
            double y = termInMonths;
            double num4 = propertyTaxesYearly / 12.0;
            double num5 = maxMonthlyPayment;
            double num6 = 0.0;
            double num7 = downPayment;
            double num8 = 0.0;
            double num9 = (1.0 - (1.0 / Math.Pow(1.0 + num2, y))) / num2;
            double num1 = (num5 - (num7 * (num + num4))) / (1.0 + (num9 * ((num + num4) + num6)));
            num8 = (((num5 - (num7 * (num + num4))) * num9) / (1.0 + (num9 * ((num + num4) + num6)))) / (1.0 + upfrontFundingFee);
            double num10 = (num8 / (num8 + num7)) * 100.0;
            if (num10 > 80.0)
            {
                num6 = MiRatio / 12.0;
                double num11 = (num5 - (num7 * (num + num4))) / (1.0 + (num9 * ((num + num4) + num6)));
                num8 = (((num5 - (num7 * (num + num4))) * num9) / (1.0 + (num9 * ((num + num4) + num6)))) / (1.0 + upfrontFundingFee);
            }
            return num8;
        }

        internal static HiBalance GetMaxLoanLimit(CcsLocalDbContext db, int fips, LoanTypeEnum lntype, PropertyTypeEnum prpType, double maxLoanAmount)
        {
            HiBalance balance = new HiBalance {
                IsHibalance = false,
                maxLoanLimit = 0M
            };
            decimal loanLimit = 0.0M;
            if (lntype == LoanTypeEnum.ConventonalConforming)
            {
                CountyLoanLimitConv conv = (from c in db.CountyLoanLimitConvs
                    where c.Fips == fips
                    select c).FirstOrDefault<CountyLoanLimitConv>();
                if (conv != null)
                {
                    if (prpType == PropertyTypeEnum.Multi_Familly_Residence_2_units)
                    {
                        loanLimit = conv.LoanLimit2Unit;
                    }
                    else if (prpType == PropertyTypeEnum.Multi_Familly_Residence_3_units)
                    {
                        loanLimit = conv.LoanLimit3Unit;
                    }
                    else if (prpType == PropertyTypeEnum.Multi_Familly_Residence_4_units)
                    {
                        loanLimit = conv.LoanLimit4Unit;
                    }
                    else
                    {
                        loanLimit = conv.LoanLimit1Unit;
                    }
                    if (conv.LoanLimit1Unit > ((decimal) maxLoanAmount))
                    {
                        balance.IsHibalance = true;
                    }
                }
            }
            else if ((lntype == LoanTypeEnum.FHA) || (lntype == LoanTypeEnum.FHA_Streamline))
            {
                CountyLoanLimitFHA tfha = (from c in db.CountyLoanLimitFHAs
                    where c.Fips == fips
                    select c).FirstOrDefault<CountyLoanLimitFHA>();
                if (tfha != null)
                {
                    if (prpType == PropertyTypeEnum.Multi_Familly_Residence_2_units)
                    {
                        loanLimit = tfha.LoanLimit2Unit;
                    }
                    else if (prpType == PropertyTypeEnum.Multi_Familly_Residence_3_units)
                    {
                        loanLimit = tfha.LoanLimit3Unit;
                    }
                    else if (prpType == PropertyTypeEnum.Multi_Familly_Residence_4_units)
                    {
                        loanLimit = tfha.LoanLimit4Unit;
                    }
                    else
                    {
                        loanLimit = tfha.LoanLimit1Unit;
                    }
                    if (tfha.LoanLimit1Unit > ((decimal) maxLoanAmount))
                    {
                        balance.IsHibalance = true;
                    }
                }
            }
            else if ((lntype == LoanTypeEnum.VA) || (lntype == LoanTypeEnum.VA_IRRL))
            {
                CountyLoanLimitVA tva = (from c in db.CountyLoanLimitVAs
                    where c.Fips == fips
                    select c).FirstOrDefault<CountyLoanLimitVA>();
                if (tva != null)
                {
                    loanLimit = tva.LoanLimit;
                    balance.IsHibalance = true;
                }
            }
            balance.maxLoanLimit = loanLimit;
            return balance;
        }

        private static double GetMaxPayment(double frontDti, double backDTI, double monthlyIncome, double monthlyDebt)
        {
            double num = backDTI * (monthlyIncome - monthlyDebt);
            double num2 = frontDti * monthlyIncome;
            return Math.Min(num2, num);
        }

        private static double GetMiFactor(Variable variable, Mortgage curMort)
        {
            double miFactor = variable.MiFactor;
            if ((variable.MortgageProgramOption.Trim() == "HARP_FannieBefore2009") && (curMort.MonthlyMortgageInsurance == 0M))
            {
                return variable.MiFactor;
            }
            if (variable.MortgageProgramOption.Trim() == "HARP_FredieBefore2009")
            {
                return variable.MiFactor;
            }
            return miFactor;
        }

        internal static int getMILTV(double LoanAmount, double TermInMonths, double interestYearly, double monthlyPayment, double targetLTV, double homeValue)
        {
            double num4 = 0.0;
            double num5 = (interestYearly / 100.0) * 0.083333333333333329;
            double num6 = LoanAmount;
            for (int i = 0; i < TermInMonths; i++)
            {
                double num2 = num6 * num5;
                double num3 = monthlyPayment - num2;
                num6 -= num3;
                num4 = (num6 / homeValue) * 100.0;
                if (num4 <= targetLTV)
                {
                    return (i + 1);
                }
            }
            return 0;
        }

        internal static decimal GetMIP(decimal loanAmount, decimal propValue, int termInYears)
        {
            double num = (double) loanAmount;
            double num2 = 0.0059000002220273018;
            double num3 = (num * num2) / 12.0;
            return (decimal) num3;
        }

        internal static List<OptionSelected> getOptionsSelected(Mortgage curMtg)
        {
            List<OptionSelected> list = new List<OptionSelected>();
            List<RefiOption> convHarpOptions = GetConvHarpOptions(curMtg);
            int num = 1;
            foreach (RefiOption option in convHarpOptions)
            {
                OptionSelected item = new OptionSelected {
                    OptIntex = num
                };
                num++;
                item.PreparedFor = option.PreparedFor;
                item.InterestRate = option.InterestRate;
                item.LoanAmount = option.LoanAmount;
                item.CostSavingsBreakEvenAnalysis = option.CostSavingsBreakEvenAnalysis;
                item.MonthlySavings = option.MonthlySavings;
                item.NewMonthlyPaymentPrincipalInterest = option.NewMonthlyPaymentPrincipalInterest;
                item.OptionName = option.OptionName;
                item.TermInYears = option.TermInYears;
                item.TotalSavingsFromOldMortgageToNewMortgage = option.TotalSavingsFromOldMortgageToNewMortgage;
                item.VarNum = option.VarNum;
                item.APR = option.APR;
                item.InterestRateSavings = option.InterestRateSavings;
                item.MonthlyPaymentsEliminated = option.MonthlyPaymentsEliminated;
                item.AcceleratedPayoffTotalAmountOfAllPayments = option.AcceleratedPayoffTotalAmountOfAllPayments;
                item.NetSavingsFromOldLoanToNewLoan = option.NetSavingsFromOldLoanToNewLoan;
                item.OldMonthlyPaymentPrincipalInterest = option.OldMonthlyPaymentPrincipalInterest;
                item.MonthsPaidRemaining = option.MonthsPaidRemaining;
                item.NumberofPaymentstoMaturity = option.NumberofPaymentstoMaturity;
                item.EffectiveInterestRate = option.EffectiveInterestRate;
                item.Cashout = option.Cashout;
                item.SecondMtgBalance = option.SecondMtgBalance;
                item.ShowNetSaving = option.ShowNetSaving;
                item.LoanBalance = option.LoanBalance;
                item.ShowCashout = option.ShowCashout;
                item.ShowSecondMtg = option.ShowSecondMtg;
                item.ShowInterestSaving = option.ShowInterestSaving;
                item.ShowBlendedRate = option.ShowBlendedRate;
                item.ShowDebtConsal = option.ShowDebtConsal;
                item.ShowMonthlySaving = option.ShowMonthlySaving;
                item.BlendedRateSavings = option.BlendedRateSavings;
                item.NewPaymentPlusMonthlySavings = option.NewPaymentPlusMonthlySavings;
                item.TotalBalanceOfDebtToConsolidate = option.TotalBalanceOfDebtToConsolidate;
                list.Add(item);
            }
            return list;
        }

        internal static List<OptionSelected> getOptionsSelected(CcsLocalDbContext db, Application app)
        {
            List<OptionSelected> list = new List<OptionSelected>();
            List<RefiOption> convHarpOptions = GetConvHarpOptions(db, app);
            int num = 1;
            foreach (RefiOption option in convHarpOptions)
            {
                OptionSelected item = new OptionSelected {
                    OptIntex = num
                };
                num++;
                item.PreparedFor = option.PreparedFor;
                item.InterestRate = option.InterestRate;
                item.LoanAmount = option.LoanAmount;
                item.CostSavingsBreakEvenAnalysis = option.CostSavingsBreakEvenAnalysis;
                item.MonthlySavings = option.MonthlySavings;
                item.NewMonthlyPaymentPrincipalInterest = option.NewMonthlyPaymentPrincipalInterest;
                item.OptionName = option.OptionName;
                item.TermInYears = option.TermInYears;
                item.TotalSavingsFromOldMortgageToNewMortgage = option.TotalSavingsFromOldMortgageToNewMortgage;
                item.VarNum = option.VarNum;
                item.APR = option.APR;
                item.InterestRateSavings = option.InterestRateSavings;
                item.MonthlyPaymentsEliminated = option.MonthlyPaymentsEliminated;
                item.AcceleratedPayoffTotalAmountOfAllPayments = option.AcceleratedPayoffTotalAmountOfAllPayments;
                item.NetSavingsFromOldLoanToNewLoan = option.NetSavingsFromOldLoanToNewLoan;
                item.OldMonthlyPaymentPrincipalInterest = option.OldMonthlyPaymentPrincipalInterest;
                item.MonthsPaidRemaining = option.MonthsPaidRemaining;
                item.NumberofPaymentstoMaturity = option.NumberofPaymentstoMaturity;
                item.EffectiveInterestRate = option.EffectiveInterestRate;
                item.Cashout = option.Cashout;
                item.SecondMtgBalance = option.SecondMtgBalance;
                item.ShowNetSaving = option.ShowNetSaving;
                item.LoanBalance = option.LoanBalance;
                item.ShowCashout = option.ShowCashout;
                item.ShowSecondMtg = option.ShowSecondMtg;
                item.ShowInterestSaving = option.ShowInterestSaving;
                item.ShowBlendedRate = option.ShowBlendedRate;
                item.ShowDebtConsal = option.ShowDebtConsal;
                item.ShowMonthlySaving = option.ShowMonthlySaving;
                item.LTV = option.LTV;
                item.CLTV = option.CLTV;
                item.FrontDTI = option.FrontDTI;
                item.BackDTI = option.BackDTI;
                item.BlendedRateSavings = option.BlendedRateSavings;
                item.NewPaymentPlusMonthlySavings = option.NewPaymentPlusMonthlySavings;
                item.TotalBalanceOfDebtToConsolidate = option.TotalBalanceOfDebtToConsolidate;
                list.Add(item);
            }
            return list;
        }

        internal static double GetPayment(double MortgageAmount, double annualInterestRate, double termYears)
        {
            double y = termYears * 12.0;
            double num3 = annualInterestRate / 1200.0;
            double num4 = MortgageAmount;
            return ((num4 * (num3 * Math.Pow(1.0 + num3, y))) / (Math.Pow(1.0 + num3, y) - 1.0));
        }

        internal static PaymentAndBalance GetPaymentAndBalanceOld(CcsLocalDbContext db, int applicantId)
        {
            PaymentAndBalance balance = new PaymentAndBalance {
                Balance = 0.0,
                payment = 0.0
            };
            List<CreditLiability> list = (from l in db.CreditLiabilities
                where l.ApplicantID == applicantId
                select l).ToList<CreditLiability>();
            new List<string>();
            double num = 0.0;
            double num2 = 0.0;
            List<double> list2 = new List<double>();
            List<double> list3 = new List<double>();
            List<double> list4 = new List<double>();
            List<DateTime?> list5 = new List<DateTime?>();
            List<DateTime?> list6 = new List<DateTime?>();
            int num3 = 0;
            bool flag = false;
            if (list != null)
            {
                foreach (CreditLiability liability in list)
                {
                    if ((liability.Balance == 0.0) || liability.ToBePaidOff)
                    {
                        continue;
                    }
                    flag = false;
                    for (int i = 0; i < list3.Count; i++)
                    {
                        if (((list2[num3] == liability.MonthlyPayment) && (list3[num3] == liability.HiCredit)) && (list5[num3] == liability.DateOpened))
                        {
                            flag = true;
                            if (liability.LastActivityDate.HasValue)
                            {
                                DateTime? nullable4 = list6[num3];
                                if (nullable4.HasValue)
                                {
                                    DateTime? nullable6 = list6[num3];
                                    int num5 = DateTime.Compare(liability.LastActivityDate.Value, nullable6.Value);
                                    if (((num5 == 0) && (liability.Balance < list4[num3])) || (num5 > 0))
                                    {
                                        num2 -= list2[num3];
                                        num -= list4[num3];
                                        flag = false;
                                    }
                                    break;
                                }
                            }
                            if (liability.LastActivityDate.HasValue)
                            {
                                DateTime? nullable8 = list6[num3];
                                if (!nullable8.HasValue && (liability.Balance < list4[num3]))
                                {
                                    num2 -= list2[num3];
                                    num -= list4[num3];
                                    flag = false;
                                }
                            }
                            break;
                        }
                    }
                    if (!flag)
                    {
                        num += liability.Balance;
                        num2 += liability.MonthlyPayment;
                        list2.Add(liability.MonthlyPayment);
                        list3.Add(liability.HiCredit);
                        list5.Add(liability.DateOpened);
                        list4.Add(liability.Balance);
                        list6.Add(liability.LastActivityDate);
                    }
                }
            }
            balance.payment = num2;
            balance.Balance = num;
            return balance;
        }

        internal static List<PurchaseOption> GetPurchaseOptions(Mortgage MtgApp)
        {
            List<PurchaseOption> list = new List<PurchaseOption>();
            List<Variable> purshaseVariables = GetPurshaseVariables();
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            for (int i = 0; i < purshaseVariables.Count; i++)
            {
                if (((!MtgApp.MortgagedProperty.OwnerShipType.HasValue || ((((((OwnershipTypeEnum) MtgApp.MortgagedProperty.OwnerShipType) != OwnershipTypeEnum.Investment) || purshaseVariables[i].Investment) && ((((OwnershipTypeEnum) MtgApp.MortgagedProperty.OwnerShipType) != OwnershipTypeEnum.Primary_Residence) || purshaseVariables[i].PrimaryResidence)) && ((((OwnershipTypeEnum) MtgApp.MortgagedProperty.OwnerShipType) != OwnershipTypeEnum.Second_Home) || purshaseVariables[i].SecondHome))) && (!MtgApp.MortgagedProperty.PropertyTypeApp.HasValue || ((((((((PropertyTypeEnum) MtgApp.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Modular_Manufactured) || purshaseVariables[i].Manufactured) && ((((PropertyTypeEnum) MtgApp.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Single_Familly_Residence_1_unit) || purshaseVariables[i].SFR)) && ((((PropertyTypeEnum) MtgApp.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Multi_Familly_Residence_5_Plus) || purshaseVariables[i].MultiUnits)) && (((((PropertyTypeEnum) MtgApp.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Multi_Familly_Residence_2_units) || (purshaseVariables[i].MaxNumberOfUnits >= 2)) && ((((PropertyTypeEnum) MtgApp.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Multi_Familly_Residence_3_units) || (purshaseVariables[i].MaxNumberOfUnits >= 3)))) && ((((((PropertyTypeEnum) MtgApp.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Multi_Familly_Residence_4_units) || (purshaseVariables[i].MaxNumberOfUnits >= 4)) && (((((PropertyTypeEnum) MtgApp.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Condo) || purshaseVariables[i].Condo) && ((((PropertyTypeEnum) MtgApp.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.MobileHomeWithLand) || purshaseVariables[i].MobileHome))) && ((((PropertyTypeEnum) MtgApp.MortgagedProperty.PropertyTypeApp) != PropertyTypeEnum.Townhome) || purshaseVariables[i].TownHome))))) && (((MtgApp.MortgageApplicant.Veteran != YesNoAns.No) || (purshaseVariables[i].LoanType != LoanTypeEnum.VA)) && ((MtgApp.MortgagedProperty.Rural != YesNoAns.No) || (purshaseVariables[i].LoanType != LoanTypeEnum.USRDA))))
                {
                    decimal? nullable23;
                    PurchaseOption item = new PurchaseOption {
                        OptionName = purshaseVariables[i].ScheduleName,
                        DatePrepared = new DateTime?(DateTime.Now),
                        PurchasePrice = MtgApp.PurchasePrice,
                        PreparedFor = MtgApp.MortgageApplicant.FirstName + " " + MtgApp.MortgageApplicant.LastName,
                        DownPayment = MtgApp.DownPayment,
                        SellerPaidCreditClosingCost = MtgApp.SellerPaidCreditClosingCost,
                        EstimatedHomeownersAssociationFeesAnnual = MtgApp.EstimatedHomeownersAssociationFeesAnnual,
                        TermInYears = new int?(purshaseVariables[i].newTermInYears)
                    };
                    item.TermInMonths = item.TermInYears * 12;
                    item.PercentCharge = new double?(purshaseVariables[i].OriginationPercent * 100.0);
                    item.ProcessingFee = new decimal?(purshaseVariables[i].ProcessingFee);
                    item.UnderwritingFee = new decimal?(purshaseVariables[i].UnderwritingFee);
                    item.CreditReportFee = new decimal?(purshaseVariables[i].CreditReportFee);
                    item.AppraisalFee = new decimal?(purshaseVariables[i].AppraisalFee);
                    item.ClosingEscrowFee = new decimal?(purshaseVariables[i].ClosingEscrowFee);
                    item.EndorsementsReconveyanceFee = new decimal?(purshaseVariables[i].EndorsementsReconveyanceFee);
                    item.MortgageRecordingCharges = new decimal?(purshaseVariables[i].MortgageRecordingfee);
                    item.NumberOfDays = 5;
                    item.InterestRate = purshaseVariables[i].NewInterestRate;
                    item.SurveyFee = purshaseVariables[i].SurveyFee;
                    item.TaxServiceFee = purshaseVariables[i].TaxServiceFee;
                    item.FloodCertificationFee = purshaseVariables[i].FloodCertificationFee;
                    item.PestInspectionFee = purshaseVariables[i].PestInspectionFee;
                    num = (purshaseVariables[i].PropertyTaxPercent * ((double) item.PurchasePrice.Value)) / 12.0;
                    item.monthlyTaxes = new decimal?((decimal) num);
                    item.MI_MonthsReserves = 2;
                    decimal? monthlyTaxes = item.monthlyTaxes;
                    decimal numofMonthstoEscrowTaxes = purshaseVariables[i].NumofMonthstoEscrowTaxes;
                    item.CountyPropertyTaxReserves = monthlyTaxes.HasValue ? new decimal?(monthlyTaxes.GetValueOrDefault() * numofMonthstoEscrowTaxes) : ((decimal?) (nullable23 = null));
                    item.CT_MonthsReserves = new decimal?(purshaseVariables[i].NumofMonthstoEscrowTaxes);
                    item.HI_MonthsReserves = new decimal?(purshaseVariables[i].NumofMonthstoEscrowHazardInsurance);
                    num2 = (((double) item.PurchasePrice.Value) * purshaseVariables[i].HazardInsurancePercent) / 12.0;
                    item.HI_MonthlyAmount = new decimal?((decimal) num2);
                    num2 *= (double) item.HI_MonthsReserves.Value;
                    item.HazardInsuranceReserves = new decimal?((decimal) num2);
                    item.TermInYears = new int?(purshaseVariables[i].newTermInYears);
                    item.OriginationChargesPercent = new double?(purshaseVariables[i].OriginationPercent);
                    item.TotalFixedFees = new decimal?(((((((purshaseVariables[i].ProcessingFee + purshaseVariables[i].UnderwritingFee) + purshaseVariables[i].AppraisalFee) + purshaseVariables[i].CreditReportFee) + purshaseVariables[i].ClosingEscrowFee) + purshaseVariables[i].EndorsementsReconveyanceFee) + purshaseVariables[i].SurveyFee) + purshaseVariables[i].MortgageRecordingfee);
                    double num5 = (0.05 * purshaseVariables[i].NewInterestRate) / 360.0;
                    double originationPercent = purshaseVariables[i].OriginationPercent;
                    double lenderCreditPercent = purshaseVariables[i].LenderCreditPercent;
                    double titleInusrancePercent = purshaseVariables[i].TitleInusrancePercent;
                    double intangibleTaxPercent = purshaseVariables[i].IntangibleTaxPercent;
                    double stateTaxPercent = purshaseVariables[i].StateTaxPercent;
                    double discountPercent = purshaseVariables[i].DiscountPercent;
                    item.LoanAmount = item.PurchasePrice - item.DownPayment;
                    item.MI_Upfront_Fee = new decimal?(Util.GetUpFrontFee(purshaseVariables[i].UpfrontMI, item.LoanAmount.Value));
                    item.LoanAmount += item.MI_Upfront_Fee;
                    num3 = (purshaseVariables[i].MiFactor * ((double) item.LoanAmount.Value)) / 12.0;
                    item.NewMI_MonthlyAmount = new decimal?((decimal) num3);
                    item.PMI_MIP_VA_FFReserves = item.NewMI_MonthlyAmount * 2.00M;
                    double mortgageAmount = (double) item.LoanAmount.Value;
                    item.NewMonthlyPaymentPrincipalInterest = new decimal?((decimal) GetPayment(mortgageAmount, item.InterestRate, (double) item.TermInYears.Value));
                    decimal? nullable47 = ((item.NewMonthlyPaymentPrincipalInterest + item.NewMI_MonthlyAmount) + item.HI_MonthlyAmount) + item.monthlyTaxes;
                    decimal? loanAmount = item.EstimatedHomeownersAssociationFeesAnnual / 12.00M;
                    item.TotalNewPrincipalInterestPaymentWithMI = (nullable47.HasValue & loanAmount.HasValue) ? new decimal?(nullable47.GetValueOrDefault() + loanAmount.GetValueOrDefault()) : ((decimal?) (monthlyTaxes = null));
                    item.CreditPoints = new double?(purshaseVariables[i].LenderCreditPercent);
                    loanAmount = item.LoanAmount;
                    numofMonthstoEscrowTaxes = (decimal) item.CreditPoints.Value;
                    item.Credit = loanAmount.HasValue ? new decimal?(loanAmount.GetValueOrDefault() * numofMonthstoEscrowTaxes) : ((decimal?) (monthlyTaxes = null));
                    loanAmount = item.LoanAmount;
                    numofMonthstoEscrowTaxes = (decimal) purshaseVariables[i].OriginationPercent;
                    loanAmount = loanAmount.HasValue ? new decimal?(loanAmount.GetValueOrDefault() * numofMonthstoEscrowTaxes) : null;
                    monthlyTaxes = item.Credit;
                    item.AdjustedLoanOriginationFee = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() - monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    numofMonthstoEscrowTaxes = (decimal) purshaseVariables[i].TitleInusrancePercent;
                    loanAmount = item.LoanAmount;
                    item.LenderTitleInsurance = loanAmount.HasValue ? new decimal?(numofMonthstoEscrowTaxes * loanAmount.GetValueOrDefault()) : ((decimal?) (monthlyTaxes = null));
                    item.DiscountPoints = new double?(purshaseVariables[i].DiscountPercent * 100.0);
                    numofMonthstoEscrowTaxes = (decimal) purshaseVariables[i].DiscountPercent;
                    loanAmount = item.LoanAmount;
                    item.Discount = loanAmount.HasValue ? new decimal?(numofMonthstoEscrowTaxes * loanAmount.GetValueOrDefault()) : ((decimal?) (monthlyTaxes = null));
                    numofMonthstoEscrowTaxes = (decimal) purshaseVariables[i].IntangibleTaxPercent;
                    loanAmount = item.LoanAmount;
                    item.IntangibleTax = loanAmount.HasValue ? new decimal?(numofMonthstoEscrowTaxes * loanAmount.GetValueOrDefault()) : ((decimal?) (monthlyTaxes = null));
                    numofMonthstoEscrowTaxes = (decimal) purshaseVariables[i].StateTaxPercent;
                    loanAmount = item.LoanAmount;
                    item.StateTax = loanAmount.HasValue ? new decimal?(numofMonthstoEscrowTaxes * loanAmount.GetValueOrDefault()) : ((decimal?) (monthlyTaxes = null));
                    numofMonthstoEscrowTaxes = (decimal) num5;
                    item.DailyInterestCharges = numofMonthstoEscrowTaxes * item.LoanAmount;
                    loanAmount = item.HazardInsuranceReserves;
                    monthlyTaxes = item.CountyPropertyTaxReserves;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.PMI_MIP_VA_FFReserves;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.DailyInterestCharges;
                    item.EstimatedPrepaidsReserves = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    loanAmount = item.ProcessingFee;
                    monthlyTaxes = item.UnderwritingFee;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.AppraisalFee;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.CreditReportFee;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.ClosingEscrowFee;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.EndorsementsReconveyanceFee;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.MortgageRecordingCharges;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.AdjustedLoanOriginationFee;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.LenderTitleInsurance;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.Discount;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.IntangibleTax;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.StateTax;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    numofMonthstoEscrowTaxes = item.SurveyFee;
                    loanAmount = loanAmount.HasValue ? new decimal?(loanAmount.GetValueOrDefault() + numofMonthstoEscrowTaxes) : ((decimal?) (monthlyTaxes = null));
                    numofMonthstoEscrowTaxes = item.TaxServiceFee;
                    loanAmount = loanAmount.HasValue ? new decimal?(loanAmount.GetValueOrDefault() + numofMonthstoEscrowTaxes) : ((decimal?) (monthlyTaxes = null));
                    numofMonthstoEscrowTaxes = item.FloodCertificationFee;
                    loanAmount = loanAmount.HasValue ? new decimal?(loanAmount.GetValueOrDefault() + numofMonthstoEscrowTaxes) : ((decimal?) (monthlyTaxes = null));
                    numofMonthstoEscrowTaxes = item.PestInspectionFee;
                    item.EstClosingCosts = loanAmount.HasValue ? new decimal?(loanAmount.GetValueOrDefault() + numofMonthstoEscrowTaxes) : null;
                    loanAmount = item.EstClosingCosts;
                    monthlyTaxes = MtgApp.SellerPaidCreditClosingCost;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() - monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.EstimatedPrepaidsReserves;
                    item.EstimatedFundsNeeded = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    loanAmount = item.PurchasePrice;
                    monthlyTaxes = item.EstimatedPrepaidsReserves;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.EstClosingCosts;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.MI_Upfront_Fee;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.Discount;
                    item.TotalAmountNeeded = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    decimal? nullable = 0M;
                    double payment = 0.0;
                    loanAmount = item.LoanAmount;
                    monthlyTaxes = item.AdjustedLoanOriginationFee;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() - monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.ProcessingFee;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() - monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.UnderwritingFee;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() - monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.MI_Upfront_Fee;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() - monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.Discount;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() - monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.ClosingEscrowFee;
                    loanAmount = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() - monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    monthlyTaxes = item.DailyInterestCharges;
                    nullable = (loanAmount.HasValue & monthlyTaxes.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() - monthlyTaxes.GetValueOrDefault()) : ((decimal?) (nullable23 = null));
                    if (purshaseVariables[i].LoanType == LoanTypeEnum.FHA)
                    {
                        payment = ((((((double) item.NewMonthlyPaymentPrincipalInterest.Value) + ((double) item.NewMI_MonthlyAmount.Value)) * purshaseVariables[i].MiDurationYears) * 12.0) + ((((double) item.NewMonthlyPaymentPrincipalInterest.Value) * (30.0 - purshaseVariables[i].MiDurationYears)) * 12.0)) / 360.0;
                    }
                    else
                    {
                        int num8 = getMILTV((double) nullable.Value, (double) item.TermInMonths.Value, item.InterestRate, (double) item.NewMonthlyPaymentPrincipalInterest.Value, 78.0, (double) item.PurchasePrice.Value);
                        payment = (((((double) item.NewMonthlyPaymentPrincipalInterest.Value) + ((double) item.NewMI_MonthlyAmount.Value)) * num8) + (((double) item.NewMonthlyPaymentPrincipalInterest.Value) * (((double) item.TermInMonths.Value) - num8))) / 360.0;
                    }
                    item.APR = new double?(GetAPR((double) nullable.Value, item.InterestRate / 1200.0, (double) item.TermInMonths.Value, payment));
                    list.Add(item);
                }
            }
            return list;
        }

        internal static List<PurchaseOption> GetPurchaseOptions(CcsLocalDbContext db, Application app)
        {
            if (!RateSheetLoaded)
            {
                RateSheetProcessor.LoadFromSerializedFile(Path.Combine(HttpContext.Current.Server.MapPath("~/Content/DataFiles"), "Plaza_JAX_Rates.phz"));
            }
            List<PurchaseOption> list = new List<PurchaseOption>();
            List<Variable> purshaseVariables = GetPurshaseVariables();
            Variables v = new Variables();
            bool flag = false;
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            if (!app.DownPaymentAmount.HasValue)
            {
                app.DownPaymentAmount = 0;
            }
            for (int i = 0; i < purshaseVariables.Count; i++)
            {
                decimal? nullable18;
                if ((((((((app.OwnerShipType == OwnershipTypeEnum.Investment) && !purshaseVariables[i].Investment) || ((app.OwnerShipType == OwnershipTypeEnum.Primary_Residence) && !purshaseVariables[i].PrimaryResidence)) || (((app.OwnerShipType == OwnershipTypeEnum.Second_Home) && !purshaseVariables[i].SecondHome) || ((app.PropertyType == PropertyTypeEnum.Modular_Manufactured) && !purshaseVariables[i].Manufactured))) || (((app.PropertyType == PropertyTypeEnum.Single_Familly_Residence_1_unit) && !purshaseVariables[i].SFR) || ((app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_5_Plus) && !purshaseVariables[i].MultiUnits))) || ((app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_2_units) && (purshaseVariables[i].MaxNumberOfUnits < 2))) || (((app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_3_units) && (purshaseVariables[i].MaxNumberOfUnits < 3)) || ((app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_4_units) && (purshaseVariables[i].MaxNumberOfUnits < 4)))) || (((app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_5_Plus) && (purshaseVariables[i].MaxNumberOfUnits < 5)) || (((((app.PropertyType == PropertyTypeEnum.Condo) && !purshaseVariables[i].Condo) || ((app.PropertyType == PropertyTypeEnum.MobileHomeWithLand) && !purshaseVariables[i].MobileHome)) || (((app.PropertyType == PropertyTypeEnum.Townhome) && !purshaseVariables[i].TownHome) || ((app.Veteran == YesNoAns.No) && (purshaseVariables[i].LoanType == LoanTypeEnum.VA)))) || ((app.RuralProperty == YesNoAns.No) && (purshaseVariables[i].LoanType == LoanTypeEnum.USRDA)))))
                {
                    continue;
                }
                if (app.creditPulled && !app.NoMorgagesOnCredit)
                {
                    if (app.lates12Credit <= purshaseVariables[i].NumOf30LateAllowedIn12Mo)
                    {
                        goto Label_02C6;
                    }
                    continue;
                }
                int num5 = ((int) app.DaysLate) - 1;
                if (num5 > purshaseVariables[i].NumOf30LateAllowedIn12Mo)
                {
                    continue;
                }
            Label_02C6:
                if (app.creditPulled)
                {
                    if (app.CreditScore >= int.Parse(purshaseVariables[i].CreditScoreRange))
                    {
                        goto Label_030A;
                    }
                    continue;
                }
                if ((int)app.CreditScoreEstimate < int.Parse(purshaseVariables[i].CreditScoreRange))
                {
                    continue;
                }
            Label_030A:
                if (app.creditPulled)
                {
                    foreach (PublicRecord record in app.publicRecords)
                    {
                        if ((record.IsQualifiedChapter13(purshaseVariables[i].Bankruptcy.Value) && record.IsQualifiedChapter7(purshaseVariables[i].Bankruptcy.Value)) && record.IsQualifiedChapter13(purshaseVariables[i].Bankruptcy.Value))
                        {
                            record.IsQualifiedforeclosure(purshaseVariables[i].Foreclosure.Value);
                        }
                    }
                }
                else if ((((app.FiledBankruptcyType == FiledBankruptcyTypeEnum.Chapter13Discharged) && !qualify(app.BankruptcyDischargeDate.Value, purshaseVariables[i].Bankruptcy.Value)) || (app.FiledBankruptcyType == FiledBankruptcyTypeEnum.Chapter13RepaymentStillOpen)) || (((app.FiledBankruptcyType == FiledBankruptcyTypeEnum.Chapter7Discharged) && !qualify(app.BankruptcyDischargeDate.Value, purshaseVariables[i].Bankruptcy.Value)) || ((app.ForeclosuresShortSaleDeedinLieu > ForeclosuresShortSaleDeedinLieuEnum.No) && !qualify(app.ForeclosureShortSaleDeedinLieuDate.Value, purshaseVariables[i].Foreclosure.Value))))
                {
                    continue;
                }
                HiBalance hibalance = GetMaxLoanLimit(db, app.Fips, purshaseVariables[i].LoanType, app.PropertyType, purshaseVariables[i].MaxLoanAmount);
                decimal purchasePrice = app.PurchasePrice;
                decimal? downPaymentAmount = app.DownPaymentAmount;
                decimal? nullable19 = downPaymentAmount.HasValue ? new decimal?(purchasePrice - downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                app.ProposedLoanAmount = nullable19.Value;
                app.CurrentLTV = (double) ((app.ProposedLoanAmount / app.PurchasePrice) * 100M);
                if (!flag)
                {
                    v = GetLoanVariablesForRateSheet(app, purshaseVariables[i], hibalance);
                    flag = true;
                }
                RateSheetProcessor.RetrieveRebate(purshaseVariables[i].ScheduleName, v);
                RatePricing pricing = RatePricing.GetPar30(PopPricingList(v, purshaseVariables[i].ScheduleName, purshaseVariables[i].LenderPaidComp));
                double maxLoanLimit = (double) hibalance.maxLoanLimit;
                if (maxLoanLimit == 0.0)
                {
                    maxLoanLimit = purshaseVariables[i].MaxLoanAmount;
                }
                PurchaseOption item = new PurchaseOption {
                    OptionName = purshaseVariables[i].ScheduleName,
                    DatePrepared = new DateTime?(DateTime.Now),
                    PurchasePrice = new decimal?(app.PurchasePrice),
                    PreparedFor = app.FirstName + " " + app.LastName,
                    DownPayment = app.DownPaymentAmount,
                    SellerPaidCreditClosingCost = app.SellerPaidCreditClosingCost,
                    EstimatedHomeownersAssociationFeesAnnual = app.EstimatedHomeownersAssociationFeesAnnual,
                    TermInYears = new int?(purshaseVariables[i].newTermInYears)
                };
                item.TermInMonths = item.TermInYears * 12;
                item.PercentCharge = new double?(purshaseVariables[i].OriginationPercent * 100.0);
                item.ProcessingFee = new decimal?(purshaseVariables[i].ProcessingFee);
                item.UnderwritingFee = new decimal?(purshaseVariables[i].UnderwritingFee);
                item.CreditReportFee = new decimal?(purshaseVariables[i].CreditReportFee);
                item.AppraisalFee = new decimal?(purshaseVariables[i].AppraisalFee);
                item.ClosingEscrowFee = new decimal?(purshaseVariables[i].ClosingEscrowFee);
                item.EndorsementsReconveyanceFee = new decimal?(purshaseVariables[i].EndorsementsReconveyanceFee);
                item.MortgageRecordingCharges = new decimal?(purshaseVariables[i].MortgageRecordingfee);
                item.NumberOfDays = 5;
                item.InterestRate = pricing.interest;
                item.SurveyFee = purshaseVariables[i].SurveyFee;
                item.TaxServiceFee = purshaseVariables[i].TaxServiceFee;
                item.FloodCertificationFee = purshaseVariables[i].FloodCertificationFee;
                item.PestInspectionFee = purshaseVariables[i].PestInspectionFee;
                num = (purshaseVariables[i].PropertyTaxPercent * ((double) item.PurchasePrice.Value)) / 12.0;
                item.monthlyTaxes = new decimal?((decimal) num);
                item.MI_MonthsReserves = 2;
                decimal? monthlyTaxes = item.monthlyTaxes;
                decimal numofMonthstoEscrowTaxes = purshaseVariables[i].NumofMonthstoEscrowTaxes;
                item.CountyPropertyTaxReserves = monthlyTaxes.HasValue ? new decimal?(monthlyTaxes.GetValueOrDefault() * numofMonthstoEscrowTaxes) : null;
                item.CT_MonthsReserves = new decimal?(purshaseVariables[i].NumofMonthstoEscrowTaxes);
                item.HI_MonthsReserves = new decimal?(purshaseVariables[i].NumofMonthstoEscrowHazardInsurance);
                num2 = (((double) item.PurchasePrice.Value) * purshaseVariables[i].HazardInsurancePercent) / 12.0;
                item.HI_MonthlyAmount = new decimal?((decimal) num2);
                num2 *= (double) item.HI_MonthsReserves.Value;
                item.HazardInsuranceReserves = new decimal?((decimal) num2);
                item.TermInYears = new int?(purshaseVariables[i].newTermInYears);
                item.OriginationChargesPercent = new double?(purshaseVariables[i].OriginationPercent);
                item.TotalFixedFees = new decimal?(((((((purshaseVariables[i].ProcessingFee + purshaseVariables[i].UnderwritingFee) + purshaseVariables[i].AppraisalFee) + purshaseVariables[i].CreditReportFee) + purshaseVariables[i].ClosingEscrowFee) + purshaseVariables[i].EndorsementsReconveyanceFee) + purshaseVariables[i].SurveyFee) + purshaseVariables[i].MortgageRecordingfee);
                double num7 = (0.05 * item.InterestRate) / 360.0;
                item.LoanAmount = item.PurchasePrice - item.DownPayment;
                double num8 = (((double) item.LoanAmount.Value) / ((double) app.PurchasePrice)) * 100.0;
                if (num8 <= purshaseVariables[i].MaxLTV)
                {
                    item.LTV = num8;
                    item.CLTV = num8;
                    if (maxLoanLimit >= ((double) item.LoanAmount.Value))
                    {
                        item.MI_Upfront_Fee = new decimal?(Util.GetUpFrontFee(purshaseVariables[i].UpfrontMI, item.LoanAmount.Value));
                        decimal? loanAmount = item.LoanAmount;
                        downPaymentAmount = item.MI_Upfront_Fee;
                        item.LoanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                        num3 = (purshaseVariables[i].MiFactor * ((double) item.LoanAmount.Value)) / 12.0;
                        if (purshaseVariables[i].LoanType == LoanTypeEnum.ConventonalConforming)
                        {
                            if (v.ltv < 80f)
                            {
                                num3 = 0.0;
                            }
                        }
                        else if (((purshaseVariables[i].LoanType != LoanTypeEnum.Jumbo) || (purshaseVariables[i].LoanType != LoanTypeEnum.VA)) || (purshaseVariables[i].LoanType != LoanTypeEnum.SubprimeNonQualified))
                        {
                            num3 = 0.0;
                        }
                        item.NewMI_MonthlyAmount = new decimal?((decimal) num3);
                        item.PMI_MIP_VA_FFReserves = item.NewMI_MonthlyAmount * 2.00M;
                        item.OptionName = purshaseVariables[i].MortgageProgramOption;
                        double mortgageAmount = (double) item.LoanAmount.Value;
                        item.NewMonthlyPaymentPrincipalInterest = new decimal?((decimal) GetPayment(mortgageAmount, item.InterestRate, (double) item.TermInYears.Value));
                        loanAmount = item.NewMonthlyPaymentPrincipalInterest;
                        downPaymentAmount = item.NewMI_MonthlyAmount;
                        loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                        downPaymentAmount = item.HI_MonthlyAmount;
                        loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                        downPaymentAmount = item.monthlyTaxes;
                        loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                        downPaymentAmount = item.EstimatedHomeownersAssociationFeesAnnual;
                        downPaymentAmount = downPaymentAmount.HasValue ? new decimal?(downPaymentAmount.GetValueOrDefault() / 12.00M) : ((decimal?) (nullable18 = null));
                        item.TotalNewPrincipalInterestPaymentWithMI = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                        double monthlyIncome = ((double) app.GrossAnnualIncome) / 12.0;
                        double monthlyDebt = 0.0;
                        if (app.creditPulled)
                        {
                            monthlyDebt = (double) app.TotalPaymentCredit.Value;
                        }
                        else
                        {
                            monthlyDebt = (double) app.TotalMontlyPayments;
                        }
                        if (GetMaxPayment(purshaseVariables[i].MaxfrontDTI, purshaseVariables[i].MaxBacktDTI, monthlyIncome, monthlyDebt) >= ((double) item.TotalNewPrincipalInterestPaymentWithMI.Value))
                        {
                            if (pricing.Cost30Days < 0.0)
                            {
                                item.CreditPoints = new double?(pricing.Cost30Days * -1.0);
                            }
                            else
                            {
                                item.CreditPoints = 0.0;
                            }
                            item.FrontDTI = (((double) item.TotalNewPrincipalInterestPaymentWithMI.Value) / monthlyIncome) * 100.0;
                            item.BackDTI = ((((double) item.TotalNewPrincipalInterestPaymentWithMI.Value) + monthlyDebt) / monthlyIncome) * 100.0;
                            if (item.BackDTI >= purshaseVariables[i].MaxBacktDTI)
                            {
                                loanAmount = item.LoanAmount;
                                purchasePrice = (decimal) item.CreditPoints.Value;
                                item.Credit = loanAmount.HasValue ? new decimal?(loanAmount.GetValueOrDefault() * purchasePrice) : ((decimal?) (downPaymentAmount = null));
                                loanAmount = item.LoanAmount;
                                purchasePrice = (decimal) purshaseVariables[i].OriginationPercent;
                                loanAmount = loanAmount.HasValue ? new decimal?(loanAmount.GetValueOrDefault() * purchasePrice) : null;
                                downPaymentAmount = item.Credit;
                                item.AdjustedLoanOriginationFee = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() - downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                purchasePrice = (decimal) purshaseVariables[i].TitleInusrancePercent;
                                loanAmount = item.LoanAmount;
                                item.LenderTitleInsurance = loanAmount.HasValue ? new decimal?(purchasePrice * loanAmount.GetValueOrDefault()) : ((decimal?) (downPaymentAmount = null));
                                if (pricing.Cost30Days > 0.0)
                                {
                                    item.Discount = new decimal?((decimal) (pricing.Cost30Days * ((double) item.LoanAmount.Value)));
                                }
                                else
                                {
                                    item.Discount = 0;
                                }
                                purchasePrice = (decimal) purshaseVariables[i].IntangibleTaxPercent;
                                loanAmount = item.LoanAmount;
                                item.IntangibleTax = loanAmount.HasValue ? new decimal?(purchasePrice * loanAmount.GetValueOrDefault()) : ((decimal?) (downPaymentAmount = null));
                                purchasePrice = (decimal) purshaseVariables[i].StateTaxPercent;
                                loanAmount = item.LoanAmount;
                                item.StateTax = loanAmount.HasValue ? new decimal?(purchasePrice * loanAmount.GetValueOrDefault()) : ((decimal?) (downPaymentAmount = null));
                                purchasePrice = (decimal) num7;
                                item.DailyInterestCharges = purchasePrice * item.LoanAmount;
                                loanAmount = item.HazardInsuranceReserves;
                                downPaymentAmount = item.CountyPropertyTaxReserves;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.PMI_MIP_VA_FFReserves;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.DailyInterestCharges;
                                item.EstimatedPrepaidsReserves = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                loanAmount = item.ProcessingFee;
                                downPaymentAmount = item.UnderwritingFee;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.AppraisalFee;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.CreditReportFee;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.ClosingEscrowFee;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.EndorsementsReconveyanceFee;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.MortgageRecordingCharges;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.AdjustedLoanOriginationFee;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.LenderTitleInsurance;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.Discount;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.IntangibleTax;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.StateTax;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                purchasePrice = item.SurveyFee;
                                loanAmount = loanAmount.HasValue ? new decimal?(loanAmount.GetValueOrDefault() + purchasePrice) : ((decimal?) (downPaymentAmount = null));
                                purchasePrice = item.TaxServiceFee;
                                loanAmount = loanAmount.HasValue ? new decimal?(loanAmount.GetValueOrDefault() + purchasePrice) : ((decimal?) (downPaymentAmount = null));
                                purchasePrice = item.FloodCertificationFee;
                                loanAmount = loanAmount.HasValue ? new decimal?(loanAmount.GetValueOrDefault() + purchasePrice) : ((decimal?) (downPaymentAmount = null));
                                purchasePrice = item.PestInspectionFee;
                                item.EstClosingCosts = loanAmount.HasValue ? new decimal?(loanAmount.GetValueOrDefault() + purchasePrice) : null;
                                loanAmount = item.EstClosingCosts;
                                downPaymentAmount = app.SellerPaidCreditClosingCost;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() - downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.EstimatedPrepaidsReserves;
                                item.EstimatedFundsNeeded = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                loanAmount = item.PurchasePrice;
                                downPaymentAmount = item.EstimatedPrepaidsReserves;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.EstClosingCosts;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.MI_Upfront_Fee;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.Discount;
                                item.TotalAmountNeeded = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() + downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                decimal? nullable = 0M;
                                double payment = 0.0;
                                loanAmount = item.LoanAmount;
                                downPaymentAmount = item.AdjustedLoanOriginationFee;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() - downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.ProcessingFee;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() - downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.UnderwritingFee;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() - downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.MI_Upfront_Fee;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() - downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.Discount;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() - downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.ClosingEscrowFee;
                                loanAmount = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() - downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                downPaymentAmount = item.DailyInterestCharges;
                                nullable = (loanAmount.HasValue & downPaymentAmount.HasValue) ? new decimal?(loanAmount.GetValueOrDefault() - downPaymentAmount.GetValueOrDefault()) : ((decimal?) (nullable18 = null));
                                if (purshaseVariables[i].LoanType == LoanTypeEnum.FHA)
                                {
                                    payment = ((((((double) item.NewMonthlyPaymentPrincipalInterest.Value) + ((double) item.NewMI_MonthlyAmount.Value)) * purshaseVariables[i].MiDurationYears) * 12.0) + ((((double) item.NewMonthlyPaymentPrincipalInterest.Value) * (30.0 - purshaseVariables[i].MiDurationYears)) * 12.0)) / 360.0;
                                }
                                else
                                {
                                    int num14 = getMILTV((double) nullable.Value, (double) item.TermInMonths.Value, item.InterestRate, (double) item.NewMonthlyPaymentPrincipalInterest.Value, 78.0, (double) item.PurchasePrice.Value);
                                    payment = (((((double) item.NewMonthlyPaymentPrincipalInterest.Value) + ((double) item.NewMI_MonthlyAmount.Value)) * num14) + (((double) item.NewMonthlyPaymentPrincipalInterest.Value) * (((double) item.TermInMonths.Value) - num14))) / 360.0;
                                }
                                item.APR = new double?(GetAPR((double) nullable.Value, item.InterestRate / 1200.0, (double) item.TermInMonths.Value, payment));
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            return list;
        }

        internal static List<PurchaseOptionSelected> getPurchaseOptionsSelected(CcsLocalDbContext db, Application app)
        {
            List<PurchaseOptionSelected> list = new List<PurchaseOptionSelected>();
            List<PurchaseOption> purchaseOptions = GetPurchaseOptions(db, app);
            int num = 1;
            foreach (PurchaseOption option in purchaseOptions)
            {
                PurchaseOptionSelected item = new PurchaseOptionSelected {
                    OptIntex = num
                };
                num++;
                item.PreparedFor = option.PreparedFor;
                item.InterestRate = new double?(option.InterestRate);
                item.MonthlyPaymentPrincipalInterest = option.NewMonthlyPaymentPrincipalInterest;
                item.OptionName = option.OptionName;
                item.TermInYears = option.TermInYears;
                item.MonthlyHomeownersInsurance = option.HI_MonthlyAmount;
                item.LoanAmount = option.LoanAmount;
                item.PurchasePrice = option.PurchasePrice;
                item.TotalMortgagePayment = option.TotalNewPrincipalInterestPaymentWithMI;
                item.MonthlyPropertyTaxes = option.monthlyTaxes;
                item.MonthlyMortgageInsurance = option.NewMI_MonthlyAmount;
                item.CashRequiredForClosing = option.EstimatedFundsNeeded;
                item.EstimatePrepaidItems = option.EstimatedPrepaidsReserves;
                item.TotalCostToPuchase = option.TotalAmountNeeded;
                item.MonthlyHomeownerAssociationFees = option.EstimatedHomeownersAssociationFeesAnnual / 12.00M;
                item.OptionName = option.OptionName;
                item.VarNum = option.VarNum;
                item.APR = option.APR;
                item.PMI_MIP_FundingFee = option.MI_Upfront_Fee;
                item.EstimatedClosingCost = option.EstClosingCosts;
                item.DiscoutRateBuyDown = option.Discount;
                item.SellerPaidClosingCost = option.SellerPaidCreditClosingCost;
                item.DownPayment = option.DownPayment;
                item.LTV = option.LTV;
                item.CLTV = option.CLTV;
                item.FrontDTI = option.FrontDTI;
                item.BackDTI = option.BackDTI;
                list.Add(item);
            }
            return list;
        }

        internal static List<Variable> GetPurshaseVariables()
        {
            CcsLocalDbContext context = new CcsLocalDbContext();
            List<Variable> list = (from v in context.Variables
                where v.Purchase && (((int) v.Active) == 2)
                select v).ToList<Variable>();
            context.Dispose();
            return list;
        }

        internal static List<RefiOption> GetRefiOptions(CcsLocalDbContext db, Application app)
        {
            if (!RateSheetLoaded)
            {
                RateSheetProcessor.LoadFromSerializedFile(Path.Combine(HttpContext.Current.Server.MapPath("~/Content/DataFiles"), "Plaza_JAX_Rates.phz"));
            }
            List<RefiOption> list = new List<RefiOption>();
            List<Variable> refiVariables = GetRefiVariables();
            Variables v = new Variables();
            for (int i = 0; i < refiVariables.Count; i++)
            {
                int? maxNumberOfUnits;
                int? numberofPaymentstoMaturity;
                int? nullable4;
                decimal? nullable10;
                LoanTypeRequestedEnum loanTypeRequested = app.LoanTypeRequested;
                if (((((((app.LoanTypeRequested == LoanTypeRequestedEnum.CashOutMortgage) && !refiVariables[i].RefiCashout) || ((app.LoanTypeRequested == LoanTypeRequestedEnum.DebtConsolidationPayOffCreditors) && !refiVariables[i].RefiCashout)) || ((((app.LoanTypeRequested == LoanTypeRequestedEnum.RateAndTermRefiLowerPayment) && !refiVariables[i].RateTerm) && !refiVariables[i].RefiCashout) || (((app.LoanTypeRequested == LoanTypeRequestedEnum.RateAndTermRefiShorterTerm) && !refiVariables[i].RateTerm) && !refiVariables[i].RefiCashout))) || ((((app.OwnerShipType == OwnershipTypeEnum.Investment) && !refiVariables[i].Investment) || ((app.OwnerShipType == OwnershipTypeEnum.Primary_Residence) && !refiVariables[i].PrimaryResidence)) || (((app.OwnerShipType == OwnershipTypeEnum.Second_Home) && !refiVariables[i].SecondHome) || ((app.PropertyType == PropertyTypeEnum.Modular_Manufactured) && !refiVariables[i].Manufactured)))) || (((((app.PropertyType == PropertyTypeEnum.Single_Familly_Residence_1_unit) && !refiVariables[i].SFR) || ((app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_2_units) && !refiVariables[i].MultiUnits)) || (((app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_3_units) && !refiVariables[i].MultiUnits) || ((app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_4_units) && !refiVariables[i].MultiUnits))) || ((((app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_5_Plus) && !refiVariables[i].MultiUnits) || ((app.PropertyType == PropertyTypeEnum.Condo) && !refiVariables[i].Condo)) || (((app.PropertyType == PropertyTypeEnum.MobileHomeWithLand) && !refiVariables[i].MobileHome) || ((app.PropertyType == PropertyTypeEnum.Townhome) && !refiVariables[i].TownHome))))) || ((((app.Veteran == YesNoAns.No) && (refiVariables[i].LoanType == LoanTypeEnum.VA)) || ((app.RuralProperty == YesNoAns.No) && (refiVariables[i].LoanType == LoanTypeEnum.USRDA))) || (((app.LoanType != LoanTypeEnum.FHA) && (refiVariables[i].LoanType == LoanTypeEnum.FHA_Streamline)) || ((app.LoanType != LoanTypeEnum.VA) && (refiVariables[i].LoanType == LoanTypeEnum.VA_IRRL)))))
                {
                    continue;
                }
                if (app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_2_units)
                {
                    maxNumberOfUnits = refiVariables[i].MaxNumberOfUnits;
                    if ((maxNumberOfUnits.GetValueOrDefault() < 2) && maxNumberOfUnits.HasValue)
                    {
                        continue;
                    }
                }
                if (app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_3_units)
                {
                    numberofPaymentstoMaturity = refiVariables[i].MaxNumberOfUnits;
                    if ((numberofPaymentstoMaturity.GetValueOrDefault() < 3) && numberofPaymentstoMaturity.HasValue)
                    {
                        continue;
                    }
                }
                if (app.PropertyType == PropertyTypeEnum.Multi_Familly_Residence_4_units)
                {
                    nullable4 = refiVariables[i].MaxNumberOfUnits;
                    if ((nullable4.GetValueOrDefault() < 4) && nullable4.HasValue)
                    {
                        continue;
                    }
                }
                if (app.creditPulled && !app.NoMorgagesOnCredit)
                {
                    if (app.lates12Credit <= refiVariables[i].NumOf30LateAllowedIn12Mo)
                    {
                        goto Label_036A;
                    }
                    continue;
                }
                int num2 = ((int) app.DaysLate) - 1;
                if (num2 > refiVariables[i].NumOf30LateAllowedIn12Mo)
                {
                    continue;
                }
            Label_036A:
                if (app.creditPulled)
                {
                    if (app.CreditScore >= int.Parse(refiVariables[i].CreditScoreRange))
                    {
                        goto Label_03AE;
                    }
                    continue;
                }
                if ((int)app.CreditScoreEstimate < int.Parse(refiVariables[i].CreditScoreRange))
                {
                    continue;
                }
            Label_03AE:
                if (app.creditPulled)
                {
                    foreach (PublicRecord record in app.publicRecords)
                    {
                        if ((record.IsQualifiedChapter13(refiVariables[i].Bankruptcy.Value) && record.IsQualifiedChapter7(refiVariables[i].Bankruptcy.Value)) && record.IsQualifiedChapter13(refiVariables[i].Bankruptcy.Value))
                        {
                            record.IsQualifiedforeclosure(refiVariables[i].Foreclosure.Value);
                        }
                    }
                }
                else if ((((app.FiledBankruptcyType == FiledBankruptcyTypeEnum.Chapter13Discharged) && !qualify(app.BankruptcyDischargeDate.Value, refiVariables[i].Bankruptcy.Value)) || (app.FiledBankruptcyType == FiledBankruptcyTypeEnum.Chapter13RepaymentStillOpen)) || (((app.FiledBankruptcyType == FiledBankruptcyTypeEnum.Chapter7Discharged) && !qualify(app.BankruptcyDischargeDate.Value, refiVariables[i].Bankruptcy.Value)) || ((app.ForeclosuresShortSaleDeedinLieu > ForeclosuresShortSaleDeedinLieuEnum.No) && !qualify(app.ForeclosureShortSaleDeedinLieuDate.Value, refiVariables[i].Foreclosure.Value))))
                {
                    continue;
                }
                double homeValue = (double) app.EstimatedHomeValue.Value;
                RefiOption item = new RefiOption();
                double num4 = (double) app.FirstMortgageBalance.Value;
                bool flag = app.Has2ndMortgage > HaveSecondMortgageEnum.No;
                bool flag2 = ((YesNoAns) app.PayOff2ndMortgage) == YesNoAns.Yes;
                if (flag && flag2)
                {
                    if (refiVariables[i].LoanType == LoanTypeEnum.VA)
                    {
                        TimeSpan span = (TimeSpan) (DateTime.Now - app.SecondMortgageOriginationDate.Value);
                        if ((span.Days < 330) && !refiVariables[i].RefiCashout)
                        {
                            continue;
                        }
                    }
                    if ((app.Has2ndMortgage == HaveSecondMortgageEnum.YesForCash) && !refiVariables[i].RefiCashout)
                    {
                        continue;
                    }
                    item.ShowSecondMtg = true;
                    item.SecondMtgBalance = app.SecondMortgageBalance;
                    num4 += (double) app.SecondMortgageBalance.Value;
                }
                HiBalance hibalance = GetMaxLoanLimit(db, app.Fips, refiVariables[i].LoanType, app.PropertyType, refiVariables[i].MaxLoanAmount);
                double maxLoanLimit = (double) hibalance.maxLoanLimit;
                if (maxLoanLimit == 0.0)
                {
                    maxLoanLimit = refiVariables[i].MaxLoanAmount;
                }
                if (refiVariables[i].LoanType == LoanTypeEnum.Jumbo)
                {
                    maxLoanLimit = refiVariables[i].MaxLoanAmount;
                    hibalance.IsHibalance = false;
                }
                double num8 = (refiVariables[i].MaxLTV / 100.0) * homeValue;
                double num9 = (refiVariables[i].CLTV / 100.0) * homeValue;
                maxLoanLimit = Math.Min(maxLoanLimit, num8);
                if (flag && !flag2)
                {
                    num9 -= (double) app.SecondMortgageBalance.Value;
                }
                maxLoanLimit = Math.Min(maxLoanLimit, num9);
                item.VarNum = refiVariables[i].OptionNumber;
                item.NewMI_MonthlyAmount = 0.00M;
                double num10 = refiVariables[i].MiFactor / 6.0;
                double upfrontMI = refiVariables[i].UpfrontMI;
                double num12 = (0.05 * refiVariables[i].NewInterestRate) / 360.0;
                item.OptionName = refiVariables[i].ScheduleName;
                item.DatePrepared = new DateTime?(DateTime.Today);
                item.LoanBalance = app.FirstMortgageBalance;
                item.CurrentInterestRate = app.CurrentInterestRate;
                item.PreparedFor = app.FirstName + " " + app.LastName;
                item.MonthlyPayment = app.FirstMortgagePayment;
                decimal? firstMortgagePayment = app.FirstMortgagePayment;
                decimal? monthlyMortgageInsur = app.MonthlyMortgageInsur;
                item.OldMonthlyPaymentPrincipalInterest = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                if (app.PymtIncludesPropTaxes)
                {
                    firstMortgagePayment = item.OldMonthlyPaymentPrincipalInterest;
                    monthlyMortgageInsur = app.AnnualPropertyTaxes;
                    monthlyMortgageInsur = monthlyMortgageInsur.HasValue ? new decimal?(monthlyMortgageInsur.GetValueOrDefault() / 12.00M) : ((decimal?) (nullable10 = null));
                    item.OldMonthlyPaymentPrincipalInterest = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                }
                if (app.PymtIncludesHomeownersInsurance)
                {
                    firstMortgagePayment = item.OldMonthlyPaymentPrincipalInterest;
                    monthlyMortgageInsur = app.AnnualHomeownersInsur;
                    monthlyMortgageInsur = monthlyMortgageInsur.HasValue ? new decimal?(monthlyMortgageInsur.GetValueOrDefault() / 12.00M) : ((decimal?) (nullable10 = null));
                    item.OldMonthlyPaymentPrincipalInterest = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                }
                item.TermInMonths = new int?(EnumNorm.TermToInt(new MortgageTermEnum?(app.MortgageTerm)) * 12);
                item.PercentCharge = new double?(refiVariables[i].OriginationPercent * 100.0);
                item.ProcessingFee = new decimal?(refiVariables[i].ProcessingFee);
                item.UnderwritingFee = new decimal?(refiVariables[i].UnderwritingFee);
                item.CreditReportFee = new decimal?(refiVariables[i].CreditReportFee);
                item.TaxServiceFee = refiVariables[i].TaxServiceFee;
                item.FloodCertificationFee = refiVariables[i].FloodCertificationFee;
                item.AppraisalFee = new decimal?(refiVariables[i].AppraisalFee);
                item.ClosingEscrowFee = new decimal?(refiVariables[i].ClosingEscrowFee);
                item.EndorsementsReconveyanceFee = new decimal?(refiVariables[i].EndorsementsReconveyanceFee);
                item.MortgageRecordingCharges = new decimal?(refiVariables[i].MortgageRecordingfee);
                item.NumberOfDays = 5;
                item.DateOfOrgination = app.FirstMortgageOriginationDate;
                item.AmountOfNewPaymentPlusMonthlySavings = item.OldMonthlyPaymentPrincipalInterest;
                if (!app.EstimateTotalDebtToPayOff.HasValue)
                {
                    app.EstimateTotalDebtToPayOff = 0;
                }
                item.TotalBalanceOfDebtToConsolidate = app.EstimateTotalDebtToPayOff;
                if (!item.TotalBalanceOfDebtToConsolidate.HasValue)
                {
                    item.TotalBalanceOfDebtToConsolidate = 0;
                }
                if (!app.SecondMortgageBalance.HasValue)
                {
                    app.SecondMortgageBalance = 0;
                }
                item.SecondMtgBalance = app.SecondMortgageBalance;
                item.MI_MonthsReserves = 2;
                item.MonthsPaidRemaining = new int?(GetRemainingMonthsTobePaid(item.DateOfOrgination.Value, (int) app.MortgageTerm));
                if (flag && flag2)
                {
                    item.MonthsPaidRemaining2nd = GetRemainingMonthsTobePaid(app.SecondMortgageOriginationDate.Value, (int)app.SecondMortgageTerm.Value);
                }
                item.MI_MonthlyAmount = app.MonthlyMortgageInsur;
                firstMortgagePayment = app.AnnualPropertyTaxes;
                item.monthlyTaxes = firstMortgagePayment.HasValue ? new decimal?(firstMortgagePayment.GetValueOrDefault() / 12.00M) : ((decimal?) (monthlyMortgageInsur = null));
                item.CT_MonthlyAmount = item.monthlyTaxes;
                firstMortgagePayment = item.monthlyTaxes;
                decimal numofMonthstoEscrowTaxes = refiVariables[i].NumofMonthstoEscrowTaxes;
                item.CountyPropertyTaxReserves = firstMortgagePayment.HasValue ? new decimal?(firstMortgagePayment.GetValueOrDefault() * numofMonthstoEscrowTaxes) : ((decimal?) (monthlyMortgageInsur = null));
                item.CT_MonthsReserves = new decimal?(refiVariables[i].NumofMonthstoEscrowTaxes);
                item.HI_MonthlyAmount = app.AnnualHomeownersInsur / 12.00M;
                item.Insurance = item.HI_MonthlyAmount;
                item.HI_MonthsReserves = new decimal?(refiVariables[i].NumofMonthstoEscrowHazardInsurance);
                firstMortgagePayment = item.HI_MonthlyAmount;
                monthlyMortgageInsur = item.HI_MonthsReserves;
                item.HazardInsuranceReserves = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() * monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                item.TermInYears = new int?(refiVariables[i].newTermInYears);
                item.OriginationChargesPercent = new double?(refiVariables[i].OriginationPercent);
                double num13 = 0.0;
                item.TotalFixedFees = new decimal?((((((((refiVariables[i].ProcessingFee + refiVariables[i].UnderwritingFee) + refiVariables[i].AppraisalFee) + refiVariables[i].CreditReportFee) + refiVariables[i].ClosingEscrowFee) + refiVariables[i].EndorsementsReconveyanceFee) + refiVariables[i].MortgageRecordingfee) + refiVariables[i].TaxServiceFee) + refiVariables[i].FloodCertificationFee);
                double num14 = (double) item.TotalFixedFees.Value;
                double num15 = (num14 + ((double) item.HazardInsuranceReserves.Value)) + ((double) item.CountyPropertyTaxReserves.Value);
                double num16 = ((((((refiVariables[i].OriginationPercent - refiVariables[i].LenderCreditPercent) + refiVariables[i].TitleInusrancePercent) + refiVariables[i].IntangibleTaxPercent) + refiVariables[i].StateTaxPercent) + refiVariables[i].DiscountPercent) + num12) + upfrontMI;
                double num17 = num4 + num15;
                double num18 = num17 / (1.0 - num16);
                double num19 = (num18 / homeValue) * 100.0;
                if (num19 > 80.0)
                {
                    num16 = (((((((refiVariables[i].OriginationPercent - refiVariables[i].LenderCreditPercent) + refiVariables[i].TitleInusrancePercent) + refiVariables[i].IntangibleTaxPercent) + refiVariables[i].StateTaxPercent) + refiVariables[i].DiscountPercent) + num12) + num10) + upfrontMI;
                    num18 = num17 / (1.0 - num16);
                }
                if (num18 <= maxLoanLimit)
                {
                    double num20 = (double) app.TotalOfMonthlyPaymentsOnDebtToPayOff.Value;
                    double num21 = (maxLoanLimit * (1.0 - num16)) - num17;
                    if (app.LoanTypeRequested == LoanTypeRequestedEnum.DebtConsolidationPayOffCreditors)
                    {
                        item.TotalBalanceOfDebtToConsolidate = app.EstimateTotalDebtToPayOff;
                        item.ShowDebtConsal = true;
                        if (num21 < ((double) app.EstimateTotalDebtToPayOff.Value))
                        {
                            item.TotalBalanceOfDebtToConsolidate = new decimal?((decimal) num21);
                            num20 *= num21 / ((double) app.EstimateTotalDebtToPayOff.Value);
                            num4 += num21;
                        }
                        else
                        {
                            num4 += (double) item.TotalBalanceOfDebtToConsolidate.Value;
                        }
                        num17 = num4 + num15;
                        num16 = ((((((refiVariables[i].OriginationPercent - refiVariables[i].LenderCreditPercent) + refiVariables[i].TitleInusrancePercent) + refiVariables[i].IntangibleTaxPercent) + refiVariables[i].StateTaxPercent) + refiVariables[i].DiscountPercent) + num12) + upfrontMI;
                        num18 = num17 / (1.0 - num16);
                        num19 = (num18 / homeValue) * 100.0;
                        if (num19 > 80.0)
                        {
                            num16 = (((((((refiVariables[i].OriginationPercent - refiVariables[i].LenderCreditPercent) + refiVariables[i].TitleInusrancePercent) + refiVariables[i].IntangibleTaxPercent) + refiVariables[i].StateTaxPercent) + refiVariables[i].DiscountPercent) + num12) + num10) + upfrontMI;
                        }
                        num18 = num17 / (1.0 - num16);
                    }
                    double num22 = (maxLoanLimit * (1.0 - num16)) - num17;
                    double num23 = Math.Min(num22, refiVariables[i].MaxCashOut);
                    if (app.LoanTypeRequested == LoanTypeRequestedEnum.CashOutMortgage)
                    {
                        num13 = (double) app.CashOutRequested.Value;
                        item.Cashout = app.CashOutRequested.Value;
                        item.ShowCashout = true;
                    }
                    else if (app.LoanTypeRequested == LoanTypeRequestedEnum.DebtConsolidationPayOffCreditors)
                    {
                        num13 = (double) app.AdditionalCashOutRequested.Value;
                        item.Cashout = app.AdditionalCashOutRequested.Value;
                        if (num13 > 0.0)
                        {
                            item.ShowCashout = true;
                        }
                    }
                    if (num23 < num13)
                    {
                        num13 = num23;
                        item.Cashout = (decimal) num13;
                    }
                    num4 += num13;
                    num17 = num4 + num15;
                    num16 = ((((((refiVariables[i].OriginationPercent - refiVariables[i].LenderCreditPercent) + refiVariables[i].TitleInusrancePercent) + refiVariables[i].IntangibleTaxPercent) + refiVariables[i].StateTaxPercent) + refiVariables[i].DiscountPercent) + num12) + upfrontMI;
                    num18 = num17 / (1.0 - num16);
                    num19 = (num18 / homeValue) * 100.0;
                    if (num19 > 80.0)
                    {
                        num16 = (((((((refiVariables[i].OriginationPercent - refiVariables[i].LenderCreditPercent) + refiVariables[i].TitleInusrancePercent) + refiVariables[i].IntangibleTaxPercent) + refiVariables[i].StateTaxPercent) + refiVariables[i].DiscountPercent) + num12) + num10) + upfrontMI;
                    }
                    if ((refiVariables[i].MortgageProgramOption.Trim() == "HARP_FannieBefore2009") || (refiVariables[i].MortgageProgramOption.Trim() == "HARP_FredieBefore2009"))
                    {
                        num17 += ((double) app.MonthlyMortgageInsur.Value) * 2.0;
                    }
                    double num24 = num17 / (1.0 - num16);
                    app.ProposedLoanAmount = (decimal) num24;
                    numofMonthstoEscrowTaxes = app.ProposedLoanAmount;
                    firstMortgagePayment = app.EstimatedHomeValue;
                    monthlyMortgageInsur = (firstMortgagePayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes / firstMortgagePayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsur = null))) * 100M;
                    app.CurrentLTV = (double) monthlyMortgageInsur.Value;
                    v = GetLoanVariablesForRateSheet(app, refiVariables[i], hibalance);
                    RateSheetProcessor.RetrieveRebate(refiVariables[i].ScheduleName, v);
                    if (v.FinalAdjustment >= -1000f)
                    {
                        double? nullable13;
                        double num42;
                        RatePricing pricing = RatePricing.GetPar30(PopPricingList(v, refiVariables[i].ScheduleName, refiVariables[i].LenderPaidComp));
                        item.LoanAmount = new decimal?(app.ProposedLoanAmount);
                        item.InterestRate = new double?(pricing.interest);
                        num12 = (0.05 * pricing.interest) / 360.0;
                        item.InterestRateSavings = item.CurrentInterestRate - item.InterestRate;
                        double? interestRateSavings = item.InterestRateSavings;
                        if ((interestRateSavings.GetValueOrDefault() > 0.0) && interestRateSavings.HasValue)
                        {
                            item.ShowInterestSaving = true;
                        }
                        if (flag && flag2)
                        {
                            num42 = ((((double) item.LoanBalance.Value) * item.CurrentInterestRate.Value) + (((double) item.SecondMtgBalance.Value) * app.SecondMortgageInterestRate.Value)) / (((double) item.LoanBalance.Value) + ((double) item.SecondMtgBalance.Value));
                            interestRateSavings = item.InterestRate;
                            item.BlendedRateSavings = interestRateSavings.HasValue ? new double?(num42 - interestRateSavings.GetValueOrDefault()) : ((double?) (nullable13 = null));
                            interestRateSavings = item.BlendedRateSavings;
                            if ((interestRateSavings.GetValueOrDefault() > 0.0) && interestRateSavings.HasValue)
                            {
                                item.ShowBlendedRate = true;
                                item.ShowInterestSaving = false;
                            }
                        }
                        if (pricing.Cost30Days > 0.0)
                        {
                            item.DiscountPoints = new double?(pricing.Cost30Days);
                            item.Discount = new decimal?((decimal) (pricing.Cost30Days * ((double) item.LoanAmount.Value)));
                            item.CreditPoints = 0.0;
                            item.Credit = 0;
                        }
                        else
                        {
                            item.DiscountPoints = 0.0;
                            item.Discount = 0;
                            item.CreditPoints = new double?(pricing.Cost30Days * -1.0);
                            interestRateSavings = item.CreditPoints;
                            num42 = (double) item.LoanAmount.Value;
                            nullable13 = interestRateSavings.HasValue ? new double?(interestRateSavings.GetValueOrDefault() * num42) : null;
                            item.Credit = new decimal?((decimal) nullable13.Value);
                        }
                        num16 = ((((((refiVariables[i].OriginationPercent - item.CreditPoints.Value) + refiVariables[i].TitleInusrancePercent) + refiVariables[i].IntangibleTaxPercent) + refiVariables[i].StateTaxPercent) + item.DiscountPoints.Value) + num12) + upfrontMI;
                        num17 = num4 + num15;
                        num18 = num17 / (1.0 - num16);
                        num19 = (num18 / homeValue) * 100.0;
                        if (num19 > 80.0)
                        {
                            num16 = (((((((refiVariables[i].OriginationPercent - item.CreditPoints.Value) + refiVariables[i].TitleInusrancePercent) + refiVariables[i].IntangibleTaxPercent) + refiVariables[i].StateTaxPercent) + item.DiscountPoints.Value) + num12) + num10) + upfrontMI;
                        }
                        num17 = num4 + num15;
                        if ((refiVariables[i].MortgageProgramOption.Trim() == "HARP_FannieBefore2009") || (refiVariables[i].MortgageProgramOption.Trim() == "HARP_FredieBefore2009"))
                        {
                            num17 += ((double) app.MonthlyMortgageInsur.Value) * 2.0;
                        }
                        num24 = num17 / (1.0 - num16);
                        item.LoanAmount = new decimal?((decimal) num24);
                        if (maxLoanLimit >= ((double) item.LoanAmount.Value))
                        {
                            item.NewMI_MonthlyAmount = 0;
                            item.PMI_MIP_VA_FFReserves = 0;
                            item.MI_Upfront_Fee = 0.00M;
                            double num26 = (num24 / homeValue) * 100.0;
                            item.LTV = num26;
                            item.CLTV = num26;
                            if ((app.Has2ndMortgage > HaveSecondMortgageEnum.No) && (((YesNoAns) app.PayOff2ndMortgage) == YesNoAns.No))
                            {
                                item.CLTV = ((num24 + ((double) app.SecondMortgageBalance.Value)) / homeValue) * 100.0;
                            }
                            int num27 = (int) (item.LTV * 1000000.0);
                            item.LTV = ((double) num27) / 1000000.0;
                            num27 = (int) (item.CLTV * 1000000.0);
                            item.CLTV = ((double) num27) / 1000000.0;
                            if (item.CLTV <= refiVariables[i].CLTV)
                            {
                                if (refiVariables[i].LoanType == LoanTypeEnum.HARP)
                                {
                                    item.NewMI_MonthlyAmount = app.MonthlyMortgageInsur;
                                    item.PMI_MIP_VA_FFReserves = item.NewMI_MonthlyAmount * 2.00M;
                                    item.MI_Upfront_Fee = 0.00M;
                                }
                                else if (refiVariables[i].LoanType == LoanTypeEnum.ConventonalConforming)
                                {
                                    if (num26 >= 80.0)
                                    {
                                        item.MI_Upfront_Fee = new decimal?((decimal) (num24 * upfrontMI));
                                        item.PMI_MIP_VA_FFReserves = new decimal?((decimal) (num24 * num10));
                                        item.NewMI_MonthlyAmount = item.PMI_MIP_VA_FFReserves / 2.00M;
                                    }
                                }
                                else if (((refiVariables[i].LoanType == LoanTypeEnum.USRDA) || (refiVariables[i].LoanType == LoanTypeEnum.VA)) || (refiVariables[i].LoanType == LoanTypeEnum.VA_IRRL))
                                {
                                    item.MI_Upfront_Fee = new decimal?((decimal) (num24 * upfrontMI));
                                    item.PMI_MIP_VA_FFReserves = new decimal?((decimal) (num24 * num10));
                                    item.NewMI_MonthlyAmount = item.PMI_MIP_VA_FFReserves / 2.00M;
                                }
                                double mortgageAmount = (double) item.LoanAmount.Value;
                                item.NewMonthlyPaymentPrincipalInterest = 0.00M;
                                item.NewMonthlyPaymentPrincipalInterest = new decimal?((decimal) GetPayment(mortgageAmount, item.InterestRate.Value, (double) item.TermInYears.Value));
                                firstMortgagePayment = item.OldMonthlyPaymentPrincipalInterest;
                                monthlyMortgageInsur = item.MI_MonthlyAmount;
                                item.TotalOldPrincipalInterestPaymentWithMI = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                firstMortgagePayment = item.NewMonthlyPaymentPrincipalInterest;
                                monthlyMortgageInsur = item.NewMI_MonthlyAmount;
                                item.TotalNewPrincipalInterestPaymentWithMI = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                double num29 = ((((double) item.TotalNewPrincipalInterestPaymentWithMI.Value) + (((double) app.AnnualPropertyTaxes.Value) / 12.0)) + (((double) app.AnnualHomeownersInsur.Value) / 12.0)) + (((double) app.AnnualHomeownersAssocDues.Value) / 12.0);
                                double monthlyIncome = ((double) app.GrossAnnualIncome) / 12.0;
                                double monthlyDebt = 0.0;
                                if (app.creditPulled)
                                {
                                    monthlyDebt = (double) app.TotalPaymentCredit.Value;
                                }
                                else
                                {
                                    monthlyDebt = ((double) app.TotalMontlyPayments) - ((double) app.TotalOfMonthlyPaymentsOnDebtToPayOff.Value);
                                    if ((app.Has2ndMortgage > HaveSecondMortgageEnum.No) && (((YesNoAns) app.PayOff2ndMortgage) == YesNoAns.No))
                                    {
                                        monthlyDebt += (double) app.SecondMortgagePayment.Value;
                                    }
                                }
                                double maxMonthlyPayment = GetMaxPayment(refiVariables[i].MaxfrontDTI, refiVariables[i].MaxBacktDTI, monthlyIncome, monthlyDebt);
                                GetMaxAffordableLoanAmount((double) app.AnnualHomeownersInsur.Value, pricing.interest, (double) item.TermInMonths.Value, (double) app.DownPaymentAmount.Value, maxMonthlyPayment, (double) app.AnnualPropertyTaxes.Value, refiVariables[i].MiFactor, refiVariables[i].UpfrontMI);
                                if (maxMonthlyPayment >= num29)
                                {
                                    item.FrontDTI = (num29 / monthlyIncome) * 100.0;
                                    item.BackDTI = ((num29 + monthlyDebt) / monthlyIncome) * 100.0;
                                    decimal? nullable = 0M;
                                    if (app.EstimateTotalDebtToPayOff != 0M)
                                    {
                                        firstMortgagePayment = app.TotalOfMonthlyPaymentsOnDebtToPayOff;
                                        monthlyMortgageInsur = item.TotalBalanceOfDebtToConsolidate;
                                        firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() * monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                        monthlyMortgageInsur = app.EstimateTotalDebtToPayOff;
                                        nullable = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() / monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    }
                                    firstMortgagePayment = item.TotalOldPrincipalInterestPaymentWithMI;
                                    monthlyMortgageInsur = item.TotalNewPrincipalInterestPaymentWithMI;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    monthlyMortgageInsur = nullable;
                                    item.MonthlySavings = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    firstMortgagePayment = item.TotalNewPrincipalInterestPaymentWithMI;
                                    monthlyMortgageInsur = item.MonthlySavings;
                                    item.NewPaymentPlusMonthlySavings = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    if (((YesNoAns) app.PayOff2ndMortgage) == YesNoAns.Yes)
                                    {
                                        firstMortgagePayment = item.TotalOldPrincipalInterestPaymentWithMI;
                                        monthlyMortgageInsur = app.SecondMortgagePayment;
                                        firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                        monthlyMortgageInsur = item.TotalNewPrincipalInterestPaymentWithMI;
                                        firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                        monthlyMortgageInsur = nullable;
                                        item.MonthlySavings = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                        firstMortgagePayment = item.TotalNewPrincipalInterestPaymentWithMI;
                                        monthlyMortgageInsur = item.MonthlySavings;
                                        item.NewPaymentPlusMonthlySavings = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    }
                                    firstMortgagePayment = item.MonthlySavings;
                                    if ((firstMortgagePayment.GetValueOrDefault() > 0M) && firstMortgagePayment.HasValue)
                                    {
                                        item.ShowMonthlySaving = true;
                                    }
                                    firstMortgagePayment = item.OldMonthlyPaymentPrincipalInterest;
                                    maxNumberOfUnits = item.MonthsPaidRemaining;
                                    item.OldTotalAmountOfAllPaymentsToBeMade = (firstMortgagePayment.HasValue & maxNumberOfUnits.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() * maxNumberOfUnits.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsur = null));
                                    if (flag && flag2)
                                    {
                                        firstMortgagePayment = item.OldTotalAmountOfAllPaymentsToBeMade;
                                        monthlyMortgageInsur = app.SecondMortgagePayment;
                                        numofMonthstoEscrowTaxes = item.MonthsPaidRemaining2nd;
                                        monthlyMortgageInsur = monthlyMortgageInsur.HasValue ? new decimal?(monthlyMortgageInsur.GetValueOrDefault() * numofMonthstoEscrowTaxes) : ((decimal?) (nullable10 = null));
                                        item.OldTotalAmountOfAllPaymentsToBeMade = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    }
                                    item.NewTotalAmountOfAllPaymentsToBeMade = item.NewMonthlyPaymentPrincipalInterest * item.TermInMonths;
                                    firstMortgagePayment = item.OldTotalAmountOfAllPaymentsToBeMade;
                                    monthlyMortgageInsur = item.NewTotalAmountOfAllPaymentsToBeMade;
                                    item.NetSavingsFromOldLoanToNewLoan = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    firstMortgagePayment = item.NetSavingsFromOldLoanToNewLoan;
                                    if ((firstMortgagePayment.GetValueOrDefault() > 0M) && firstMortgagePayment.HasValue)
                                    {
                                        item.ShowNetSaving = true;
                                    }
                                    firstMortgagePayment = item.LoanAmount;
                                    numofMonthstoEscrowTaxes = (decimal) refiVariables[i].OriginationPercent;
                                    firstMortgagePayment = firstMortgagePayment.HasValue ? new decimal?(firstMortgagePayment.GetValueOrDefault() * numofMonthstoEscrowTaxes) : null;
                                    monthlyMortgageInsur = item.Credit;
                                    item.AdjustedLoanOriginationFee = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    firstMortgagePayment = item.LoanAmount;
                                    numofMonthstoEscrowTaxes = ((decimal) refiVariables[i].OriginationPercent) - ((decimal) item.CreditPoints.Value);
                                    if (firstMortgagePayment.HasValue)
                                    {
                                        new decimal?(firstMortgagePayment.GetValueOrDefault() * numofMonthstoEscrowTaxes);
                                    }
                                    numofMonthstoEscrowTaxes = (decimal) refiVariables[i].TitleInusrancePercent;
                                    firstMortgagePayment = item.LoanAmount;
                                    item.LenderTitleInsurance = firstMortgagePayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * firstMortgagePayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsur = null));
                                    numofMonthstoEscrowTaxes = (decimal) refiVariables[i].IntangibleTaxPercent;
                                    firstMortgagePayment = item.LoanAmount;
                                    item.IntangibleTax = firstMortgagePayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * firstMortgagePayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsur = null));
                                    numofMonthstoEscrowTaxes = (decimal) refiVariables[i].StateTaxPercent;
                                    firstMortgagePayment = item.LoanAmount;
                                    item.StateTax = firstMortgagePayment.HasValue ? new decimal?(numofMonthstoEscrowTaxes * firstMortgagePayment.GetValueOrDefault()) : ((decimal?) (monthlyMortgageInsur = null));
                                    numofMonthstoEscrowTaxes = (decimal) num12;
                                    item.DailyInterestCharges = numofMonthstoEscrowTaxes * item.LoanAmount;
                                    firstMortgagePayment = item.HazardInsuranceReserves;
                                    monthlyMortgageInsur = item.CountyPropertyTaxReserves;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    monthlyMortgageInsur = item.PMI_MIP_VA_FFReserves;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    monthlyMortgageInsur = item.DailyInterestCharges;
                                    item.EstimatedPrepaidsReserves = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    firstMortgagePayment = item.ProcessingFee;
                                    monthlyMortgageInsur = item.UnderwritingFee;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    monthlyMortgageInsur = item.AppraisalFee;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    monthlyMortgageInsur = item.CreditReportFee;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    monthlyMortgageInsur = item.ClosingEscrowFee;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    monthlyMortgageInsur = item.EndorsementsReconveyanceFee;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    monthlyMortgageInsur = item.MortgageRecordingCharges;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    monthlyMortgageInsur = item.AdjustedLoanOriginationFee;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    monthlyMortgageInsur = item.LenderTitleInsurance;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    monthlyMortgageInsur = item.Discount;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    monthlyMortgageInsur = item.IntangibleTax;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    monthlyMortgageInsur = item.StateTax;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    monthlyMortgageInsur = item.MI_Upfront_Fee;
                                    firstMortgagePayment = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    numofMonthstoEscrowTaxes = item.TaxServiceFee;
                                    firstMortgagePayment = firstMortgagePayment.HasValue ? new decimal?(firstMortgagePayment.GetValueOrDefault() + numofMonthstoEscrowTaxes) : ((decimal?) (monthlyMortgageInsur = null));
                                    numofMonthstoEscrowTaxes = item.FloodCertificationFee;
                                    item.EstClosingCosts = firstMortgagePayment.HasValue ? new decimal?(firstMortgagePayment.GetValueOrDefault() + numofMonthstoEscrowTaxes) : ((decimal?) (monthlyMortgageInsur = null));
                                    numofMonthstoEscrowTaxes = (decimal) num4;
                                    firstMortgagePayment = numofMonthstoEscrowTaxes + item.EstClosingCosts;
                                    monthlyMortgageInsur = item.EstimatedPrepaidsReserves;
                                    item.TotalAmountNeeded = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    firstMortgagePayment = item.LoanAmount;
                                    monthlyMortgageInsur = item.TotalAmountNeeded;
                                    item.EstimatedFundsNeeded = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    firstMortgagePayment = item.EstClosingCosts;
                                    monthlyMortgageInsur = item.MonthlySavings;
                                    item.CostSavingsBreakEvenAnalysis = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() / monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    double num34 = (double) item.LoanAmount.Value;
                                    interestRateSavings = item.InterestRate;
                                    double num35 = (interestRateSavings.Value / 100.0) / 12.0;
                                    double num36 = (double) app.FirstMortgagePayment.Value;
                                    if (((YesNoAns) app.PayOff2ndMortgage) == YesNoAns.Yes)
                                    {
                                        num36 += (double) app.SecondMortgagePayment.Value;
                                    }
                                    double a = -Math.Log(1.0 - ((num34 / num36) * num35)) / Math.Log(1.0 + num35);
                                    item.NumberofPaymentstoMaturity = new int?((int) Math.Ceiling(a));
                                    maxNumberOfUnits = item.NumberofPaymentstoMaturity;
                                    float? nullable15 = maxNumberOfUnits.HasValue ? new float?(((float) maxNumberOfUnits.GetValueOrDefault()) / 12f) : null;
                                    item.YearsRequiredToMaturity = nullable15.HasValue ? new double?((double) nullable15.GetValueOrDefault()) : null;
                                    item.AcceleratedPayoffTotalAmountOfAllPayments = item.NewPaymentPlusMonthlySavings * item.NumberofPaymentstoMaturity;
                                    firstMortgagePayment = item.OldTotalAmountOfAllPaymentsToBeMade;
                                    monthlyMortgageInsur = item.AcceleratedPayoffTotalAmountOfAllPayments;
                                    item.TotalSavingsFromOldMortgageToNewMortgage = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() - monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    maxNumberOfUnits = item.MonthsPaidRemaining;
                                    numberofPaymentstoMaturity = item.NumberofPaymentstoMaturity;
                                    maxNumberOfUnits = (maxNumberOfUnits.HasValue & numberofPaymentstoMaturity.HasValue) ? new int?(maxNumberOfUnits.GetValueOrDefault() - numberofPaymentstoMaturity.GetValueOrDefault()) : ((int?) (nullable4 = null));
                                    item.MonthlyPaymentsEliminated = maxNumberOfUnits.HasValue ? new decimal?(maxNumberOfUnits.GetValueOrDefault()) : null;
                                    firstMortgagePayment = item.OldMonthlyPaymentPrincipalInterest;
                                    monthlyMortgageInsur = item.MI_MonthlyAmount;
                                    item.TotalOldPrincipalInterestPaymentWithMI = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    firstMortgagePayment = item.NewMonthlyPaymentPrincipalInterest;
                                    monthlyMortgageInsur = item.NewMI_MonthlyAmount;
                                    item.TotalNewPrincipalInterestPaymentWithMI = (firstMortgagePayment.HasValue & monthlyMortgageInsur.HasValue) ? new decimal?(firstMortgagePayment.GetValueOrDefault() + monthlyMortgageInsur.GetValueOrDefault()) : ((decimal?) (nullable10 = null));
                                    double loanAmount = ((((((((double) item.LoanAmount.Value) - ((double) item.AdjustedLoanOriginationFee.Value)) - ((double) item.Discount.Value)) - ((double) item.ProcessingFee.Value)) - ((double) item.UnderwritingFee.Value)) - ((double) item.ClosingEscrowFee.Value)) - ((double) item.MI_Upfront_Fee.Value)) - ((double) item.DailyInterestCharges.Value);
                                    double payment = 0.0;
                                    if (refiVariables[i].LoanType == LoanTypeEnum.FHA)
                                    {
                                        payment = ((((((double) item.NewMonthlyPaymentPrincipalInterest.Value) + ((double) item.NewMI_MonthlyAmount.Value)) * refiVariables[i].MiDurationYears) * 12.0) + ((((double) item.NewMonthlyPaymentPrincipalInterest.Value) * (30.0 - refiVariables[i].MiDurationYears)) * 12.0)) / 360.0;
                                    }
                                    else
                                    {
                                        int num40 = getMILTV(loanAmount, (double) item.TermInMonths.Value, item.InterestRate.Value, (double) item.NewMonthlyPaymentPrincipalInterest.Value, 78.0, homeValue);
                                        payment = (((((double) item.NewMonthlyPaymentPrincipalInterest.Value) + ((double) item.NewMI_MonthlyAmount.Value)) * num40) + (((double) item.NewMonthlyPaymentPrincipalInterest.Value) * (((double) item.TermInMonths.Value) - num40))) / 360.0;
                                    }
                                    item.APR = new double?(GetAPR(loanAmount, item.InterestRate.Value / 1200.0, (double) item.TermInMonths.Value, payment));
                                    item.PercentCharge /= 100.0;
                                    item.ShowBlendedRate = true;
                                    item.ShowCashout = true;
                                    item.ShowDebtConsal = true;
                                    item.ShowInterestSaving = true;
                                    item.ShowMonthlySaving = true;
                                    item.ShowNetSaving = true;
                                    item.ShowSecondMtg = true;
                                    list.Add(item);
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }

        internal static List<Variable> GetRefiVariables()
        {
            CcsLocalDbContext context = new CcsLocalDbContext();
            List<Variable> list = (from v in context.Variables
                where v.Refi && (((int) v.Active) == 2)
                select v).ToList<Variable>();
            context.Dispose();
            return list;
        }

        internal static int GetRemainingMonthsTobePaid(DateTime DateOfOrgination, int termInYears)
        {
            DateTime time = DateOfOrgination;
            int num2 = DateTime.Now.Year - time.Year;
            int month = time.Month;
            int num4 = DateTime.Now.Month;
            int num5 = Math.Abs((int) (((12 * num2) + month) - num4));
            return ((termInYears * 12) - num5);
        }

        internal static PaymentAndBalance GetTotalPaymentAndBalance(List<CreditMortgage> mortgages, List<CreditLiability> liabilities)
        {
            PaymentAndBalance balance = new PaymentAndBalance {
                Balance = 0.0,
                payment = 0.0
            };
            new List<string>();
            double num = 0.0;
            double num2 = 0.0;
            foreach (CreditLiability liability in liabilities)
            {
                if (!liability.IsDuplicate && ((liability.AccType.ToLower().Trim() != "installment") || (liability.MonthsRemaining >= 12)))
                {
                    num2 += liability.MonthlyPayment;
                    num += liability.Balance;
                }
            }
            foreach (CreditMortgage mortgage in mortgages)
            {
                if (!mortgage.IsDuplicate && (mortgage.MonthsRemaining >= 12))
                {
                    num += mortgage.MonthlyPayment;
                    num += mortgage.Balance;
                }
            }
            balance.payment = num2;
            balance.Balance = num;
            return balance;
        }

        internal static PaymentAndBalance GetTotalPaymentAndBalance(CcsLocalDbContext db, int applicantId, bool? exludeproForSale)
        {
            PaymentAndBalance balance = new PaymentAndBalance {
                Balance = 0.0,
                payment = 0.0
            };
            List<CreditLiability> list = (from l in db.CreditLiabilities
                where l.ApplicantID == applicantId
                select l).ToList<CreditLiability>();
            List<CreditMortgage> list2 = (from m in db.CreditMortgages
                where m.ApplicantID == applicantId
                select m).ToList<CreditMortgage>();
            new List<string>();
            double num = 0.0;
            double num2 = 0.0;
            foreach (CreditLiability liability in list)
            {
                if (!liability.IsDuplicate && ((liability.AccType.ToLower().Trim() != "installment") || (liability.MonthsRemaining >= 12)))
                {
                    num2 += liability.MonthlyPayment;
                    num += liability.Balance;
                }
            }
            foreach (CreditMortgage mortgage in list2)
            {
                if (((!exludeproForSale.HasValue || (mortgage.PropertyType == CreditMortgageTypeEnum.ForSale)) || (exludeproForSale != true)) && (!mortgage.IsDuplicate && (mortgage.MonthsRemaining >= 12)))
                {
                    num += mortgage.MonthlyPayment;
                    num += mortgage.Balance;
                }
            }
            balance.payment = num2;
            balance.Balance = num;
            return balance;
        }

        internal static PaymentAndBalance GetTotalPaymentAndBalanceWithMortgages(CcsLocalDbContext db, int applicantId)
        {
            PaymentAndBalance balance = new PaymentAndBalance {
                Balance = 0.0,
                payment = 0.0
            };
            List<CreditLiability> list = (from l in db.CreditLiabilities
                where l.ApplicantID == applicantId
                select l).ToList<CreditLiability>();
            List<CreditMortgage> list2 = (from m in db.CreditMortgages
                where m.ApplicantID == applicantId
                select m).ToList<CreditMortgage>();
            new List<string>();
            double num = 0.0;
            double num2 = 0.0;
            foreach (CreditLiability liability in list)
            {
                if (!liability.IsDuplicate && ((liability.AccType.ToLower().Trim() != "installment") || (liability.MonthsRemaining >= 12)))
                {
                    num2 += liability.MonthlyPayment;
                    num += liability.Balance;
                }
            }
            foreach (CreditMortgage mortgage in list2)
            {
                if (mortgage.PropertyType != CreditMortgageTypeEnum.ForSale)
                {
                    num += mortgage.MonthlyPayment;
                    num += mortgage.Balance;
                }
                int monthsRemaining = mortgage.MonthsRemaining;
            }
            balance.payment = num2;
            balance.Balance = num;
            return balance;
        }

        internal static decimal GetUpFrontFee(double upFrontFundingFee, decimal LoanAmount)
        {
            double num = (double) LoanAmount;
            double num2 = 0.0;
            num2 = num * upFrontFundingFee;
            return (decimal) num2;
        }

        private static double GetUpFrontMI(Variable variable)
        {
            if (((((variable.MortgageProgramOption.Trim() != "VA") && (variable.MortgageProgramOption.Trim() != "VA_IRRL")) && ((variable.MortgageProgramOption.Trim() != "FHA") && (variable.MortgageProgramOption.Trim() != "FHA_SteamlineBefore2009"))) && (((variable.MortgageProgramOption.Trim() != "FHA_SteamlineAfter2009") && (variable.MortgageProgramOption.Trim() != "Conventional")) && ((variable.MortgageProgramOption.Trim() != "USRDA") && (variable.MortgageProgramOption.Trim() != "Jumbo")))) && ((((variable.MortgageProgramOption.Trim() != "Subprime") && (variable.MortgageProgramOption.Trim() != "Conventional")) && ((variable.MortgageProgramOption.Trim() != "HARP_FannieBefore2009") && (variable.MortgageProgramOption.Trim() != "HARP_FredieBefore2009"))) && (variable.MortgageProgramOption.Trim() != "HELOC")))
            {
                bool flag1 = variable.MortgageProgramOption.Trim() == "Other";
            }
            return 0.0;
        }

        internal static ApplicationUser GetUser(CcsLocalDbContext db, string userName) => 
            (from r in db.Users
                where r.UserName == userName
                select r).FirstOrDefault<ApplicationUser>();

        internal static List<Variable> GetVariables()
        {
            CcsLocalDbContext context = new CcsLocalDbContext();
            List<Variable> list = context.Variables.ToList<Variable>();
            context.Dispose();
            return list;
        }

        internal static List<CreditLiability> MarkDublicateLiabilities(List<CreditLiability> liabilities)
        {
            if (liabilities == null)
            {
                return null;
            }
            for (int i = 0; i < liabilities.Count; i++)
            {
                CreditLiability liability = liabilities[i];
                if (!liability.IsDuplicate)
                {
                    for (int j = 0; j < liabilities.Count; j++)
                    {
                        if (j != i)
                        {
                            CreditLiability liability2 = liabilities[j];
                            if ((!liability2.IsDuplicate && (liability.HiCredit == liability2.HiCredit)) && (liability.DateOpened == liability2.DateOpened))
                            {
                                if (liability.LastActivityDate.HasValue && liability2.LastActivityDate.HasValue)
                                {
                                    int num3 = DateTime.Compare(liability.LastActivityDate.Value, liability2.LastActivityDate.Value);
                                    if (((num3 == 0) && (liability.Balance < liability2.Balance)) || (num3 > 0))
                                    {
                                        liability2.IsDuplicate = true;
                                    }
                                }
                                else if (!liability.LastActivityDate.HasValue || !liability2.LastActivityDate.HasValue)
                                {
                                    if (liability.Balance < liability2.Balance)
                                    {
                                        liability2.IsDuplicate = true;
                                    }
                                    if ((liability.Balance == liability2.Balance) && !liability2.LastActivityDate.HasValue)
                                    {
                                        liability2.IsDuplicate = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return liabilities;
        }

        internal static List<CreditLiability> MarkDublicateLiabilities(CcsLocalDbContext db, int applicantId)
        {
            List<CreditLiability> list = (from l in db.CreditLiabilities
                where l.ApplicantID == applicantId
                select l).ToList<CreditLiability>();
            if (list == null)
            {
                return null;
            }
            for (int i = 0; i < list.Count; i++)
            {
                CreditLiability liability = list[i];
                if (!liability.IsDuplicate)
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (j != i)
                        {
                            CreditLiability liability2 = list[j];
                            if ((!liability2.IsDuplicate && (liability.HiCredit == liability2.HiCredit)) && (liability.DateOpened == liability2.DateOpened))
                            {
                                if (liability.LastActivityDate.HasValue && liability2.LastActivityDate.HasValue)
                                {
                                    int num3 = DateTime.Compare(liability.LastActivityDate.Value, liability2.LastActivityDate.Value);
                                    if (((num3 == 0) && (liability.Balance < liability2.Balance)) || (num3 > 0))
                                    {
                                        liability2.IsDuplicate = true;
                                    }
                                }
                                else if (!liability.LastActivityDate.HasValue || !liability2.LastActivityDate.HasValue)
                                {
                                    if (liability.Balance < liability2.Balance)
                                    {
                                        liability2.IsDuplicate = true;
                                    }
                                    if ((liability.Balance == liability2.Balance) && !liability2.LastActivityDate.HasValue)
                                    {
                                        liability2.IsDuplicate = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            db.SaveChanges();
            return list;
        }

        internal static List<CreditMortgage> MarkDublicateMortgages(List<CreditMortgage> mortgages)
        {
            for (int i = 0; i < mortgages.Count; i++)
            {
                CreditMortgage mortgage = mortgages[i];
                if (!mortgage.IsDuplicate)
                {
                    for (int j = 0; j < mortgages.Count; j++)
                    {
                        if (j != i)
                        {
                            CreditMortgage mortgage2 = mortgages[j];
                            if (!mortgage2.IsDuplicate && (((mortgage2.DateOpened == mortgage.DateOpened) && (mortgage2.HiCredit == mortgage.HiCredit)) && ((mortgage2.MonthlyPayment == mortgage.MonthlyPayment) && (mortgage2.Balance <= mortgage.Balance))))
                            {
                                mortgage2.IsDuplicate = true;
                            }
                        }
                    }
                }
            }
            return mortgages;
        }

        internal static List<CreditMortgage> MarkDublicateMortgages(CcsLocalDbContext db, int applicantId)
        {
            List<CreditMortgage> list = (from l in db.CreditMortgages
                where l.ApplicantID == applicantId
                select l).ToList<CreditMortgage>();
            for (int i = 0; i < list.Count; i++)
            {
                CreditMortgage mortgage = list[i];
                if (!mortgage.IsDuplicate)
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (j != i)
                        {
                            CreditMortgage mortgage2 = list[j];
                            if (!mortgage2.IsDuplicate && (((mortgage2.DateOpened == mortgage.DateOpened) && (mortgage2.HiCredit == mortgage.HiCredit)) && ((mortgage2.MonthlyPayment == mortgage.MonthlyPayment) && (mortgage2.Balance <= mortgage.Balance))))
                            {
                                mortgage2.IsDuplicate = true;
                            }
                        }
                    }
                }
            }
            db.SaveChanges();
            return list;
        }

        public static List<RatePricing> PopPricingList(Variables loanVariables, string SheduleName, double lenderPaid)
        {
            DataTable table = RateSheetProcessor.ReturnRateSheet(SheduleName);
            List<RatePricing> list = new List<RatePricing>();
            int num = -1;
            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (table.Rows[0][i].ToString() == "30 Day")
                {
                    num = i;
                    break;
                }
            }
            for (int j = 1; j < table.Rows.Count; j++)
            {
                RatePricing item = new RatePricing {
                    Cost30Days = (loanVariables.FinalAdjustment + double.Parse(table.Rows[j][num].ToString())) + lenderPaid
                };
                if (item.Cost30Days < loanVariables.MaxRebate)
                {
                    item.Cost30Days = loanVariables.MaxRebate;
                }
                item.Cost30Days /= 100.0;
                item.interest = double.Parse(table.Rows[j][0].ToString());
                list.Add(item);
            }
            return list;
        }

        internal static bool PullCredit(CcsLocalDbContext db, int applicantId, RequestCred requestcred)
        {
            Applicant applicant = (from app in db.Applicants
                where app.Applicant_Id == applicantId
                select app).Include<Applicant, List<Application>>(app => app.Applications).FirstOrDefault<Applicant>();
            if ((applicant == null) || (applicant.Applications == null))
            {
                return false;
            }
            if (applicant.Applications.Count == 0)
            {
                return false;
            }
            requestcred.RequestDate = DateTime.Now;
            db.RequestCreds.Add(requestcred);
            db.SaveChanges();
            ResponseData entity = new ResponseData();
            REQUEST_GROUP request_group = new REQUEST_GROUP {
                MISMOVersionID = "2.3.1",
                RECEIVING_PARTY = new RECEIVING_PARTY()
            };
            request_group.RECEIVING_PARTY._Identifier = requestcred.RECEIVING_PARTY_Identifier;
            request_group.SUBMITTING_PARTY = new SUBMITTING_PARTY();
            request_group.SUBMITTING_PARTY._Name = requestcred.SUBMITTING_PARTY_Name;
            request_group.SUBMITTING_PARTY._Identifier = requestcred.SUBMITTING_PARTY_Identifier;
            request_group.SUBMITTING_PARTY.PREFERRED_RESPONSE = new PREFERRED_RESPONSE();
            request_group.SUBMITTING_PARTY.PREFERRED_RESPONSE._Format = requestcred.PREFERRED_RESPONSE_Format;
            request_group.SUBMITTING_PARTY.PREFERRED_RESPONSE._FormatOtherDescription = requestcred.PREFERRED_RESPONSE_FormatOtherDescription;
            request_group.SUBMITTING_PARTY.PREFERRED_RESPONSE._UseEmbeddedFileIndicator = requestcred.PREFERRED_RESPONSE_UseEmbeddedFileIndicator;
            request_group.REQUEST = new REQUEST();
            request_group.REQUEST.RequestDatetime = requestcred.RequestDatetime;
            if (requestcred.InternalAccountIdentifier == null)
            {
                requestcred.InternalAccountIdentifier = "";
            }
            request_group.REQUEST.InternalAccountIdentifier = requestcred.InternalAccountIdentifier;
            request_group.REQUEST.LoginAccountIdentifier = requestcred.LoginAccountIdentifier;
            request_group.REQUEST.LoginAccountPassword = requestcred.LoginAccountPassword;
            request_group.REQUEST.REQUEST_DATA = new REQUEST_DATA();
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST = new CREDIT_REQUEST();
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.MISMOVersionID = requestcred.MISMOVersionID;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.LenderCaseIdentifier = requestcred.LenderCaseIdentifier;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.RequestingPartyRequestedByName = requestcred.RequestingPartyRequestedByName;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.CREDIT_REQUEST_DATA = new CreditRequestLib.CREDIT_REQUEST_DATA();
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.CREDIT_REQUEST_DATA.CreditRequestID = requestcred.CreditRequestID;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.CREDIT_REQUEST_DATA.BorrowerID = requestcred.REQUEST_DATA_BorrowerID;
            if (requestcred.CreditReportIdentifier == null)
            {
                requestcred.CreditReportIdentifier = "";
            }
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.CREDIT_REQUEST_DATA.CreditReportIdentifier = requestcred.CreditReportIdentifier;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.CREDIT_REQUEST_DATA.CreditReportRequestActionType = requestcred.CreditReportRequestActionType;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.CREDIT_REQUEST_DATA.CreditReportType = requestcred.CreditReportType;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.CREDIT_REQUEST_DATA.CreditRequestDateTime = requestcred.CreditRequestDateTime;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.CREDIT_REQUEST_DATA.CreditRequestType = requestcred.CreditRequestType;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.CREDIT_REQUEST_DATA.CREDIT_REPOSITORY_INCLUDED = new CreditRequestLib.CREDIT_REPOSITORY_INCLUDED();
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.CREDIT_REQUEST_DATA.CREDIT_REPOSITORY_INCLUDED._EquifaxIndicator = requestcred.EquifaxIndicator;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.CREDIT_REQUEST_DATA.CREDIT_REPOSITORY_INCLUDED._ExperianIndicator = requestcred.ExperianIndicator;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.CREDIT_REQUEST_DATA.CREDIT_REPOSITORY_INCLUDED._TransUnionIndicator = requestcred.TransUnionIndicator;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.LOAN_APPLICATION = new LOAN_APPLICATION();
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.LOAN_APPLICATION.BORROWER = new CreditRequestLib.BORROWER();
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.LOAN_APPLICATION.BORROWER.BorrowerID = requestcred.BorrowerID;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.LOAN_APPLICATION.BORROWER._FirstName = requestcred.FirstName;
            if (requestcred.MiddleName == null)
            {
                requestcred.MiddleName = "";
            }
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.LOAN_APPLICATION.BORROWER._MiddleName = requestcred.MiddleName;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.LOAN_APPLICATION.BORROWER._LastName = requestcred.LastName;
            if (requestcred.NameSuffix == null)
            {
                requestcred.NameSuffix = "";
            }
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.LOAN_APPLICATION.BORROWER._NameSuffix = requestcred.NameSuffix;
            if (requestcred.AgeAtApplicationYears == null)
            {
                requestcred.AgeAtApplicationYears = "";
            }
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.LOAN_APPLICATION.BORROWER._AgeAtApplicationYears = requestcred.AgeAtApplicationYears;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.LOAN_APPLICATION.BORROWER._PrintPositionType = requestcred.PrintPositionType;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.LOAN_APPLICATION.BORROWER._SSN = requestcred.SSN;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.LOAN_APPLICATION.BORROWER.MaritalStatusType = requestcred.MaritalStatusType;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.LOAN_APPLICATION.BORROWER._RESIDENCE = new CreditRequestLib._RESIDENCE();
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.LOAN_APPLICATION.BORROWER._RESIDENCE._StreetAddress = requestcred.StreetAddress;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.LOAN_APPLICATION.BORROWER._RESIDENCE._City = requestcred.CITY;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.LOAN_APPLICATION.BORROWER._RESIDENCE._State = requestcred.STATE;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.LOAN_APPLICATION.BORROWER._RESIDENCE._PostalCode = requestcred.PostalCode;
            request_group.REQUEST.REQUEST_DATA.CREDIT_REQUEST.LOAN_APPLICATION.BORROWER._RESIDENCE.BorrowerResidencyType = requestcred.BorrowerResidencyType;
            string s = request_group.ToXml(false, Formatting.None, EOLType.LF);
            int index = s.IndexOf("-->");
            s = s.Remove(0, index + 3);
            int startIndex = s.IndexOf("xmlns");
            index = s.IndexOf("instance") + 9;
            s = s.Remove(startIndex, index - startIndex);
            string xmlIn = "";
            WebRequest request = WebRequest.Create("https://credit.meridianlink.com/inetapi/AU/get_credit_report.aspx");
            request.Method = "Post";
            request.ContentType = "text/xml; encoding='utf-8'";
            request.Timeout = 0x15f90;
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
            request.ContentLength = bytes.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            WebResponse response = request.GetResponse();
            string statusDescription = ((HttpWebResponse) response).StatusDescription;
            requestStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(requestStream);
            xmlIn = reader.ReadToEnd();
            entity.XMLResponse = xmlIn;
            entity.XMLRequest = s;
            requestcred.ResponseData = entity;
            db.ResponseDatas.Add(entity);
            applicant.creditPulled = true;
            db.SaveChanges();
            bool flag = false;
            RESPONSE_GROUP response_group = new RESPONSE_GROUP();
            response_group.FromXml(xmlIn);
            ResponseCred cred = new ResponseCred {
                FileNumber = response_group.RESPONSE.RESPONSE_DATA.CREDIT_RESPONSE.CreditReportIdentifier,
                FullName = response_group.RESPONSE.RESPONSE_DATA.CREDIT_RESPONSE.BORROWER._UnparsedName,
                REPOSITORIES = ""
            };
            foreach (CREDIT_SCORE credit_score in response_group.RESPONSE.RESPONSE_DATA.CREDIT_RESPONSE.CREDIT_SCORE)
            {
                if (credit_score.CreditRepositorySourceType == "Experian")
                {
                    cred.CreditScoreEP = credit_score._Value;
                    cred.REPOSITORIES = cred.REPOSITORIES + "XP/";
                }
                else if (credit_score.CreditRepositorySourceType == "TransUnion")
                {
                    cred.CreditScoreTU = credit_score._Value;
                    cred.REPOSITORIES = cred.REPOSITORIES + "TU/";
                }
                else if (credit_score.CreditRepositorySourceType == "Equifax")
                {
                    cred.CreditScoreEF = credit_score._Value;
                    cred.REPOSITORIES = cred.REPOSITORIES + "EF/";
                }
            }
            foreach (CREDIT_SUMMARY credit_summary in response_group.RESPONSE.RESPONSE_DATA.CREDIT_RESPONSE.CREDIT_SUMMARY)
            {
                if (credit_summary._Name == "MLTradeSummary")
                {
                    foreach (_DATA_SET _data_set in credit_summary._DATA_SET)
                    {
                        if (_data_set._Name == "TotalLiabilityBalance")
                        {
                            cred.TotalBalance = double.Parse(_data_set._Value);
                        }
                        else if (_data_set._Name == "TotalLiabilityHighCredit")
                        {
                            cred.TotalHiCredit = double.Parse(_data_set._Value);
                        }
                        else if (_data_set._Name == "TotalLiabilityPayment")
                        {
                            cred.TotalPayments = double.Parse(_data_set._Value);
                        }
                        else if (_data_set._Name == "TotalLiabilityPastDue")
                        {
                            cred.TotalPassdue = double.Parse(_data_set._Value);
                        }
                    }
                }
                else if (credit_summary._Name == "MLDerogatorySummary")
                {
                    foreach (_DATA_SET _data_set2 in credit_summary._DATA_SET)
                    {
                        if (_data_set2._Name == "TotalSecuredLoanBalance")
                        {
                            cred.TotalSecureDebt = double.Parse(_data_set2._Value);
                        }
                        else if (_data_set2._Name == "TotalUnsecuredLoanBalance")
                        {
                            cred.TotalUnSecureDebt = double.Parse(_data_set2._Value);
                        }
                        else if (_data_set2._Name == "TotalLiabilityCount")
                        {
                            cred.TotalAccount = int.Parse(_data_set2._Value);
                        }
                    }
                }
            }
            if (response_group.RESPONSE.RESPONSE_DATA.CREDIT_RESPONSE.EMBEDDED_FILE != null)
            {
                cred.HTMLfile = response_group.RESPONSE.RESPONSE_DATA.CREDIT_RESPONSE.EMBEDDED_FILE.DOCUMENT;
                int num3 = cred.HTMLfile.IndexOf("<style>");
                int num4 = cred.HTMLfile.LastIndexOf("</style>") + 8;
                if ((num3 != -1) && (num4 != -1))
                {
                    cred.HTMLfile = cred.HTMLfile.Remove(num3, num4 - num3);
                }
            }
            List<CreditMortgage> mortgages = new List<CreditMortgage>();
            List<CreditLiability> liabilities = new List<CreditLiability>();
            List<Lates> list3 = new List<Lates>();
            string str3 = "";
            string str4 = "";
            string str5 = "";
            foreach (CREDIT_LIABILITY credit_liability in response_group.RESPONSE.RESPONSE_DATA.CREDIT_RESPONSE.CREDIT_LIABILITY)
            {
                if (credit_liability._AccountType == "Mortgage")
                {
                    CreditMortgage item = new CreditMortgage {
                        Lates = new List<Lates>(),
                        Whose = credit_liability.BorrowerID,
                        Sourse = credit_liability.CREDIT_REPOSITORY._SourceType,
                        Lender = credit_liability._CREDITOR._Name
                    };
                    if (credit_liability.IsValid_UnpaidBalanceAmount)
                    {
                        item.Balance = double.Parse(credit_liability._UnpaidBalanceAmount);
                    }
                    if (credit_liability.IsValid_MonthlyPaymentAmount)
                    {
                        item.MonthlyPayment = double.Parse(credit_liability._MonthlyPaymentAmount);
                    }
                    if (credit_liability.IsValid_AccountOpenedDate)
                    {
                        string str6 = credit_liability._AccountOpenedDate.Substring(0, 4);
                        string str7 = credit_liability._AccountOpenedDate.Substring(5, 2);
                        item.DateOpened = new DateTime(int.Parse(str6), int.Parse(str7), 1);
                    }
                    if (credit_liability.IsValid_LastActivityDate)
                    {
                        string str8 = credit_liability._LastActivityDate.Substring(0, 4);
                        string str9 = credit_liability._LastActivityDate.Substring(5, 2);
                        item.LastActivityDate = new DateTime(int.Parse(str8), int.Parse(str9), 1);
                    }
                    if (credit_liability.IsValid_HighCreditAmount)
                    {
                        item.HiCredit = double.Parse(credit_liability._HighCreditAmount);
                    }
                    if (credit_liability.IsValid_TermsMonthsCount)
                    {
                        item.Term = int.Parse(credit_liability._TermsMonthsCount);
                    }
                    if (credit_liability.IsValid_AccountType)
                    {
                        item.AccType = credit_liability._AccountType;
                    }
                    if (credit_liability.IsValid_MonthsRemainingCount)
                    {
                        item.MonthsRemaining = int.Parse(credit_liability._MonthsRemainingCount);
                    }
                    if (credit_liability.IsValid_AccountIdentifier)
                    {
                        item.AccountIdentifier = credit_liability._AccountIdentifier;
                    }
                    foreach (_PRIOR_ADVERSE_RATING _prior_adverse_rating in credit_liability._PRIOR_ADVERSE_RATING)
                    {
                        Lates lates = new Lates();
                        flag = false;
                        str3 = _prior_adverse_rating._Date;
                        str4 = str3.Substring(0, 4);
                        str5 = str3.Substring(5, 2);
                        if (_prior_adverse_rating._Type == "Late30Days")
                        {
                            lates.Late30 = new DateTime(int.Parse(str4), int.Parse(str5), 1);
                            flag = true;
                        }
                        else if (_prior_adverse_rating._Type == "Late60Days")
                        {
                            lates.Late60 = new DateTime(int.Parse(str4), int.Parse(str5), 1);
                            flag = true;
                        }
                        else if (_prior_adverse_rating._Type == "Late90Days")
                        {
                            lates.Late90 = new DateTime(int.Parse(str4), int.Parse(str5), 1);
                            flag = true;
                        }
                        else if (_prior_adverse_rating._Type == "LateOver120Days")
                        {
                            lates.Late120 = new DateTime(int.Parse(str4), int.Parse(str5), 1);
                            flag = true;
                        }
                        if (flag)
                        {
                            lates.Lender = credit_liability._CREDITOR._Name;
                            lates.AccountType = credit_liability._AccountType;
                            lates.AccountIdentifier = credit_liability._AccountIdentifier;
                            lates.AccountOpenedDate = item.DateOpened;
                            lates.HighCreditAmount = item.HiCredit;
                            lates.MonthlyPaymentAmount = item.MonthlyPayment;
                            list3.Add(lates);
                            item.Lates.Add(lates);
                        }
                    }
                    mortgages.Add(item);
                }
                else
                {
                    CreditLiability liability = new CreditLiability {
                        Lates = new List<Lates>(),
                        Whose = credit_liability.BorrowerID,
                        Sourse = credit_liability.CREDIT_REPOSITORY._SourceType
                    };
                    if (credit_liability._CREDITOR != null)
                    {
                        liability.creditor = credit_liability._CREDITOR._Name;
                    }
                    if (credit_liability.IsValid_UnpaidBalanceAmount)
                    {
                        liability.Balance = double.Parse(credit_liability._UnpaidBalanceAmount);
                    }
                    if (credit_liability.IsValid_MonthlyPaymentAmount)
                    {
                        liability.MonthlyPayment = double.Parse(credit_liability._MonthlyPaymentAmount);
                    }
                    if (credit_liability.IsValid_AccountOpenedDate)
                    {
                        string str10 = credit_liability._AccountOpenedDate.Substring(0, 4);
                        string str11 = credit_liability._AccountOpenedDate.Substring(5, 2);
                        liability.DateOpened = new DateTime(int.Parse(str10), int.Parse(str11), 1);
                    }
                    if (credit_liability.IsValid_LastActivityDate)
                    {
                        string str12 = credit_liability._LastActivityDate.Substring(0, 4);
                        string str13 = credit_liability._LastActivityDate.Substring(5, 2);
                        liability.LastActivityDate = new DateTime(int.Parse(str12), int.Parse(str13), 1);
                    }
                    if (credit_liability.IsValid_HighCreditAmount)
                    {
                        liability.HiCredit = double.Parse(credit_liability._HighCreditAmount);
                    }
                    liability.AccType = credit_liability._AccountType;
                    if (credit_liability.IsValid_TermsMonthsCount)
                    {
                        liability.Term = int.Parse(credit_liability._TermsMonthsCount);
                    }
                    if (credit_liability.IsValid_MonthsRemainingCount)
                    {
                        liability.Term = int.Parse(credit_liability._MonthsRemainingCount);
                    }
                    liability.AccountIdentifier = credit_liability._AccountIdentifier;
                    foreach (_PRIOR_ADVERSE_RATING _prior_adverse_rating2 in credit_liability._PRIOR_ADVERSE_RATING)
                    {
                        Lates lates2 = new Lates();
                        flag = false;
                        str3 = _prior_adverse_rating2._Date;
                        str4 = str3.Substring(0, 4);
                        str5 = str3.Substring(5, 2);
                        if (_prior_adverse_rating2._Type == "Late30Days")
                        {
                            lates2.Late30 = new DateTime(int.Parse(str4), int.Parse(str5), 1);
                            flag = true;
                        }
                        else if (_prior_adverse_rating2._Type == "Late60Days")
                        {
                            lates2.Late60 = new DateTime(int.Parse(str4), int.Parse(str5), 1);
                            flag = true;
                        }
                        else if (_prior_adverse_rating2._Type == "Late90Days")
                        {
                            lates2.Late90 = new DateTime(int.Parse(str4), int.Parse(str5), 1);
                            flag = true;
                        }
                        else if (_prior_adverse_rating2._Type == "LateOver120Days")
                        {
                            lates2.Late120 = new DateTime(int.Parse(str4), int.Parse(str5), 1);
                            flag = true;
                        }
                        if (flag)
                        {
                            lates2.Lender = credit_liability._CREDITOR._Name;
                            lates2.AccountType = credit_liability._AccountType;
                            lates2.AccountIdentifier = credit_liability._AccountIdentifier;
                            lates2.AccountOpenedDate = liability.DateOpened;
                            lates2.HighCreditAmount = liability.HiCredit;
                            lates2.MonthlyPaymentAmount = liability.MonthlyPayment;
                            list3.Add(lates2);
                            liability.Lates.Add(lates2);
                        }
                    }
                    liabilities.Add(liability);
                }
            }
            List<PublicRecord> list4 = new List<PublicRecord>();
            foreach (CREDIT_PUBLIC_RECORD credit_public_record in response_group.RESPONSE.RESPONSE_DATA.CREDIT_RESPONSE.CREDIT_PUBLIC_RECORD)
            {
                PublicRecord record = new PublicRecord {
                    Whose = credit_public_record.BorrowerID,
                    Sourse = credit_public_record.CREDIT_REPOSITORY._SourceType
                };
                if (credit_public_record.IsValid_PlaintiffName)
                {
                    record.Plaintiff = credit_public_record._PlaintiffName;
                }
                string str14 = credit_public_record._FiledDate.Substring(0, 4);
                string str15 = credit_public_record._FiledDate.Substring(5, 2);
                record.FileDate = new DateTime(int.Parse(str14), int.Parse(str15), 1);
                if (credit_public_record.IsValid_LegalObligationAmount)
                {
                    record.Amount = double.Parse(credit_public_record._LegalObligationAmount);
                }
                record.Status = credit_public_record._DispositionType;
                if (credit_public_record.IsValid_DispositionDate)
                {
                    str14 = credit_public_record._DispositionDate.Substring(0, 4);
                    str15 = credit_public_record._DispositionDate.Substring(5, 2);
                    record.StatusDate = new DateTime(int.Parse(str14), int.Parse(str15), 1);
                }
                record.ActionType = credit_public_record._Type;
                list4.Add(record);
            }
            List<Inquiry> list5 = new List<Inquiry>();
            foreach (CREDIT_INQUIRY credit_inquiry in response_group.RESPONSE.RESPONSE_DATA.CREDIT_RESPONSE.CREDIT_INQUIRY)
            {
                Inquiry inquiry = new Inquiry {
                    Whose = credit_inquiry.BorrowerID,
                    Company = credit_inquiry._Name
                };
                string str16 = credit_inquiry._Date.Substring(0, 4);
                string str17 = credit_inquiry._Date.Substring(5, 2);
                string str18 = credit_inquiry._Date.Substring(8, 2);
                inquiry.InquiryDate = new DateTime(int.Parse(str16), int.Parse(str17), int.Parse(str18));
                inquiry.Bureau = credit_inquiry.CREDIT_REPOSITORY._SourceType;
                inquiry.BusninessType = credit_inquiry.CreditBusinessType;
                list5.Add(inquiry);
            }
            if (liabilities.Count > 0)
            {
                cred.Liabilities = Util.MarkDublicateLiabilities(liabilities);
            }
            if (mortgages.Count > 0)
            {
                cred.CreditMortgages = Util.MarkDublicateMortgages(mortgages);
            }
            else
            {
                applicant.NoMorgagesOnCredit = true;
            }
            cred.Inquiries = list5;
            cred.lates = list3;
            cred.PublicRecords = list4;
            cred.ResponseData = entity;
            db.ResponseCreds.Add(cred);
            applicant.creditPulled = true;
            applicant.CreditLiabilities = cred.Liabilities;
            applicant.CreditMortgages = cred.CreditMortgages;
            applicant.Inquiries = list5;
            applicant.Lates = list3;
            applicant.publicRecords = list4;
            applicant.CreditResponseDatas.Add(entity);
            applicant.CreditScore = cred.GetCreditScore();
            applicant.lates12Credit = Util.getLates30_12mo(applicant.Lates);
            applicant.lates24Credit = Util.getLates30_24mo(applicant.Lates);
            Util.PaymentAndBalance totalPaymentAndBalance = Util.GetTotalPaymentAndBalance(applicant.CreditMortgages, applicant.CreditLiabilities);
            applicant.TotalBalanceCredit = new decimal?((decimal) totalPaymentAndBalance.Balance);
            applicant.TotalPaymentCredit = new decimal?((decimal) totalPaymentAndBalance.payment);
            foreach (Application application in applicant.Applications)
            {
                application.CreditScore = applicant.CreditScore;
                application.lates12Credit = applicant.lates12Credit;
                application.lates24Credit = applicant.lates24Credit;
                application.TotalBalanceCredit = applicant.TotalBalanceCredit;
                application.TotalPaymentCredit = applicant.TotalPaymentCredit;
                application.NoMorgagesOnCredit = applicant.NoMorgagesOnCredit;
                application.creditPulled = true;
                application.publicRecords = applicant.publicRecords;
            }
            db.SaveChanges();
            reader.Close();
            requestStream.Close();
            if (response != null)
            {
                response.Close();
            }
            return true;
        }

        public static bool qualify(DateTime dt1, int months)
        {
            bool flag = false;
            DateTime time = dt1;
            DateTime now = DateTime.Now;
            int year = months / 12;
            int month = months - (year * 12);
            if (now.Month <= month)
            {
                year++;
                month = (now.Month + 12) - month;
            }
            else
            {
                month = now.Month - month;
            }
            year = now.Year - year;
            DateTime time3 = new DateTime(year, month, now.Day);
            if (DateTime.Compare(time3, time) >= 0)
            {
                flag = true;
            }
            return flag;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HiBalance
        {
            internal bool IsHibalance;
            internal decimal maxLoanLimit;
            internal string StateAb;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PaymentAndBalance
        {
            internal double payment;
            internal double Balance;
        }
    }
}

