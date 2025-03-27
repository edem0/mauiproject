namespace Solution.ValidationLibrary.ValidationRules;

public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
{
    public string ValidationMessage { get; set; } = "Required field";

    public string Selectable { get; set; } = string.Empty;

    public bool Check(object value)
    {
        var isTypeOfT = value is T;
        var isEmpty = string.IsNullOrWhiteSpace(value?.ToString());

        return isTypeOfT && !isEmpty;
    }
}
