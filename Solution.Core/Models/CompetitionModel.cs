using Solution.Database.Entities;

namespace Solution.Core.Models;

public partial class CompetitionModel
{
    public uint Id { get; set; }

    public ValidatableObject<string> Name { get; protected set; }

    public ValidatableObject<DateTime> Date { get; protected set; }

    public ValidatableObject<List<JuryModel>> Jury { get; set; }

    public  List<TeamModel> Teams { get; set; }

    public ValidatableObject<LocationModel> Location { get; set; }

    public CompetitionModel() 
    {
        this.Name = new ValidatableObject<string>();
        this.Date = new ValidatableObject<DateTime>();
        this.Location = new ValidatableObject<LocationModel>();
        this.Jury = new ValidatableObject<List<JuryModel>>();

        AddValidators();
    }

    public CompetitionModel(CompetitionEntity entity): this()
    {
        this.Id = entity.Id;
        this.Name.Value = entity.Name;
        this.Date.Value = entity.Date;
        this.Jury.Value = entity.Jury.Select(x => new JuryModel(x)).ToList();
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
            Jury = this.Jury.Value.Select(x => new JudgeEntity
            { 
                Id = x.Id,
                Name = x.Name.Value,
                EmailAddress = x.EmailAddress.Value,
                PhoneNumber = x.PhoneNumber.Value
            }).ToList(),
            Teams = this.Teams.Select(x => new TeamEntity
            {
                Id = x.Id,
                Name = x.Name.Value,
                Points = x.Points.Value,
            }).ToList(),
            Location = new LocationEntity{
                Id = Location.Value.Id,
                AreaName = Location.Value.AreaName.Value,
                HouseNumber = Location.Value.HouseNumber.Value,
                CityId = Location.Value.City.Value.Id
            }

        };
    }

    public void ToEntity(CompetitionEntity entity)
    {
        entity.Id = this.Id;
        entity.Name = this.Name.Value;
        entity.Date = this.Date.Value;
        entity.Jury = this.Jury.Value.Select(x => new JudgeEntity
        {
            Id = x.Id,
            Name = x.Name.Value,
            EmailAddress = x.EmailAddress.Value,
            PhoneNumber = x.PhoneNumber.Value
        }).ToList();
        entity.Teams = this.Teams.Select(x => new TeamEntity
        {
            Id = x.Id,
            Name = x.Name.Value,
            Points = x.Points.Value,
        }).ToList();
    }

    private void AddValidators()
    {
        this.Name.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "Name field is required"
        });

        this.Date.Validations.AddRange(
        [
            new IsNotNullOrEmptyRule<DateTime> 
            {
                ValidationMessage = "Date field is required!"
            },
            new DateTimeValidationRule<DateTime>(Date.Value) 
            {
                ValidationMessage = "Given date can't be greater than the current date!"
            }
        ]);

        this.Jury.Validations.Add(new NotMoreOrLessThanXRule<List<JuryModel>>(3, "jury"){});
    }

}
