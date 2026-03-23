using Api.Controllers.Tasks.Response;

namespace Api.UseCases.Tasks.Interfaces;

/// <summary>
/// UseCase для создания задачи
/// </summary>
public interface ICreateTaskUseCase
{
    /// <summary>
    /// Создать задачу
    /// </summary>
    Task<TaskResponse> ExecuteAsync(string title, Guid createdByUserId, CancellationToken cancellationToken);
}