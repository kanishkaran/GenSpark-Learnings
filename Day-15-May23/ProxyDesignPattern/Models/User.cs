using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProxyDesignPattern.Models
{
    public class User
    {
        public string UserName = string.Empty;
        public string Role = string.Empty;

        public void GetUserDetails()
        {
            Console.Write("Kindly Enter your UserName: ");
            UserName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(UserName))
            {
                Console.WriteLine("Enter proper Username");
                UserName = Console.ReadLine();
            }

            Console.Write("Kindly Enter your Role: ");
            Role = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(Role)) 
            {
                Console.WriteLine("Enter proper Username");
                Role = Console.ReadLine();
            }
        }
    }
}