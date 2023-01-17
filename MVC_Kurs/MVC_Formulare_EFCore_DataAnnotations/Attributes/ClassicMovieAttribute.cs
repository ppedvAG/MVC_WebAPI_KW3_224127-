using MVC_Formulare_EFCore_DataAnnotations.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_Formulare_EFCore_DataAnnotations.Attributes
{
    public class ClassicMovieAttribute : ValidationAttribute
    {
        public ClassicMovieAttribute(int year)
        {
            Year = year;
        }

        public int Year { get; }

        public string GetErrorMessage() => $"Klassische Filme müssen vor/gleich dem Jahr {Year} sein";


        //Wird 
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            Movie movie = (Movie)validationContext.ObjectInstance;
            int releaseYear = ((DateTime)value!).Year;

            if (movie.Genre == GenreTyp.Classic && releaseYear > Year)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
