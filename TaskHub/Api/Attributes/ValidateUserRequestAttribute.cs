using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace Api.Attributes;

/// <summary>
/// Атрибут для валидации запросов пользователя
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class ValidateUserRequestAttribute : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var argument in context.ActionArguments)
        {
            if (argument.Value == null)
            {
                context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
                return;
            }

            var nameProperty = argument.Value.GetType().GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);

            if (nameProperty != null)
            {
                var nameValue = nameProperty.GetValue(argument.Value) as string;

                if (string.IsNullOrWhiteSpace(nameValue))
                {
                    context.Result = new BadRequestObjectResult("Имя пользователя не задано");
                    return;
                }
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Ничего не делаем
    }
}