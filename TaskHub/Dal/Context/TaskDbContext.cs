using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal.Context;

/// <summary>
/// Контекст базы данных для задач
/// </summary>
public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Задачи
    /// </summary>
    public DbSet<TaskEntity> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Настройка связи с пользователем
        modelBuilder.Entity<TaskEntity>()
            .HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedByUserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Индексы для оптимизации
        modelBuilder.Entity<TaskEntity>()
            .HasIndex(t => t.CreatedByUserId);

        modelBuilder.Entity<TaskEntity>()
            .HasIndex(t => t.Id);
    }
}