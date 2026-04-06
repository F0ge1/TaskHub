using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

/// <summary>
/// Фильтр для добавления заголовков с информацией о студенте
/// </summary>
public class StudentInfoHeadersFilter : IResultFilter
{
    private const string NameHeader = "X-Student-Name";
    private const string GroupHeader = "X-Student-Group";

    private readonly string _studentName;
    private readonly string _studentGroup;

    public StudentInfoHeadersFilter(string studentName, string studentGroup)
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

    public void OnResultExecuted(ResultExecutedContext context) { }
}