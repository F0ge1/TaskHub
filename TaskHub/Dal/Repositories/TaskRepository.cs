using Dal.Context;
using Dal.Entities;
using Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories;

/// <inheritdoc />
public sealed class TaskRepository : ITaskRepository
{
    private readonly TaskDbContext _dbContext;

    public TaskRepository(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TaskEntity> CreateTaskAsync(string title, Guid createdByUserId, CancellationToken cancellationToken)
    {
        var task = new TaskEntity
        {
            Id = Guid.NewGuid(),
            Title = title,
            CreatedByUserId = createdByUserId,
            CreatedUtc = DateTimeOffset.UtcNow
        };

        _dbContext.Tasks.Add(task);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return task;
    }

    public async Task<IReadOnlyCollection<TaskEntity>> GetAllTasksAsync(CancellationToken cancellationToken)
    {
        var tasks = await _dbContext.Tasks
            .AsNoTracking()
            .OrderByDescending(t => t.CreatedUtc)
            .ToListAsync(cancellationToken);

        return tasks.AsReadOnly();
    }

    public async Task<TaskEntity?> GetTaskByIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        return await _dbContext.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken);
    }

    public async Task UpdateTaskTitleAsync(Guid taskId, string title, CancellationToken cancellationToken)
    {
        var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken);
        if (task is null)
        {
            return;
        }

        task.Title = title;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteTaskByIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken);
        if (task is null)
        {
            return false;
        }

        _dbContext.Tasks.Remove(task);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        var tasks = await _dbContext.Tasks.ToListAsync(cancellationToken);
        if (tasks.Count is 0)
        {
            return;
        }

        _dbContext.Tasks.RemoveRange(tasks);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}