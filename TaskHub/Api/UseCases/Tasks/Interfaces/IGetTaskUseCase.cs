using Api.Controllers.Tasks.Response;

namespace Api.UseCases.Tasks.Interfaces;

/// <summary>
/// UseCase для получения задачи по идентификатору
/// </summary>
public interface IGetTaskUseCase
{
    /// <summary>
    /// Получить задачу по идентификатору
    /// </summary>
    Task<TaskResponse?> ExecuteAsync(Guid taskId, CancellationToken cancellationToken);
}