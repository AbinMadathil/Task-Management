using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.Console.Dtos.User;

namespace TaskMgmt.Console.Services
{
    public class UserService
    {
        public string userToken;
        public LocalDB db = new LocalDB();
        private readonly ApiClient apiClient;

        public UserService()
        {
            apiClient = new ApiClient(ApiConstants.ApiUrl);
        }
        public void SignIn()
        {
            System.Console.Write("Enter Email: ");
            string email = System.Console.ReadLine();
            System.Console.Write("Enter Password: ");
            string password = System.Console.ReadLine();

            //call signin rest api
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                System.Console.WriteLine("Please provide both email and password.");
                
            }
            if (!IsValidEmail(email))
            {
                System.Console.WriteLine("Invalid email format. Please enter a valid email address.");
                
            }

            if (!IsStrongPassword(password))
            {
                System.Console.WriteLine("Weak password. Please use a stronger password.");
                
            }
            var loginDtoConsole = new LoginDTO
            {
                Email = email,
                Password = password
            };
            try
            {
                var response = apiClient.Post(ApiConstants.Login, loginDtoConsole);
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    db.token = content;
                    db.userName = email;
                }
                else
                {
                    System.Console.WriteLine("Login Failed");
                    return;
                }

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            
        }
        //SignUp
        public void SignUp()
        {
            System.Console.Write("Enter Name: ");
            string name = System.Console.ReadLine();
            System.Console.Write("Enter Email: ");
            string email = System.Console.ReadLine();
            System.Console.Write("Enter Password: ");
            string password = System.Console.ReadLine();
            System.Console.Write("Enter Groupname: ");
            string groupName = System.Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                System.Console.WriteLine("Please provide both email and password.");
                return;
            }
            if (!IsValidEmail(email))
            {
                System.Console.WriteLine("Invalid email format. Please enter a valid email address.");
                return;
            }

            if (!IsStrongPassword(password))
            {
                System.Console.WriteLine("Weak password. Please use a stronger password.");
                return;
            }
            var signUpDtoConsole = new SignUpDTO
            {
                Email = email,
                Name = name,
                Password = password,
                GroupName = groupName
            };
            try
            {
                var response = apiClient.Post(ApiConstants.Signup, signUpDtoConsole);
                if (response.IsSuccessStatusCode)
                {
                    db.token = response.Content.ReadAsStringAsync().Result;
                    System.Console.WriteLine("Welcome.");
                    
                }
                else
                {
                    System.Console.WriteLine("SignUp Failed.");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return;
        }

        //Referal 
        public void SignUpReferral()
        {
            System.Console.Write("Enter Name: ");
            string name = System.Console.ReadLine();
            System.Console.Write("Enter Email: ");
            string email = System.Console.ReadLine();
            System.Console.Write("Enter Password: ");
            string password = System.Console.ReadLine();
            System.Console.Write("Enter Groupname: ");
            string groupName = System.Console.ReadLine();
            System.Console.Write("Enter Referral: ");
            string referral = System.Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                System.Console.WriteLine("Please provide both email and password.");
                return;
            }
            if (!IsValidEmail(email))
            {
                System.Console.WriteLine("Invalid email format. Please enter a valid email address.");
                return;
            }

            if (!IsStrongPassword(password))
            {
                System.Console.WriteLine("Weak password. Please use a stronger password.");
                return;
            }
            var signUpReferralDtoConsole = new SignUpReferralDTO
            {
                Email = email,
                Name = name,
                Password = password,
                GroupName = groupName,
                ReferralCode = referral
            };
            try
            {
                var response = apiClient.Post("signup", signUpReferralDtoConsole);
                if (response.IsSuccessStatusCode)
                {
                    db.token = response.Content.ReadAsStringAsync().Result;
                    System.Console.WriteLine("Welcome.");
                    
                }
                else
                {
                    System.Console.WriteLine("SignUp Failed.");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        public bool IsUserValid()
        {
            return !String.IsNullOrEmpty(db.token);
        }

        public static bool IsValidEmail(string email)
        {
            return new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(email);
        }
        //function for checking strength of password.
        public static bool IsStrongPassword(string password)
        {
            return password.Length > 8;
        }
    }

}
