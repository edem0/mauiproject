using Solution.Database.Entities;

namespace Solution.Core.Models;

public partial class CompetitionModel
{
    public uint Id { get; set; }

    public string Name { get; set; }

    public DateTime Date { get; set; }

    public ValidatableObject<JuryModel> Jury { get; set; }

    public ValidatableObject<LocationModel> Location { get; set; }

    public ValidatableObject<ICollection<TeamModel>> Teams { get; set; }

    public CompetitionModel() 
    {
        this.Jury = new ValidatableObject<JuryModel>();
        this.Location = new ValidatableObject<LocationModel>();
        this.Teams = new ValidatableObject<ICollection<TeamModel>>();

        // AddValidators();
    }

    public CompetitionModel(CompetitionEntity entity): this()
    {
        this.Id = entity.Id;
        this.Name = entity.Name;
        this.Date = entity.Date;
        this.Jury.Value = new JuryModel(entity.Jury);
        this.Location.Value = new LocationModel(entity.Location);
        this.Teams.Value = null;
    }

}
