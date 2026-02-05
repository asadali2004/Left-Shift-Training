using System;
using System.Collections.Generic;

namespace _15_Task_Management_System
{
    // Represents a single task
    public class TaskItem
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; } // High / Medium / Low
        public string Status { get; set; } // ToDo / InProgress / Completed
        public DateTime DueDate { get; set; }
        public string AssignedTo { get; set; }
    }

    // Represents a project
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectManager { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
