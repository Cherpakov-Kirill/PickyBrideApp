using Microsoft.EntityFrameworkCore;
using HallWebApi.model.database.entity;

namespace HallWebApi.model.database.context;

public abstract class BaseDbContext : DbContext
{
    public DbSet<AttemptStepEntity> AttemptSteps { get; set; }
    
    public abstract BaseDbContext GetDbContext();
}