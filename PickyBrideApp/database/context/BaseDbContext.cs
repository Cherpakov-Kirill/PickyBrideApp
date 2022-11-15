using Microsoft.EntityFrameworkCore;
using PickyBride.database.entity;

namespace PickyBride.database.context;

public abstract class BaseDbContext : DbContext
{
    public DbSet<AttemptStepEntity> AttemptSteps { get; set; }
    
    public abstract BaseDbContext GetDbContext();
}