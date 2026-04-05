namespace Api.Controllers.Tasks.Request;

/// <summary>
/// Запрос на изменение названия задачи
/// </summary>
public class SetTaskTitleRequest
{
    /// <summary>
    /// Новое название задачи
    /// </summary>
    public string? Title { get; set; }
}