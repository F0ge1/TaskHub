namespace Api.UseCases.Tasks.Interfaces;

/// <summary>
/// UseCase для удаления задачи по идентификатору
/// </summary>
public interface IDeleteTaskUseCase
{
    /// <summary>
    /// Удалить задачу по идентификатору
    /// </summary>
    Task<bool> ExecuteAsync(Guid taskId, CancellationToken cancellationToken);
}