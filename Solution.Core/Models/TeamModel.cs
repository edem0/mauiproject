using Solution.Database.Entities;

namespace Solution.Core.Models;

public class TeamModel : IObjectValidator<uint>
{
    public uint Id { get; set; }

    public ValidatableObject<string> Name { get; set; }

    public ValidatableObject<uint?> Points { get; set; }

    public ValidatableObject<List<MemberModel>> Members { get; set; }

    public TeamModel()
    {
        this.Name = new ValidatableObject<string>();
        this.Points = new ValidatableObject<uint?>();
        this.Members = new ValidatableObject<List<MemberModel>>();

        AddValidators();
    }

    public TeamModel(TeamEntity entity): this()
    {
        this.Id = entity.Id;
        this.Name.Value = entity.Name;
        this.Points.Value = entity.Points;
        this.Members.Value = entity.Members.Select(x => new MemberModel(x)).ToList();
    }

    public TeamEntity ToEntity()
    {
        return new TeamEntity
        {
            Id = this.Id,
            Name = this.Name.Value,
            Points = this.Points.Value,
            Members = this.Members.Value.Select(x => new MemberEntity
            {
                Id = x.Id,
                Name = x.Name.Value,
                TeamId = x.Team.Value.Id,
            }).ToList()
        };
    }

    public void ToEntity(TeamEntity entity)
    {
        entity.Id = this.Id;
        entity.Name = this.Name.Value;
        entity.Points = this.Points.Value;
        entity.Members = this.Members.Value.Select(x => new MemberEntity 
        { 
            Id = x.Id,
            Name = x.Name.Value,
            TeamId = x.Team.Value.Id,
        }).ToList();
    }

    private void AddValidators()
    {
        this.Name.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "Name field can't be empty!"
        });

        this.Points.Validations.Add(new MinValueRule<uint?>(0)
        {
            ValidationMessage = "A value must be given to this field!"
        });

        this.Members.Validations.Add(new NotMoreOrLessThanXRule<List<MemberModel>>(10, "members"){});
    }
}
