namespace Solution.ValidationLibrary.ValidationRules;

public class MinValueRule<T>(int minValue) : IValidationRule<T>
{
  public string ValidationMessage { get; set; } = $"Length can't bee less then {minValue}.";

  public string Selectable { get; set; } = string.Empty;

    public bool Check(object value)
  {
      if(!int.TryParse(value?.ToString(), out int data))
      { 
          return false;
      }
        
      return data >= minValue;
  }
}
