namespace Solution.ValidationLibrary;

public interface IValidationRule<T>
{
    string ValidationMessage { get; set; }

    string Selectable { get; set; } 

    bool Check(object value);
}

