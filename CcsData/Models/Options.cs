namespace CcsData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class Options
    {
        [Key]
        public virtual int Options_Id { get; set; }

        public virtual List<RefiOption> RefOptions { get; set; }

        public virtual List<bool> Selectedoption { get; set; }
    }
}

