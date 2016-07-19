﻿namespace CcsData.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class Bank
    {
        public int Applicant_ID { get; set; }

        [Key]
        public int Bank_Id { get; set; }

        [ForeignKey("BankAddress_ID")]
        public virtual Address BankAddress { get; set; }

        public virtual int BankAddress_ID { get; set; }

        [Display(Name="Bank Name"), MaxLength(50)]
        public virtual string BankName { get; set; }

        [Display(Name="Checking Balance")]
        public virtual decimal? CheckingAccBalance { get; set; }

        [Display(Name="Checking Account Number"), MaxLength(50)]
        public virtual string CheckingAccNumber { get; set; }

        [Display(Name="Is Joint Acount"), DefaultValue(1)]
        public virtual YesNoAns JointAccount { get; set; }

        [ForeignKey("Applicant_ID")]
        public virtual Applicant MortgageApplicant { get; set; }

        [Display(Name="Phone Number"), MaxLength(50)]
        public virtual string PhoneNumber { get; set; }

        [Display(Name="Routing Number"), MaxLength(50)]
        public virtual string RoutingNumber { get; set; }

        [DataType(DataType.Currency), Display(Name="Savings Balance")]
        public virtual decimal? SavingsAccBalance { get; set; }

        [MaxLength(50), Display(Name="Savings Account Number")]
        public virtual string SavingsAccNumber { get; set; }
    }
}

