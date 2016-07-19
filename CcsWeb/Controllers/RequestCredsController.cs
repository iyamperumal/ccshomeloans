namespace CcsWeb.Controllers
{
    using CcsData.Models;
    using CcsData.Models.CreditPull;
    using CcsWeb.DataContexts;
    using CreditRequestLib;
    using LiquidTechnologies.Runtime.Net45;
    using ResponseLib;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web.Mvc;
    using System.Xml;

    public class RequestCredsController : Controller
    {
        private CcsLocalDbContext db = new CcsLocalDbContext();

        public ActionResult Create()
        {
            RequestCred model = new RequestCred {
                RECEIVING_PARTY_Identifier = "D1",
                SUBMITTING_PARTY_Name = "submitterName",
                SUBMITTING_PARTY_Identifier = "SYNCTestSubmittingPartyID",
                PREFERRED_RESPONSE_Format = "Other",
                PREFERRED_RESPONSE_FormatOtherDescription = "HTML",
                PREFERRED_RESPONSE_UseEmbeddedFileIndicator = "Y",
                RequestDatetime = DateTime.Now.ToString("s"),
                InternalAccountIdentifier = "",
                LoginAccountIdentifier = "ConsumerCred",
                LoginAccountPassword = "25S206Z9",
                MISMOVersionID = "2.3.1",
                LenderCaseIdentifier = "lenderID",
                RequestingPartyRequestedByName = "name",
                CreditRequestID = "CreditRequest1",
                REQUEST_DATA_BorrowerID = "Borrower",
                CreditReportIdentifier = "",
                CreditReportRequestActionType = "Submit",
                CreditReportType = "Merge"
            };
            model.CreditRequestDateTime = model.RequestDatetime;
            model.CreditRequestType = "Individual";
            model.EquifaxIndicator = "Y";
            model.ExperianIndicator = "Y";
            model.TransUnionIndicator = "Y";
            model.BorrowerID = "Borrower";
            model.FirstName = "Jim";
            model.MiddleName = "L";
            model.LastName = "TESTCASE";
            model.NameSuffix = "";
            model.AgeAtApplicationYears = "";
            model.PrintPositionType = "Borrower";
            model.SSN = "000000001";
            model.MaritalStatusType = "NotProvided";
            model.StreetAddress = "220 Locust Ave";
            model.CITY = "Anthill";
            model.STATE = "MO";
            model.PostalCode = "65488";
            model.BorrowerResidencyType = "Current";
            return base.View(model);
        }

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult Create([Bind(Include="RequestCred_Id,RECEIVING_PARTY_Identifier,SUBMITTING_PARTY_Name,SUBMITTING_PARTY_Identifier,PREFERRED_RESPONSE_Format,PREFERRED_RESPONSE_FormatOtherDescription,PREFERRED_RESPONSE_UseEmbeddedFileIndicator,RequestDatetime,InternalAccountIdentifier,LoginAccountIdentifier,LoginAccountPassword,CreditRequestID,REQUEST_DATA_BorrowerID,CreditReportIdentifier,CreditReportRequestActionType,CreditReportType,CreditRequestDateTime,CreditRequestType,MISMOVersionID,LenderCaseIdentifier,RequestingPartyRequestedByName,EquifaxIndicator,ExperianIndicator,TransUnionIndicator,BorrowerID,FirstName,MiddleName,LastName,NameSuffix,AgeAtApplicationYears,PrintPositionType,SSN,MaritalStatusType,StreetAddress,CITY,STATE,PostalCode,BorrowerResidencyType,RequestDate,Exeption")] RequestCred requestcred)
        {
            int applicantId = Util.GetApplicantId(this.db, base.User.Identity.Name);
            Applicant applicant = (from app in this.db.Applicants
                where app.Applicant_Id == applicantId
                select app).Include<Applicant, List<Application>>(app => app.Applications).FirstOrDefault<Applicant>();
            if ((applicant == null) || (applicant.Applications == null))
            {
                return base.HttpNotFound();
            }
            if (applicant.Applications.Count == 0)
            {
                return base.HttpNotFound();
            }
            requestcred.RequestDate = DateTime.Now;
            this.db.RequestCreds.Add(requestcred);
            this.db.SaveChanges();
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
            this.db.ResponseDatas.Add(entity);
            applicant.creditPulled = true;
            this.db.SaveChanges();
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
            this.db.ResponseCreds.Add(cred);
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
            this.db.SaveChanges();
            reader.Close();
            requestStream.Close();
            if (response != null)
            {
                response.Close();
            }
            return base.RedirectToAction("index", "Response");
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestCred model = this.db.RequestCreds.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequestCred entity = this.db.RequestCreds.Find(new object[] { id });
            this.db.RequestCreds.Remove(entity);
            this.db.SaveChanges();
            return base.RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestCred model = this.db.RequestCreds.Find(new object[] { id });
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
        public ActionResult Edit([Bind(Include="RequestCred_Id,RECEIVING_PARTY_Identifier,SUBMITTING_PARTY_Name,SUBMITTING_PARTY_Identifier,PREFERRED_RESPONSE_Format,PREFERRED_RESPONSE_FormatOtherDescription,PREFERRED_RESPONSE_UseEmbeddedFileIndicator,RequestDatetime,InternalAccountIdentifier,LoginAccountIdentifier,LoginAccountPassword,CreditRequestID,REQUEST_DATA_BorrowerID,CreditReportIdentifier,CreditReportRequestActionType,CreditReportType,CreditRequestDateTime,CreditRequestType,MISMOVersionID,LenderCaseIdentifier,RequestingPartyRequestedByName,EquifaxIndicator,ExperianIndicator,TransUnionIndicator,BorrowerID,FirstName,MiddleName,LastName,NameSuffix,AgeAtApplicationYears,PrintPositionType,SSN,MaritalStatusType,StreetAddress,CITY,STATE,PostalCode,BorrowerResidencyType,RequestDate,Exeption")] RequestCred requestCred)
        {
            if (base.ModelState.IsValid)
            {
                this.db.Entry<RequestCred>(requestCred).State = EntityState.Modified;
                this.db.SaveChanges();
                return base.RedirectToAction("Index");
            }
            return base.View(requestCred);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestCred model = this.db.RequestCreds.Find(new object[] { id });
            if (model == null)
            {
                return base.HttpNotFound();
            }
            return base.View(model);
        }

        public ActionResult Index() => 
            base.View(this.db.RequestCreds.ToList<RequestCred>());
    }
}

