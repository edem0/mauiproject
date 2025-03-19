using Solution.Database.Entities;

namespace Solution.Core.Models;

public class JuryModel : IObjectValidator<uint> 
{
    public uint Id { get; set; }

    public string Name { get; set; }

    public string EmailAddress { get; set; }

    public string PhoneNumber { get; set; }

    public JuryModel() { }

    public JuryModel(uint id, string name, string emailAddress, string phoneNumber) 
    {
        Id = id;
        Name = name;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
    }

    public JuryModel(JudgeEntity judge)
    {
        if(judge == null)
        {
            return;
        }

        Id = judge.Id;
        Name = judge.Name;
        EmailAddress = judge.EmailAddress;
        PhoneNumber = judge.PhoneNumber;
    }

    public override bool Equals(object? obj)
    {
        return obj is JuryModel model &&
            this.Id == model.Id &&
            this.Name == model.Name &&
            this.EmailAddress == model.EmailAddress &&
            this.PhoneNumber == model.PhoneNumber;
    }
}
