using Microsoft.EntityFrameworkCore;
using Solution.DataBase;

namespace Solution.Services;

public class CompetitionService(AppDbContext dbContext) : ICompetitionService
{
    public Task<ErrorOr<CompetitionModel>> CreateAsync(CompetitionModel model)
    {
        throw new NotImplementedException();
    }

    // COMPETITION MODEL TOENTITY FUGGVENYE HIANYZIK

    //public async Task<ErrorOr<CompetitionModel>> CreateAsync(CompetitionModel model)
    //{
    //    bool exists = await dbContext.Competitions.AnyAsync(x => x.Id == model.Id);

    //    if (exists)
    //    {
    //        return Error.Conflict(description: "Motorcycle already exists!");
    //    }

    //    var competition = model.ToEntity();
    //    competition.PublicId = Guid.NewGuid().ToString();

    //    await dbContext.Competitions.AddAsync(competition);
    //    await dbContext.SaveChangesAsync();

    //    return new CompetitionModel(competition);
    //}

    public Task<ErrorOr<Success>> DeleteAsync(string competitionId)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<List<CompetitionModel>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<CompetitionModel>> GetByIdAsync(string competitionId)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<PaginationModel<CompetitionModel>>> GetPagedAsync(int page = 0)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<Success>> UpdateAsync(CompetitionModel model)
    {
        throw new NotImplementedException();
    }
}
