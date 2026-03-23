using Api.UseCases.Tasks.Interfaces;
using Dal.Repositories.Interfaces;

namespace Api.UseCases.Tasks;

/// <inheritdoc />
public sealed class DeleteTaskUseCase : IDeleteTaskUseCase
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTaskUseCase(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<bool> ExecuteAsync(Guid taskId, CancellationToken cancellationToken)
    {
        return await _taskRepository.DeleteTaskByIdAsync(taskId, cancellationToken);
    }
}