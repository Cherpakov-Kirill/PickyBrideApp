using Microsoft.EntityFrameworkCore;
using PickyBride.database.entity;

namespace PickyBride.database.context;

public abstract class AbstractDbContext : DbContext
{
    public DbSet<AttemptStepEntity> AttemptSteps { get; set; }
    
    public abstract AbstractDbContext GetDbContext();
}