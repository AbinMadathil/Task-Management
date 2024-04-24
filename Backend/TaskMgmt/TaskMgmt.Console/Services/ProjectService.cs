using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskMgmt.Console.Dtos;
using TaskMgmt.Console.Dtos.Group;

namespace TaskMgmt.Console.Services
{
    public class ProjectService
    {
        private int groupID;
        private readonly ApiClient apiClient;
        private readonly string userToken;
        public ProjectService(string userToken)
        {
            apiClient = new ApiClient(ApiConstants.ApiUrl);
            userToken = userToken;
        }
        public int GetProjects(string token )
        {
            System.Console.Write("Enter Group ID: ");
            groupID = System.Convert.ToInt32(System.Console.ReadLine());
            var response = apiClient.GetToken($"groups/{groupID}/projects", token);
            var content = response.Content.ReadAsStringAsync().Result;
            var projects = JsonSerializer.Deserialize<List<ProjectResponseDto>>(content);
            System.Console.WriteLine("\t\t\tPROJECTS");
            foreach (var project in projects)
            {
                System.Console.WriteLine($"\t\t\tProjectID: {project.projectId} ProjectName: {project.projectName}");
            }
            System.Console.WriteLine("\n\n");
            return groupID;
        }



        public string ProjectMenu( string token , int groupID)
        {
            
                System.Console.WriteLine("1.Invite Others\n2.Create Project\n3.Select Project");
                System.Console.WriteLine("Enter your choice");
                string choice = System.Console.ReadLine();
                //switch (choice)
                //{
                //    case "1":
                //        System.Console.Write("Enter email of the user you want to invite: ");
                //        string email = System.Console.ReadLine();
                //        var emailConsoleDTO = new EmailDTO
                //        {
                //            Email = email
                //        };
                //        var response = apiClient.PostToken($"groups/{groupID}/invitations", emailConsoleDTO, userToken);
                //        if (response.IsSuccessStatusCode)
                //        {
                //            System.Console.WriteLine("Invite Sent Successfully");
                //        }
                //        else
                //        {
                //            System.Console.WriteLine("Unsuccessful.");
                //        }
                //        break;
                //    default:
                //        System.Console.WriteLine("Wrong option");
                //        break;
                //}
                return choice;
            
        }

        public void InviteUser(string token , int groupID)
        {
            System.Console.Write("Enter email of the user you want to invite: ");
            string email = System.Console.ReadLine();
            var emailConsoleDTO = new EmailDTO
            {
                Email = email
            };
            var response = apiClient.PostToken($"groups/{groupID}/invitations", emailConsoleDTO, token);
            if (response.IsSuccessStatusCode)
            {
                System.Console.WriteLine("Invite Sent Successfully");
            }
            else
            {
                System.Console.WriteLine("Unsuccessful.");
            }
        }

        public bool CreateNewProject(string token , int groupID)
        {
            System.Console.WriteLine("Enter project name: ");
            var projName = System.Console.ReadLine();
            System.Console.WriteLine("Enter project description: ");
            var projDescription = System.Console.ReadLine();
            ProjectDto proj = new Dtos.ProjectDto
            {
                ProjectName = projName,
                ProjectDescription = projDescription,
            };

            //string postRequest = $"{ApiUrl}/api/groups/{groupId}/projects";
            //System.Console.WriteLine($"POST: {postRequest} ...");

            //var request = new HttpRequestMessage(HttpMethod.Post, postRequest);
            //string jsonContent = JsonSerializer.Serialize(proj);

            //StringContent stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            //request.Content = stringContent;

            var response = apiClient.PostToken($"groups/{groupID}/projects", proj, token);

            if (response == null)
            {
                System.Console.WriteLine("Invalid response!");
                return false;
            }
            if (!response.IsSuccessStatusCode)
            {
                System.Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return false;
            }
            System.Console.WriteLine($"SUCCESS!");
            return true;
        }
    }
}
