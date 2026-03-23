using Api.UseCases.Tasks.Interfaces;
using Dal.Repositories.Interfaces;

namespace Api.UseCases.Tasks;

/// <inheritdoc />
public sealed class DeleteTasksUseCase : IDeleteTasksUseCase
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTasksUseCase(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await _taskRepository.DeleteAllTasksAsync(cancellationToken);
    }
}