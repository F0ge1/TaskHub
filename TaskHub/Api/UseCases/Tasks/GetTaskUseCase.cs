using Api.Controllers.Tasks.Response;
using Api.UseCases.Tasks.Interfaces;
using Dal.Repositories.Interfaces;

namespace Api.UseCases.Tasks;

/// <inheritdoc />
public sealed class GetTaskUseCase : IGetTaskUseCase
{
    private readonly ITaskRepository _taskRepository;

    public GetTaskUseCase(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskResponse?> ExecuteAsync(Guid taskId, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetTaskByIdAsync(taskId, cancellationToken);

        if (task is null)
        {
            return null;
        }

        return new TaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            CreatedByUserId = task.CreatedByUserId,
            CreatedUtc = task.CreatedUtc
        };
    }
}