namespace Solution.ValidationLibrary.ValidationRules;

public class NotMoreOrLessThanXRule<T>(int count) : IValidationRule<T>
{
    public string ValidationMessage { get; set; }

    public int Amount { get; set; }

    public string Selectable { get; set; }

    public bool Check(object value)
    {
        if (value is IList<T> list)
        {
            if (count > Amount)
            {
                ValidationMessage = $"You can't select more than {count} {Selectable}!";
            }
            else 
            {
                ValidationMessage = $"You need to select {count} {Selectable}!";
            }

            return list.Count == count;
        }

        return false;
    }
}

