using Solution.Database.Entities;

namespace Solution.Core.Models;

public class MemberModel : IObjectValidator<uint>
{
    public uint Id { get; set; }

    public string ImageId { get; set; }

    public string WebContentLink { get; set; }

    public ValidatableObject<string> Name { get; set; }

    public ValidatableObject<TeamModel> Team { get; set; }

    public MemberModel()
    {
        this.Name = new ValidatableObject<string>();
        this.Team = new ValidatableObject<TeamModel>();

        AddValidators();
    }

    public MemberModel(MemberEntity entity): this()
    {
        this.Id = entity.Id;
        this.Name.Value = entity.Name;
        this.Team.Value = new TeamModel(entity.Team);
    }

    public MemberEntity ToEntity()
    {
        return new MemberEntity
        {
            Id = this.Id,
            Name = this.Name.Value,
            TeamId = Team.Value.Id,
        };
    }

    public void ToEntity(MemberEntity entity)
    {
        entity.Id = this.Id;
        entity.Name = this.Name.Value;
        entity.TeamId = this.Team.Value.Id;
    } 

    private void AddValidators()
    {
        this.Name.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "Name field can't be empty!"
        });

        this.Team.Validations.Add(new PickerValidationRule<TeamModel<uint>>()
        {
            ValidationMessage = "A team must be selected!"
        });
    }
}
