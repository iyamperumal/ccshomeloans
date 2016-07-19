namespace CcsWeb.Helpers
{
    using CcsData.Models;
    using CcsWeb.DataContexts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Util
    {
        internal static Mortgage GetCurMortgage(Applicant app)
        {
            CcsLocalDbContext context = new CcsLocalDbContext();
            Mortgage mortgage = context.Mortgages.FirstOrDefault<Mortgage>(x => x.Applicant_ID == app.Applicant_Id);
            context.Dispose();
            return mortgage;
        }

        internal static UsStateEnum getState(string name)
        {
            switch (name.Trim().ToUpper())
            {
                case "ALABAMA":
                    return UsStateEnum.AL;

                case "AL":
                    return UsStateEnum.AL;

                case "ALASKA":
                    return UsStateEnum.AK;

                case "AK":
                    return UsStateEnum.AK;

                case "ARIZONA":
                    return UsStateEnum.AZ;

                case "AZ":
                    return UsStateEnum.AZ;

                case "ARKANSAS":
                    return UsStateEnum.AR;

                case "AR":
                    return UsStateEnum.AR;

                case "CALIFORNIA":
                    return UsStateEnum.CA;

                case "CA":
                    return UsStateEnum.CA;

                case "COLORADO":
                    return UsStateEnum.CO;

                case "CO":
                    return UsStateEnum.CO;

                case "CONNECTICUT":
                    return UsStateEnum.CT;

                case "CT":
                    return UsStateEnum.CT;

                case "DELAWARE":
                    return UsStateEnum.DE;

                case "DE":
                    return UsStateEnum.DE;

                case "DISTRICT OF COLUMBIA":
                    return UsStateEnum.DC;

                case "DC":
                    return UsStateEnum.DC;

                case "FLORIDA":
                    return UsStateEnum.FL;

                case "FL":
                    return UsStateEnum.FL;

                case "GEORGIA":
                    return UsStateEnum.GA;

                case "GA":
                    return UsStateEnum.GA;

                case "HAWAII":
                    return UsStateEnum.HI;

                case "HI":
                    return UsStateEnum.HI;

                case "IDAHO":
                    return UsStateEnum.ID;

                case "ID":
                    return UsStateEnum.ID;

                case "ILLINOIS":
                    return UsStateEnum.IL;

                case "IL":
                    return UsStateEnum.IL;

                case "INDIANA":
                    return UsStateEnum.IN;

                case "IN":
                    return UsStateEnum.IN;

                case "IOWA":
                    return UsStateEnum.IA;

                case "KANSAS":
                    return UsStateEnum.KS;

                case "KS":
                    return UsStateEnum.KS;

                case "KENTUCKY":
                    return UsStateEnum.KY;

                case "KY":
                    return UsStateEnum.KY;

                case "LOUISIANA":
                    return UsStateEnum.LA;

                case "MAINE":
                    return UsStateEnum.ME;

                case "ME":
                    return UsStateEnum.ME;

                case "MARYLAND":
                    return UsStateEnum.MD;

                case "MASSACHUSETTS":
                    return UsStateEnum.MA;

                case "MA":
                    return UsStateEnum.MA;

                case "MICHIGAN":
                    return UsStateEnum.MI;

                case "MI":
                    return UsStateEnum.MI;

                case "MINNESOTA":
                    return UsStateEnum.MN;

                case "MN":
                    return UsStateEnum.MN;

                case "MISSISSIPPI":
                    return UsStateEnum.MS;

                case "MS":
                    return UsStateEnum.MS;

                case "MISSOURI":
                    return UsStateEnum.MO;

                case "MO":
                    return UsStateEnum.MO;

                case "MONTANA":
                    return UsStateEnum.MT;

                case "MT":
                    return UsStateEnum.MT;

                case "NEBRASKA":
                    return UsStateEnum.NE;

                case "NE":
                    return UsStateEnum.NE;

                case "NEVADA":
                    return UsStateEnum.NV;

                case "NV":
                    return UsStateEnum.NV;

                case "NEW HAMPSHIRE":
                    return UsStateEnum.NH;

                case "NH":
                    return UsStateEnum.NH;

                case "NEW JERSEY":
                    return UsStateEnum.NJ;

                case "NJ":
                    return UsStateEnum.NJ;

                case "NEW MEXICO":
                    return UsStateEnum.NM;

                case "NM":
                    return UsStateEnum.NM;

                case "NEW YORK":
                    return UsStateEnum.NY;

                case "NY":
                    return UsStateEnum.NY;

                case "NORTH CAROLINA":
                    return UsStateEnum.NC;

                case "NC":
                    return UsStateEnum.NC;

                case "NORTH DAKOTA":
                    return UsStateEnum.ND;

                case "ND":
                    return UsStateEnum.ND;

                case "OHIO":
                    return UsStateEnum.OH;

                case "OH":
                    return UsStateEnum.OH;

                case "OKLAHOMA":
                    return UsStateEnum.OK;

                case "OK":
                    return UsStateEnum.OK;

                case "OREGON":
                    return UsStateEnum.OR;

                case "OR":
                    return UsStateEnum.OR;

                case "PENNSYLVANIA":
                    return UsStateEnum.PA;

                case "PA":
                    return UsStateEnum.PA;

                case "RHODE ISLAND":
                    return UsStateEnum.RI;

                case "RI":
                    return UsStateEnum.RI;

                case "SOUTH CAROLINA":
                    return UsStateEnum.SC;

                case "SC":
                    return UsStateEnum.SC;

                case "SOUTH DAKOTA":
                    return UsStateEnum.SD;

                case "SD":
                    return UsStateEnum.SD;

                case "TENNESSEE":
                    return UsStateEnum.TN;

                case "TN":
                    return UsStateEnum.TN;

                case "TEXAS":
                    return UsStateEnum.TX;

                case "TX":
                    return UsStateEnum.TX;

                case "UTAH":
                    return UsStateEnum.UT;

                case "UT":
                    return UsStateEnum.UT;

                case "VERMONT":
                    return UsStateEnum.VT;

                case "VT":
                    return UsStateEnum.VT;

                case "VIRGINIA":
                    return UsStateEnum.VA;

                case "VA":
                    return UsStateEnum.VA;

                case "WASHINGTON":
                    return UsStateEnum.WA;

                case "WA":
                    return UsStateEnum.WA;

                case "WEST VIRGINIA":
                    return UsStateEnum.WV;

                case "WV":
                    return UsStateEnum.WV;

                case "WISCONSIN":
                    return UsStateEnum.WI;

                case "WN":
                    return UsStateEnum.WI;

                case "WYOMING":
                    return UsStateEnum.WY;

                case "WY":
                    return UsStateEnum.WY;
            }
            return UsStateEnum.FL;
        }

        internal static List<Variable> GetVariables()
        {
            CcsLocalDbContext context = new CcsLocalDbContext();
            List<Variable> list = context.Variables.ToList<Variable>();
            context.Dispose();
            return list;
        }
    }
}

