using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10445734_PROG6221_POE_Part_3.Services
{
    public class TaskService
    {
        private List<Task> tasks = new List<Task>() ;

        public void AddTask(Task task)
        {
            tasks.Add(task);
        }

        public List<Task> GetTasks()
        {
            return tasks;
        }

        public void DeleteTask(Task task)
        {
            tasks.Remove(task);
        }

        public void MarkTaskAsCompleted(Task task)
        {
            var existingTask = tasks.FirstOrDefault(t => t.Title == task.Title && t.Description == task.Description);
            if (existingTask != null)
            {
                existingTask.IsCompleted = true;
            }
        }

        public List<Task> GetPendingTasks()
        {
            return tasks.Where(t => !t.IsCompleted).ToList();
        }

        public void ClearCompletedTasks()
        {
            tasks.RemoveAll(t => t.IsCompleted);
        }

        public void ClearAllTasks()
        {
            tasks.Clear();
        }
    }
}
