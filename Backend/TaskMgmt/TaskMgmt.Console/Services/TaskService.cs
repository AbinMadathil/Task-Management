using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskMgmt.Console.Dtos.Group;

namespace TaskMgmt.Console.Services
{

    public class TaskService
    {
        private readonly ApiClient apiClient = new ApiClient(ApiConstants.ApiUrl);

        public void GetTasks(string userToken, int groupId, int projectId)
        {
            var response = apiClient.GetToken($"groups/{groupId}/projects/{projectId}/tasks", userToken);
            var content = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                System.Console.WriteLine("\t\t\tTask Details");
                var tasks = JsonSerializer.Deserialize<List<TaskDto>>(content);
                foreach (var task in tasks)
                {
                    System.Console.WriteLine($"Task ID: {task.taskId} Desc.: {task.description} Due Date: {task.dueDate}");
                }
                System.Console.WriteLine("\n\n");
            }
        }

        public void CreateTask(string userToken, int groupId, int projectId)
        {
            var newTask = new NewTaskDto();
            System.Console.WriteLine("Description: ");
            newTask.description = System.Console.ReadLine() ?? string.Empty;
            System.Console.WriteLine("Due Date: ");
            newTask.dueDate = DateTime.Parse(System.Console.ReadLine() ?? string.Empty);
            System.Console.WriteLine("Assignee Mail ID: ");
            newTask.assigneeMail = System.Console.ReadLine() ?? string.Empty;
            System.Console.WriteLine("Current Status ID: ");
            newTask.currentStatusId = Convert.ToInt32(System.Console.ReadLine() ?? "0");

            var response = apiClient.PostToken($"groups/{groupId}/projects/{projectId}/tasks", newTask, userToken);
            var content = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                System.Console.WriteLine("\t\t\tGroup Details");
                var task = JsonSerializer.Deserialize<TaskDto>(content);
                System.Console.WriteLine($"Task ID: {task.taskId} Desc.: {task.description} Due Date: {task.dueDate}\n");
                System.Console.WriteLine("\n\n");
            }
        }
    }

    public class NewTaskDto
    {
        public string description { get; set; }
        public DateTime dueDate { get; set; }
        public string assigneeMail { get; set; }
        public int currentStatusId { get; set; }
    }

    public class TaskDto
    {
        public int taskId { get; set; }
        public string description { get; set; }
        public string assignee { get; set; }
        public string creator { get; set; }
        public string dueDate { get; set; }
    }
}
