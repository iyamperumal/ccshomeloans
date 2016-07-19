namespace CcsWeb.DataMigration
{
    using CcsData.Models;
    using CcsWeb.DataContexts;
    using CcsWeb.Helpers;
    using CcsWeb.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class PopList
    {
        public string csvPathlocal = @"E:\0Hamid\proj_stf\ccs\leadFiles\Fannie_Harp.csv";
        public string csvPathRemote = @"E:\0Hamid\proj_stf\ccs\leadFiles\Remote\Fannie_Harp.csv";

        public void AddApplicantListLocal(int startRecord = 0, int endRecord = 0)
        {
            string username;
            if (endRecord >= startRecord)
            {
                CcsRemoteDbContext context = new CcsRemoteDbContext();
                string password = "password";
                StreamReader reader = new StreamReader(this.csvPathlocal);
                string[] strArray = reader.ReadLine().Split(new char[] { ',' });
                int numRecords = endRecord - startRecord;
                LeadData data = this.getLeadData(numRecords);
                int num2 = 0;
                username = "";
                while (!reader.EndOfStream && (num2 < startRecord))
                {
                    reader.ReadLine();
                    num2++;
                }
                numRecords = num2 + numRecords;
                while (!reader.EndOfStream && (num2 < numRecords))
                {
                    string str3 = reader.ReadLine();
                    if (numRecords == 0)
                    {
                        numRecords = num2 + 1;
                    }
                    strArray = str3.Split(new char[] { ',' });
                    Console.WriteLine("hello from datamigration");
                    Applicant applicant = new Applicant();
                    Address item = new Address();
                    Mortgage mortgage = new Mortgage();
                    Mortgage mortgage2 = new Mortgage();
                    Property property = new Property();
                    Credit credit = new Credit();
                    applicant.LastName = strArray[0];
                    applicant.FirstName = strArray[1];
                    applicant.MiddleName = strArray[2];
                    string firstName = applicant.FirstName;
                    if ((applicant.MiddleName != null) && (applicant.MiddleName.Trim() != ""))
                    {
                        firstName = firstName + " " + applicant.MiddleName;
                    }
                    firstName = firstName + " " + applicant.LastName;
                    applicant.FullName = firstName;
                    applicant.CashOutAmountRequested = 0.00M;
                    item.StreetAddress = strArray[3];
                    property.Address = strArray[3];
                    item.UnitNumber = strArray[4];
                    property.UnitNumber = strArray[4];
                    item.City = strArray[5];
                    property.City = strArray[5];
                    item.State = strArray[6];
                    property.State = Util.getState(strArray[6]);
                    item.ZipCode = strArray[7];
                    property.ZipCode = strArray[7];
                    item.Zip4 = strArray[8];
                    property.Zip4 = strArray[8];
                    applicant.HomePhone = strArray[11];
                    credit.LastKnownCreditScore = new int?(int.Parse(strArray[12]));
                    credit.LastScoreDate = new DateTime?(DateTime.Now);
                    item.County = strArray[13];
                    property.County = strArray[13];
                    mortgage2.Balance = new decimal?(decimal.Parse(strArray[14]));
                    decimal result = 0.00M;
                    decimal.TryParse(strArray[15], out result);
                    mortgage2.MonthlyPayment = new decimal?(result);
                    mortgage.LanderName = strArray[0x10];
                    mortgage.Balance = new decimal?(decimal.Parse(strArray[0x12]));
                    mortgage.MonthlyPayment = new decimal?(decimal.Parse(strArray[0x16]));
                    int num4 = int.Parse(strArray[0x15]);
                    DateTime now = DateTime.Now;
                    int days = num4 * 30;
                    TimeSpan span = new TimeSpan(days, 0, 0, 0);
                    now = now.Subtract(span);
                    mortgage.OriginationDate = new DateTime?(now);
                    decimal num6 = 0.00M;
                    decimal.TryParse(strArray[0x17], out num6);
                    property.PurshasePrice = new decimal?(num6);
                    string str5 = strArray[0x18];
                    if (str5.Trim() != "")
                    {
                        string s = str5.Substring(0, 4);
                        string str7 = str5.Substring(4, 2);
                        string str8 = str5.Substring(6, 2);
                        int year = int.Parse(s);
                        int month = int.Parse(str7);
                        int day = int.Parse(str8);
                        DateTime time2 = new DateTime(year, month, day);
                        property.PurshaseDate = new DateTime?(time2);
                    }
                    applicant.SocialSecurity4 = strArray[0x1a];
                    property.EstimatedLTV = new decimal?(decimal.Parse(strArray[0x1f]));
                    property.EstimatedLTVdate = new DateTime?(DateTime.Now);
                    property.EstimatedMarketValue = ((mortgage.Balance + mortgage2.Balance) * property.EstimatedLTV) / 100.00M;
                    property.EstimatedMarketValueDate = new DateTime?(DateTime.Now);
                    mortgage.Position = 1;
                    mortgage2.Position = 2;
                    mortgage.MortageType = (MortgageProgramOptionsEnum)9;
                    applicant.LeadData = data;
                    applicant.Mortgages = new List<Mortgage>();
                    applicant.Mortgages.Add(mortgage);
                    if (mortgage2.Balance.HasValue && (mortgage2.Balance != 0.00M))
                    {
                        applicant.Mortgages.Add(mortgage2);
                    }
                    applicant.Addresses = new List<Address>();
                    applicant.Addresses.Add(item);
                    applicant.Credit = credit;
                    applicant.Properties = new List<Property>();
                    applicant.Properties.Add(property);
                    mortgage.MortgagedProperty = property;
                    username = applicant.FirstName + applicant.LastName + applicant.SocialSecurity4.Substring(1, 2);
                    password = applicant.LastName + ((credit.LastKnownCreditScore + 20)).ToString();
                    if (password.Length < 6)
                    {
                        password = password + "1234";
                    }
                    RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(context);
                    new RoleManager<IdentityRole>(store);
                    UserStore<ApplicationUser> store2 = new UserStore<ApplicationUser>(context);
                    UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(store2);
                    new CallLog();
                    if (!context.Users.Any<ApplicationUser>(u => (u.UserName == username)))
                    {
                        ApplicationUser user = new ApplicationUser {
                            UserName = username
                        };
                        manager.Create<ApplicationUser, string>(user, password);
                        manager.AddToRole<ApplicationUser, string>(user.Id, "Lead");
                        user.Applicant = applicant;
                    }
                    else
                    {
                        ApplicationUser user3 = (from u in context.Users
                            where u.UserName == username
                            select u).FirstOrDefault<ApplicationUser>();
                        if (!user3.Applicant_id.HasValue)
                        {
                            user3.Applicant = applicant;
                        }
                    }
                    num2++;
                }
                context.SaveChanges();
                reader.Close();
                reader.Dispose();
                context.Dispose();
            }
        }

        public void AddApplicantListRemote(int startRecord = 0, int endRecord = 0)
        {
            string username;
            if (endRecord >= startRecord)
            {
                CcsLocalDbContext context = new CcsLocalDbContext();
                string password = "password";
                StreamReader reader = new StreamReader(this.csvPathRemote);
                string[] strArray = reader.ReadLine().Split(new char[] { ',' });
                int numRecords = endRecord - startRecord;
                LeadData data = this.getLeadData(numRecords);
                int num2 = 0;
                username = "";
                while (!reader.EndOfStream && (num2 < startRecord))
                {
                    reader.ReadLine();
                    num2++;
                }
                numRecords = num2 + numRecords;
                while (!reader.EndOfStream && (num2 < numRecords))
                {
                    string str3 = reader.ReadLine();
                    if (numRecords == 0)
                    {
                        numRecords = num2 + 1;
                    }
                    strArray = str3.Split(new char[] { ',' });
                    Console.WriteLine("hello from datamigration");
                    Applicant applicant = new Applicant();
                    Address item = new Address();
                    Mortgage mortgage = new Mortgage();
                    Mortgage mortgage2 = new Mortgage();
                    Property property = new Property();
                    Credit credit = new Credit();
                    applicant.LastName = strArray[0];
                    applicant.FirstName = strArray[1];
                    applicant.MiddleName = strArray[2];
                    string firstName = applicant.FirstName;
                    if ((applicant.MiddleName != null) && (applicant.MiddleName.Trim() != ""))
                    {
                        firstName = firstName + " " + applicant.MiddleName;
                    }
                    firstName = firstName + " " + applicant.LastName;
                    applicant.FullName = firstName;
                    applicant.CashOutAmountRequested = 0.00M;
                    item.StreetAddress = strArray[3];
                    property.Address = strArray[3];
                    item.UnitNumber = strArray[4];
                    property.UnitNumber = strArray[4];
                    item.City = strArray[5];
                    property.City = strArray[5];
                    item.State = strArray[6];
                    property.State = Util.getState(strArray[6]);
                    item.ZipCode = strArray[7];
                    property.ZipCode = strArray[7];
                    item.Zip4 = strArray[8];
                    property.Zip4 = strArray[8];
                    applicant.HomePhone = strArray[11];
                    credit.LastKnownCreditScore = new int?(int.Parse(strArray[12]));
                    credit.LastScoreDate = new DateTime?(DateTime.Now);
                    item.County = strArray[13];
                    property.County = strArray[13];
                    mortgage2.Balance = new decimal?(decimal.Parse(strArray[14]));
                    decimal result = 0.00M;
                    decimal.TryParse(strArray[15], out result);
                    mortgage2.MonthlyPayment = new decimal?(result);
                    mortgage.LanderName = strArray[0x10];
                    mortgage.Balance = new decimal?(decimal.Parse(strArray[0x12]));
                    mortgage.MonthlyPayment = new decimal?(decimal.Parse(strArray[0x16]));
                    int num4 = int.Parse(strArray[0x15]);
                    DateTime now = DateTime.Now;
                    int days = num4 * 30;
                    TimeSpan span = new TimeSpan(days, 0, 0, 0);
                    now = now.Subtract(span);
                    mortgage.OriginationDate = new DateTime?(now);
                    decimal num6 = 0.00M;
                    decimal.TryParse(strArray[0x17], out num6);
                    property.PurshasePrice = new decimal?(num6);
                    string str5 = strArray[0x18];
                    if (str5.Trim() != "")
                    {
                        string s = str5.Substring(0, 4);
                        string str7 = str5.Substring(4, 2);
                        string str8 = str5.Substring(6, 2);
                        int year = int.Parse(s);
                        int month = int.Parse(str7);
                        int day = int.Parse(str8);
                        DateTime time2 = new DateTime(year, month, day);
                        property.PurshaseDate = new DateTime?(time2);
                    }
                    applicant.SocialSecurity4 = strArray[0x1a];
                    property.EstimatedLTV = new decimal?(decimal.Parse(strArray[0x1f]));
                    property.EstimatedLTVdate = new DateTime?(DateTime.Now);
                    property.EstimatedMarketValue = ((mortgage.Balance + mortgage2.Balance) * property.EstimatedLTV) / 100.00M;
                    property.EstimatedMarketValueDate = new DateTime?(DateTime.Now);
                    mortgage.Position = 1;
                    mortgage2.Position = 2;
                    mortgage.MortageType = (MortgageProgramOptionsEnum)9;
                    applicant.LeadData = data;
                    applicant.Mortgages = new List<Mortgage>();
                    applicant.Mortgages.Add(mortgage);
                    if (mortgage2.Balance.HasValue && (mortgage2.Balance != 0.00M))
                    {
                        applicant.Mortgages.Add(mortgage2);
                    }
                    applicant.Addresses = new List<Address>();
                    applicant.Addresses.Add(item);
                    applicant.Credit = credit;
                    applicant.Properties = new List<Property>();
                    applicant.Properties.Add(property);
                    mortgage.MortgagedProperty = property;
                    username = applicant.FirstName + applicant.LastName + applicant.SocialSecurity4.Substring(1, 2);
                    password = applicant.LastName + ((credit.LastKnownCreditScore + 20)).ToString();
                    if (password.Length < 6)
                    {
                        password = password + "1234";
                    }
                    UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(context);
                    UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(store);
                    new CallLog();
                    if (!context.Users.Any<ApplicationUser>(u => (u.UserName == username)))
                    {
                        ApplicationUser user = new ApplicationUser {
                            UserName = username
                        };
                        manager.Create<ApplicationUser, string>(user, password);
                        manager.AddToRole<ApplicationUser, string>(user.Id, "Lead");
                        user.Applicant = applicant;
                    }
                    else
                    {
                        ApplicationUser user3 = (from u in context.Users
                            where u.UserName == username
                            select u).FirstOrDefault<ApplicationUser>();
                        if (!user3.Applicant_id.HasValue)
                        {
                            user3.Applicant = applicant;
                        }
                    }
                    num2++;
                }
                context.SaveChanges();
                reader.Close();
                reader.Dispose();
                context.Dispose();
            }
        }

        public void AddApplicantListVps(int startRecord = 0, int endRecord = 0)
        {
            string username;
            if (endRecord >= startRecord)
            {
                VpsContext context = new VpsContext();
                string password = "password";
                StreamReader reader = new StreamReader(this.csvPathRemote);
                string[] strArray = reader.ReadLine().Split(new char[] { ',' });
                int numRecords = endRecord - startRecord;
                LeadData data = this.getLeadData(numRecords);
                int num2 = 0;
                username = "";
                while (!reader.EndOfStream && (num2 < startRecord))
                {
                    reader.ReadLine();
                    num2++;
                }
                numRecords = num2 + numRecords;
                while (!reader.EndOfStream && (num2 < numRecords))
                {
                    string str3 = reader.ReadLine();
                    if (numRecords == 0)
                    {
                        numRecords = num2 + 1;
                    }
                    strArray = str3.Split(new char[] { ',' });
                    Console.WriteLine("hello from datamigration");
                    Applicant applicant = new Applicant();
                    Address item = new Address();
                    Mortgage mortgage = new Mortgage();
                    Mortgage mortgage2 = new Mortgage();
                    Property property = new Property();
                    Credit credit = new Credit();
                    applicant.LastName = strArray[0];
                    applicant.FirstName = strArray[1];
                    applicant.MiddleName = strArray[2];
                    string firstName = applicant.FirstName;
                    if ((applicant.MiddleName != null) && (applicant.MiddleName.Trim() != ""))
                    {
                        firstName = firstName + " " + applicant.MiddleName;
                    }
                    firstName = firstName + " " + applicant.LastName;
                    applicant.FullName = firstName;
                    applicant.CashOutAmountRequested = 0.00M;
                    item.StreetAddress = strArray[3];
                    property.Address = strArray[3];
                    item.UnitNumber = strArray[4];
                    property.UnitNumber = strArray[4];
                    item.City = strArray[5];
                    property.City = strArray[5];
                    item.State = strArray[6];
                    property.State = Util.getState(strArray[6]);
                    item.ZipCode = strArray[7];
                    property.ZipCode = strArray[7];
                    item.Zip4 = strArray[8];
                    property.Zip4 = strArray[8];
                    applicant.HomePhone = strArray[11];
                    credit.LastKnownCreditScore = new int?(int.Parse(strArray[12]));
                    credit.LastScoreDate = new DateTime?(DateTime.Now);
                    item.County = strArray[13];
                    property.County = strArray[13];
                    mortgage2.Balance = new decimal?(decimal.Parse(strArray[14]));
                    decimal result = 0.00M;
                    decimal.TryParse(strArray[15], out result);
                    mortgage2.MonthlyPayment = new decimal?(result);
                    mortgage.LanderName = strArray[0x10];
                    mortgage.Balance = new decimal?(decimal.Parse(strArray[0x12]));
                    mortgage.MonthlyPayment = new decimal?(decimal.Parse(strArray[0x16]));
                    int num4 = int.Parse(strArray[0x15]);
                    DateTime now = DateTime.Now;
                    int days = num4 * 30;
                    TimeSpan span = new TimeSpan(days, 0, 0, 0);
                    now = now.Subtract(span);
                    mortgage.OriginationDate = new DateTime?(now);
                    decimal num6 = 0.00M;
                    decimal.TryParse(strArray[0x17], out num6);
                    property.PurshasePrice = new decimal?(num6);
                    string str5 = strArray[0x18];
                    if (str5.Trim() != "")
                    {
                        string s = str5.Substring(0, 4);
                        string str7 = str5.Substring(4, 2);
                        string str8 = str5.Substring(6, 2);
                        int year = int.Parse(s);
                        int month = int.Parse(str7);
                        int day = int.Parse(str8);
                        DateTime time2 = new DateTime(year, month, day);
                        property.PurshaseDate = new DateTime?(time2);
                    }
                    applicant.SocialSecurity4 = strArray[0x1a];
                    property.EstimatedLTV = new decimal?(decimal.Parse(strArray[0x1f]));
                    property.EstimatedLTVdate = new DateTime?(DateTime.Now);
                    property.EstimatedMarketValue = ((mortgage.Balance + mortgage2.Balance) * property.EstimatedLTV) / 100.00M;
                    property.EstimatedMarketValueDate = new DateTime?(DateTime.Now);
                    mortgage.Position = 1;
                    mortgage2.Position = 2;
                    mortgage.MortageType = (MortgageProgramOptionsEnum)9;
                    applicant.LeadData = data;
                    applicant.Mortgages = new List<Mortgage>();
                    applicant.Mortgages.Add(mortgage);
                    if (mortgage2.Balance.HasValue && (mortgage2.Balance != 0.00M))
                    {
                        applicant.Mortgages.Add(mortgage2);
                    }
                    applicant.Addresses = new List<Address>();
                    applicant.Addresses.Add(item);
                    applicant.Credit = credit;
                    applicant.Properties = new List<Property>();
                    applicant.Properties.Add(property);
                    mortgage.MortgagedProperty = property;
                    username = applicant.FirstName + applicant.LastName + applicant.SocialSecurity4.Substring(1, 2);
                    password = applicant.LastName + ((credit.LastKnownCreditScore + 20)).ToString();
                    if (password.Length < 6)
                    {
                        password = password + "1234";
                    }
                    UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(context);
                    UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(store);
                    new CallLog();
                    if (!context.Users.Any<ApplicationUser>(u => (u.UserName == username)))
                    {
                        ApplicationUser user = new ApplicationUser {
                            UserName = username
                        };
                        manager.Create<ApplicationUser, string>(user, password);
                        manager.AddToRole<ApplicationUser, string>(user.Id, "Lead");
                        user.Applicant = applicant;
                    }
                    else
                    {
                        ApplicationUser user3 = (from u in context.Users
                            where u.UserName == username
                            select u).FirstOrDefault<ApplicationUser>();
                        if (!user3.Applicant_id.HasValue)
                        {
                            user3.Applicant = applicant;
                        }
                    }
                    num2++;
                }
                context.SaveChanges();
                reader.Close();
                reader.Dispose();
                context.Dispose();
            }
        }

        public LeadData getLeadData(int NumRecords) => 
            new LeadData { 
                UploadDate = new DateTime?(DateTime.Now),
                FileName = Path.GetFileName(this.csvPathRemote),
                Quantity = NumRecords
            };

        public void PopulateLocal(LeadFile leadFile, CcsLocalDbContext context)
        {
        }
    }
}

