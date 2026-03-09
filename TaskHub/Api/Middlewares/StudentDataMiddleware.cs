namespace Api.Middlewares
{
    /// <summary>
    /// Класс для добавления данных о студенте
    /// </summary>
    public class StudentDataMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _studentName;
        private readonly string _studentGroup;

        public StudentDataMiddleware(RequestDelegate next, string studentName, string studentGroup)
        {
            _next = next;
            _studentName = studentName;
            _studentGroup = studentGroup;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Проверяем, что ответ еще не начался
            if (!context.Response.HasStarted)
            {
                // Добавляем заголовки с данными студента
                context.Response.Headers.Add("X-Student-Name", _studentName);
                context.Response.Headers.Add("X-Student-Group", _studentGroup);
            }

            // Передаем управление следующему middleware
            await _next(context);
        }
    }
}
