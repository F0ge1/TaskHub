using Api.Controllers.Tasks.Response;
using Api.UseCases.Tasks.Interfaces;
using Dal.Repositories.Interfaces;

namespace Api.UseCases.Tasks;

/// <inheritdoc />
public sealed class CreateTaskUseCase : ICreateTaskUseCase
{
    private readonly ITaskRepository _taskRepository;

    public CreateTaskUseCase(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskResponse> ExecuteAsync(string title, Guid createdByUserId, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.CreateTaskAsync(title, createdByUserId, cancellationToken);

        return new TaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            CreatedByUserId = task.CreatedByUserId,
            CreatedUtc = task.CreatedUtc
        };
    }
}