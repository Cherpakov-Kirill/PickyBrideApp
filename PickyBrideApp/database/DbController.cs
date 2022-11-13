using Microsoft.EntityFrameworkCore;
using PickyBride.contender;
using PickyBride.database.context;
using PickyBride.database.entity;

namespace PickyBride.database;

public class DbController : IDbController
{
    private readonly AbstractDbContext _dbContext;
    
    public DbController(AbstractDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbContext.Database.EnsureCreated();
        _dbContext.Dispose();
    }

    public List<AttemptStepEntity> GetAllByAttemptNumber(int attemptNumber)
    {
        using var context = _dbContext.GetDbContext();
        return context.AttemptSteps.Include("Contender").Where(entity => entity.AttemptNumber.Equals(attemptNumber)).ToList();
    }

    public void Add(Contender contender, int attemptNumber, int contenderPosition)
    {
        using var context = _dbContext.GetDbContext();
        var newRecord = new AttemptStepEntity()
        {
            Contender = new ContenderEntity()
                { Name = contender.Name, Patronymic = contender.Patronymic, Prettiness = contender.Prettiness },
            AttemptNumber = attemptNumber,
            ContenderPosition = contenderPosition
        };
        context.AttemptSteps.Add(newRecord);
        context.SaveChanges();
    }
}