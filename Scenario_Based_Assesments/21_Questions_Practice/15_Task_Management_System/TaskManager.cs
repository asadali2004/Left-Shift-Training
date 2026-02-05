using System;
using System.Collections.Generic;
using System.Linq;

namespace _15_Task_Management_System
{
    public class TaskManager
    {
        public Dictionary<int, Project> Projects = new Dictionary<int, Project>();

        private int nextProjectId = 1;
        private int nextTaskId = 1;

        // Create project
        public void CreateProject(string name, string manager,
                                  DateTime start, DateTime end)
        {
            int id = nextProjectId++;

            Projects.Add(id, new Project
            {
                ProjectId = id,
                ProjectName = name,
                ProjectManager = manager,
                StartDate = start,
                EndDate = end
            });
        }

        // Add task to project
        public void AddTask(int projectId, string title, string description,
                            string priority, DateTime dueDate, string assignee)
        {
            if (!Projects.ContainsKey(projectId))
                return;

            TaskItem task = new TaskItem
            {
                TaskId = nextTaskId++,
                Title = title,
                Description = description,
                Priority = priority,
                Status = "ToDo", // default
                DueDate = dueDate,
                AssignedTo = assignee
            };

            Projects[projectId].Tasks.Add(task);
        }

        // Group tasks by priority
        public Dictionary<string, List<TaskItem>> GroupTasksByPriority()
        {
            return Projects.Values
                .SelectMany(p => p.Tasks)
                .GroupBy(t => t.Priority)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        // Get overdue tasks
        public List<TaskItem> GetOverdueTasks()
        {
            return Projects.Values
                .SelectMany(p => p.Tasks)
                .Where(t => t.DueDate < DateTime.Now && t.Status != "Completed")
                .ToList();
        }

        // Tasks by assignee
        public List<TaskItem> GetTasksByAssignee(string assigneeName)
        {
            return Projects.Values
                .SelectMany(p => p.Tasks)
                .Where(t => t.AssignedTo.Equals(assigneeName, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
