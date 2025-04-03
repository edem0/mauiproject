namespace Solution.Core.Interfaces;

public interface ICompetitionService
{
    Task<ErrorOr<CompetitionModel>> CreateAsync(CompetitionModel model);

    Task<ErrorOr<Success>> UpdateAsync(CompetitionModel model);

    Task<ErrorOr<Success>> DeleteAsync(uint competitionId);

    Task<ErrorOr<CompetitionModel>> GetByIdAsync(uint competitionId);

    Task<ErrorOr<List<CompetitionModel>>> GetAllAsync();

    Task<ErrorOr<PaginationModel<CompetitionModel>>> GetPagedAsync(int page = 0);
}
