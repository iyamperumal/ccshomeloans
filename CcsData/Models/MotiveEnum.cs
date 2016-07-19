namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum MotiveEnum
    {
        Cashout = 3,
        [Display(Name="Debt Consolidation")]
        Debt_Consolidation = 4,
        [Display(Name="Debt Consolidation With Cashout")]
        Debt_Consolidation_With_Cashout = 5,
        Purshase = 6,
        [Display(Name="Rate And Term, Lower my Payment")]
        Rate_And_Term_lower_Payment = 1,
        [Display(Name="Rate And Term, Reduce Term")]
        Rate_And_Term_reduce_Term = 2
    }
}

