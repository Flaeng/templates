using CSharp.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CSharp.Domain;

public class ApplicationDataContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public ApplicationDataContext(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        var user = modelBuilder.Entity<User>();
        // HandleEntity(user);
        user.HasMany(x => x.Lists).WithOne(x => x.Owner).HasForeignKey(x => x.OwnerId);

        var todoList = modelBuilder.Entity<TodoList>();
        // HandleEntity(todoList);
        todoList.HasMany(x => x.Items).WithOne(x => x.List).HasForeignKey(x => x.ListId);

        var todoItem = modelBuilder.Entity<TodoItem>();
        // HandleEntity(todoItem);
    }

    private void SetBaseEntityProperties()
    {
        var entities = ChangeTracker
            .Entries()
            .Where(x => x.State == EntityState.Added
                || x.State == EntityState.Modified)
            .Select(x => x.Entity is BaseEntity be ? be : null)
            .Where(x => x is not null)
            .OfType<BaseEntity>();

        foreach (var entity in entities)
        {
            if (entity.CreatedAt == default)
                entity.CreatedAt = DateTimeOffset.UtcNow;

            entity.LastModifiedAt = DateTimeOffset.UtcNow;
        }
    }
    
    public override int SaveChanges()
    {
        SetBaseEntityProperties();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SetBaseEntityProperties();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetBaseEntityProperties();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        SetBaseEntityProperties();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}