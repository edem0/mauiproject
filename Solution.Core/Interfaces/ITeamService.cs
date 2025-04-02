namespace Solution.Core.Interfaces;

public interface ITeamService
{
    Task<ErrorOr<TeamModel>> CreateAsync(TeamModel model);

    Task<ErrorOr<Success>> UpdateAsync(TeamModel model);

    Task<ErrorOr<Success>> DeleteAsync(uint teamId);

    Task<ErrorOr<TeamModel>> GetByIdAsync(uint teamId);

    Task<ErrorOr<List<TeamModel>>> GetAllAsync();

    Task<ErrorOr<PaginationModel<TeamModel>>> GetPagedAsync(int page = 0);
}
