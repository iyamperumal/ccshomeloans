namespace CcsData.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class UsdaCountiesAndIncome
    {
        [MaxLength(50)]
        public string County { get; set; }

        public int Fips { get; set; }

        [DefaultValue(0)]
        public decimal IncomeLimit1 { get; set; }

        [MaxLength(50)]
        public string State { get; set; }

        [Key]
        public virtual int UsdaCountiesAndIncome_Id { get; set; }
    }
}

