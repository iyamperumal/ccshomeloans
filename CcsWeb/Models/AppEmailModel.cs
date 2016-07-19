namespace CcsWeb.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class AppEmailModel
    {
        public string EmailBody { get; set; }

        public string FirstName { get; set; }

        public string FromEmailAddess { get; set; }

        public int Id { get; set; }

        public bool IsPurchase { get; set; }

        public string LastName { get; set; }

        public string Message { get; set; }

        public string RealtorEmail { get; set; }

        public string RealtorFName { get; set; }

        public string RealtorLName { get; set; }

        public string RealtorName { get; set; }

        public string Subject { get; set; }

        public string ToEmailAddess { get; set; }
    }
}

