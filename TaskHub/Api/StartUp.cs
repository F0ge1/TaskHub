using Api.Filters;
using Api.Middlewares;
using Api.Services;
using Api.UseCases.Tasks;
using Api.UseCases.Tasks.Interfaces;
using Api.UseCases.Users;
using Api.UseCases.Users.Interfaces;
using Dal;
using Dal.Context;
using Dal.Repositories;
using Dal.Repositories.Interfaces;
using Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Api;

/// <summary>
/// Конфигурация приложения
/// </summary>
public sealed class Startup
{
    /// <summary>
    /// Конфигурация приложения
    /// </summary>
    private IConfiguration Configuration { get; }

    /// <summary>
    /// Окружение приложения
    /// </summary>
    private IWebHostEnvironment Environment { get; }

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        Environment = env;
    }

    /// <summary>
    /// Регистрация сервисов
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.SuppressAsyncSuffixInActionNames = false;
        }).ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true; // Отключаем автоматическую валидацию
        });
        services.AddDal();
        services.AddLogic();

        services.AddScoped<IManageUserUseCase, ManageUserUseCase>();

        // Репозиторий
        services.AddScoped<ITaskRepository, TaskRepository>();

        // Usecase
        services.AddScoped<ICreateTaskUseCase, CreateTaskUseCase>();
        services.AddScoped<IGetTasksUseCase, GetTasksUseCase>();
        services.AddScoped<IGetTaskUseCase, GetTaskUseCase>();
        services.AddScoped<ISetTaskTitleUseCase, SetTaskTitleUseCase>();
        services.AddScoped<IDeleteTaskUseCase, DeleteTaskUseCase>();
        services.AddScoped<IDeleteTasksUseCase, DeleteTasksUseCase>();

        // Фильтры
        services.AddScoped<RequestLoggingFilter>();
        services.AddScoped<ValidateCreateTaskRequestFilter>();
        services.AddScoped<ValidateSetTaskTitleRequestFilter>();

        services.AddScoped<ITaskService, TaskService>();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        services.AddEndpointsApiExplorer();


        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "TaskHub Api",
                Version = "v1"
            });
        });
    }

    /// <summary>
    /// Конфигурация middleware пайплайна
    /// </summary>
    /// <param name="app">Построитель приложения</param>
    public void Configure(IApplicationBuilder app)
    {
        if (Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskHub API v1");
            });
        }


        //app.UseMiddleware<ResponseTimeMiddleware>();
        //app.UseMiddleware<StudentDataMiddleware>("Murzin Kirill Andreevich", "RI-240931");
        


        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}