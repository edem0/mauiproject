namespace Solution.Core.Interfaces;

public interface ILocationService
{
    Task<ErrorOr<LocationModel>> CreateAsync(LocationModel model);

    Task<ErrorOr<Success>> UpdateAsync(LocationModel model);

    Task<ErrorOr<Success>> DeleteAsync(uint locationId);

    Task<ErrorOr<LocationModel>> GetByIdAsync(uint locationId);

    Task<ErrorOr<List<LocationModel>>> GetAllAsync();

    Task<ErrorOr<PaginationModel<LocationModel>>> GetPagedAsync(int page = 0);
}
