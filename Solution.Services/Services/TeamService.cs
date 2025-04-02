﻿using Microsoft.EntityFrameworkCore;
using Solution.DataBase;
using System.Linq.Expressions;

namespace Solution.Services.Services;

public class TeamService(AppDbContext dbContext) : ITeamService
{
    private const int ROW_COUNT = 10;

    public async Task<ErrorOr<TeamModel>> CreateAsync(TeamModel model)
    {
        bool exists = await dbContext.Teams.AnyAsync(x => x.Name.ToLower() == model.Name.Value.ToLower().Trim() &&
                                                         x.Points == model.Points.Value &&
                                                         x.Members == model.Members.Value);

        if (exists)
        {
            return Error.Conflict(description: "Team already exists!");
        }

        var team = model.ToEntity();

        await dbContext.Teams.AddAsync(team);
        await dbContext.SaveChangesAsync();

        return new TeamModel(team);
    }

    public async Task<ErrorOr<Success>> UpdateAsync(TeamModel model)
    {
        try
        {
            var team = await dbContext.Teams.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (team is null)
            {
                return Error.NotFound(description: "Team not found.");
            }

            model.ToEntity(team);
            await dbContext.SaveChangesAsync();

            return Result.Success;
        }
        catch(Exception ex)
        {
            return Error.Conflict(description: ex.Message);
        }
    }

    public async Task<ErrorOr<Success>> DeleteAsync(uint teamId)
    {
        var result = await dbContext.Teams.AsNoTracking()
                                         .Where(x => x.Id == teamId)
                                         .ExecuteDeleteAsync();

        return result > 0 ? Result.Success : Error.NotFound();
    }

    public async Task<ErrorOr<TeamModel>> GetByIdAsync(uint teamId)
    {
        var team = await dbContext.Teams.AsNoTracking()
                                 .FirstOrDefaultAsync(x => x.Id == teamId);

        if (team is null)
        {
            return Error.NotFound(description: "Team not found.");
        }

        return new TeamModel(team);
    }

    public async Task<ErrorOr<List<TeamModel>>> GetAllAsync() => await dbContext.Teams.AsNoTracking()
                                         .Select(x => new TeamModel(x))
                                         .ToListAsync();

    public async Task<ErrorOr<PaginationModel<TeamModel>>> GetPagedAsync(int page = 0)
    {

        page = page < 0 ? 0 : page - 1;

        var teams = await dbContext.Teams.AsNoTracking()
                                          .Skip(page * ROW_COUNT)
                                          .Take(ROW_COUNT)
                                          .Select(x => new TeamModel(x))
                                          .ToListAsync();
        var paginationModel = new PaginationModel<TeamModel>
        {
            Items = teams,
            Count = await dbContext.Teams.CountAsync()
        };

        return paginationModel;
    }
}
