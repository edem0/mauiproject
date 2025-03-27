using Solution.Database.Entities;

namespace Solution.Core.Models;

public class JuryModel : IObjectValidator<uint> 
{
    public uint Id { get; set; }

    public string ImageId { get; set; }

    public string WebContentLink { get; set; }

    public ValidatableObject<string> Name { get; set; }

    public ValidatableObject<string> EmailAddress { get; set; }

    public ValidatableObject<string> PhoneNumber { get; set; }

    public JuryModel()
    {
        this.Name = new ValidatableObject<string>();
        this.EmailAddress = new ValidatableObject<string>();
        this.PhoneNumber = new ValidatableObject<string>();

        AddValidators();
    }

    public JuryModel(JudgeEntity judge): this()
    {
        this.Id = judge.Id;
        this.Name.Value = judge.Name;
        this.EmailAddress.Value = judge.EmailAddress;
        this.PhoneNumber.Value = judge.PhoneNumber;
    }

    public JudgeEntity ToEntity()
    {
        return new JudgeEntity
        {
            Id = this.Id,
            Name = this.Name.Value,
            EmailAddress = this.EmailAddress.Value,
            PhoneNumber = this.PhoneNumber.Value
        };
    }

    public void ToEntity(JudgeEntity entity)
    {
        entity.Id = this.Id;
        entity.Name = this.Name.Value;
        entity.EmailAddress = this.EmailAddress.Value;
        entity.PhoneNumber = this.PhoneNumber.Value;
    }

    private void AddValidators()
    {
        this.Name.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "Name field can't be empty!"
        });

        this.EmailAddress.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "Email address' field can't be empty!"
        });

        this.PhoneNumber.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "Phone number's field can't be empty!"
        });
    }
}
