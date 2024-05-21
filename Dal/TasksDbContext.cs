using Bme.Swlab1.Rest.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bme.Swlab1.Rest.Dal;

public class TasksDbContext : DbContext
{
    // DO NOT CHANGE THE CONSTRUCTOR - NE VALTOZTASD MEG A KONSTRUKTORT
    public TasksDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<DbStatus> Statuses { get; set; }
    public DbSet<DbTask> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //status
        modelBuilder.Entity<DbStatus>()
        .ToTable("statuses");

        modelBuilder.Entity<DbStatus>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<DbStatus>()
            .Property(s => s.Name)
            .HasMaxLength(50)
            .IsRequired(required: true)
            .IsUnicode(unicode: true);

        modelBuilder.Entity<DbStatus>()
            .HasData(new[]
            {
                new DbStatus() { Id = 1, Name = "new" },
                new DbStatus() { Id = 2, Name = "in progress" },
            });

        //task
        modelBuilder.Entity<DbTask>()
            .ToTable("tasks");

        modelBuilder.Entity<DbTask>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<DbTask>()
            .HasOne(d => d.Status)
            .WithMany(s => s.Tasks)
            .HasForeignKey(d => d.StatusId);

        modelBuilder.Entity<DbTask>()
            .Property(t => t.Title)
            .HasMaxLength(50)
            .IsRequired(required: true)
            .IsUnicode(unicode: true);

       modelBuilder.Entity<DbTask>()
            .HasData(new[]
            {
                new DbTask() { Id = 1, Title = "task1", IsDone = false, StatusId = 1},
                new DbTask() { Id = 2, Title = "task2", IsDone = false, StatusId = 2 },
            });

        base.OnModelCreating(modelBuilder);
    }
}