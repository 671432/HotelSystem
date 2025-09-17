using HotelLibrary.Interfaces;
using HotelLibrary.Models;
using HotelLibrary.Repos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLibrary.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepo _taskRepo;

        public TaskService(ITaskRepo taskRepo)
        {
            this._taskRepo = taskRepo ?? throw new ArgumentNullException(nameof(taskRepo));
        }

        public async System.Threading.Tasks.Task AddTaskAsync(Models.Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task), "Task object is empty (null).");
            }
            else if (task.RoomNr == 0)
            {
                throw new ArgumentNullException(nameof(task), $"There was no Room assigned to {task}.");
            }
            else  if (task.Type == null)
            {
                throw new ArgumentNullException(nameof(task), $"There was no Type assigned to {task}.");
            }
            else if (task.Status == null)
            {
                throw new ArgumentNullException(nameof(task), $"There was no Status assigned to {task}.");
            }

            await _taskRepo.AddAsync(task);
        }

        public System.Threading.Tasks.Task DeleteTaskAsync(int taskId)
        {
            throw new NotImplementedException();
        }
 
        public async Task<IEnumerable<Models.Task>> GetAllTasksAsync()
        {
            var allTasks = await _taskRepo.GetAllAsync();
            if (allTasks == null)
            {
                throw new ArgumentNullException( "No tasks where found.");
            }
            return allTasks;
        }

        public async Task<Models.Task> GetTaskByIdAsync(int taskId)
        {
            var taskById = await _taskRepo.GetByIdAsync(taskId);
            if (taskById?.TaskId != taskId)
            {
                throw new ArgumentNullException("No task was found.");
            }
            return taskById;
        }

        public async Task<IEnumerable<Models.Task>> GetTasksByStatusAsync(string status)
        {
            var taskByStatus = await _taskRepo.GetTaskByStatusAsync(status);
            if (taskByStatus == null)
            {
                throw new ArgumentException($"No task with '{status}' as status was found.");
            }
            return taskByStatus;
        }

        public async System.Threading.Tasks.Task UpdateTaskAsync(Models.Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task), "Task object is null.");
            }

            await _taskRepo.UpdateAsync(task);
        }
    }
}
