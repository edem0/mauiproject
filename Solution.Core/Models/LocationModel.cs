using Solution.Database.Entities;

namespace Solution.Core.Models;

public class LocationModel : IObjectValidator<uint>
{
    public uint Id { get; set; }

    public uint PostalCode { get; set; }

    public string LocationName { get; set; }

    public LocationModel() { }

    public LocationModel(uint id, uint postalCode, string locationName)
    {
        Id = id;
        PostalCode = postalCode;
        LocationName = locationName;
    }

    public LocationModel(LocationEntity entity)
    {
        if(entity == null)
        {
            return;
        }

        Id = entity.Id;
        PostalCode = entity.PostalCode;
        LocationName = entity.LocationName;
    }
}
