namespace Api.UseCases.Tasks.Interfaces;

/// <summary>
/// UseCase для удаления всех задач
/// </summary>
public interface IDeleteTasksUseCase
{
    /// <summary>
    /// Удалить все задачи
    /// </summary>
    Task ExecuteAsync(CancellationToken cancellationToken);
}