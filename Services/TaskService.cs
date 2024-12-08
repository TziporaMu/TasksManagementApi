using TasksManagement.Interfaces;
using Task = TasksManagement.Models.Task;

namespace TasksManagement.Services
{
    public class TaskService : ITaskService
    {
        private readonly List<Models.Task> _tasks = new List<Models.Task>
    {
        new Models.Task { Id = 1, Title = "Task 1", Description = "Description for Task 1", IsCompleted = false },
        new Models.Task { Id = 2, Title = "Task 2", Description = "Description for Task 2", IsCompleted = true }
    };

        public async Task<IEnumerable<Task>> GetAllTasksAsync()
        {
            return _tasks;
        }

        public async Task<Task> GetTaskByIdAsync(int id)
        {
            return  _tasks.FirstOrDefault(t => t.Id == id);
        }

        public async System.Threading.Tasks.Task AddTaskAsync(Models.Task taskItem)
        {
            taskItem.Id = _tasks.Max(t => t.Id) + 1;
            _tasks.Add(taskItem);
        }

        public async System.Threading.Tasks.Task UpdateTaskAsync(int id, Models.Task taskItem)
        {
            var existingTask = _tasks.FirstOrDefault(t => t.Id == id);
            if (existingTask != null)
            {
                existingTask.Title = taskItem.Title;
                existingTask.Description = taskItem.Description;
                existingTask.IsCompleted = taskItem.IsCompleted;
            }
        }

        public async System.Threading.Tasks.Task DeleteTaskAsync(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                _tasks.Remove(task);
            }
        }
    }
}