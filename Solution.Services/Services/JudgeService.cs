using Microsoft.EntityFrameworkCore;
using Solution.DataBase;
using System.ComponentModel;

namespace Solution.Services.Services;

public class JudgeService(AppDbContext dbContext) : IJudgeService
{
    private const int ROW_COUNT = 10;

    public async Task<ErrorOr<JuryModel>> CreateAsync(JuryModel model)
    {
        bool exists = await dbContext.Jury.AnyAsync(x => x.Name.ToLower() == model.Name.Value.ToLower().Trim() &&
                                                         x.EmailAddress == model.EmailAddress.Value &&
                                                         x.PhoneNumber == model.PhoneNumber.Value);

        if (exists)
        {
            return Error.Conflict(description: "Judge already exists!");
        }

        var judge = model.ToEntity();

        await dbContext.Jury.AddAsync(judge);
        await dbContext.SaveChangesAsync();

        return new JuryModel(judge);
    }

    public async Task<ErrorOr<Success>> UpdateAsync(JuryModel model)
    {
        try
        {
            var judge = await dbContext.Jury.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (judge is null)
            {
                return Error.NotFound(description: "Judge not found.");
            }

            model.ToEntity(judge);
            await dbContext.SaveChangesAsync();

            return Result.Success;
        }
        catch (Exception ex)
        {
            return Error.Conflict(description: ex.Message);
        }
    }

    public async Task<ErrorOr<Success>> DeleteAsync(uint judgeId)
    {
        var result = await dbContext.Jury.AsNoTracking()
                                         .Where(x => x.Id == judgeId)
                                         .ExecuteDeleteAsync();

        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<JuryModel>> GetByIdAsync(uint judgeId)
    {
        var judge = await dbContext.Jury.AsNoTracking()
                                 .FirstOrDefaultAsync(x => x.Id == judgeId);

        if(judge is null)
        {
            return Error.NotFound(description: "Judge not found.");
        }

        return new JuryModel(judge);
    }

    public async Task<ErrorOr<List<JuryModel>>> GetAllAsync() => await dbContext.Jury.AsNoTracking()
                                         .Select(x => new JuryModel(x))
                                         .ToListAsync();

    public async Task<ErrorOr<PaginationModel<JuryModel>>> GetPagedAsync(int page = 0)
    {

        page = page < 0 ? 0 : page - 1;

        var jury = await dbContext.Jury.AsNoTracking()
                                          .Skip(page * ROW_COUNT)
                                          .Take(ROW_COUNT)
                                          .Select(x => new JuryModel(x))
                                          .ToListAsync();
        var paginationModel = new PaginationModel<JuryModel>
        {
            Items = jury,
            Count = await dbContext.Jury.CountAsync()
        };

        return paginationModel;
    }
}
