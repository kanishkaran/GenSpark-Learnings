using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApplication.Exceptions
{
    public class CollectionEmptyException : Exception
    {
        private string _message = "The Collection is Empty";

        public CollectionEmptyException(string message)
        {
            _message = message;
        }

        public override string Message => _message;
        
    }
}