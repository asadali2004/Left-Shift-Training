# Question 15: Task Management System

## Scenario
A project management tool needs to manage tasks, deadlines, and team assignments.

---

## Requirements

### Class: `TaskItem`
- `int TaskId`
- `string Title`
- `string Description`
- `string Priority` (High / Medium / Low)
- `string Status` (ToDo / InProgress / Completed)
- `DateTime DueDate`
- `string AssignedTo`

---

### Class: `Project`
- `int ProjectId`
- `string ProjectName`
- `string ProjectManager`
- `DateTime StartDate`
- `DateTime EndDate`
- `List<TaskItem> Tasks`

---

### Class: `TaskManager`

```csharp
public void CreateProject(string name, string manager, 
                         DateTime start, DateTime end);
public void AddTask(int projectId, string title, string description,
                   string priority, DateTime dueDate, string assignee);
public Dictionary<string, List<TaskItem>> GroupTasksByPriority();
public List<TaskItem> GetOverdueTasks();
public List<TaskItem> GetTasksByAssignee(string assigneeName);
````

---

## Use Cases

* Create projects
* Add tasks with priorities
* Group tasks by priority level
* Check overdue tasks
* View tasks assigned to team members

