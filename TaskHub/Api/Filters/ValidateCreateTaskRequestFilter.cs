using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

/// <summary>
/// Фильтр для валидации создания задачи
/// </summary>
public class ValidateCreateTaskRequestFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var request = context.ActionArguments.Values
            .FirstOrDefault(v => v?.GetType().Name == "CreateTaskRequest");

        if (request == null)
        {
            context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
            return;
        }

        var titleProperty = request.GetType().GetProperty("Title");
        var title = titleProperty?.GetValue(request) as string;

        if (string.IsNullOrWhiteSpace(title))
        {
            context.Result = new BadRequestObjectResult("Название задачи не задано");
            return;
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}