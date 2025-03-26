using Solution.Database.Entities;

namespace Solution.Core.Models;

public class LocationModel : IObjectValidator<uint>
{
    public uint Id { get; set; }

    public string AreaName { get; set; }

    public uint HouseNumber { get; set; }

    public LocationModel() { }

    public LocationModel(uint id, string areaName, uint houseNumber)
    {
        Id = id;
        AreaName = areaName;
        HouseNumber = houseNumber;
    }

    public LocationModel(LocationEntity entity)
    {
        if(entity == null)
        {
            return;
        }

        Id = entity.Id;
        AreaName = entity.AreaName;
        HouseNumber = entity.HouseNumber;
    }

    public override bool Equals(object? obj)
    {
        return obj is LocationModel model &&
            this.Id == model.Id &&
            this.AreaName == model.AreaName &&
            this.HouseNumber == model.HouseNumber;
    }
}
