using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Attributes;

/// <summary>
/// Атрибут для валидации и привязки id задачи из маршрута
/// </summary>
public class FromRouteTaskIdAttribute : Attribute, IAsyncResourceFilter, IBindingSourceMetadata
{
    public BindingSource BindingSource => BindingSource.Path;

    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        var routeData = context.RouteData;

        // Пробуем получить id из route
        if (!routeData.Values.TryGetValue("id", out var idObject))
        {
            context.Result = new BadRequestObjectResult("Идентификатор задачи не задан");
            return;
        }

        var idValue = idObject?.ToString();

        if (string.IsNullOrWhiteSpace(idValue))
        {
            context.Result = new BadRequestObjectResult("Идентификатор задачи не задан");
            return;
        }

        if (!Guid.TryParse(idValue, out var guidValue))
        {
            context.Result = new BadRequestObjectResult("Идентификатор задачи имеет некорректный формат");
            return;
        }

        // Сохраняем распарсенный Guid в HttpContext.Items
        context.HttpContext.Items["TaskId"] = guidValue;

        await next();
    }
}