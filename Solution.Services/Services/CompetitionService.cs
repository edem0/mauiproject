using Microsoft.EntityFrameworkCore;
using Solution.DataBase;

namespace Solution.Services.Services;

public class CompetitionService(AppDbContext dbContext) : ICompetitionService
{
    private const int ROW_COUNT = 10;

    public async Task<ErrorOr<CompetitionModel>> CreateAsync(CompetitionModel model)
    {
        bool exists = await dbContext.Competitions.AnyAsync(x => x.Name.ToLower() == model.Name.Value.ToLower().Trim() &&
                                                         x.Jury == model.Jury.Value &&
                                                         x.Teams == model.Teams);

        if (exists)
        {
            return Error.Conflict(description: "Competition already exists!");
        }

        var competition = model.ToEntity();

        await dbContext.Competitions.AddAsync(competition);
        await dbContext.SaveChangesAsync();

        return new CompetitionModel(competition);
    }

    public async Task<ErrorOr<Success>> UpdateAsync(CompetitionModel model)
    {
        try
        {
            var competition = await dbContext.Competitions.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (competition is null)
            {
                return Error.NotFound(description: "Competition not found.");
            }

            model.ToEntity(competition);
            await dbContext.SaveChangesAsync();

            return Result.Success;
        }
        catch (Exception ex)
        {
            return Error.Conflict(description: ex.Message);
        }
    }

    public async Task<ErrorOr<Success>> DeleteAsync(uint competitionId)
    {
        var result = await dbContext.Competitions.AsNoTracking()
                                         .Where(x => x.Id == competitionId)
                                         .ExecuteDeleteAsync();

        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<CompetitionModel>> GetByIdAsync(uint competitionId)
    {
        var competition = await dbContext.Competitions.AsNoTracking()
                                 .FirstOrDefaultAsync(x => x.Id == competitionId);

        if (competition is null)
        {
            return Error.NotFound(description: "Competition not found.");
        }

        return new CompetitionModel(competition);
    }

    public async Task<ErrorOr<List<CompetitionModel>>> GetAllAsync() => await dbContext.Competitions.AsNoTracking()
                                         .Select(x => new CompetitionModel(x))
                                         .ToListAsync();

    public async Task<ErrorOr<PaginationModel<CompetitionModel>>> GetPagedAsync(int page = 0)
    {

        page = page < 0 ? 0 : page - 1;

        var competitions = await dbContext.Competitions.AsNoTracking()
                                          .Skip(page * ROW_COUNT)
                                          .Take(ROW_COUNT)
                                          .Select(x => new CompetitionModel(x))
                                          .ToListAsync();
        var paginationModel = new PaginationModel<CompetitionModel>
        {
            Items = competitions,
            Count = await dbContext.Competitions.CountAsync()
        };

        return paginationModel;
    }
}
