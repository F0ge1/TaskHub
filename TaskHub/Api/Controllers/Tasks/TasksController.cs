using Api.Attributes;
using Api.Controllers.Tasks.Request;
using Api.Controllers.Tasks.Response;
using Api.Services;
using Logic.Users.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Tasks;

/// <summary>
/// Контроллер для работы с задачами
/// </summary>
[ApiController]
[Route("tasks")]
public sealed class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly IUserService _userService;

    public TasksController(ITaskService taskService, IUserService userService)
    {
        _taskService = taskService;
        _userService = userService;
    }

    /// <summary>
    /// Создать задачу
    /// </summary>
    /// <param name="request">Данные для создания задачи</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Созданная задача</returns>
    [HttpPost]
    [ValidateRequestField("Title", "Название задачи не задано")]
    public async Task<ActionResult<TaskResponse>> CreateTaskAsync(
        [FromBody] CreateTaskRequest? request,
        CancellationToken cancellationToken)
    {
        // TODO: В реальном приложении ID пользователя берется из контекста авторизации
        var currentUserId = Guid.Parse("3a8567f5-95b3-40e1-afa2-20daa9cbd3ea");

        // Проверяем существование пользователя
        var userExists = await _userService.GetUserByIdAsync(currentUserId, cancellationToken);
        if (userExists is null)
        {
            return BadRequest("Пользователь не найден");
        }

        var task = await _taskService.CreateTaskAsync(request!.Title!, currentUserId, cancellationToken);
        return Ok(task);
    }

    /// <summary>
    /// Получить все задачи
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список задач</returns>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<TaskResponse>>> GetAllTasksAsync(
        CancellationToken cancellationToken)
    {
        var tasks = await _taskService.GetAllTasksAsync(cancellationToken);
        return Ok(tasks);
    }

    /// <summary>
    /// Получить задачу по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Задача или 404</returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskResponse>> GetTaskByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var task = await _taskService.GetTaskByIdAsync(id, cancellationToken);

        if (task is null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    /// <summary>
    /// Изменить название задачи
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="request">Новое название задачи</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>204 NoContent</returns>
    [HttpPut("{id:guid}/title")]
    [ValidateRequestField("Title", "Название задачи не задано")]
    public async Task<IActionResult> SetTaskTitleAsync(
        [FromRoute] Guid id,
        [FromBody] SetTaskTitleRequest? request,
        CancellationToken cancellationToken)
    {
        await _taskService.SetTaskTitleAsync(id, request!.Title!, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Удалить задачу по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>204 NoContent или 404</returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTaskByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var deleted = await _taskService.DeleteTaskByIdAsync(id, cancellationToken);
        if (deleted == false)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Удалить все задачи
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>204 NoContent</returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        await _taskService.DeleteAllTasksAsync(cancellationToken);
        return NoContent();
    }
}