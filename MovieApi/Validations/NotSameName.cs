using Movies.Core.DTOs;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.Validations;

public class NotSameName : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        //Example
        //var context = validationContext.GetRequiredService<MovieContext>();

        if (value is string input)
        {
            if (validationContext.ObjectInstance is MovieCreateDto dto)
            {
                return dto.Title.Trim().Equals(input.Trim(), StringComparison.OrdinalIgnoreCase) ?
                    new ValidationResult("Title cannot be same") :
                    ValidationResult.Success;
            }
        }

        return new ValidationResult("Error");
    }
}
