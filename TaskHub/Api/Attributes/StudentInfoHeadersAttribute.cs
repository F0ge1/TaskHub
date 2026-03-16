using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes;

/// <summary>
/// Атрибут для добавления заголовков с информацией о студенте
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class StudentInfoHeadersAttribute : Attribute, IResultFilter
{
    private const string NameHeader = "X-Student-Name";
    private const string GroupHeader = "X-Student-Group";

    private readonly string _studentName;
    private readonly string _studentGroup;

    public StudentInfoHeadersAttribute(string studentName, string studentGroup)
    {
        _studentName = studentName;
        _studentGroup = studentGroup;
    }

    public void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.OnStarting(() =>
        {
            context.HttpContext.Response.Headers.TryAdd(NameHeader, _studentName);
            context.HttpContext.Response.Headers.TryAdd(GroupHeader, _studentGroup);

            return Task.CompletedTask;
        });
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
        // Ничего не делаем
    }
}