using Api.Controllers.Tasks.Response;

namespace Api.UseCases.Tasks.Interfaces;

/// <summary>
/// UseCase для получения всех задач
/// </summary>
public interface IGetTasksUseCase
{
    /// <summary>
    /// Получить все задачи
    /// </summary>
    Task<IReadOnlyCollection<TaskResponse>> ExecuteAsync(CancellationToken cancellationToken);
}