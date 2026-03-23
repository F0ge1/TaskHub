namespace Api.UseCases.Tasks.Interfaces;

/// <summary>
/// UseCase для изменения названия задачи
/// </summary>
public interface ISetTaskTitleUseCase
{
    /// <summary>
    /// Изменить название задачи
    /// </summary>
    Task ExecuteAsync(Guid taskId, string title, CancellationToken cancellationToken);
}