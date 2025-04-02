namespace Solution.Core.Interfaces;

public interface IJudgeService
{
    Task<ErrorOr<JuryModel>> CreateAsync(JuryModel model);

    Task<ErrorOr<Success>> UpdateAsync(JuryModel model);

    Task<ErrorOr<Success>> DeleteAsync(uint judgeId);

    Task<ErrorOr<JuryModel>> GetByIdAsync(uint judgeId);

    Task<ErrorOr<List<JuryModel>>> GetAllAsync();

    Task<ErrorOr<PaginationModel<JuryModel>>> GetPagedAsync(int page = 0);
}
