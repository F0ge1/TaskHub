using Api.UseCases.Tasks.Interfaces;
using Dal.Repositories.Interfaces;

namespace Api.UseCases.Tasks;

/// <inheritdoc />
public sealed class SetTaskTitleUseCase : ISetTaskTitleUseCase
{
    private readonly ITaskRepository _taskRepository;

    public SetTaskTitleUseCase(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task ExecuteAsync(Guid taskId, string title, CancellationToken cancellationToken)
    {
        await _taskRepository.UpdateTaskTitleAsync(taskId, title, cancellationToken);
    }
}