namespace Api.Controllers.Tasks.Request;

/// <summary>
/// Запрос на создание задачи
/// </summary>
public class CreateTaskRequest
{
    /// <summary>
    /// Название задачи
    /// </summary>
    public string? Title { get; set; }
}