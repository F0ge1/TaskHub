using Api.Controllers.Tasks.Response;
using Api.Services;
using Api.UseCases.Tasks.Interfaces;

namespace Api.Services;

/// <inheritdoc />
public sealed class TaskService : ITaskService
{
    private readonly ICreateTaskUseCase _createTaskUseCase;
    private readonly IGetTasksUseCase _getTasksUseCase;
    private readonly IGetTaskUseCase _getTaskUseCase;
    private readonly ISetTaskTitleUseCase _setTaskTitleUseCase;
    private readonly IDeleteTaskUseCase _deleteTaskUseCase;
    private readonly IDeleteTasksUseCase _deleteTasksUseCase;

    public TaskService(
        ICreateTaskUseCase createTaskUseCase,
        IGetTasksUseCase getTasksUseCase,
        IGetTaskUseCase getTaskUseCase,
        ISetTaskTitleUseCase setTaskTitleUseCase,
        IDeleteTaskUseCase deleteTaskUseCase,
        IDeleteTasksUseCase deleteTasksUseCase)
    {
        _createTaskUseCase = createTaskUseCase;
        _getTasksUseCase = getTasksUseCase;
        _getTaskUseCase = getTaskUseCase;
        _setTaskTitleUseCase = setTaskTitleUseCase;
        _deleteTaskUseCase = deleteTaskUseCase;
        _deleteTasksUseCase = deleteTasksUseCase;
    }

    public async Task<TaskResponse> CreateTaskAsync(string title, Guid createdByUserId, CancellationToken cancellationToken)
    {
        return await _createTaskUseCase.ExecuteAsync(title, createdByUserId, cancellationToken);
    }

    public async Task<IReadOnlyCollection<TaskResponse>> GetAllTasksAsync(CancellationToken cancellationToken)
    {
        return await _getTasksUseCase.ExecuteAsync(cancellationToken);
    }

    public async Task<TaskResponse?> GetTaskByIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        return await _getTaskUseCase.ExecuteAsync(taskId, cancellationToken);
    }

    public async Task SetTaskTitleAsync(Guid taskId, string title, CancellationToken cancellationToken)
    {
        await _setTaskTitleUseCase.ExecuteAsync(taskId, title, cancellationToken);
    }

    public async Task<bool> DeleteTaskByIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        return await _deleteTaskUseCase.ExecuteAsync(taskId, cancellationToken);
    }

    public async Task DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        await _deleteTasksUseCase.ExecuteAsync(cancellationToken);
    }
}