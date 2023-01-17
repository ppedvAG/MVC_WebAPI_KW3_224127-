using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Formulare_EFCore_DataAnnotations.Data;
using MVC_Formulare_EFCore_DataAnnotations.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MVC_Formulare_EFCore_DataAnnotations.Attributes
{
    public class UniversalSerialnumberAttribute : ValidationAttribute
    {
        public UniversalSerialnumberAttribute()
        {
        }

        public string GetErrorMessage() => $"Kein valides Seriennummer-Format";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            Movie currentMovie = (Movie)validationContext.ObjectInstance;

            string serialNumber = currentMovie.SerialNumber;
            
            //string pattern = @"\b[M]\w+";
            //Regex rg = new Regex(pattern);

            //if (rg.IsMatch(serialNumber))
            //{
            //}

            if (string.IsNullOrEmpty(serialNumber) || string.IsNullOrWhiteSpace(serialNumber)) 
            {
                return new ValidationResult(GetErrorMessage());
            }

            //Seriennummer aus der EU 
            if (serialNumber.StartsWith("EU-"))
            {
                return ValidationResult.Success;
            }

            if (serialNumber.StartsWith("EG-"))
            {
                return ValidationResult.Success;
            }

            if (serialNumber.StartsWith("AS-"))
            {
                return ValidationResult.Success;
            }


            return new ValidationResult(GetErrorMessage());

        }
    }
}
