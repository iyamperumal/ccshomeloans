namespace CcsData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class CallLog
    {
        public CallLog()
        {
            this.Date = new DateTime?(DateTime.Now);
            this.Time = new DateTime?(DateTime.Now);
        }

        [Key]
        public virtual int CallLog_Id { get; set; }

        [DataType(DataType.Date)]
        public virtual DateTime? Date { get; set; }

        public virtual string Note { get; set; }

        [DataType(DataType.Time)]
        public virtual DateTime? Time { get; set; }
    }
}

