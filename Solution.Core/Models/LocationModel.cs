using Solution.Database.Entities;

namespace Solution.Core.Models;

public class LocationModel : IObjectValidator<uint>
{
    public uint Id { get; set; }

    public ValidatableObject<string> AreaName { get; set; }

    public ValidatableObject<uint?> HouseNumber { get; set; }

    public ValidatableObject<CityModel> City { get; protected set; }

    public LocationModel() 
    {
        this.AreaName = new ValidatableObject<string>();
        this.HouseNumber = new ValidatableObject<uint?>();

        AddValidators();
    }

    public LocationModel(LocationEntity entity): this()
    {
        this.Id = entity.Id;
        this.AreaName.Value = entity.AreaName;
        this.HouseNumber.Value = entity.HouseNumber;

    }

    public LocationEntity ToEntity()
    {
        return new LocationEntity
        {
            AreaName = AreaName.Value,
            HouseNumber = HouseNumber.Value ?? 0,
            CityId = City.Value.Id
        };
    }

    public void ToEntity(LocationEntity entity)
    {
        entity.AreaName = AreaName.Value;
        entity.HouseNumber = HouseNumber.Value ?? 0;
        entity.CityId = City.Value.Id;
    }

    private void AddValidators()
    {
        this.AreaName.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "This field can't be empty!"
        });

        this.HouseNumber.Validations.Add(new NullableIntegerRule<uint?>
        {
            ValidationMessage = "This field can't be empty!"
        });
    }
}
