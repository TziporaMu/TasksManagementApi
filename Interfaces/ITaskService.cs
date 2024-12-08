using Task = TasksManagement.Models.Task;

namespace TasksManagement.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<Task>> GetAllTasksAsync();
        Task<Task> GetTaskByIdAsync(int id);
        System.Threading.Tasks.Task AddTaskAsync(Task task);
        System.Threading.Tasks.Task UpdateTaskAsync(int id, Task task);
        System.Threading.Tasks.Task DeleteTaskAsync(int id);
    }
}
