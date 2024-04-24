using TaskMgmt.Console;
using TaskMgmt.Console.Services;
using TaskMgmt.Services.ProjectTasks;

static class Program
{
    public static void Main()
    {       
        //global user maagment
        UserService userService = new UserService();
        GroupService groupService = new GroupService(userService.userToken);
        ProjectService projectService = new ProjectService(userService.userToken);
        TaskService taskService = new TaskService();
        TaskStatusService statusService = new TaskStatusService();

        while (true)
        {

            //Console.Clear();
            Console.WriteLine("Welcome to the Task Management Console App!");
            
            try
            {
                Console.WriteLine("\n1. Sign In");
                Console.WriteLine("2. Sign Up");
                Console.WriteLine("3. Sign Up With Referal");
                Console.WriteLine("4. Exit");

                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        userService.SignIn();
                        if (userService.IsUserValid())
                        {
                            Console.WriteLine("Login Success!");
                            groupService.DisplayGroup(userService.db.token);
                            string response = groupService.DisplayMenu();
                            switch(response)
                            {
                                case "1":
                                    userService.db.groupId = projectService.GetProjects(userService.db.token);
                                    var res = projectService.ProjectMenu(userService.db.token, userService.db.groupId);
                                    switch(res)
                                    {
                                        case "1":
                                            projectService.InviteUser(userService.db.token, userService.db.groupId);
                                            break;
                                        case "2":
                                            var flag = projectService.CreateNewProject(userService.db.token ,userService.db.groupId);
                                          
                                            break;
                                        case "3":                                            
                                            System.Console.WriteLine("Project ID: ");
                                            var projectId = Convert.ToInt32(Console.ReadLine() ?? "0");
                                            statusService.GetTasks(userService.db.token, userService.db.groupId, projectId);
                                            taskService.GetTasks(userService.db.token, userService.db.groupId, projectId);
                                            System.Console.WriteLine("1. Create New Task\n2. Create New Task Status\n3. Go Back\n");
                                            switch (Console.ReadLine() ?? string.Empty)
                                            {
                                                case "1":
                                                    taskService.CreateTask(userService.db.token, userService.db.groupId, projectId);
                                                    break;
                                                case "2":
                                                    statusService.CreateTaskStatus(userService.db.token, userService.db.groupId, projectId);
                                                    break;
                                                default:
                                                    break;
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case "2":
                                    groupService.Enroll(userService.db.token);
                                    break;
                                case "3":
                                    groupService.CreateGroup(userService.db.token);
                                    break;
                            }
                        }

                        break;
                    case "2":
                        userService.SignUp();
                        if (userService.IsUserValid())
                        {
                            Console.WriteLine("Signup Success!");
                            groupService.DisplayGroup(userService.db.token);
                            string response = groupService.DisplayMenu();
                            switch (response)
                            {
                                case "1":
                                    userService.db.groupId = projectService.GetProjects(userService.db.token);
                                    var res = projectService.ProjectMenu(userService.db.token, userService.db.groupId);
                                    switch (res)
                                    {
                                        case "1":
                                            projectService.InviteUser(userService.db.token, userService.db.groupId);
                                            break;
                                        case "2":
                                            var flag = projectService.CreateNewProject(userService.db.token, userService.db.groupId);

                                            break;
                                        case "3":
                                            System.Console.WriteLine("Project ID: ");
                                            var projectId = Convert.ToInt32(Console.ReadLine() ?? "0");
                                            statusService.GetTasks(userService.db.token, userService.db.groupId, projectId);
                                            taskService.GetTasks(userService.db.token, userService.db.groupId, projectId);
                                            System.Console.WriteLine("1. Create New Task\n2. Go Back");
                                            switch (Console.ReadLine() ?? string.Empty)
                                            {
                                                case "1":
                                                    taskService.CreateTask(userService.db.token, userService.db.groupId, projectId);
                                                    break;
                                                default:
                                                    break;
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case "2":
                                    groupService.Enroll(userService.db.token);
                                    break;
                                case "3":
                                    groupService.CreateGroup(userService.db.token);
                                    break;
                            }
                        }

                        break;
                    case "3":
                        userService.SignUpReferral();
                        if (userService.IsUserValid())
                        {
                            Console.WriteLine("Referral Success!");
                            groupService.DisplayGroup(userService.db.token);
                            string response = groupService.DisplayMenu();
                            switch (response)
                            {
                                case "1":
                                    userService.db.groupId = projectService.GetProjects(userService.db.token);
                                    var res = projectService.ProjectMenu(userService.db.token, userService.db.groupId);
                                    switch (res)
                                    {
                                        case "1":
                                            projectService.InviteUser(userService.db.token, userService.db.groupId);
                                            break;
                                        case "2":
                                            var flag = projectService.CreateNewProject(userService.db.token, userService.db.groupId);

                                            break;
                                        case "3":
                                            System.Console.WriteLine("Project ID: ");
                                            var projectId = Convert.ToInt32(Console.ReadLine() ?? "0");
                                            statusService.GetTasks(userService.db.token, userService.db.groupId, projectId);
                                            taskService.GetTasks(userService.db.token, userService.db.groupId, projectId);
                                            System.Console.WriteLine("1. Create New Task\n2. Go Back");
                                            switch (Console.ReadLine() ?? string.Empty)
                                            {
                                                case "1":
                                                    taskService.CreateTask(userService.db.token, userService.db.groupId, projectId);
                                                    break;
                                                default:
                                                    break;
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case "2":
                                    groupService.Enroll(userService.db.token);
                                    break;
                                case "3":
                                    groupService.CreateGroup(userService.db.token);
                                    break;
                            }
                        }
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.WriteLine("Press 0 to exit.");
                        var ans2 = Console.ReadLine();
                        if (ans2 == "0")
                        {
                            break;
                        }
                        else
                        {
                            Console.Clear();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }





}
