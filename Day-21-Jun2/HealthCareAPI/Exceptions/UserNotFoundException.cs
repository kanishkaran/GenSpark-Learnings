using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareAPI.Exceptions
{
    public class UserNotFoundException : Exception
    {
        private string _message = "User not found";

        public UserNotFoundException(string msg)
        {
            _message = msg;
        }

        public override string Message => _message; 
    }
}