using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace Api.Attributes;

/// <summary>
/// Атрибут для валидации обязательного поля в теле запроса
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class ValidateRequestFieldAttribute : Attribute, IActionFilter
{
    private readonly string _fieldName;
    private readonly string _errorMessage;

    public ValidateRequestFieldAttribute(string fieldName, string errorMessage)
    {
        _fieldName = fieldName;
        _errorMessage = errorMessage;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var argument in context.ActionArguments)
        {
            // Проверяем, что тело запроса не null
            if (argument.Value == null)
            {
                context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
                return;
            }

            // Ищем указанное поле через рефлексию
            var property = argument.Value.GetType().GetProperty(_fieldName,
                BindingFlags.Public | BindingFlags.Instance);

            if (property != null)
            {
                var value = property.GetValue(argument.Value) as string;

                // Проверяем, что поле не пустое
                if (string.IsNullOrWhiteSpace(value))
                {
                    context.Result = new BadRequestObjectResult(_errorMessage);
                    return;
                }
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}