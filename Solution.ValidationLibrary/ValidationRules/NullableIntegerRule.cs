namespace Solution.ValidationLibrary.ValidationRules;

public class NullableIntegerRule<T> : IValidationRule<T>
{
    public string ValidationMessage { get; set; }

    public string Selectable { get; set; } = string.Empty;


    public bool Check(object value)
    {
        if(value is not uint data)
        { 
            return false;
        }
        
        return !string.IsNullOrEmpty(data.ToString());
    }
}
