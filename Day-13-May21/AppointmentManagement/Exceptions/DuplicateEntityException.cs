using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentManagement.Exceptions
{
    public class DuplicateEntityException : Exception
    {
        private string _message = "Entity Not Found";

        public DuplicateEntityException(string message)
        {
            _message = message;
        }

        public override string Message => _message;
        
    }
}