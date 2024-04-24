using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskMgmt.Console.Dtos.Group;

namespace TaskMgmt.Console.Services
{
    
    public class GroupService
    {
        private readonly ApiClient apiClient;
        private readonly string userToken;
        public GroupService(string userToken)
        {
            apiClient = new ApiClient(ApiConstants.ApiUrl);
            userToken = userToken;
        }
        public void DisplayGroup(string userTokens)
        {
            System.Console.Clear();
            System.Console.WriteLine("\t\t\t\t\t\tHOME DASHBOARD");
            try
            {
                var response = apiClient.GetToken(ApiConstants.Groups, userTokens);
                var content = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    System.Console.WriteLine("\t\t\tGroup Details");
                    var groups = JsonSerializer.Deserialize<List<GroupDTO>>(content);
                    foreach (var group in groups)
                    {
                        System.Console.WriteLine($"GroupID: {group.groupId} GroupName: {group.groupName} CreatedAt: {group.createdAt}\n");
                    }
                    System.Console.WriteLine("\n\n");
                    
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        public string DisplayMenu()
        {
            
                System.Console.WriteLine("1.Select Group (Id) \n2.Enroll to Group\n3.Create Group\n\n");
                System.Console.Write("Choose an option: ");
                string choice = System.Console.ReadLine();
                return choice;
        }

        public void Enroll(string token)
        {
            System.Console.Write("Enter group name");
            string groupName = System.Console.ReadLine();
            System.Console.WriteLine("Enter referral code: ");
            string referral = System.Console.ReadLine();
            InvitationDTO invitationDTO = new InvitationDTO
            {
                GroupName = groupName,
                ReferralCode = referral
            };
            try
            {
                var response = apiClient.PostToken("groups/enrollments", invitationDTO , token);
                if (response.IsSuccessStatusCode)
                {
                    System.Console.WriteLine("Successfully Enrolled");
                }
                else
                {
                    System.Console.WriteLine("Enrollment Failed.");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
        public void CreateGroup(string Token)
        {
            System.Console.Write("Enter Group Name : ");
            var groupName = System.Console.ReadLine();
            GroupRequestDTO GroupRequestDTO = new GroupRequestDTO
            {
                GroupName = groupName
            };
            try
            {
                var response = apiClient.PostToken("groups", GroupRequestDTO, Token);
                if (response.IsSuccessStatusCode)
                {
                    System.Console.WriteLine("Group created successfully\n");
                }
                else
                {
                    System.Console.WriteLine("Group Creation Failed.");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

    }
    }
