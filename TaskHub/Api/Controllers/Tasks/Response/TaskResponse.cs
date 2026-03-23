namespace Api.Controllers.Tasks.Response;

/// <summary>
/// Ответ с данными задачи
/// </summary>
public class TaskResponse
{
    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название задачи
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Идентификатор пользователя, создавшего задачу
    /// </summary>
    public Guid CreatedByUserId { get; set; }

    /// <summary>
    /// Дата и время создания задачи (UTC)
    /// </summary>
    public DateTimeOffset CreatedUtc { get; set; }
}