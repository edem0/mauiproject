using Microsoft.EntityFrameworkCore;
using Solution.DataBase;

namespace Solution.Services.Services;

public class LocationService(AppDbContext dbContext) : ILocationService
{
    private const int ROW_COUNT = 10;

    public async Task<ErrorOr<LocationModel>> CreateAsync(LocationModel model)
    {
        bool exists = await dbContext.Locations.AnyAsync(x => x.AreaName.ToLower() == model.AreaName.Value.ToLower().Trim() &&
                                                         x.HouseNumber == model.HouseNumber.Value);

        if (exists)
        {
            return Error.Conflict(description: "Location already exists!");
        }

        var location = model.ToEntity();

        await dbContext.Locations.AddAsync(location);
        await dbContext.SaveChangesAsync();

        return new LocationModel(location)
        {
            AreaName = model.AreaName,
            HouseNumber = model.HouseNumber
        };
    }

    public async Task<ErrorOr<Success>> UpdateAsync(LocationModel model)
    {
        try
        {
            var location = await dbContext.Locations.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (location is null)
            {
                return Error.NotFound(description: "Location not found.");
            }

            model.ToEntity(location);
            await dbContext.SaveChangesAsync();

            return Result.Success;
        }
        catch (Exception ex)
        {
            return Error.Conflict(description: ex.Message);
        }
    }

    public async Task<ErrorOr<Success>> DeleteAsync(uint locationId)
    {
        var result = await dbContext.Locations.AsNoTracking()
                                         .Where(x => x.Id == locationId)
                                         .ExecuteDeleteAsync();

        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<LocationModel>> GetByIdAsync(uint locationId)
    {
        var location = await dbContext.Locations.AsNoTracking()
                                 .FirstOrDefaultAsync(x => x.Id == locationId);

        if (location is null)
        {
            return Error.NotFound(description: "Location not found.");
        }

        return new LocationModel(location);
    }

    public async Task<ErrorOr<List<LocationModel>>> GetAllAsync() => await dbContext.Locations.AsNoTracking()
                                         .Select(x => new LocationModel(x))
                                         .ToListAsync();

    public async Task<ErrorOr<PaginationModel<LocationModel>>> GetPagedAsync(int page = 0)
    {

        page = page < 0 ? 0 : page - 1;

        var locations = await dbContext.Locations.AsNoTracking()
                                          .Skip(page * ROW_COUNT)
                                          .Take(ROW_COUNT)
                                          .Select(x => new LocationModel(x))
                                          .ToListAsync();
        var paginationModel = new PaginationModel<LocationModel>
        {
            Items = locations,
            Count = await dbContext.Locations.CountAsync()
        };

        return paginationModel;
    }
}
