using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProxyDesignPattern.Interfaces;
using ProxyDesignPattern.Models;

namespace ProxyDesignPattern.Services
{
    public class ProxyFile : IFile
    {
        private Files _file;

        public ProxyFile(Files file)
        {
            _file = file;
        }
        public string Read()
        {
            User user = new User();
            user.GetUserDetails();

            if (user.Role == "Admin")
            {
                return "[Access Granted] " + _file.Read();
            }
            else if (user.Role == "User")
            {
                return "[Partial Access] " + _file._sensitiveFilePath;
            }
            else
            {
                return "[Access Denied] You do not have the permission to access this file";
            }


        }
    }
}