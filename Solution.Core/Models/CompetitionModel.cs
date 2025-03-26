using Solution.Database.Entities;

namespace Solution.Core.Models;

public partial class CompetitionModel
{
    public uint Id { get; set; }

    public ValidatableObject<string> Name { get; protected set; }

    public ValidatableObject<DateTime> Date { get; protected set; }

    public List<JuryModel> Jury { get; set; }

    public List<TeamModel> Teams { get; set; }

    public ValidatableObject<LocationModel> Location { get; set; }

    public CompetitionModel() 
    {
        this.Name = new ValidatableObject<string>();
        this.Date = new ValidatableObject<DateTime>();
        this.Location = new ValidatableObject<LocationModel>();

        AddValidators();
    }

    public CompetitionModel(CompetitionEntity entity): this()
    {
        this.Id = entity.Id;
        this.Name.Value = entity.Name;
        this.Date.Value = entity.Date;
        this.Jury = entity.Jury.Select(x => new JuryModel(x)).ToList();
        this.Location.Value = new LocationModel(entity.Location);
        this.Teams = entity.Teams.Select(x => new TeamModel(x)).ToList();
    }

    public CompetitionEntity ToEntity()
    {
        return new CompetitionEntity
        {
            Id = this.Id,
            Name = this.Name.Value,
            Date = this.Date.Value,
            Jury = this.Jury.Select(x => new JudgeEntity
            { 
                Id = x.Id,
                Name = x.Name,
                EmailAddress = x.EmailAddress,
                PhoneNumber = x.PhoneNumber
            }).ToList(),
            Teams = this.Teams.Select(x => new TeamEntity
            {
                Id = x.Id,
                Name = x.Name,
                Points = x.Points,
            }).ToList()

        };
    }

    public void ToEntity(CompetitionEntity entity)
    {
        entity.Id = this.Id;
        entity.Name = this.Name.Value;
        entity.Date = this.Date.Value;
        entity.Jury = this.Jury.Select(x => new JudgeEntity
        {
            Id = x.Id,
            Name = x.Name,
            EmailAddress = x.EmailAddress,
            PhoneNumber = x.PhoneNumber
        }).ToList();
        entity.Teams = this.Teams.Select(x => new TeamEntity
        {
            Id = x.Id,
            Name = x.Name,
            Points = x.Points,
        }).ToList();
    }

    private void AddValidators()
    {
        this.Name.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "Name field is required"
        });

        this.Date.Validations.Add(new IsNotNullOrEmptyRule<DateTime>
        {
            ValidationMessage = "Name field is required"
        });



    }

}
