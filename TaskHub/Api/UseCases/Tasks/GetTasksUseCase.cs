using Api.Controllers.Tasks.Response;
using Api.UseCases.Tasks.Interfaces;
using Dal.Repositories.Interfaces;

namespace Api.UseCases.Tasks;

/// <inheritdoc />
public sealed class GetTasksUseCase : IGetTasksUseCase
{
    private readonly ITaskRepository _taskRepository;

    public GetTasksUseCase(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IReadOnlyCollection<TaskResponse>> ExecuteAsync(CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllTasksAsync(cancellationToken);

        return tasks.Select(t => new TaskResponse
        {
            Id = t.Id,
            Title = t.Title,
            CreatedByUserId = t.CreatedByUserId,
            CreatedUtc = t.CreatedUtc
        }).ToList().AsReadOnly();
    }
}