using Api.Controllers.Tasks.Response;

namespace Api.Services;

/// <summary>
/// Сервис для работы с задачами
/// </summary>
public interface ITaskService
{
    /// <summary>
    /// Создать задачу
    /// </summary>
    Task<TaskResponse> CreateTaskAsync(string title, Guid createdByUserId, CancellationToken cancellationToken);

    /// <summary>
    /// Получить все задачи
    /// </summary>
    Task<IReadOnlyCollection<TaskResponse>> GetAllTasksAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Получить задачу по идентификатору
    /// </summary>
    Task<TaskResponse?> GetTaskByIdAsync(Guid taskId, CancellationToken cancellationToken);

    /// <summary>
    /// Изменить название задачи
    /// </summary>
    Task SetTaskTitleAsync(Guid taskId, string title, CancellationToken cancellationToken);

    /// <summary>
    /// Удалить задачу по идентификатору
    /// </summary>
    Task<bool> DeleteTaskByIdAsync(Guid taskId, CancellationToken cancellationToken);

    /// <summary>
    /// Удалить все задачи
    /// </summary>
    Task DeleteAllTasksAsync(CancellationToken cancellationToken);
}