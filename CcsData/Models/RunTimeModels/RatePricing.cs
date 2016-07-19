namespace CcsData.Models.RunTimeModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class RatePricing
    {
        public static RatePricing GetPar30(List<RatePricing> RateList)
        {
            RatePricing pricing = new RatePricing();
            List<double> list = new List<double>();
            for (int i = 0; i < RateList.Count; i++)
            {
                list.Add(Math.Abs(RateList[i].Cost30Days));
            }
            double item = ((IEnumerable<double>) list).Min();
            int index = list.IndexOf(item);
            return RateList[index];
        }

        public static RatePricing GetPar30higherInterest(List<RatePricing> RateList, RatePricing ratePrice)
        {
            RatePricing pricing = new RatePricing();
            int index = RateList.IndexOf(ratePrice);
            if (index > 0)
            {
                return RateList[index - 1];
            }
            return null;
        }

        public static RatePricing GetPar30LowerInterest(List<RatePricing> RateList, RatePricing ratePrice)
        {
            RatePricing pricing = new RatePricing();
            int index = RateList.IndexOf(ratePrice);
            if (index < (RateList.Count - 1))
            {
                return RateList[index + 1];
            }
            return null;
        }

        public double Cost15Days { get; set; }

        public double Cost30Days { get; set; }

        public double Cost45Days { get; set; }

        public double Cost60Days { get; set; }

        public string DaysLock { get; set; }

        public double interest { get; set; }

        public string ScheduleName { get; set; }
    }
}

