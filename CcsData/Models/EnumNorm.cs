namespace CcsData.Models
{
    using System;

    public static class EnumNorm
    {
        public static YesNoAns BoolToYesNo(bool b)
        {
            if (b)
            {
                return YesNoAns.Yes;
            }
            return YesNoAns.No;
        }

        public static int MonthsTaxResurve(DateTime date, UsStateEnum state)
        {
            int num = 0;
            switch (state)
            {
                case UsStateEnum.AL:
                case UsStateEnum.AK:
                case (UsStateEnum.AK | UsStateEnum.AL):
                case UsStateEnum.AZ:
                case UsStateEnum.AR:
                case UsStateEnum.CA:
                case (UsStateEnum.CA | UsStateEnum.AL):
                case UsStateEnum.CO:
                case UsStateEnum.CT:
                case UsStateEnum.DE:
                case UsStateEnum.DC:
                case UsStateEnum.GA:
                case (UsStateEnum.FL | UsStateEnum.AK):
                case UsStateEnum.HI:
                case UsStateEnum.ID:
                case UsStateEnum.IL:
                case UsStateEnum.IN:
                case UsStateEnum.IA:
                case UsStateEnum.KS:
                case UsStateEnum.KY:
                case UsStateEnum.LA:
                case UsStateEnum.ME:
                case UsStateEnum.MD:
                case UsStateEnum.MA:
                case UsStateEnum.MI:
                case UsStateEnum.MN:
                case UsStateEnum.MS:
                case UsStateEnum.MO:
                case UsStateEnum.MT:
                case UsStateEnum.NE:
                case UsStateEnum.NV:
                case UsStateEnum.NH:
                case UsStateEnum.NJ:
                case UsStateEnum.NM:
                case UsStateEnum.NY:
                case UsStateEnum.NC:
                case UsStateEnum.ND:
                case UsStateEnum.OH:
                case UsStateEnum.OK:
                case UsStateEnum.OR:
                case UsStateEnum.PA:
                case (UsStateEnum.PA | UsStateEnum.AL):
                case UsStateEnum.RI:
                case UsStateEnum.SC:
                case UsStateEnum.SD:
                case UsStateEnum.TN:
                case UsStateEnum.TX:
                case UsStateEnum.UT:
                case UsStateEnum.VT:
                case UsStateEnum.VA:
                case (UsStateEnum.TX | UsStateEnum.AZ):
                case UsStateEnum.WA:
                case UsStateEnum.WV:
                case UsStateEnum.WI:
                case UsStateEnum.WY:
                    return num;

                case UsStateEnum.FL:
                {
                    int month = date.Month;
                    if (month == 9)
                    {
                        return 13;
                    }
                    if (month < 9)
                    {
                        return (month + 4);
                    }
                    return (month - 8);
                }
            }
            return num;
        }

        public static int TermToInt(MortgageTermEnum? mtgTetm)
        {
            MortgageTermEnum valueOrDefault = mtgTetm.GetValueOrDefault();
            if (mtgTetm.HasValue)
            {
                if (valueOrDefault > MortgageTermEnum.fifteen)
                {
                    if (valueOrDefault == MortgageTermEnum.twenty)
                    {
                        return 20;
                    }
                    if (valueOrDefault == MortgageTermEnum.twentyFive)
                    {
                        return 0x19;
                    }
                    if (valueOrDefault == MortgageTermEnum.thirty)
                    {
                        return 30;
                    }
                }
                else
                {
                    switch (valueOrDefault)
                    {
                        case MortgageTermEnum.five:
                            return 5;

                        case MortgageTermEnum.ten:
                            return 10;

                        case MortgageTermEnum.fifteen:
                            return 15;
                    }
                }
            }
            return 0;
        }

        public static bool YesNoToBool(YesNoAns ans) => 
            (ans == YesNoAns.Yes);
    }
}

