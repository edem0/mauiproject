using Solution.Database.Entities;

namespace Solution.Core.Models;

public class CityModel : IObjectValidator<uint>
{
    public uint Id { get; set; }

    public uint PostalCode { get; set; }

    public string CityName { get; set; }

    public CityModel() { }

    public CityModel(uint id, uint postalCode, string cityName)
    {
        Id = id;
        PostalCode = postalCode;
        CityName = cityName;
    }

    public CityModel(CityEntity entity)
    {
        if(entity == null)
        {
            return;
        }

        Id = entity.Id;
        PostalCode = entity.PostalCode;
    }

    public override bool Equals(object? obj)
    {
        return obj is CityModel model &&
            this.Id == model.Id &&
            this.PostalCode == model.PostalCode &&
            this.CityName == model.CityName;
    }
}
