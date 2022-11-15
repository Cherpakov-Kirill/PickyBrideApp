using Microsoft.EntityFrameworkCore;
using PickyBride.contender;
using PickyBride.database.context;
using PickyBride.database.entity;

namespace PickyBride.database;

public class DbController : IDbController
{
    private readonly BaseDbContext _dbContext;
    
    public DbController(BaseDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbContext.Database.EnsureCreated();
        _dbContext.Dispose();
    }

    public async Task<List<AttemptStepEntity>> GetAllByAttemptNumber(int attemptNumber)
    {
        await using var context = _dbContext.GetDbContext();
        return await (context.AttemptSteps.Include("Contender").Where(entity => entity.AttemptNumber == attemptNumber).ToListAsync());
    }

    public async Task SaveAllContendersToDb(List<Contender> contenders, int attemptNumber)
    {
        await using var context = _dbContext.GetDbContext();
        var contenderPosition = 1;
        foreach (var newRecord in contenders.Select(contender => new AttemptStepEntity()
                 {
                     Contender = new ContenderEntity()
                         { Name = contender.Name, Patronymic = contender.Patronymic, Prettiness = contender.Prettiness },
                     AttemptNumber = attemptNumber,
                     ContenderPosition = contenderPosition
                 }))
        {
            context.AttemptSteps.Add(newRecord);
            contenderPosition++;
        }
        await context.SaveChangesAsync();
    }
}