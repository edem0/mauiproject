using Solution.ValidationLibrary;

namespace Solution.ValidationLibrary.ValidationRules;

public class DateTimeValidationRule<T>(DateTime dateTime) : IValidationRule<T>
{
    public string ValidationMessage { get; set; } = $"Value can't be larger than {dateTime}!";

    public string Selectable { get; set; } = string.Empty;

    public bool Check(object value)
    {
        if (dateTime > DateTime.Now)
        {
            return false;
        }

        return dateTime <= DateTime.Now;
    }
}