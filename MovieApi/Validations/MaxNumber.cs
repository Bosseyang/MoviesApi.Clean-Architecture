using System.ComponentModel.DataAnnotations;

namespace MovieApi.Validations;

public class MaxNumber : ValidationAttribute
{
    private readonly int max;
    public MaxNumber(int max)
    {
        this.max = max;
    }

    public override bool IsValid(object? value)
    {
        if (value is string input)
        {
            var num = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Last();
            return int.TryParse(num, out int result) && result <= max;
        }

        return false;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"The street number must be less than {max}";
    }
}
