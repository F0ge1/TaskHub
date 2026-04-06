using Api.Attributes;
using Api.Controllers.Tasks.Request;
using Api.Controllers.Tasks.Response;
using Api.Filters;
using Api.Services;
using Logic.Users.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Tasks;

/// <summary>
/// Контроллер для работы с задачами
/// </summary>
[ApiController]
[Route("tasks")]
[ServiceFilter(typeof(RequestLoggingFilter))] // логирование для всех методов
[TypeFilter(typeof(StudentInfoHeadersFilter), Arguments = new object[] { "Murzin Kirill Andreevich", "RI-240931" })]
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
    [HttpPost]
    [ServiceFilter(typeof(ValidateCreateTaskRequestFilter))]
    public async Task<ActionResult<TaskResponse>> CreateTaskAsync(
        [FromBody] CreateTaskRequest request,
        CancellationToken cancellationToken)
    {
        var currentUserId = Guid.Parse("3a8567f5-95b3-40e1-afa2-20daa9cbd3ea");
        if (currentUserId == Guid.Empty)
        {
            return BadRequest("Идентификатор пользователя не задан");
        }

        var userExists = await _userService.GetUserByIdAsync(currentUserId, cancellationToken);
        if (userExists is null)
        {
            return BadRequest("Пользователь не найден");
        }

            var task = await _taskService.CreateTaskAsync(request.Title!, currentUserId, cancellationToken);

        return Ok(task);
    }

    /// <summary>
    /// Получить все задачи
    /// </summary>
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
    [HttpGet("{id}")]
    [FromRouteTaskId]
    public async Task<ActionResult<TaskResponse>> GetTaskByIdAsync(
        CancellationToken cancellationToken)
    {
        var id = (Guid)HttpContext.Items["TaskId"]!;

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
    [HttpPut("{id}/title")]
    [FromRouteTaskId]
    [ServiceFilter(typeof(ValidateSetTaskTitleRequestFilter))]
    public async Task<IActionResult> SetTaskTitleAsync(
        [FromBody] SetTaskTitleRequest request,
        CancellationToken cancellationToken)
    {
        var id = (Guid)HttpContext.Items["TaskId"]!;

        await _taskService.SetTaskTitleAsync(id, request.Title!, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Удалить задачу по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    [FromRouteTaskId]  // Атрибут на уровне метода
    public async Task<IActionResult> DeleteTaskByIdAsync(
        CancellationToken cancellationToken)
    {
        var id = (Guid)HttpContext.Items["TaskId"]!;

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
    [HttpDelete]
    public async Task<IActionResult> DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        await _taskService.DeleteAllTasksAsync(cancellationToken);
        return NoContent();
    }
}