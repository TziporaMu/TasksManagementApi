using TasksManagement.Interfaces;
using TasksManagement.Services;

namespace TaskItemsManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowApp", policy =>
                {
                    policy.WithOrigins("http://localhost:4200") 
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<ITaskService, TaskService>(); 

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
         

            app.UseCors("AllowApp"); 
            app.UseAuthorization();
            app.MapControllers();

            app.MapGet("/api/tasks/GetAllTasks", async (ITaskService taskService) =>
            {
                var tasks = await taskService.GetAllTasksAsync();
                return Results.Ok(tasks);
            });

            app.MapGet("/api/tasks/GetTaskById/{id}", async (int id, ITaskService taskService) =>
            {
                var task = await taskService.GetTaskByIdAsync(id);
                if (task == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(task);
            });

            app.MapPost("/api/tasks/AddTask", async (TasksManagement.Models.Task taskItem, ITaskService taskService) =>
            {
                await taskService.AddTaskAsync(taskItem);
                return Results.Created($"/api/tasks/{taskItem.Id}", taskItem);
            });

            app.MapPut("/api/tasks/UpdateTask/{id}", async (int id, TasksManagement.Models.Task updatedTaskItem, ITaskService taskService) =>
            {
                await taskService.UpdateTaskAsync(id, updatedTaskItem);
                return Results.NoContent();
            });

            app.MapDelete("/api/tasks/DeleteTask/{id}", async (int id, ITaskService taskService) =>
            {
                await taskService.DeleteTaskAsync(id);
                return Results.NoContent();
            });

            app.Run();
        }
    }
}