namespace CcsWeb.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CurrencyVal : ValidationAttribute
    {
        public CurrencyVal() : base("{0} Must Be a dollard amount")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            value.ToString();
            return ValidationResult.Success;
        }
    }
}

